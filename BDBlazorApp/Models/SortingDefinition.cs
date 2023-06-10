namespace BDBlazorApp.Models
{
    public class SortingDefinition
    {
        public bool Enabled { get; set; } = true;
        public bool Chosen { get; set; }
        public SortingDirection Direction { get; set; }
    }
}
