using System;
using Domain;

namespace PublicApi.DTO
{
    public class Submission : DomainEntityId
    {
        public string Value { get; set; } = default!;
        
        public Guid HomeworkId { get; set; }
        
        // public Guid? QuizId { get; set; }
        // public Quiz? Quiz { get; set; }
        
        public Guid? GradeId { get; set; }
        public Grade? Grade { get; set; }
        
        public Guid AppUserId { get; set; }
    }
    
    public class NewSubmission : DomainEntityId
    {
        public string Value { get; set; } = default!;
        
        public Guid HomeworkId { get; set; }

        public Guid? GradeId { get; set; }
        
        public Guid AppUserId { get; set; }
    }
}