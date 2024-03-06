using BlazorBPAR.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using BlazorBPAR.Objects;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace BlazorBPAR.Pages.BPAR_Pages
{
    partial class BinoffWeeklyReport
    {
        [Inject] public IJSRuntime? JSRuntime { get; set; }

        public IList<Dictionary<string, object>>? queryResults;
        private SelectOptions? plantSelect;
        private SelectOptions? fiscalYearSelect;
        private SelectOptions? monthSelect;
        private SelectOptions? FWKFYSelect;
        private SelectOptions? LineSelect;
        private List<SelectOptions>? selectList;
        private GraphOptions? rollingGraph;

        protected override void OnInitialized()
        {
            plantSelect = new SelectOptions()
            {
                Options = new List<string>() { "All", "Allendale", "Lemoore East", "Lemoore West", "Roswell", "Tracy" },
                IDName = "PlantOptions",
                UseQuery = false,
                Label = "Plant"
            };

            fiscalYearSelect = new SelectOptions()
            {
                Options = new List<string>() { "2021", "2022", "2023", "2024" },
                IDName = "FiscalYearOptions",
                UseQuery = false,
                Label = "Fiscal Year"
            };

            monthSelect = new SelectOptions()
            {
                Options = new List<string>() { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" },
                IDName = "MonthOptions",
                UseQuery = false,
                Label = "Month"
            };

            FWKFYSelect = new SelectOptions()
            {
                UseQuery = true,
                Query = "select CONCAT( fw, ' | ',max(FiscalDate)) as [Option] from LIT.dbo.fiscalwktbl where FiscalYear = '2024' and FiscalDate <= (select CASE WHEN DATEPART(WEEKDAY, getdate()) <> 7 THEN DATEADD(DAY, 7 - DATEPART(WEEKDAY, getdate()), getdate()) ELSE getdate() end) group by fw order by fw desc",
                Connection = config.GetConnectionString("DEN_2012"),
                IDName = "FWKFYOptions",
                Label = "Fiscal Week/Year"
            };

            LineSelect = new SelectOptions()
            {
                Options = new List<string>() { "1", "2", "3", "4" },
                IDName = "LineOptions",
                UseQuery = false,
                Label = "Line"
            };

            selectList = new List<SelectOptions> { plantSelect, fiscalYearSelect, monthSelect, FWKFYSelect, LineSelect };

            rollingGraph = new GraphOptions()
            {
                Connection = config.GetConnectionString("DEN_2012"),
                Query = "EXEC _usp_CspBinoffTotalWeekly_AllPlants @plant = LEW,  @productionDate = '02/11/2024', @isPlannedBulkoff = 1",
                GraphType = "LineChart",
                GraphElement = "demoGraph",
                Columns = new List<cols>() 
                { 
                    new() { label = "ProductionDate", type = "datetime" }, 
                    new() { label = "Rolling Average", type = "number" } 
                },
                GraphSettings = new graphSettings()
                {
                    title = "Rolling Trend Data",
                    vAxis = new vAxis() { viewWindow = new viewWindow() { min = "0" } }                   
                }
            };

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
