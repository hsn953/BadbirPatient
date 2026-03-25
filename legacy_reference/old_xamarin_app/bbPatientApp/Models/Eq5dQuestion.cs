using System;
using System.Collections.Generic;
using System.Text;

namespace bbPatientApp.Models
{
    public class Eq5dQuestion
    {
        public string TopLabel { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public string Answer0 { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public int? AnswerValue { get; set; }
        public string RbGroupName { get; set; }
    }
}
