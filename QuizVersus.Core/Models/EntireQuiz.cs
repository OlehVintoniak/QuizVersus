using System.Collections.Generic;

namespace QuizVersus.Core.Models
{
    public class EntireQuiz
    {
        public EntireQuiz()
        {
            Questions = new List<Question>();
        }
        public int Id { get; set; }
        public string ReciverFullName { get; set; }
        public string SenderFullName { get; set; }

        public IEnumerable<Question> Questions { get; set; }
    }
}
