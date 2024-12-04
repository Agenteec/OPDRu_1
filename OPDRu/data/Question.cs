namespace OPDRu.data
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int TestId { get; set; }
        public Test Test { get; set; } = null!;
        public List<Answer> Answers { get; set; } = new();
    }

}
