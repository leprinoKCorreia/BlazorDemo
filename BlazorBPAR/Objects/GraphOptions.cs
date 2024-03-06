namespace BlazorBPAR.Objects
{
    public class GraphOptions
    {
        public string? GraphElement { get; set; }

        public string? Query { get; set; }

        public string? Connection { get; set; }

        public string? GraphType { get; set; }

        public List<cols>? Columns { get; set; }

        public graphSettings? GraphSettings { get; set; } = new graphSettings();  

        public List<string>? QueryParams { get; set; }

        public string? Width { get; set; } = "100%";
        public string? Height { get; set; } = "100%";


    }
    
    #pragma warning disable IDE1006 // Naming Styles convention ignored for javascript library

    public class cols

    {
        public string? label { get; set; }
        public string? type { get; set; }
    }

    public class graphSettings
    {
        public string? aggregationTarget { get; set; } = "none"; // Multiple Selections. Options: 'none', 'auto', 'series', or 'category'
        public string? axisTitlesPosition { get; set; } = "out"; // Location of Titles on Axis. Options: 'in', 'out', 'none'
        public string? backgroundColor { get; set; } = "white"; // Color of whole Graph. Options: color name or Hex value
        public chartArea? chartArea { get; set; } // Object for managing actual graph size and color inside of graph div
        public string? curveType { get; set; } = "function"; // Controls curve of lines on graph. Options: 'none', 'function'
        public string? focusTarget { get; set; } = "datum"; // Controls single point or many points on hover. Options: 'datum', 'category'
        public string? fontSize { get; set; } // Font Size of graph elements. Options: number
        public string? fontName { get; set; } = "Arial"; // Font Family CSS. Options: Same as CSS
        public bool? interpolateNulls { get; set; } = true;  
        public List<series>? series { get; set; }
        public string? title { get; set; }
        public vAxis? vAxis { get; set; } = new vAxis();
        public string? XAxisType { get; set; } = "datetime";
    }

    public class chartArea
    {
        public string? left { get; set; } // Options: pixels or percentage
        public string? right { get; set; } // Options: pixels or percentage
        public string? top { get; set; } // Options: pixels or percentage
        public string? bottom { get; set; } // Options: pixels or percentage
        public string? backgroundColor { get; set; } // Options: string, color name or Hex value
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
