using System;
using Domain;

namespace PublicApi.DTO
{
    public class Semester : DomainEntityId
    {
        // [Display] 
        public string Name { get; set; } = default!;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}