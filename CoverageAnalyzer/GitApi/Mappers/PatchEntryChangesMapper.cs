using AutoMapper;
using LibGit2Sharp;
using CoverageAnalyzer.GitApi.Entities;

namespace CoverageAnalyzer.GitApi.Mappers
{
    public class PatchEntryChangesMapper : Profile
    {
        public PatchEntryChangesMapper()
        {
            CreateMap<PatchEntryChanges, GitDiff>()
                .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.Path))
                .ForMember(dest => dest.ChangeType, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest, opt => opt.MapFrom(src => src.LinesAdded))
                .ForMember(dest => dest.LinesDeleted, opt => opt.MapFrom(src => src.LinesDeleted));
        }
    }
}
