using BlazorBPAR.Objects;
using BlazorBPAR.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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

        public void RunGraph()
        {
            // Update query and confirm results exist
            if (GraphOptions?.Query == null || GraphOptions.Connection == null || GraphOptions.GraphSettings == null) return;

            var queryToRun = GraphOptions.QueryParams?.Aggregate(GraphOptions.Query,
                (current, param) => current.Replace($"${param}$", inputData.GetValue(param)?.ToString() ?? "")) ?? GraphOptions.Query;

            queryResults = SQLQueryService.RunQuery(queryToRun, GraphOptions.Connection);

            // read through unique columns to get the data and their datatypes
            if (GraphOptions.GraphType == "LineChart" && queryResults.Any())
            {
                GraphOptions.Columns = new List<cols>();
                foreach (var row in queryResults)
                {
                    foreach (var entry in row)
                    {
                        if (entry.Value.ToString() != "")
                        {
                            cols newCol = new()
                            {
                                label = entry.Key.ToString(),
                                type = entry.Key.ToString() == "XAxis" ? GraphOptions.GraphSettings.XAxisType :
                               float.TryParse(entry.Value.ToString(), out _) || Int64.TryParse(entry.Value.ToString(), out _) ? "number" : "string"
                            };

                            bool exists = GraphOptions.Columns.Exists(x => x.label == newCol.label && x.type == newCol.type);
                            if (!exists)
                            {
                                GraphOptions.Columns.Add(newCol);
                            }
                        }
                    }
                }

                _ = RerunGraph();

            }
        }

        public async Task RerunGraph()
        {
            if (JSRuntime != null && GraphOptions != null && GraphOptions.GraphType == "LineChart" && GraphOptions.GraphElement != null)
            {
                await JSRuntime.InvokeVoidAsync("loadGraphService.loadLineGraph", GraphOptions.GraphElement, JsonConvert.SerializeObject(queryResults), JsonConvert.SerializeObject(GraphOptions.Columns), JsonConvert.SerializeObject(GraphOptions.GraphSettings));
            }
        }

    }
}
