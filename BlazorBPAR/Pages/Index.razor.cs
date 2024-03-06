using BlazorBPAR.Objects;
using BlazorBPAR.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorBPAR.Pages
{
    partial class Index
    {
        [Inject] public IJSRuntime? JSRuntime { get; set; }

        public IList<Dictionary<string, object>>? queryResults;


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && JSRuntime != null) // only needs to be called once per page render
            {
                await JSRuntime.InvokeVoidAsync("selectPickerService.init");
            }
        }

        protected override void OnInitialized()
        {
            var queryToRun = "SELECT * FROM LPSDB.dbo.ChecklistTasksConfig";
            queryResults = SQLQueryService.RunQuery(queryToRun, config.GetConnectionString("ROS_LIT"));
        }

        public void openPage(string page)
        {
            @nav.NavigateTo(page);
        }
    }
}
