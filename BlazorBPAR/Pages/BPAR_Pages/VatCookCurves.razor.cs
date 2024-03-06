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

        public BootstrapSelectList? RefList { get; set; }
        private SelectOptions? PlantSelect;
        private SelectOptions? LineSelect;
        private SelectOptions? CookProgramSelect;
        private SelectOptions? FiscalYearSelect;
        private SelectOptions? MonthSelect;
        private SelectOptions? DateSelect;
        private SelectOptions? ProductCodeSelect;
        private List<SelectOptions>? selectList;
        private List<GraphOptions>? GraphOptions;
        public List<DynamicGraph> _refs = new();
        public DynamicGraph Ref { set => _refs.Add(value); }

        protected override void OnInitialized()
        {
            PlantSelect = new SelectOptions()
            {
                Options = new List<string>() { "Allendale","Fort Morgan", "Greeley", "Lemoore East", "Lemoore West", "Roswell", "Tracy", "Waverly" },
                IDName = "PlantOptions",
                UseQuery = false,
                Label = "Plant",
                DefaultValue = "Greeley"
            };

            LineSelect = new SelectOptions()
            {
                Options = new List<string>() { "1", "2", "3"},
                IDName = "LineOptions",
                UseQuery = false,
                Label = "Line Number",
                DefaultValue = "1"
            };

            CookProgramSelect = new SelectOptions()
            {
                Options = new List<string>() { "All" },
                IDName = "CookProgramOptions",
                UseQuery = false,
                Label = "Cook Program"
            };

            FiscalYearSelect = new SelectOptions()
            {
                UseQuery = false,
                Options = new List<string>() { "2024" },
                IDName = "FiscalYearOptions",
                Label = "Fiscal Year"
            };

            DateSelect = new SelectOptions()
            {
                UseQuery = false,
                Options = new List<string>() { "02/16/2024", "02/25/2024" },
                IDName = "DateOptions",
                Label = "Date",
                Dependencies = new List<string>() { "ProductCodeOptions" },
                DataMaxOptions = 1,
                DefaultValue = "02/25/2024"
            };

            ProductCodeSelect = new SelectOptions()
            {
                Query = "select distinct ProductCode as [Option] from GRE.BIReports.dbo.r_VatCookCurveTemps where ProductionDate = '$DateOptions$'",
                Connection = config.GetConnectionString("DEN_2012"),
                IDName = "ProductCodeOptions",
                Label = "Product Code",
                QueryParams = new List<string>() { "DateOptions" }
            };

            selectList = new List<SelectOptions>() { PlantSelect, LineSelect, CookProgramSelect, FiscalYearSelect, DateSelect, ProductCodeSelect };

            GraphOptions = new List<GraphOptions>();
            for (var i = 1; i < 13; i++)
            {
                GraphOptions.Add(new GraphOptions
                {
                    Query = $"EXEC LIT.dbo._usp_VatCookCurvesAllPlants_dw @CookProgram = null, @PhysicalVat = {i}, @Line = 1, @Date = '$DateOptions$', @ProductCode = null, @Plant = 'GRE'",
                    Connection = config.GetConnectionString("DEN_2012"),
                    QueryParams = new List<string> { "DateOptions" },
                    GraphType = "LineChart",
                    GraphElement = "demoGraph" + i,
                    GraphSettings = new graphSettings()
                    {
                        title = i + "",
                        XAxisType = "number",
                        focusTarget = "category"
                    },
                    Height = "100%",
                    Width = "50%"
                });
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && JSRuntime != null && RefList != null) // only needs to be called once per page render
            {
                await JSRuntime.InvokeVoidAsync("selectPickerService.init");

                await RefList.InitDefaultValues();

                foreach(var graph in _refs)
                {
                    graph.RunGraph();
                }

            }
        }       
    }
}
