using MassTransit;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SignalR.Host
{
    public class ServiceManager : IHostedService
    {
        private IBusControl _busControl;
        public ServiceManager(IBusControl busControl)
        {
            _busControl = busControl;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //>http add urlacl url=http://events.hostel.com:80/ user=Everyone
            //Had issues reserving url using this method, I went manual
            /*Console.WriteLine("Test!!!");
            //I created SSL cert using xca => https://github.com/chris2511/xca
            var repse = NetSH.CMD.Http.Add.UrlAcl("http://events.hostel.com:80/", "user", false, false);
            Console.WriteLine(JsonConvert.SerializeObject(repse, Formatting.Indented));
            var reps = NetSH.CMD.Http.Add.UrlAcl("https://events.hostel.com:443/", "user", false, false);
            Console.WriteLine(JsonConvert.SerializeObject(reps, Formatting.Indented));
            var rep = NetSH.CMD.Http.Add.SSLCert(hostnamePort: "events.hostel.com:443", certHash: "8bb08e88fcef9a225f26d090eed0b702f8c3e237", appId: Guid.Parse("921328e8-5cdf-4b7f-a3cc-4e50147d1521"), certStoreName: "my");
            Console.WriteLine(JsonConvert.SerializeObject(rep, Formatting.Indented));*/
            await _busControl.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {

                /*NetSH.CMD.Http.Delete.SSLCert(hostnamePort: "events.hostel.com:443");
                NetSH.CMD.Http.Delete.UrlAcl("https://events.hostel.com:443");
                NetSH.CMD.Http.Delete.UrlAcl("http://events.hostel.com:80");*/
                _busControl.StopAsync(cancellationToken);
            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }
            return Task.CompletedTask;
        }
    }
}
