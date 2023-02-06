using System;

namespace StudentEnrolment.API.Models.Student
{
	public class StudentDetails
	{
        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string KnownAs { get; set; }

        public string DisplayName { get; set; }

        public string DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string UniversityEmail { get; set; }

        public string NetworkId { get; set; }

        public string HomeOrOverseas { get; set; }

        public List<CourseEnrolment> CourseEnrolment { get; set; }
    }
}
