using Application.DTO;
using Application.DTO.Candidates;
using Application.Features.Candidates.Commands;
using AutoMapper;
using Domain.Common;
using Domain.Entities;

namespace Application.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class MapperHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Mapper InitializeAutomapper()
        {
            // Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                // Configuring Entities and EntitiesDTO

                #region Base

                cfg.CreateMap<BaseEntity, BaseDTO>().ReverseMap();

                #endregion

                #region Candidates

                cfg.CreateMap<Candidates, CandidatesDTO>().ReverseMap();

                cfg.CreateMap<Candidates, SubmitCandidatesCommand>().ReverseMap();

                cfg.CreateMap<SubmitCandidatesCommand, CandidatesDTO>().ReverseMap();

                #endregion

            });

            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}