using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Components;

namespace eShopLegacyWebForms
{
    public partial class SiteMaster : LayoutComponentBase
    {
        /// <summary>
        /// Example legacy usage of a session variable embedded in a MasterPage
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionInfoLabel_Text = $"{HttpContext.Current.Session["MachineName"]}, {HttpContext.Current.Session["SessionStartTime"]}";
        }

        public String SessionInfoLabel_Text { get; set; }
    }
}