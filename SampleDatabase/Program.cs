using System;
using AutoMapper;
using AutoMapper.EF6.Pagination.Models;

namespace SampleDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            //var ctx = new CompanyDbContext();

            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<Bar, BarDTO>();
            //});

            //var page = 5;
            //var pageSize = 10;

            //var barDTOs = ctx.Bars.AsQueryable()
            //    .SortAndPaginate(
            //         Ascending<Bar, Guid>.By(x => x.FooId),
            //         Pagination.Set(page: 2, pageSize: 20))
            //    .ProjectTo<BarDTO>(config)
            //    .ToList();

            //Console.WriteLine($"collection size {barDTOs.Count}");
            //Console.WriteLine(barDTOs.Last().Value);

            //Console.ReadKey();
        }
    }
}