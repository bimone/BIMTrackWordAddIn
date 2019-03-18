namespace WordAddIn1.ApiObjects
{
    public class Issue
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public int AssignedToUserId { get; set; }
        public string LastModificationDate { get; set; }
    }
}