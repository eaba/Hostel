using System;

namespace Hostel.Web.Portal.MVC.AutoRefresh
{
    public class AutoRefreshOptions
    {
        public string Scheme { get; set; }
        public TimeSpan RefreshBeforeExpiration { get; set; } = TimeSpan.FromMinutes(1);
    }
}
