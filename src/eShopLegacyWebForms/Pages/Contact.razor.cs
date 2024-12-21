using log4net;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace eShopLegacyWebForms
{
    public partial class Contact : ComponentBase
    {
        RequestDelegate _next = null;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Contact(RequestDelegate next)
        {
        }

        protected override void OnInitialized()
        {
            // This code replaces the original handling
            // of the Load event
            _log.Info("Now loading... /Contact.aspx");
        }
    }
}