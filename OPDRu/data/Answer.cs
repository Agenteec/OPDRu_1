using System.ComponentModel.DataAnnotations.Schema;

namespace OPDRu.data
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
        public bool IsCorrect { get; set; }
        [NotMapped]
        public bool IsSelected { get; set; } = false;
    }

}
