using System;
using Car.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class CarAccessType : DomainEntityId
    {
        public Guid NameId { get; set; }
        public LangString? Name { get; set; }
        public int AccessLevel { get; set; }
    }
}