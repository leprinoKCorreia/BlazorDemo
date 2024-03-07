
using Radzen;

namespace BlazorBPAR.Objects
{
    public class GridOptions
    {
        // Documentation https://blazor.radzen.com/docs/guides/components/datagrid.html
        // Sorting
        public bool AllowSorting { get; set; } = true;
        public bool AllowMultiColumnSorting { get; set; } = true;
        // Paging
        public bool AllowPaging { get; set; } = false;
        public int PageSize { get; set; } = 0;
        // Filtering
        public bool AllowFiltering { get; set; } = true;
        public string LogicalFilterOperator { get; set; } = string.Empty;
        // Grouping
        public bool AllowGrouping { get; set; } = false;
        public List<GroupDescriptor>? Groups { get; set; }
        // Columns
        public bool AllowColumnResize { get; set; } = true;
        public bool AllowColumnReorder { get; set; } = true;
        public bool AllowVirtualization { get; set; } = true;

        public string Height { get; set; } = "400px";

        public List<GridCols>? Columns { get; set; }

    }

    public class GridCols
    {
        // Column Specfic
        public string ColumnName { get; set; } = string.Empty;
        public bool Sortable { get; set; } = true;
        public bool Filterable { get; set; } = true;
        public bool Groupable { get; set; } = true;
    }

}
