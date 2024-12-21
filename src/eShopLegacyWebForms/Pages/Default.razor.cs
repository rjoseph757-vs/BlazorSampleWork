using eShopLegacyWebForms.Models;
using eShopLegacyWebForms.Services;
using eShopLegacyWebForms.ViewModel;
using System;
using System.Linq;
using log4net;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace eShopLegacyWebForms
{
    public partial class _Default : ComponentBase
    {
        RequestDelegate _next = null;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public const int DefaultPageIndex = 0;
        public const int DefaultPageSize = 10;
        public ICatalogService CatalogService { get; set; }

        protected PaginatedItemsViewModel<CatalogItem> Model { get; set; }

        private bool PaginationParamsAreSet()
        {
            return Page.RouteData.Values.Keys.Contains("size") && Page.RouteData.Values.Keys.Contains("index");
        }

        private void ConfigurePagination()
        {
            PaginationNext_NavigateUrl = GetRouteUrl("ProductsByPageRoute", new { index = Model.ActualPage + 1, size = Model.ItemsPerPage });
            var pagerNextExtraStyles = Model.ActualPage < Model.TotalPages - 1 ? "" : " esh-pager-item--hidden";
            PaginationNext_CssClass = PaginationNext_CssClass + pagerNextExtraStyles;
            PaginationPrevious_NavigateUrl = GetRouteUrl("ProductsByPageRoute", new { index = Model.ActualPage - 1, size = Model.ItemsPerPage });
            var pagerPreviousExtraStyles = Model.ActualPage > 0 ? "" : " esh-pager-item--hidden";
            PaginationPrevious_CssClass = PaginationPrevious_CssClass + pagerPreviousExtraStyles;
        }

        public _Default(RequestDelegate next)
        {
        }

        public Object productList_DataSource { get; set; }

        // The initial value "class="esh-pager-item
        // esh-pager-item--navigable"" was removed from
        // the view layer in favor of a binding to the
        // following property
        public String PaginationPrevious_CssClass { get; set; }

        public String PaginationPrevious_NavigateUrl { get; set; }

        // The initial value "class="esh-pager-item
        // esh-pager-item--navigable"" was removed from
        // the view layer in favor of a binding to the
        // following property
        public String PaginationNext_CssClass { get; set; }

        public String PaginationNext_NavigateUrl { get; set; }

        protected override void OnInitialized()
        {
            // This code replaces the original handling
            // of the Load event
            if (PaginationParamsAreSet())
            {
                var size = Convert.ToInt32(Page.RouteData.Values["size"]);
                var index = Convert.ToInt32(Page.RouteData.Values["index"]);
                Model = CatalogService.GetCatalogItemsPaginated(size, index);
                _log.Info($"Now loading... /Default.aspx?size={size}&index={index}");
            }
            else
            {
                Model = CatalogService.GetCatalogItemsPaginated(DefaultPageSize, DefaultPageIndex);
                _log.Info($"Now loading... /Default.aspx?size={DefaultPageSize}&index={DefaultPageIndex}");
            }

            productList_DataSource = Model.Data;
             // productList.DataBind();
            ConfigurePagination();
        }
    }
}