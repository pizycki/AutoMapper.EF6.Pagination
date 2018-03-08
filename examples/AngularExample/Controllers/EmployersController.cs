//using System.Linq;
//using ExampleDbContext;
//using ExampleDbContext.Entities;
//using Microsoft.AspNetCore.Mvc;
//using PagiNET.Pager;
//using PagiNET.Paginate;
//using PagiNET.Queryable;

//namespace AngularExample.Controllers
//{

//    public class EmployersController : Controller
//    {
//        private readonly Context _context;

//        public EmployersController(Context context) => _context = context;

//        [HttpGet, Route("api/employers/sorted")]
//        public Page<Person> GetCustomersSortedPage(AllCustomersQuery query) =>
//            query.IncludeTotalPages
//            ? QueryForSortedCustomersPage(query)
//            : query.GetPageAndTotalPages(
//                getPage: QueryForSortedCustomersPage,
//                getTotalPages: CountCustomerPages);

//        [HttpGet, Route("api/customers")]
//        public Page<Person> GetCustomersPage(AllCustomersQuery query) =>
//            query.IncludeTotalPages
//                ? QueryForCustomersPage(query)
//                : query.GetPageAndTotalPages(
//                    getPage: QueryForSortedCustomersPage,
//                    getTotalPages: CountCustomerPages);

//        private Page<Person> QueryForSortedCustomersPage(IQueryWithPage query) =>
//            AllCustomersQuery
//                .SortAndTakePage(query)
//                .ToList()
//                .AsPage(query);

//        private Page<Person> QueryForCustomersPage(IQueryWithPage query) =>
//            AllCustomersQuery
//                .TakePage(query)
//                .ToList()
//                .AsPage(query);

//        private int CountCustomerPages(IQueryWithPage query) =>
//            AllCustomersQuery.CountPages(query);

//        private IQueryable<Person> AllCustomersQuery => _context.People.AsQueryable();
        
//        public class AllEmployersQueryParams : IQueryWithPage
//        {
//            // Sorting
//            public string OrderBy { get; set; }
//            public bool Descending { get; set; }

//            // Pagination
//            public int Number { get; set; }
//            public int Size { get; set; }
//            public bool IncludeTotalPages { get; set; } = false;
//        }
//    }
//}