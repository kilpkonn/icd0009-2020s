using System;
using System.Collections.Generic;

namespace Domain
{
    public class QuizQuestion: DomainEntityId
    {
        public string Text { get; set; } = default!;
        
        public Guid QuizId { get; set; }
        public Quiz? Quiz { get; set; }

        public ICollection<QuizOption>? QuizOptions { get; set; } = new List<QuizOption>();

    }
}