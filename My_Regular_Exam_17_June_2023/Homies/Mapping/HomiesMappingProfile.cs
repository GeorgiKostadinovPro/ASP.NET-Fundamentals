using AutoMapper;
using Homies.Data.Models;
using Homies.Models.Events;
using System.Diagnostics.Eventing.Reader;

namespace Homies.Mapping
{
    public class HomiesMappingProfile : Profile
    {
        public HomiesMappingProfile()
        {
            this.CreateMap<Event, EventViewModel>()
                .ForMember(d => d.Type, src => src.MapFrom(opt => opt.Type.Name))
                .ForMember(d => d.Organiser, src => src.MapFrom(opt => opt.Organiser.Email))
                .ForMember(d => d.Start, src => src.MapFrom(opt => opt.Start.ToString("yyyy-MM-dd H:mm")));

            this.CreateMap<Event, EventDetailsViewModel>()
                .ForMember(d => d.Type, src => src.MapFrom(opt => opt.Type.Name))
                .ForMember(d => d.Start, src => src.MapFrom(opt => opt.Start.ToString("yyyy-MM-dd H:mm")))
                .ForMember(d => d.End, src => src.MapFrom(opt => opt.End.ToString("yyyy-MM-dd H:mm")))
                .ForMember(d => d.CreatedOn, src => src.MapFrom(opt => opt.CreatedOn.ToString("yyyy-MM-dd H:mm")))
                .ForSourceMember(src => src.Organiser, opt => opt.DoNotValidate());

            this.CreateMap<AddEventViewModel, Event>();
        }
    }
}
