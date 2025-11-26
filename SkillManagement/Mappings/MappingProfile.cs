using AutoMapper;
using SkillManagement.Models;
using SkillManagement.Dtos;

namespace SkillManagement.Mappings 
{
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreatePersonDto → Person (создание)
        CreateMap<CreatePersonDto, Person>()
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills))
            .ReverseMap(); // Для двунаправленного маппинга

        // Person → PersonDto (чтение)
        CreateMap<Person, PersonDto>()
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills));

        // Skill → SkillDto (чтение навыков)
        CreateMap<Skill, SkillDto>();


        // CreateSkillDto → Skill (создание навыков)
        CreateMap<CreateSkillDto, Skill>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level));


        // UpdatePersonDto → Person (обновление)
        CreateMap<UpdatePersonDto, Person>()
            .ForMember(dest => dest.Name, opt => 
                opt.Condition(src => src.Name != null) // Обновлять только если Name не null
            )
            .ForMember(dest => dest.DisplayName, opt =>
                opt.Condition(src => src.DisplayName != null) // Аналогично для DisplayName
            )
            .ForMember(dest => dest.Skills, opt =>
                opt.MapFrom(src => src.Skills) // Мапить навыки, если они есть
            );

        // Обратные маппинги (опционально, если нужны)
        CreateMap<PersonDto, Person>();
        CreateMap<SkillDto, Skill>();
    }
}
}
