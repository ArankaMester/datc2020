using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace L02.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public Student GetStudent(int id)
        {
            return web_api.StudentRepo.Students.FirstOrDefault(s=>s.id=id);

        }

        [HttpPost]
        public string Post()
        {
            try{
                web_api.StudentRepo.Students.Add(Student);
                return "S-a adaugat!";

            }
            catch(ArgumentException e)
            {
                return "Eroare!";
                throw;
                
            }

        }
    }
}
