using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class QuizQuestion: DomainEntityId
    {
        [Display(Name = "Question")]
        public string Text { get; set; } = default!;
        
        public Guid QuizId { get; set; }
        public Quiz? Quiz { get; set; }

        public ICollection<QuizOption>? QuizOptions { get; set; } = new List<QuizOption>();

    }
}