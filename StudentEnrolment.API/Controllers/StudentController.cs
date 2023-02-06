using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StudentEnrolment.API.Models.Student;
using StudentEnrolment.API.Models.Response;
using StudentEnrolment.API.Services;

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
            StudentDetails student = (from stud in studentDetails where stud.StudentId == id select stud).First();
            return new Response {
                message = "Student Sucessfully Updated"
            };
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

