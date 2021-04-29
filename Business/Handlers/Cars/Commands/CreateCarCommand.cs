
using Business.BusinessAspects;
using Business.Constants;
//using Business.Handlers.Cars.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Cars.Commands
{
    /// <summary>
    /// 
    /// </summary>

    public class CreateCarCommand : IRequest<IResult>
    {

        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public short ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }


        public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, IResult>
        {
            private readonly ICarRepository _carRepository;
            private readonly IMediator _mediator;
            public CreateCarCommandHandler(ICarRepository carRepository, IMediator mediator)
            {
                _carRepository = carRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            //[ValidationAspect(typeof(CreateCarValidator), Priority = 2)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateCarCommand request, CancellationToken cancellationToken)
            {
                var isThereCarRecord = _carRepository.Query().Any(u => u.Description == request.Description);

                if (isThereCarRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedCar = new Car
                {
                    BrandId = request.BrandId,
                    ColorId = request.ColorId,
                    ModelYear = request.ModelYear,
                    DailyPrice = request.DailyPrice,
                    Description = request.Description

                };

                _carRepository.Add(addedCar);
                await _carRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}