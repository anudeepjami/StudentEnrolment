using System;
namespace StudentEnrolment.API.Models.Student
{
	public class UpdateStudentDetails
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

        public List<UpdateCourseEnrolment> UpdateCourseEnrolment { get; set; }

    }
    public class UpdateCourseEnrolment
    {
        public string EnrolmentId { get; set; }

        public string AcademicYear { get; set; }

        public string YearOfStudy { get; set; }

        public string Occurrence { get; set; }

        public string ModeOfAttendance { get; set; }

        public string EnrolmentStatus { get; set; }

        public string CourseEntryDate { get; set; }

        public string ExpectedEndDate { get; set; }

        public UpdateCourse UpdateCourse { get; set; }
    }
    public class UpdateCourse
    {
        public string CourseCode { get; set; }

        public string CourseName { get; set; }
    }
}

