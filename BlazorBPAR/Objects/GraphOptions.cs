namespace BlazorBPAR.Objects
{
    public class GraphOptions
    {
        public string? GraphElement { get; set; }

        public string? Query { get; set; }

        public string? Connection { get; set; }

        public string? GraphType { get; set; }

        public List<cols>? Columns { get; set; }

        public graphSettings? GraphSettings { get; set; }
    }

    public class cols
    {
        public string? label { get; set; }
        public string? type { get; set; }
    }

    public class graphSettings
    {
        public string? Title { get; set; }
        public string? curveType { get; set; } = "function";
        public bool? interpolateNulls { get; set; } = true;
        public List<series>? series { get; set; }
        public string? width { get; set; } = "100%";
        public string? height { get; set; } = "100%";
        public vAxis? vAxis { get; set; } = new vAxis();
    }

    public class series
    {
        public string? lineWidth { get; set; }
        public string? pointSize { get; set; }
        public string? color { get; set; }
    }

    public class vAxis
    {
        public viewWindow? viewWindow { get; set; }
        public textStyle? textStyle { get; set; } = new textStyle();
    }

    public class viewWindow
    {
        public string? min { get; set; }
        public string? max { get; set; }
    }

    public class textStyle
    {
        public string? color { get; set; } = "black";
        public string? fontName { get; set; } = "";
        public string? fontSize { get; set; } = "7px";
        public bool? bold { get; set; } = false;
        public bool? italic { get; set; } = false;
    }


}
