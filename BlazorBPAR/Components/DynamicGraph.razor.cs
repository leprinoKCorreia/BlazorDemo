using BlazorBPAR.Objects;
using BlazorBPAR.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace BlazorBPAR.Components
{
    partial class DynamicGraph
    {
        [Parameter]
        public GraphOptions? GraphOptions { get; set; }
        
        [Inject] public IJSRuntime? JSRuntime { get; set; }
        public IList<Dictionary<string, object>>? queryResults;

        protected override void OnInitialized()
        {
            if(GraphOptions != null && GraphOptions.Query != null && GraphOptions.Connection != null)
            {
                queryResults = SQLQueryService.RunQuery(GraphOptions.Query, GraphOptions.Connection);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && JSRuntime != null && queryResults != null && GraphOptions != null) // only needs to be called once per page render
            {
                if(GraphOptions.GraphType == "LineChart" && GraphOptions.GraphElement != null)
                {
                    await JSRuntime.InvokeVoidAsync("loadGraphService.loadLineGraph", GraphOptions.GraphElement, JsonConvert.SerializeObject(queryResults), JsonConvert.SerializeObject(GraphOptions.Columns), JsonConvert.SerializeObject(GraphOptions.GraphSettings));
                }
            }
        }     


        public async Task rerunGraph()
        {
            if (GraphOptions != null && GraphOptions.Query != null && GraphOptions.Connection != null)
            {
                queryResults = SQLQueryService.RunQuery(GraphOptions.Query, GraphOptions.Connection);
            }
            if (JSRuntime != null && GraphOptions != null && GraphOptions.GraphType == "LineChart" && GraphOptions.GraphElement != null)
            {
                await JSRuntime.InvokeVoidAsync("loadGraphService.loadLineGraph", GraphOptions.GraphElement, JsonConvert.SerializeObject(queryResults), JsonConvert.SerializeObject(GraphOptions.Columns), JsonConvert.SerializeObject(GraphOptions.GraphSettings));
            }
        }

    }
}
