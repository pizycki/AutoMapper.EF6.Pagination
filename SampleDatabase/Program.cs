using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using SampleDatabase.DbContext;
using SampleDatabase.DbContext.Entities;

namespace SampleDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new SampleDbContext();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Bar, BarDTO>();
            });

            var page = 5;
            var pageSize = 10;

            var barDTOs = ctx.Bars.AsQueryable()
                .SortAndPaginate(Ascending<Bar, Guid>.By(x => x.FooId), Pagination.For(page: 2, pageSize: 20))
                .ProjectTo<BarDTO>(config)
                .ToList();

            Console.WriteLine($"collection size {barDTOs.Count}");
            Console.WriteLine(barDTOs.Last().Value);

            Console.ReadKey();
        }
    }
}