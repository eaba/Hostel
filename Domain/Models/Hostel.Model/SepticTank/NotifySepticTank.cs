
namespace Hostel.Model.SepticTank
{
    public class NotifySepticTank
    {
        public string Person { get; }
        public string Value { get; }
        public string ValuePercent { get; }
        public string ActualTankDepth { get; }
        public string TimeOfValue { get; }
        public NotifySepticTank(string person, string value, string percent, string actual, string time)
        {
            Person = person;
            Value = value;
            ValuePercent = percent;
            ActualTankDepth = actual;
            TimeOfValue = time;
        }
    }
}
