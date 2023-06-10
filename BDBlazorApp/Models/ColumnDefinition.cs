namespace BDBlazorApp.Models
{
    public class ColumnDefinition
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public bool EditMode { get; set; }
        public SortingDefinition Sorting { get; set; }
    }
}
