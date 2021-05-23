using System;
using System.Collections.Generic;
using Domain;

namespace PublicApi.DTO
{
    public class Subject : DomainEntityId
    {
        public string Title { get; set; } = default!;
        
        public string Description { get; set; } = default!;
        
        public Guid SemesterId { get; set; }

        public ICollection<Declaration>? Declarations { get; set; }
        
        public ICollection<Homework>? Homeworks { get; set; }
    }
    
    public class NewSubject : DomainEntityId
    {
        public string Title { get; set; } = default!;
        
        public string Description { get; set; } = default!;
        
        public Guid SemesterId { get; set; }

    }
}