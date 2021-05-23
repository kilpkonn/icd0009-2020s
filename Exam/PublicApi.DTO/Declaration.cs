using System;
using Domain;

namespace PublicApi.DTO
{
    public class Declaration : DomainEntityId
    {
        public Guid AppUserId { get; set; }

        public Guid SubjectId { get; set; }
        
        public Guid? GradeId { get; set; }
        public Grade? Grade { get; set; }
        
        public EDeclarationStatus DeclarationStatus { get; set; }
    }
    
    public class NewDeclaration : DomainEntityId
    {
        public Guid AppUserId { get; set; }

        public Guid SubjectId { get; set; }
        
        public Guid? GradeId { get; set; }
        
        public EDeclarationStatus DeclarationStatus { get; set; }
    }
}