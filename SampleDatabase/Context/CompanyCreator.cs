using SampleDatabase.Context.Entities;

namespace SampleDatabase.Context
{
    public static class CompanyCreator
    {
        /// <summary>
        /// This method is handy shortcut for genereting sample data.
        /// It should never be designed like this, but for this specific scenario, it works well.
        /// </summary>
        public static Company Create(
            string company,
            string firstName,
            string lastName,
            string street,
            string city,
            IdGenerator idProvider)
        {
            return new Company
            {
                Id = idProvider.NextId,

                Name = company,

                Owner = new Person
                {
                    Id = idProvider.NextId,
                    FirstName = firstName,
                    LastName = lastName
                },
                OwnerId = idProvider.LastId,

                Address = new Address
                {
                    Id = idProvider.NextId,
                    Street = street,
                    City = city
                },
                AddressId = idProvider.LastId
            };
        }
    }

    public class IdGenerator
    {
        private int lastId = 0;
        public int LastId => lastId;
        public int NextId => ++lastId;
    }
}