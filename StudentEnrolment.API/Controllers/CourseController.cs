using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentEnrolment.API.Models.Course;
using StudentEnrolment.API.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentEnrolment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : Controller
    {
        [HttpGet]
        public List<Course> Get()
        {
            ReadJSON readJson = new ReadJSON();
            List<Course> courseDetails = new List<Course>();
            courseDetails = readJson.ReadCoursesJson();
            return courseDetails;
        }
    }
}

