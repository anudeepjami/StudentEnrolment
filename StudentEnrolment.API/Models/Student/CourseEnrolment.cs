using System;
using StudentEnrolment.API.Models;

namespace StudentEnrolment.API.Models.Student
{
	public class CourseEnrolment
	{
        public string EnrolmentId { get; set; }

        public string AcademicYear { get; set; }

        public string YearOfStudy { get; set; }

        public string Occurrence { get; set; }

        public string ModeOfAttendance { get; set; }

        public string EnrolmentStatus { get; set; }

        public string CourseEntryDate { get; set; }

        public string ExpectedEndDate { get; set; }

        public Course.Course Course { get; set; }
    }
}

