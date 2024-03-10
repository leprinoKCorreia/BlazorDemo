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

        // Takes in a string (the option) and an object which contains the dropdown name and what to set it to.
        // EX: [Key: Greeley, Value: Object("LineSelect",Dictionary<string, string> optionValues or List<string> options)]
        public Dictionary<string,object>? FixedDependencies { get; set; } 

    }

}
