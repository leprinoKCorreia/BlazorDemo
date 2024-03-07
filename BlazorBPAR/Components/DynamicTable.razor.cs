using BlazorBPAR.Objects;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace BlazorBPAR.Components
{
    partial class DynamicTable
    {
        // Params
        [Parameter]
        public IEnumerable<IDictionary<string, object>>? data { get; set; }
        [Parameter]
        public string TableCssClass { get; set; } = string.Empty;
        [Parameter]
        public GridOptions? GridOptions { get; set; }

        // other important features
        public IDictionary<string, Type>? columns { get; set; }
        public IList<IDictionary<string, object>>? selectedItems { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (data != null && data.Any())
            {
                columns = GetColumnTypes(data.First());
            }
        }

        public IDictionary<string, Type> GetColumnTypes(IDictionary<string, object> row)
        {
            Dictionary<string, Type>? columnTypes = new Dictionary<string, Type>();

            foreach (var column in row)
            {
                columnTypes[column.Key] = column.Value.GetType() ?? typeof(object);
            }
            return columnTypes;
        }

        public string GetColumnPropertyExpression(string name, Type type)
        {
            var expression = $@"it[""{name}""].ToString()";

            if (type == typeof(int))
            {
                return $"int.Parse({expression})";
            }
            else if (type == typeof(DateTime))
            {
                return $"DateTime.Parse({expression})";
            }

            return expression;
        }



    }
}
