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

        public bool ProvideOptionValues { get; set; } = false;

        public Dictionary<string,string>? OptionValues { get; set; }

        public List<FixedSelectDependency>? FixedDependencies { get; set; }

    }

    public class FixedSelectDependency
    {
        public string Dropdown { get; set; } = "";
        public Dictionary<string, List<string>>? Options { get; set; }
        public Dictionary<string, Dictionary<string, string>>? OptionValues { get; set; }
    }

}
