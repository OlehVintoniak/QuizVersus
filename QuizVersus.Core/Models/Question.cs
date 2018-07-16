﻿namespace QuizVersus.Core.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string Category { get; set; }
    }
}