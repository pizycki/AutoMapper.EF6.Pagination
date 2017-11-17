using AutoMapper;
using SampleDatabase.Context.Entities;
using SampleDatabase.ViewModels;

namespace SampleDatabase.Mapper
{
    public class CompanyMappings : Profile
    {
        public CompanyMappings()
        {
            CreateMap<Company, CompanyOwnerVM>()
                .ForMember(destinationMember: vm => vm.Owner,
                           memberOptions: opt => opt.MapFrom(e => e.Owner.FirstName + " " + e.Owner.LastName));
        }
    }
}
