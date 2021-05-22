using Domain;

namespace WebApp.Areas.Admin.Models
{
    public class QuizViewModel
    {
        public Quiz? Quiz { get; set; }
        public QuizQuestion? QuizQuestion { get; set; }
        
        public QuizOption? QuizOption { get; set; }
    }
}