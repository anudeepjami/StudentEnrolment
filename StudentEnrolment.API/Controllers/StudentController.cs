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
    [Route("{StudentId:int}")]
    public StudentDetails GetStudentById(int StudentId)
    {
        try
        {
            ReadJSON readJson = new ReadJSON();
            List<StudentDetails> studentDetails = new List<StudentDetails>();
            studentDetails = readJson.ReadStudentJSON();
            StudentDetails student = (from stud in studentDetails where stud.StudentId == StudentId select stud).First();
            return student;
        }
        catch (Exception ex) {
            return new StudentDetails();
        }
    }


    [HttpPut]
    [Route("{StudentId:int}")]
    public Response UpdateStudentDetailsById(
        [FromRoute] int StudentId,
        [FromBody] UpdateStudentDetails studentUpdate
    )
    {
        try
        {
            ReadJSON readJson = new ReadJSON();
            List<StudentDetails> studentDetails = new List<StudentDetails>();
            studentDetails = readJson.ReadStudentJSON();
            bool canBeUpdated = false;
            foreach (StudentDetails student in studentDetails) {
                if (student.StudentId == StudentId)
                {
                    int index = studentDetails.IndexOf(student);
                    studentDetails[index].FirstName = studentUpdate.FirstName;
                    studentDetails[index].LastName = studentUpdate.LastName;
                    studentDetails[index].KnownAs = studentUpdate.KnownAs;
                    studentDetails[index].DisplayName = studentUpdate.DisplayName;
                    studentDetails[index].DateOfBirth = studentUpdate.DateOfBirth;
                    studentDetails[index].Gender = studentUpdate.Gender;
                    studentDetails[index].HomeOrOverseas = studentUpdate.HomeOrOverseas;
                    };
                    canBeUpdated = true;
                }
                if (canBeUpdated == false)
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

    [HttpDelete]
    [Route("{StudentId:int}/{EnrolmentId:int}")]
    public Response DeleteStudentEnrollentByStudentIdEnrollmentId(
                [FromRoute] int StudentId,
                [FromRoute] int EnrolmentId)
    {
        try
        {
            ReadJSON readJson = new ReadJSON();
            List<StudentDetails> studentDetails = new List<StudentDetails>();
            studentDetails = readJson.ReadStudentJSON();
            bool canBeUpdated = false;
            int studentIndex = 0;
            int enrolmentIndex = 0;
            foreach (StudentDetails student in studentDetails)
            {
                if (student.StudentId == StudentId)
                {
                    studentIndex = studentDetails.IndexOf(student);
                    foreach (CourseEnrolment enrolment in studentDetails[studentIndex].CourseEnrolment)
                    {
                        string studentEnrolmentId = StudentId.ToString() + "/" + EnrolmentId.ToString();
                        if (enrolment.EnrolmentId == studentEnrolmentId)
                        {
                            enrolmentIndex = studentDetails[studentIndex].CourseEnrolment.IndexOf(enrolment);
                            canBeUpdated = true;
                        }
                    }
                }
            }
            if (canBeUpdated == false)
                throw new Exception("Student Enrollment Delete Error");
            else
            {
                studentDetails[studentIndex].CourseEnrolment.RemoveAt(enrolmentIndex);
                UpdateJSON updateJSON = new UpdateJSON();
                string message = updateJSON.UpdateStudentJSON(studentDetails);
                return new Response
                {
                    message = "Student Enrollment Deleted Sucessfully "
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

    [HttpPost]
    [Route("{StudentId:int}")]
    public Response AddStudentEnrollentByStudentIdEnrollmentId(
                [FromRoute] int StudentId,
                [FromBody] CourseEnrolment AddCourse)
    {
        try
        {
            ReadJSON readJson = new ReadJSON();
            List<StudentDetails> studentDetails = new List<StudentDetails>();
            studentDetails = readJson.ReadStudentJSON();
            List<Course> coursesDetails = new List<Course>();
            coursesDetails = readJson.ReadCoursesJson();
            bool canBeUpdated = false;
            int studentIndex = 0;
            int newEnrolmentId = 0;
            foreach (StudentDetails student in studentDetails)
            {
                if (student.StudentId == StudentId)
                {
                    studentIndex = studentDetails.IndexOf(student);
                    int lastEnrolmentId = 0;
                    foreach (CourseEnrolment enroledCourse in studentDetails[studentIndex].CourseEnrolment)
                    {
                        lastEnrolmentId = Int32.Parse(enroledCourse.EnrolmentId.Split('/')[1]);
                        newEnrolmentId = lastEnrolmentId >= newEnrolmentId ? lastEnrolmentId : newEnrolmentId;
                        if (enroledCourse.Course.CourseCode == AddCourse.Course.CourseCode) {
                            return new Response
                            {
                                message = "Course Already Exists for user"
                            };
                        }
                    }
                }
                if (studentDetails.IndexOf(student) == (studentDetails.Count - 1)) {
                    foreach (Course course in coursesDetails)
                    {
                        if (course.CourseCode == AddCourse.Course.CourseCode)
                        {
                            canBeUpdated = true;
                        }
                        if (coursesDetails.IndexOf(course) == (coursesDetails.Count - 1) && canBeUpdated == false)
                        {
                            return new Response
                            {
                                message = "Course Doesn't Exist"
                            };
                        }
                    }
                }
            }

            if (canBeUpdated == false)
                throw new Exception("Student Enrollment Add Error");
            else
            {
                CourseEnrolment courseEnrolment= AddCourse;
                courseEnrolment.EnrolmentId = studentDetails[studentIndex].StudentId.ToString() + "/" + (newEnrolmentId + 1).ToString();
                studentDetails[studentIndex].CourseEnrolment.Add(courseEnrolment);
                UpdateJSON updateJSON = new UpdateJSON();
                string message = updateJSON.UpdateStudentJSON(studentDetails);
                return new Response
                {
                    message = "Student Enrollment Added Sucessfully "
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

