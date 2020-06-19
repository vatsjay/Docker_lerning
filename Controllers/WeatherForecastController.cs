using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            List<Person> personList = new List<Person>();
            personList.Add(new Person { name = "jay", age = 26 });
            personList.Add(new Person { name = "jay1", age = 27 });
            personList.Add(new Person { name = "jay2", age = 28 });
            personList.Add(new Person { name = "jay", age = 29 });
            personList.Add(new Person { name = "jay1", age = 25 });
            personList.Add(new Person { name = "jay2", age = 24 });

            var name = personList.Where(x => x.name == "jay").Select(a => a.name);
            var abc = personList.Where(x => x.name == "ja").Select(a => a.name);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("get-person/{id}")]
        public async Task<ActionResult> parameterisedGet(int id)
        {
            var returnValue = id * 3;
            return Ok(returnValue);
        }

        [HttpPost("put-person")]
        public async Task<ActionResult> post(int id)
        {
            var returnValue = id * 3;
            return Ok(returnValue);
        }

        [HttpPut("update-person/{id}")]
        public ActionResult Put(int id, [FromBody] int value)
        {
            var returnString = $"{id} was replaced by {value}";
            return Ok(returnString);
        }

        [HttpPatch("update-person/{id}")]
        public string Patch(int id, [FromBody] object value)
        {
            var returnString = $"{id} was replaced with the instructions";

            return returnString;
        }

        [HttpDelete("delete-person/{id}")]
        public void Delete(int id)
        {
            //delete person
        }

        [HttpGet("get-person-authorize/{id}")]
        [Authorize()]
        [AllowAnonymous]
        public ActionResult authorisedEntry(int id)
        {
            return Ok();
        }

        [HttpGet("abc/{id}")]
        public void getVoid(int id)
        {
            Thread.Sleep(10000);
            Console.WriteLine(id);
        }
    }

    public class Person
    {
        public string name { get; set; }
        public int age { get; set; }

        public void walk()
        {
            Console.WriteLine("walk in person class");

            run();


        }

        public void run()
        {
            Console.WriteLine("run in person class");
        }
    }

    public class beta : Person
    {
        public string name { get; set; }
        public int age { get; set; }

        public void walk()
        {
            Console.WriteLine("walk in beta class");
            base.walk();
        }

        public void run()
        {
            Console.WriteLine("run in beta class");
            base.run();
        }
    }
}
