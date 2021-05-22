using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain
{
    public class Subject : DomainEntityId
    {
        [MaxLength(64)]
        public string Title { get; set; } = default!;
        
        [MaxLength(500)]
        public string Description { get; set; } = default!;

        public Guid SemesterId { get; set; }
        public Semester? Semester { get; set; }

        public ICollection<Declaration>? Declarations { get; set; }
        
        public ICollection<LecturerSubject>? LecturerSubjects { get; set; }
        
        public ICollection<Homework>? Homeworks { get; set; }
        
        public ICollection<Grade>? Grades { get; set; }
        
    }
}