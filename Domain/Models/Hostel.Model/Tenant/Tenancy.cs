using System;

namespace Hostel.Model.Tenant
{
    public enum Rate{
        Monthly,
        Yearly
    }
    public enum PaymentType
    {
        Shared,
        Whole
    }
    public class Tenancy
    {
        public DateTime Start { get; }
        public DateTime End { get; }
        public Rate Rate { get; }
        public PaymentType PaymentType { get; }
        public decimal Price { get; }
        public Tenancy(Rate rate, PaymentType type, DateTime start, DateTime end, decimal price)
        {
            Rate = rate;
            PaymentType = type;
            Start = start;
            End = end;
            Price = price;
        }
    }
}
