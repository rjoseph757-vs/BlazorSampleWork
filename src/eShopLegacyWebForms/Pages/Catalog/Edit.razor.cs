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
    public partial class Edit : ComponentBase
    {
        RequestDelegate _next = null;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected CatalogItem product;
        public ICatalogService CatalogService { get; set; }

        public IEnumerable<CatalogBrand> GetBrands()
        {
            return CatalogService.GetCatalogBrands();
        }

        public IEnumerable<CatalogType> GetTypes()
        {
            return CatalogService.GetCatalogTypes();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            if (this.ModelState.IsValid)
            {
                var catalogItem = new CatalogItem
                {
                    Id = Convert.ToInt32(Page.RouteData.Values["id"]),
                    Name = Name_Text,
                    Description = Description_Text,
                    CatalogBrandId = int.Parse(BrandDropDownList.SelectedValue),
                    CatalogTypeId = int.Parse(TypeDropDownList.SelectedValue),
                    Price = decimal.Parse(Price_Text),
                    PictureFileName = PictureFileName_Text,
                    AvailableStock = int.Parse(Stock_Text),
                    RestockThreshold = int.Parse(Restock_Text),
                    MaxStockThreshold = int.Parse(Maxstock_Text)
                };
                CatalogService.UpdateCatalogItem(catalogItem);
                Response.Redirect("~");
            }
        }

        public Edit(RequestDelegate next)
        {
        }

        // The initial value "value="@(product.Name)""
        // was removed from the view layer in favor
        // of a binding to the following property
        public String Name_Text { get; set; }

        // The initial value "value="@(product.Description)""
        // was removed from the view layer in favor
        // of a binding to the following property
        public String Description_Text { get; set; }

        // The initial value "value="@(product.Price)""
        // was removed from the view layer in favor
        // of a binding to the following property
        public String Price_Text { get; set; }

        // The initial value "value="@(product.PictureFileName)""
        // was removed from the view layer in favor
        // of a binding to the following property
        public String PictureFileName_Text { get; set; }

        // The initial value "value="@(product.AvailableStock)""
        // was removed from the view layer in favor
        // of a binding to the following property
        public String Stock_Text { get; set; }

        // The initial value "value="@(product.RestockThreshold)""
        // was removed from the view layer in favor
        // of a binding to the following property
        public String Restock_Text { get; set; }

        // The initial value "value="@(product.MaxStockThreshold)""
        // was removed from the view layer in favor
        // of a binding to the following property
        public String Maxstock_Text { get; set; }

        protected override void OnInitialized()
        {
            // This code replaces the original handling
            // of the Load event
            if (!Page.IsPostBack)
            {
                var productId = Convert.ToInt32(Page.RouteData.Values["id"]);
                _log.Info($"Now loading... /Catalog/Edit.aspx?id={productId}");
                product = CatalogService.FindCatalogItem(productId);
                BrandDropDownList.DataSource = CatalogService.GetCatalogBrands();
                BrandDropDownList.SelectedValue = product.CatalogBrandId.ToString();
                TypeDropDownList.DataSource = CatalogService.GetCatalogTypes();
                TypeDropDownList.SelectedValue = product.CatalogTypeId.ToString();
                this.DataBind();
            }
        }
    }
}