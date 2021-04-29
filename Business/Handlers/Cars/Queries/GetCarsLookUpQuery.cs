
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Cars.Queries
{
    public class GetCarsLookUpQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public class GetCarsLookUpQueryHandler : IRequestHandler<GetCarsLookUpQuery, IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly ICarRepository _carRepository;
            private readonly IMediator _mediator;

            public GetCarsLookUpQueryHandler(ICarRepository carRepository, IMediator mediator)
            {
                _carRepository = carRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetCarsLookUpQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<SelectionItem>>(await _carRepository.GetCarsLookUp());
            }
        }
    }
}