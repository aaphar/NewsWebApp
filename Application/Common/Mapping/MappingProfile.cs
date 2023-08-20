using Application.Common.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryTranslation, CategoryTranslationDto>();
            CreateMap<Hashtag, HashtagDto>();
            CreateMap<PostHashtag, PostHashtagDto>();
            CreateMap<Language, LanguageDto>();
            CreateMap<Post, PostDto>();
            CreateMap<PostTranslation, PostTranslationDto>();
            CreateMap<User, UserDto>();
            CreateMap<Role, RoleDto>();
        }
    }
}
