using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorBPAR.Pages
{
    partial class Index
    {
        [Inject] public IJSRuntime? JSRuntime { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && JSRuntime != null) // only needs to be called once per page render
            {
                await JSRuntime.InvokeVoidAsync("selectPickerService.init");
            }
        }


        public void openPage(string page)
        {
            @nav.NavigateTo(page);
        }
    }
}
