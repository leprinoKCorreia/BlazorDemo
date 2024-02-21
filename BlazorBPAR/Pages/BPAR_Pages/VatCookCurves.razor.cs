using BlazorBPAR.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using BlazorBPAR.Objects;
using System.Data;
using Microsoft.Extensions.Configuration;


namespace BlazorBPAR.Pages.BPAR_Pages
{
    partial class VatCookCurves
    {
        [Inject] public IJSRuntime? JSRuntime { get; set; }

        private SelectOptions? PlantSelect;
        private SelectOptions? LineSelect;
        private SelectOptions? CookProgramSelect;
        private SelectOptions? FiscalYearSelect;
        private SelectOptions? MonthSelect;
        private SelectOptions? DateSelect;
        private SelectOptions? ProductCodeSelect;
        private List<SelectOptions>? selectList;


        protected override void OnInitialized()
        {
            PlantSelect = new SelectOptions()
            {
                options = new List<string>() { "All", "Allendale","Fort Morgan", "Greeley", "Lemoore East", "Lemoore West", "Roswell", "Tracy", "Waverly" },
                IDName = "PlantOptions",
                useQuery = false,
                Label = "Plant"
            };

            LineSelect = new SelectOptions()
            {
                options = new List<string>() { "1", "2", "3"},
                IDName = "LineOptions",
                useQuery = false,
                Label = "Line Number"
            };

            CookProgramSelect = new SelectOptions()
            {
                options = new List<string>() { "All" },
                IDName = "CookProgramOptions",
                useQuery = false,
                Label = "Cook Program"
            };

            FiscalYearSelect = new SelectOptions()
            {
                useQuery = false,
                options = new List<string>() { "2024" },
                IDName = "FiscalYearOptions",
                Label = "Fiscal Year"
            };

            DateSelect = new SelectOptions()
            {
                useQuery = false,
                options = new List<string>() { "02/16/2024" },
                IDName = "DateOptions",
                Label = "Date",
                dependencies = new List<string>() { "ProductCodeOptions" }
            };

            ProductCodeSelect = new SelectOptions()
            {
                useQuery = true,
                query = "select distinct ProductCode as [Option] from LEW.BIReports.dbo.r_VatCookCurveTemps where ProductionDate = '$DateOptions$'",
                connection = config.GetConnectionString("DEN_2012"),
                IDName = "ProductCodeOptions",
                Label = "Product Code",
                queryParams = new List<string>() { "DateOptions" }
            };

            selectList = new List<SelectOptions>() { PlantSelect, LineSelect, CookProgramSelect, FiscalYearSelect, DateSelect, ProductCodeSelect };

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && JSRuntime != null) // only needs to be called once per page render
            {
                await JSRuntime.InvokeVoidAsync("selectPickerService.init");
            }
        }

    }
}
