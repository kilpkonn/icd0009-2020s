using System;
using Domain.Identity;

namespace Domain
{
    public class QuizAnswer : DomainEntityId
    {
        public Guid QuizOptionId { get; set; }
        public QuizOption? QuizOption { get; set; }
        
        // public Guid SubmissionId { get; set; }
        // public Submission? Submission { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}