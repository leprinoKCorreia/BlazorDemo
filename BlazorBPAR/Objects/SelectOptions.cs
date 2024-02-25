using Microsoft.AspNetCore.Components;

namespace BlazorBPAR.Objects
{
    public class SelectOptions
    {
        // Required Fields
        public string? IDName { get; set; }

        public string? Label { get; set; }


        // Optional Fields
        public List<string>? dependencies { get; set; }

        public List<string>? options { get; set; }

        public int? dataMaxOptions { get; set; }

        public string? defaultValue { get; set; }

        public bool useQuery { get; set; } = true;

        public string? query { get; set; }

        public string? connection { get; set; }

        public List<string>? queryParams { get; set; }
    }

}
