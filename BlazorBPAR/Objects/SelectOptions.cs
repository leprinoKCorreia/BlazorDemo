using Microsoft.AspNetCore.Components;

namespace BlazorBPAR.Objects
{
    public class SelectOptions
    {
        // Required Fields
        public string? IDName { get; set; }

        public string? Label { get; set; }


        // Optional Fields
        public List<string>? Dependencies { get; set; }

        public List<string>? Options { get; set; }

        public int? DataMaxOptions { get; set; }

        public string? DefaultValue { get; set; }

        public bool UseQuery { get; set; } = true;

        public string? Query { get; set; }

        public string? Connection { get; set; }

        public List<string>? QueryParams { get; set; }
    }

}
