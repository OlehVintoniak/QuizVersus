using System.Collections.Generic;

namespace QuizVersus.Core.Models.Quiz
{
    public class CommitedQuiz
    {
        public int Id { get; set; }
        public List<CommitedQuestion> Questions { get; set; } = new List<CommitedQuestion>();
    }

    public class CommitedQuestion
    {
        public int Id { get; set; }
        public int CheckedAnswer  { get; set; }
    }
}
