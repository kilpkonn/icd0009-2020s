using Domain;

namespace PublicApi.DTO
{
    public class Grade : DomainEntityId
    {
        public int? Value { get; set; }

        public EGradeType GradeType { get; set; }
    }
}