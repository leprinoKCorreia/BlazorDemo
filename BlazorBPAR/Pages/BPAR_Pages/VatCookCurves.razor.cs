using BlazorBPAR.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using BlazorBPAR.Objects;
using System.Data;
using Microsoft.Extensions.Configuration;
using BlazorBPAR.Components;


namespace BlazorBPAR.Pages.BPAR_Pages
{
    partial class VatCookCurves
    {
        [Inject] public IJSRuntime? JSRuntime { get; set; }

        public BootstrapSelectList? refList { get; set; }

        private SelectOptions? PlantSelect;
        private SelectOptions? LineSelect;
        private SelectOptions? CookProgramSelect;
        private SelectOptions? FiscalYearSelect;
        private SelectOptions? MonthSelect;
        private SelectOptions? DateSelect;
        private SelectOptions? ProductCodeSelect;
        private List<SelectOptions>? selectList;
        private List<GraphOptions>? graphOptions;
        private GraphOptions? graph1;
        public IList<Dictionary<string, object>>? queryResults;
        public DynamicGraph? graph1Comp;


        protected override void OnInitialized()
        {
            PlantSelect = new SelectOptions()
            {
                options = new List<string>() { "Allendale","Fort Morgan", "Greeley", "Lemoore East", "Lemoore West", "Roswell", "Tracy", "Waverly" },
                IDName = "PlantOptions",
                useQuery = false,
                Label = "Plant",
                defaultValue = "Greeley"
            };

            LineSelect = new SelectOptions()
            {
                options = new List<string>() { "1", "2", "3"},
                IDName = "LineOptions",
                useQuery = false,
                Label = "Line Number",
                defaultValue = "1"
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
                options = new List<string>() { "02/16/2024", "02/25/2024" },
                IDName = "DateOptions",
                Label = "Date",
                dependencies = new List<string>() { "ProductCodeOptions" },
                dataMaxOptions = 1,
                defaultValue = "02/25/2024"
            };

            ProductCodeSelect = new SelectOptions()
            {
                query = "select distinct ProductCode as [Option] from GRE.BIReports.dbo.r_VatCookCurveTemps where ProductionDate = '$DateOptions$'",
                connection = config.GetConnectionString("DEN_2012"),
                IDName = "ProductCodeOptions",
                Label = "Product Code",
                queryParams = new List<string>() { "DateOptions" }
            };

            selectList = new List<SelectOptions>() { PlantSelect, LineSelect, CookProgramSelect, FiscalYearSelect, DateSelect, ProductCodeSelect };

            graph1 = new GraphOptions
            {
                Query = "EXEC LIT.dbo._usp_VatCookCurvesAllPlants_dw @CookProgram = null, @PhysicalVat = 1, @Line = 1, @Date = '$DateOptions$', @ProductCode = null, @Plant = 'GRE'",
                Connection = config.GetConnectionString("DEN_2012"),
                queryParams = new List<string> { "DateOptions" },
                GraphType = "LineChart",
                GraphElement = "demoGraph",
                GraphSettings = new graphSettings()
                {
                    Title = "1"
                }
            };

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && JSRuntime != null && refList != null) // only needs to be called once per page render
            {
                await JSRuntime.InvokeVoidAsync("selectPickerService.init");

                await refList.initDefaultValues();

                //if (graph1Comp != null)
                //{
                //    graph1Comp.runGraph();
                //}
            }
        }

        public void runGraph()
        {
            if (graph1Comp != null)
            {
                graph1Comp.runGraph();
            }
        }
        

    }
}
