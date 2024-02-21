using Microsoft.AspNetCore.Components;

namespace BlazorBPAR.Objects
{
    public class SelectOptions
    {

        public string? IDName { get; set; }

        public string? Label { get; set; }

        public bool useQuery { get; set; }

        public string? query { get; set; }

        public string? connection { get; set; }

        public List<string>? options { get; set; }

        public List<string>? queryParams { get; set; }

        public List<string>? dependencies { get; set; }

    }

}
