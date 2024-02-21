using BlazorBPAR.Objects;
using Microsoft.AspNetCore.Components;


namespace BlazorBPAR.Components
{
    partial class BootstrapSelectList
    {
        [Parameter]
        public List<SelectOptions>? selectOptions { get; set; }
        private List<BootstrapSelect> _refs = new();
        public BootstrapSelect Ref { set => _refs.Add(value); }
    }
}
