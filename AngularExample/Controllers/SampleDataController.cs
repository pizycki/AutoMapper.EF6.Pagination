using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.EF6.Pagination;
using AutoMapper.EF6.Pagination.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularExample.Controllers
{
    public class AllCompaniesQuery : IQueryWithPagination
    {
        public string OrderBy { get; set; }
        public bool Descending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class Company
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }

    public class CompanyController : Controller
    {
        [HttpGet, Route("api/companies")]
        public object GetAllCompanies(AllCompaniesQuery query)
        {
            var companies = new[]
            {
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
                new Company (),
            }.AsQueryable();

            return companies.SortAndPaginate(query).ToList();
        }
    }

    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
