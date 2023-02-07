using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StudentEnrolment.API.Models.Student;
using StudentEnrolment.API.Models.Response;
using StudentEnrolment.API.Services;
using StudentEnrolment.API.Models.Course;
using System.Linq;

namespace StudentEnrolment.API.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{

    [HttpGet]
    public List<StudentDetails> GetAll()
    {
        try
        {
            ReadJSON readJson = new ReadJSON();
            List<StudentDetails> studentDetails = new List<StudentDetails>();
            studentDetails = readJson.ReadStudentJSON();
            return studentDetails;
        }
        catch (Exception ex) {
            return new List<StudentDetails>();
        }
    }

    [HttpGet]
    [Route("{id:int}")]
    public StudentDetails GetStudentById(int id)
    {
        try
        {
            int i = id;
            ReadJSON readJson = new ReadJSON();
            List<StudentDetails> studentDetails = new List<StudentDetails>();
            studentDetails = readJson.ReadStudentJSON();
            StudentDetails student = (from stud in studentDetails where stud.StudentId == id select stud).First();
            return student;
        }
        catch (Exception ex) {
            return new StudentDetails();
        }
    }

    //[HttpPost]
    //[Route("{id:int}")]
    //public StudentDetails DeleteStudentById(int id)
    //{
    //    try
    //    {
    //        int i = id;
    //        ReadJSON readJson = new ReadJSON();
    //        var studentDetails = new List<StudentDetails>();
    //        studentDetails = readJson.ReadStudentJSON();
    //        StudentDetails student = (from stud in studentDetails where stud.StudentId == id select stud).First();
    //        studentDetails = studentDetails.Except((IEnumerable<StudentDetails>)student).ToList();
    //        return student;
    //    }
    //    catch (Exception ex)
    //    {
    //        return new StudentDetails();
    //    }
    //}

    [HttpPost]
    [Route("{id:int}")]
    public Response UpdateStudentById(
        [FromRoute] int id,
        [FromBody] UpdateStudentDetails studentUpdate
    )
    {
        try
        {
            int i = id;
            ReadJSON readJson = new ReadJSON();
            List<StudentDetails> studentDetails = new List<StudentDetails>();
            studentDetails = readJson.ReadStudentJSON();
            bool updateSuccess = false;
            foreach (StudentDetails student in studentDetails) {
                if (student.StudentId == id)
                {
                    int index = studentDetails.IndexOf(student);
                    studentDetails[index].FirstName = studentUpdate.FirstName;
                    studentDetails[index].LastName = studentUpdate.LastName;
                    studentDetails[index].KnownAs = studentUpdate.KnownAs;
                    studentDetails[index].DisplayName = studentUpdate.DisplayName;
                    studentDetails[index].DateOfBirth = studentUpdate.DateOfBirth;
                    studentDetails[index].Gender = studentUpdate.Gender;
                    studentDetails[index].UniversityEmail = studentUpdate.UniversityEmail;
                    studentDetails[index].NetworkId = studentUpdate.NetworkId;
                    studentDetails[index].HomeOrOverseas = studentUpdate.HomeOrOverseas;
                    foreach(var item in studentUpdate.UpdateCourseEnrolment)
                    {
                        CourseEnrolment courseEnrolment = new CourseEnrolment();
                        courseEnrolment.EnrolmentId = item.EnrolmentId;
                        courseEnrolment.AcademicYear = item.AcademicYear;
                        courseEnrolment.YearOfStudy = item.YearOfStudy;
                        courseEnrolment.Occurrence = item.Occurrence;
                        courseEnrolment.ModeOfAttendance = item.ModeOfAttendance;
                        courseEnrolment.EnrolmentStatus = item.EnrolmentStatus;
                        courseEnrolment.CourseEntryDate = item.CourseEntryDate;
                        courseEnrolment.ExpectedEndDate = item.ExpectedEndDate;
                        Course course = new Course()
                        {
                            CourseCode = item.UpdateCourse.CourseCode,
                            CourseName = item.UpdateCourse.CourseName
                        };
                        
                    }
                    };
                    updateSuccess = true;
                }
                if (updateSuccess == false)
                    throw new Exception("Student Update Error");
                else {
                    UpdateJSON updateJSON = new UpdateJSON();
                    string message = updateJSON.UpdateStudentJSON(studentDetails);
                    return new Response
                    {
                        message = "Student Sucessfully Updated"
                    };
            }
        }
        catch (Exception ex)
        {
            return new Response
            {
                message = ex.Message
            };
        }

    }

}

