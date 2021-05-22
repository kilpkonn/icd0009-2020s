using System;
using System.Collections.Generic;

namespace Domain
{
    public class QuizOption: DomainEntityId
    {
        public string Text { get; set; } = default!;
        
        // [Range(0, 1)]
        public float Score { get; set; }
        
        public Guid QuizQuestionId { get; set; }
        public QuizQuestion? QuizQuestion { get; set; }
        
        public ICollection<QuizAnswer>? QuizAnswers { get; set; }
    }
}