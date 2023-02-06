using Microsoft.AspNetCore.Mvc;
using StudentEnrolment.API.Models.Student;
using StudentEnrolment.API.Services;

namespace StudentEnrolment.API.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{

    [HttpGet]
    public List<StudentDetails> GetAll()
    {
        ReadJSON readJson = new ReadJSON();
        List<StudentDetails> studentDetails = new List<StudentDetails>();
        studentDetails = readJson.ReadStudentJSON();
        return studentDetails;
    }

}

