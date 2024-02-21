using Microsoft.AspNetCore.Components.Web;

namespace BlazorBPAR.Objects
{
    public class Series
    {
        public bool Smooth { get; set; } = true;
        public List<SeriesData>? Data {  get; set; }
        public string? CategoryProperty { get; set; } = "XAxis";
        public string? Title {  get; set; }
        public string? ValueProperty { get; set; } = "Value";
        public bool showMarkers { get; set; } = true;
        public bool showDataLabels { get; set; } = true;
    }
}
