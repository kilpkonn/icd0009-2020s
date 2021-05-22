using System;
using Domain.Identity;

namespace Domain
{
    public class LecturerSubject: DomainEntityId
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public Guid SubjectId { get; set; }
        public Subject? Subject { get; set; }
    }
}