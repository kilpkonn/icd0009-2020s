using System;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain
{
    public class Grade : DomainEntityId
    {
        [Range(0, 5)]
        public int? Value { get; set; }

        public EGradeType GradeType { get; set; }

        public Guid SubjectId { get; set; }
        public Subject? Subject { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Submission? Submission { get; set; }
    }

    public enum EGradeType
    {
        Subject, Submission
    }
}