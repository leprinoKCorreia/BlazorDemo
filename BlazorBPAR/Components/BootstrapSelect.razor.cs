using BlazorBPAR.Objects;
using BlazorBPAR.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace BlazorBPAR.Components
{
    partial class BootstrapSelect
    {
        [Parameter]
        public SelectOptions? SelectOptions { get; set; }
        [Parameter]
        public List<BootstrapSelect>? bootstrapSelects { get; set; }

        [Inject] public IJSRuntime? JSRuntime { get; set; }

        public IList<Dictionary<string, object>>? queryResults;
        bool isntFirstRun = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            PopulateDropdown();
        }

        public async void PopulateDropdown()
        {
            if (SelectOptions != null)
            {
                if (SelectOptions.options == null || SelectOptions.options.Count() == 0)
                {
                    if (SelectOptions.useQuery && SelectOptions.query != null && SelectOptions.connection != null)
                    {
                        string? queryToRun = "";
                        if (SelectOptions.queryParams != null)
                        {
                            foreach (var param in SelectOptions.queryParams)
                            {
                                if (inputData.GetValue(param) != null)
                                {
                                    queryToRun = SelectOptions.query.Replace("$" + param + "$", inputData.GetValue(param).ToString());
                                }
                                else
                                {
                                    queryToRun = SelectOptions.query.Replace("$" + param + "$", "");
                                }
                            }
                        }
                        else
                        {
                            queryToRun = SelectOptions.query;
                        }

                        SelectOptions.options = new List<string>();
                        queryResults = SQLQueryService.RunQuery(queryToRun, SelectOptions.connection);

                        // Cycle through query results to get the values for the dropdown add to options list
                        foreach (var result in queryResults)
                        {
                            SelectOptions.options.Add((string)result["Option"]);
                        }

                        StateHasChanged();
                        
                        refreshDropdown();
                    }
                }
            }
        }

        public async void refreshDropdown()
        {
            if (JSRuntime != null && isntFirstRun && SelectOptions != null)
            {
                await Task.Delay(1); // DO NOT EVER MOVE THIS. I DONT KNOW WHY BUT THIS WONT WORK UNLESS WE DELAY A MILISECOND
                string js = "$('#" + SelectOptions.IDName + "').selectpicker('refresh');";
                Console.WriteLine(js);
                await JSRuntime.InvokeVoidAsync("eval", js);
                await Task.Delay(1); // DO NOT EVER MOVE THIS. I DONT KNOW WHY BUT THIS WONT WORK UNLESS WE DELAY A MILISECOND
                js = "$('#" + SelectOptions.IDName + "').selectpicker('selectAll');";
                Console.WriteLine(js);
                await JSRuntime.InvokeVoidAsync("eval", js);
            }
        }


        private async Task OnDataChange(ChangeEventArgs e, string key)
        {
            //var SelectVal = string.Join(",", e.Value);
            if(e.Value is IEnumerable<string> values)
            {
                string SelectVal = string.Join(",", values);
                inputData.SetValue(key, value: SelectVal);
                
                if (SelectOptions != null && SelectOptions.dependencies != null && bootstrapSelects != null)
                {
                    foreach (var dependency in SelectOptions.dependencies)
                    {
                        foreach (var select in bootstrapSelects)
                        {
                            if (select.SelectOptions != null && dependency == select.SelectOptions.IDName)
                            {
                                select.SelectOptions.options = new List<string>();
                                select.isntFirstRun = true;
                                select.PopulateDropdown();
                            }
                        }
                    }
                }
            }               
        }
    }
}
