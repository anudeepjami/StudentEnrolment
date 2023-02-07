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


    [HttpPut]
    [Route("{StudentId:int}")]
    public Response UpdateStudentById(
        [FromRoute] int StudentId,
        [FromBody] UpdateStudentDetails studentUpdate
    )
    {
        try
        {
            int i = StudentId;
            ReadJSON readJson = new ReadJSON();
            List<StudentDetails> studentDetails = new List<StudentDetails>();
            studentDetails = readJson.ReadStudentJSON();
            bool updateSuccess = false;
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

