
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Languages.Queries
{
    public class GetCarQuery : IRequest<IDataResult<Car>>
    {
        public int Id { get; set; }

        public class GetCarQueryHandler : IRequestHandler<GetCarQuery, IDataResult<Car>>
        {
            private readonly ICarRepository _carRepository;
            private readonly IMediator _mediator;

            public GetCarQueryHandler(ICarRepository carRepository, IMediator mediator)
            {
                _carRepository = carRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Car>> Handle(GetCarQuery request, CancellationToken cancellationToken)
            {
                var car = await _carRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Car>(car);
            }
        }
    }
}
