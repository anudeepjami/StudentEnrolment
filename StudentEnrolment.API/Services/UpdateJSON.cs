using System;
using Newtonsoft.Json;
using StudentEnrolment.API.Models.Course;
using StudentEnrolment.API.Models.Student;

namespace StudentEnrolment.API.Services
{
	public class UpdateJSON
	{
        public string UpdateStudentJSON(List<StudentDetails> studentDetails)
        {

            string path = System.IO.Directory.GetCurrentDirectory();
            //string studentDetailsText = File.ReadAllText(path + "/Data/students.json");
            var jsonString = JsonConvert.SerializeObject(studentDetails, Formatting.Indented);
            File.WriteAllText(path+ "/Data/students.json", jsonString);
            return "Updated Successfully";

        }
    }
}

