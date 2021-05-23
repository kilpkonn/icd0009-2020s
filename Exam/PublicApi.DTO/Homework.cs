using System;
using System.Collections.Generic;
using Domain;

namespace PublicApi.DTO
{
    public class Homework : DomainEntityId
    {
        public string Title { get; set; } = default!;
        
        public string Description { get; set; } = default!;

        public Guid SubjectId { get; set; }

        public ICollection<Submission>? Submissions { get; set; }
    }
    
    public class NewHomework : DomainEntityId
    {
        public string Title { get; set; } = default!;
        
        public string Description { get; set; } = default!;

        public Guid SubjectId { get; set; }
    }
}