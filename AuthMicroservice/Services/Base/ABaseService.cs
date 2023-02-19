using AuthMicroservice.Repositories.Base;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AuthMicroservice.Services.Base
{
    public abstract class ABaseService<TEntity, TRepository>
        where TEntity : class
        where TRepository : class, IBaseRepository<TEntity>
    {
        private readonly TRepository _repository;
        private readonly IMapper _mapper;

        public ABaseService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider?.GetService<TRepository>() ?? throw new Exception("Repository is null in DI");
            _mapper = serviceProvider?.GetService<IMapper>() ?? throw new Exception("Mapper is null in DI");
        }

        public IQueryable<TEntity> GetFileterdList()
        {
            return _repository.GetList();
        }
    }
}
