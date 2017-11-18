using System;
using System.Collections.Generic;
using System.Linq;
using AngularExample.EF;
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

    public class CompanyController : Controller
    {
        private readonly Context _context;

        public CompanyController(Context context)
        {
            _context = context;
        }

        [HttpGet, Route("api/companies")]
        public IEnumerable<Customer> GetAllCompanies(AllCompaniesQuery query) =>
            _context.Customers
                    .SortAndPaginate(query)
                    .ToList();
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
