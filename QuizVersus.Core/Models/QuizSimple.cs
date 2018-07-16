namespace QuizVersus.Core.Models
{
    public class SendedQuizSimple
    {
        public int Id { get; set; }
        public string ReciverFullName { get; set; }
        public int QuestionCount { get; set; }
        public int? ReciverResult { get; set; }
        public int? SenderResult { get; set; }
    }

    public class RecivedQuizSimple
    {
        public int Id { get; set; }
        public string SenderFullName { get; set; }
        public int QuestionCount { get; set; }
        public int? ReciverResult { get; set; }
        public int? SenderResult { get; set; }
    }
}
