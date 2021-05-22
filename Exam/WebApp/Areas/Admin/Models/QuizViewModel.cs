using System.Collections.Generic;
using Domain;

namespace WebApp.Areas.Admin.Models
{
    public class QuizViewModel
    {
        public Quiz? Quiz { get; set; }
        public QuizQuestion? QuizQuestion { get; set; }
    }
}