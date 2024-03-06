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
        public GraphAxis? hAxis { get; set; } = new GraphAxis();
        public bool? interpolateNulls { get; set; } = true;  // Options: true, false
        public graphLegend? legend { get; set; } // controls options for legend. Options: graphLegend object
        public string? lineWidth { get; set; } // all series will follow this unless they are specified. Options: int
        public List<series>? series { get; set; }
        public string? title { get; set; }
        public GraphAxis? vAxis { get; set; } = new GraphAxis();
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

    public class GraphAxis
    {
        public int? baseline { get; set; } // Baseline for axis if axis is Continuous
        public string? baselineColor { get; set; } // String for color of baseline. Options: color name, Hex value
        public string? direction { get; set; } = "1"; // Direction of values on the axis. Options 1 or -1
        public string? format { get; set; } // Controls numeric or date formatting. Options: 'none', 'decimal', 'scientific', 'currency', 'percent', 'short','long', valid ICU Date pattern
        public graphGridLines? gridlines { get; set; }
        public string? textPosition { get; set; } = "out"; // Position of axis text. Options: 'out', 'in', 'none'
        public textStyle? textStyle { get; set; } // Style of axis text. Takes a textStyle option
        public List<object>? ticks { get; set; } // Define specific values to show along Axis. Options: Array of numbers, array of dates, array of strings
        public string? title { get; set; } // Title for Axis. Options: String
        public textStyle? titleTextStyle { get; set; } // Text Style Object defines Axis Title settings
        public viewWindow? viewWindow { get; set; } // Controls axis lowest and highest values. Options: min => number, max => number
    }

    public class graphGridLines
    {
        public string? color { get; set; } // color string. Options: Hex value or color name
        public string? count { get; set; } // number of gridlines in the area. Options: number, 0, -1, etc
        public int? minSpacing { get; set; } // Space in pixels between major gridlines. default is 40, if not specified when using count, this is calculated. 
        public int? multiple {  get; set; } // number of gridlines must be a multiple of this. Options: integer
    }

    public class graphLegend
    {
        public string? alignment { get; set; } // Alignment of legend. Options: 'start','center','end'
        public string? position { get; set; } // Position of Legend. Options: 'bottom','left','in','none','right','top'
        public textStyle? textStyle { get; set; } // Text Style Object for Legend
    }

    public class series
    {
        public string? lineWidth { get; set; }
        public string? pointSize { get; set; }
        public string? color { get; set; }
    }

    public class textStyle
    {
        public string? color { get; set; } = "black";
        public string? fontName { get; set; } = "";
        public string? fontSize { get; set; } = "7px";
        public bool? bold { get; set; } = false;
        public bool? italic { get; set; } = false;
    }

    public class viewWindow
    {
        public string? min { get; set; }
        public string? max { get; set; }
    }
}
