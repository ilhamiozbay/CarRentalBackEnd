
using Business.Handlers.Cars.Commands;
using FluentValidation;

namespace Business.Handlers.Cars.ValidationRules
{
    public class CreateCarValidator : AbstractValidator<CreateCarCommand>
    {
        public CreateCarValidator()
        {
            RuleFor(x => x.ModelYear).NotEmpty();
            RuleFor(x => x.DailyPrice).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
    public class UpdateCarValidator : AbstractValidator<UpdateCarCommand>
    {
        public UpdateCarValidator()
        {
            RuleFor(x => x.ModelYear).NotEmpty();
            RuleFor(x => x.DailyPrice).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}