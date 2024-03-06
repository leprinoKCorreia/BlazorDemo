using BlazorBPAR.Objects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;


namespace BlazorBPAR.Components
{
    partial class BootstrapSelectList
    {
        [Inject] public IJSRuntime? JSRuntime { get; set; }

        [Parameter]
        public List<SelectOptions>? SelectOptions { get; set; }

        public List<BootstrapSelect> _refs = new();

        public BootstrapSelect Ref { set => _refs.Add(value); }


        public async Task InitDefaultValues()
        {
            foreach (var select in _refs)
            {
                if (select != null && select.SelectOptions != null && select.SelectOptions.DefaultValue != null && select.SelectOptions.Dependencies != null && JSRuntime != null && select.SelectOptions.IDName != null)
                {
                    await Task.Delay(1); // DO NOT EVER MOVE THIS. I DONT KNOW WHY BUT THIS WONT WORK UNLESS WE DELAY A MILISECOND
                    string js = "$('#" + select.SelectOptions.IDName + "').selectpicker('val', '" + select.SelectOptions.DefaultValue + "');";
                    await JSRuntime.InvokeVoidAsync("eval", js);
                    await Task.Delay(1); // DO NOT EVER MOVE THIS. I DONT KNOW WHY BUT THIS WONT WORK UNLESS WE DELAY A MILISECOND
                    js = "$('#" + select.SelectOptions.IDName + "').selectpicker('refresh');";
                    await JSRuntime.InvokeVoidAsync("eval", js);

                    inputData.SetValue(select.SelectOptions.IDName, value: select.SelectOptions.DefaultValue);

                    foreach (var dependency in select.SelectOptions.Dependencies)
                    {
                        foreach (var selectToRefresh in _refs)
                        {
                            if (selectToRefresh.SelectOptions != null && dependency == selectToRefresh.SelectOptions.IDName)
                            {
                                selectToRefresh.SelectOptions.Options = new List<string>();
                                selectToRefresh.isntFirstRun = true;
                                selectToRefresh.PopulateDropdown();
                            }
                        }
                    }
                }
            }
        }


    }
}
