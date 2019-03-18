namespace WordAddIn1.ApiObjects
{
    public class Viewpoint
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public string ViewType { get; set; }
        public string Source { get; set; }
        public string ViewName { get; set; }
        public string ModelName { get; set; }
        public Image Image { get; set; }
    }
}