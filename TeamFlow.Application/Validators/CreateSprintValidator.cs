using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Sprint;

namespace TeamFlow.Application.Validators
{
    public class CreateSprintValidator : AbstractValidator<CreateSprintDto>
    {
        public CreateSprintValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Sprint adı boş olamaz.")
                .MaximumLength(100).WithMessage("Sprint adı 100 karakterden uzun olamaz.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Başlangıç tarihi boş olamaz.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("Bitiş tarihi boş olamaz.")
                .GreaterThan(x => x.StartDate)
                .WithMessage("Bitiş tarihi başlangıç tarihinden sonra olmalıdır.");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0).WithMessage("Geçerli bir proje seçiniz.");
        }

    }
}
