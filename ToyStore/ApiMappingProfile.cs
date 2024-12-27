using AutoMapper;
using ToyStore.Services;
using ToyStore.Controllers;
using ToyStore.Api_mapping;
using ToyStore.Api_mapping.Toys.Create;
using ToyStore.Api_mapping.Toys.GetAll;
using ToyStore.Api_mapping.Toys.Update;
using ToyStore.Api_mapping.Toys.Delete;


namespace ToyStore.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CreateToyRequest, Toy>();
            CreateMap<Toy,GetAllToysDTO>();
            CreateMap<UpdateToysRequest, Toy>();
            CreateMap<DeleteToyRequest, Toy>();
        }
    }
}
