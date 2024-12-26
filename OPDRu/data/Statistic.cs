namespace OPDRu.data
{
    public class Statistic
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int TestId { get; set; }
        public Test Test { get; set; } = null!;
        public DateTime DateTaken { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public double ScorePercentage => (double)CorrectAnswers / TotalQuestions * 100;
    }

}
