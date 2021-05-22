using System.Collections.Generic;

namespace Domain
{
    public class Quiz : DomainEntityId
    {
        public ICollection<QuizQuestion>? QuizQuestions { get; set; }
    }
}