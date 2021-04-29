
using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Cars.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Cars.Commands
{
    public class UpdateCarCommand : IRequest<IResult>
    {

        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public short ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }

        public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, IResult>
        {
            private readonly ICarRepository _carRepository;
            private readonly IMediator _mediator;

            public UpdateCarCommandHandler(ICarRepository carRepository, IMediator mediator)
            {
                _carRepository = carRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [ValidationAspect(typeof(UpdateCarValidator), Priority = 2)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
            {
                var isThereCarRecord = await _carRepository.GetAsync(u => u.Id == request.Id);

                //isThereCarRecord.Id = request.Id;
                isThereCarRecord.Description = request.Description;
                isThereCarRecord.BrandId = request.BrandId;
                isThereCarRecord.ColorId = request.ColorId;
                isThereCarRecord.ModelYear = request.ModelYear;


                _carRepository.Update(isThereCarRecord);
                await _carRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

