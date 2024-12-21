using eShopLegacyWebForms.Models;
using eShopLegacyWebForms.Services;
using log4net;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace eShopLegacyWebForms.Catalog
{
    public partial class Delete : ComponentBase
    {
        RequestDelegate _next = null;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected CatalogItem productToDelete;
        public ICatalogService CatalogService { get; set; }

        protected void Delete_Click(object sender, EventArgs e)
        {
            CatalogService.RemoveCatalogItem(productToDelete);
            Response.Redirect("~");
        }

        public Delete(RequestDelegate next)
        {
        }

        protected override void OnInitialized()
        {
            // This code replaces the original handling
            // of the Load event
            var productId = Convert.ToInt32(Page.RouteData.Values["id"]);
            _log.Info($"Now loading... /Catalog/Delete.aspx?id={productId}");
            productToDelete = CatalogService.FindCatalogItem(productId);
            this.DataBind();
        }
    }
}