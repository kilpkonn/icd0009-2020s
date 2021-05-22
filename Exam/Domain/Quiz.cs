using System.Collections.Generic;

namespace Domain
{
    public class Quiz : DomainEntityId
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        
        public ICollection<QuizQuestion>? QuizQuestions { get; set; }
    }
}