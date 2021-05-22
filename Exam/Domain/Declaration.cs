using System;
using Domain.Identity;

namespace Domain
{
    public class Declaration : DomainEntityId
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public Guid SubjectId { get; set; }
        public Subject? Subject { get; set; }
        
        public EDeclarationStatus DeclarationStatus { get; set; }
    }

    public enum EDeclarationStatus
    {
        
    }
}