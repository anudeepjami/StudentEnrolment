using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StudentEnrolment.API.Models.Student;
using StudentEnrolment.API.Models.Course;

namespace StudentEnrolment.API.Services
{
	public class ReadJSON
	{
		public List<StudentDetails> ReadStudentJSON()
		{

            string path = System.IO.Directory.GetCurrentDirectory();
            string studentDetailsText = File.ReadAllText(path+"/Data/students.json");
            List<StudentDetails> studentDetailsList = JsonConvert.DeserializeObject<List<StudentDetails>>(studentDetailsText);
            return studentDetailsList;
            
        }

        public List<Course> ReadCoursesJson()
        {

            string path = System.IO.Directory.GetCurrentDirectory();
            string coursesText = File.ReadAllText(path + "/Data/courses.json");
            List<Course> coursesList = JsonConvert.DeserializeObject<List<Course>>(coursesText);
            return coursesList;

        }
    }
}

