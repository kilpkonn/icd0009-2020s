using System.Collections.Generic;

namespace PublicApi.DTO
{
    public class SemesterStats
    {
        public string Name { get; set; } = default!; 
        public int Subjects { get; set; }
        public int Students { get; set; }
        public int Homeworks { get; set; }
        public double AverageGrade { get; set; }
        public int GradedStudents { get; set; }
        public int FailedStudents { get; set; }
    }
    public class Stats
    {
        public List<SemesterStats> Semesters = new();
    }
}