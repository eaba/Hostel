using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Ignite.SharpNetSH
{
	public class ActionProxy<T> : DispatchProxy  //RealProxy
    {
		private string _priorText;
		private string _actionName;
		private IExecutionHarness _harness;
        
		public static T Create(string actionName, string priorText, IExecutionHarness harness)
		{
            object proxy = Create<T, ActionProxy<T>>();
            ((ActionProxy<T>)proxy).SetParameters(actionName, priorText, harness);
            return (T)proxy;
        }

        private void SetParameters(string actionName, string priorText, IExecutionHarness harness)
        {
            _actionName = actionName;
            _harness = harness;
            _priorText = priorText;
        }
        protected override object Invoke(MethodInfo targetMethod, object[] args)
		{
			var method = targetMethod;
			var result = ProcessParameters(method, args);
			int exitCode;
			var response = _harness.Execute(_priorText + " " + _actionName + " " + result, out exitCode);
			var processorType = method.GetResponseProcessorType();
			var splitRegEx = method.GetSplitRegEx();

			// If there is a ResponseProcessorAttribute defined, it overrides any response processors on the return type
			if (processorType != null)
			{
				// We use the Activator here because we want custom processors to use their own constructor if they so desire
				var processorUnknown = FormatterServices.GetUninitializedObject(processorType);
				var isSimpleProcessor = processorUnknown.GetType().GetInterfaces().Contains(typeof (IResponseProcessor));

				if (!isSimpleProcessor)
					throw new Exception("Custom processor cannot inherit from both IResponseProcessor and IMultiResponseProcessor");

				var simpleProcessor = (IResponseProcessor) processorUnknown;
				var processedResponse = simpleProcessor.ProcessResponse(response, exitCode, splitRegEx);
				return processedResponse;
			}

            return null;
		}

        private string ProcessParameters(MethodBase method, object[] args)
		{
			var results = new List<string>();
			var i = 0;
            
            foreach (var value in args)
			{
				var parameter = method.GetParameters().FirstOrDefault(x => x.Position == i);
                var parameterName = parameter.GetParameterName();
                i++;
                if (value == null) continue;

				if (value is bool)
					// We have to process booleans differently based upon the configured boolean type (i.e. Yes/No, Enable/Disable, True/False outputs)
					results.Add(parameterName + "=" + parameter.GetBooleanType().GetBooleanValue((bool)value));
				else if (value is Guid)
					// Guids have to contain braces
					results.Add(parameterName + "=" + ((Guid)value).ToString("B"));
				else if (value.GetType().IsEnum)
					// Enums might be configured with a custom description to change how to output their text
					results.Add(parameterName + "=" + value.GetDescription());
				else
					// Otherwise it's a stringable (i.e. ToString()) property
					results.Add(parameterName + "=" + value);
			}
			if (results.Count == 0) return method.GetMethodName();
            var input = method.GetMethodName() + " " + results.Aggregate((x, y) => string.IsNullOrWhiteSpace(x) ? y : x + " " + y);
            return input;
		}
	}
}