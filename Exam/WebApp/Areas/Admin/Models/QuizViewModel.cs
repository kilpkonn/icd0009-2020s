using Domain;

namespace WebApp.Areas.Admin.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class QuizViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Quiz? Quiz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public QuizQuestion? QuizQuestion { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public QuizOption? QuizOption { get; set; }
    }
}