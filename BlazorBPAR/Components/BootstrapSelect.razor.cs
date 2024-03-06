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
        public List<BootstrapSelect>? BootstrapSelects { get; set; }

        [Inject] public IJSRuntime? JSRuntime { get; set; }

        public IList<Dictionary<string, object>>? queryResults;
        public bool isntFirstRun = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            PopulateDropdown();
        }

        public void PopulateDropdown()
        {
            if (SelectOptions != null)
            {
                if (SelectOptions.Options == null || SelectOptions.Options.Count == 0)
                {
                    if (SelectOptions.UseQuery && SelectOptions.Query != null && SelectOptions.Connection != null)
                    {
                        string? queryToRun = "";
                        if (SelectOptions.QueryParams != null)
                        {
                            foreach (var param in SelectOptions.QueryParams)
                            {
                                if (inputData.GetValue(param) != null)
                                {
                                    queryToRun = SelectOptions.Query.Replace("$" + param + "$", inputData.GetValue(param).ToString());
                                }
                                else
                                {
                                    queryToRun = SelectOptions.Query.Replace("$" + param + "$", "");
                                }
                            }
                        }
                        else
                        {
                            queryToRun = SelectOptions.Query;
                        }

                        SelectOptions.Options = new List<string>();
                        queryResults = SQLQueryService.RunQuery(queryToRun, SelectOptions.Connection);

                        // Cycle through query results to get the values for the dropdown add to options list
                        foreach (var result in queryResults)
                        {
                            SelectOptions.Options.Add((string)result["Option"]);
                        }

                        StateHasChanged();

                        RefreshDropdown();
                    }
                }
            }
        }

        public async void RefreshDropdown()
        {
            if (JSRuntime != null && isntFirstRun && SelectOptions != null)
            {
                await Task.Delay(1); // DO NOT EVER MOVE THIS. I DONT KNOW WHY BUT THIS WONT WORK UNLESS WE DELAY A MILISECOND
                string js = "$('#" + SelectOptions.IDName + "').selectpicker('refresh');";
                await JSRuntime.InvokeVoidAsync("eval", js);
                await Task.Delay(1); // DO NOT EVER MOVE THIS. I DONT KNOW WHY BUT THIS WONT WORK UNLESS WE DELAY A MILISECOND
                js = "$('#" + SelectOptions.IDName + "').selectpicker('selectAll');";
                await JSRuntime.InvokeVoidAsync("eval", js);
                isntFirstRun = false;
            }
        }

        private void OnDataChange(ChangeEventArgs e, string key)
        {
            if (e.Value is IEnumerable<string> values)
            {
                string SelectVal = string.Join(",", values);
                inputData.SetValue(key, value: SelectVal);

                if (SelectOptions != null && SelectOptions.Dependencies != null && BootstrapSelects != null)
                {
                    foreach (var dependency in SelectOptions.Dependencies)
                    {
                        foreach (var select in BootstrapSelects)
                        {
                            if (select.SelectOptions != null && dependency == select.SelectOptions.IDName)
                            {
                                select.SelectOptions.Options = new List<string>();
                                select.isntFirstRun = true;
                                select.PopulateDropdown();
                            }
                        }
                    }
                }
            }
        }

        public string ReturnFalseIfNull(int? value)
        {
            return value.HasValue ? value.Value.ToString() : "false";
        }

    }
}
