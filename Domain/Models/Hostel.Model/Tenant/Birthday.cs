
namespace Hostel.Model.Tenant
{
    public class Birthday
    {
        public int Month { get; }
        public int Day { get; }
        public int Year { get; }
        public Birthday(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }
    }
}
