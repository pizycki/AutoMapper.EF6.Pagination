using System.Linq;

namespace AngularExample.EF
{
    public static class DbInitializer
    {
        public static void Initialize(Context context)
        {
            context.Database.EnsureCreated();

            if (context.Customers.Any())
            {
                return;
            }

            var companies = new[]
            {
                 
            };

            context.AddRange(companies);
            context.SaveChanges();


        }
    }
}