using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Homework : DomainEntityId
    {
        [MaxLength(64)]
        public string Title { get; set; } = default!;
        
        [MaxLength(500)]
        public string Description { get; set; } = default!;

        public Guid SubjectId { get; set; }
        public Subject? Subject { get; set; }
        
        public Guid? QuizId { get; set; }
        public Quiz? Quiz { get; set; }
        
        public ICollection<Submission>? Submissions { get; set; }
    }
}