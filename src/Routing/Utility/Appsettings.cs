using System.Collections.Generic;

namespace Utility
{
    public class Appsettings
    {
        public string Token { get; set; }
        public string UrlMail { get; set; }
        public IEnumerable<string> PathIgnore { get; set; }
        public IEnumerable<string> ConnectionString { get; set; }
        public Polly Polly { get; set; }
        public SettingSAP SettingSAP { get; set; }
        public SettingEndPoint SettingEndPoint { get; set; }
    }

    public class Polly
    {
        public int Intent { get; set; }
        public int Delay { get; set; }
    }
    public class SettingSAP
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
    public class SettingEndPoint
    {
        public string ClientCenter { get; set; }
        public string OrderBonuses { get; set; }
        public string OrderTracing { get; set; }
        public string OrderSimulation { get; set; }
        public string OrderSellerName { get; set; }
        public string OrderRegister { get; set; }
        public string ClientDetails { get; set; }
        public string ProductPrice { get; set; }
        public string SimulatePrice { get; set; }

    }
}
