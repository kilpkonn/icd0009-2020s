using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public ICollection<LecturerSubject>? LecturerSubjects { get; set; }
        public ICollection<Declaration>? Declarations { get; set; }
        public ICollection<Grade>? Grades { get; set; }
    }
    
}