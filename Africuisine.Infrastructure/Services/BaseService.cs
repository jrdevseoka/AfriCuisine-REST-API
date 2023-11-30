using Africuisine.Application.Interfaces.Log;
using AutoMapper;

namespace Africuisine.Infrastructure.Services
{
    public class BaseService {
        protected readonly  IMapper Mapper;
        protected readonly INLogger Logger;

        public BaseService(INLogger logger, IMapper mapper)
        {
            Logger = logger;
            Mapper = mapper;
        }
    }
}