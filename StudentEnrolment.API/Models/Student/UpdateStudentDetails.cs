using System;
namespace StudentEnrolment.API.Models.Student
{
	public class UpdateStudentDetails
	{

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string KnownAs { get; set; }

        public string DisplayName { get; set; }

        public string DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string HomeOrOverseas { get; set; }

    }
}

