using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentEnrolment.API.Models.Course;
using StudentEnrolment.API.Models.Response;
using StudentEnrolment.API.Models.Student;
using StudentEnrolment.API.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentEnrolment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : Controller
    {
        [HttpGet]
        public List<Course> GetCourses()
        {
            ReadJSON readJson = new ReadJSON();
            List<Course> courseDetails = new List<Course>();
            courseDetails = readJson.ReadCoursesJson();
            return courseDetails;
        }

        [HttpDelete]
        [Route("{Studentid:int}/{EnrolmentId:int}")]
        public Response DeleteStudentEnrollentByStudentIdEnrollmentId(
                [FromRoute] int Studentid,
                [FromRoute] int EnrolmentId)
        {
            try
            {
                int i = Studentid;
                ReadJSON readJson = new ReadJSON();
                List<StudentDetails> studentDetails = new List<StudentDetails>();
                studentDetails = readJson.ReadStudentJSON();
                bool updateSuccess = false;
                int studentIndex = 0;
                int enrolmentIndex = 0;
                foreach (StudentDetails student in studentDetails)
                {
                    if (student.StudentId == Studentid)
                    {
                        studentIndex = studentDetails.IndexOf(student);
                        foreach (CourseEnrolment enrolment in studentDetails[studentIndex].CourseEnrolment)
                        {
                            string studentEnrolmentId = Studentid.ToString() + "/" + EnrolmentId.ToString();
                            if (enrolment.EnrolmentId == studentEnrolmentId) {
                                enrolmentIndex = studentDetails[studentIndex].CourseEnrolment.IndexOf(enrolment);
                                updateSuccess = true;
                            }
                        }
                     }
                }
                if (updateSuccess == false)
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
    }

}

