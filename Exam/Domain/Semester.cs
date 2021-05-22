using System;
using System.Collections.Generic;

namespace Domain
{
    public class Semester : DomainEntityId
    {
        // [Display] 
        public string Name { get; set; } = default!;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<Subject>? Subjects { get; set; }
    }
}