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

        protected override void OnInitialized()
        {

        }

        public void runGraph()
        {
            // Update query and confirm results exist
            if (GraphOptions != null && GraphOptions.Query != null && GraphOptions.Connection != null)
            {
                string? queryToRun = "";
                if (GraphOptions.queryParams != null)
                {
                    foreach (var param in GraphOptions.queryParams)
                    {
                        if (inputData.GetValue(param) != null)
                        {
                            queryToRun = GraphOptions.Query.Replace("$" + param + "$", inputData.GetValue(param).ToString());
                        }
                        else
                        {
                            queryToRun = GraphOptions.Query.Replace("$" + param + "$", "");
                        }
                    }
                }
                else
                {
                    queryToRun = GraphOptions.Query;
                }

                queryResults = SQLQueryService.RunQuery(queryToRun, GraphOptions.Connection);

                // read through unique columns to get the data and their datatypes
                if (GraphOptions.GraphType == "LineChart" && queryResults.Count() > 0)
                {
                    GraphOptions.Columns = new List<cols>();
                    float f = 0;
                    Int64 intVal = 0;
                    foreach (var row in queryResults)
                    {
                        foreach (var entry in row)
                        {
                            if(entry.Value.ToString() != "")
                            {
                                cols newCol;
                                if (entry.Key.ToString() == "XAxis")
                                {
                                    newCol = new cols { label = entry.Key.ToString(), type = "datetime" };
                                }
                                else
                                {
                                    if (float.TryParse(entry.Value.ToString(), out f) || Int64.TryParse(entry.Value.ToString(), out intVal))
                                    {
                                        newCol = new cols { label = entry.Key.ToString(), type = "number" };
                                    }
                                    else
                                    {
                                        newCol = new cols { label = entry.Key.ToString(), type = "string" };
                                    }
                                }

                                bool exists = GraphOptions.Columns.Exists(x => x.label == newCol.label && x.type == newCol.type);
                                if (!exists)
                                {
                                    GraphOptions.Columns.Add(newCol);
                                }
                            }
                        }
                    }

                    _ = rerunGraph();
                }
            }
        }

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender && JSRuntime != null && queryResults != null && GraphOptions != null) // only needs to be called once per page render
        //    {
        //        if(GraphOptions.GraphType == "LineChart" && GraphOptions.GraphElement != null)
        //        {
        //            await JSRuntime.InvokeVoidAsync("loadGraphService.loadLineGraph", GraphOptions.GraphElement, JsonConvert.SerializeObject(queryResults), JsonConvert.SerializeObject(GraphOptions.Columns), JsonConvert.SerializeObject(GraphOptions.GraphSettings));
        //        }
        //    }
        //}     


        public async Task rerunGraph()
        {
            //if (GraphOptions != null && GraphOptions.Query != null && GraphOptions.Connection != null)
            //{
            //    queryResults = SQLQueryService.RunQuery(GraphOptions.Query, GraphOptions.Connection);
            //}
            if (JSRuntime != null && GraphOptions != null && GraphOptions.GraphType == "LineChart" && GraphOptions.GraphElement != null)
            {
                await JSRuntime.InvokeVoidAsync("loadGraphService.loadLineGraph", GraphOptions.GraphElement, JsonConvert.SerializeObject(queryResults), JsonConvert.SerializeObject(GraphOptions.Columns), JsonConvert.SerializeObject(GraphOptions.GraphSettings));
            }
        }

    }
}
