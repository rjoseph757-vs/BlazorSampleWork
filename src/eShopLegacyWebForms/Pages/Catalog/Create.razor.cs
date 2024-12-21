using eShopLegacyWebForms.Models;
using eShopLegacyWebForms.Services;
using log4net;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace eShopLegacyWebForms.Catalog
{
    public partial class Create : ComponentBase
    {
        RequestDelegate _next = null;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ICatalogService CatalogService { get; set; }

        public IEnumerable<CatalogBrand> GetBrands()
        {
            return CatalogService.GetCatalogBrands();
        }

        public IEnumerable<CatalogType> GetTypes()
        {
            return CatalogService.GetCatalogTypes();
        }

        protected void Create_Click(object sender, EventArgs e)
        {
            if (this.ModelState.IsValid)
            {
                var catalogItem = new CatalogItem
                {
                    Name = Name_Text,
                    Description = Description_Text,
                    CatalogBrandId = int.Parse(Brand.SelectedValue),
                    CatalogTypeId = int.Parse(Type.SelectedValue),
                    Price = decimal.Parse(Price_Text),
                    AvailableStock = int.Parse(Stock_Text),
                    RestockThreshold = int.Parse(Restock_Text),
                    MaxStockThreshold = int.Parse(Maxstock_Text)
                };
                CatalogService.CreateCatalogItem(catalogItem);
                Response.Redirect("~");
            }
        }

        public Create(RequestDelegate next)
        {
        }

        public String Name_Text { get; set; }

        public String Description_Text { get; set; }

        public String Price_Text { get; set; }

        public String Stock_Text { get; set; }

        public String Restock_Text { get; set; }

        public String Maxstock_Text { get; set; }

        protected override void OnInitialized()
        {
            // This code replaces the original handling
            // of the Load event
            _log.Info($"Now loading... /Catalog/Create.aspx");
        }
    }
}