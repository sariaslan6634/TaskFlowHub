using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Task;

namespace TeamFlow.Application.Validators
{
    public class UpdateTaskValidator : AbstractValidator<UpdateTaskDto>
    {
        public UpdateTaskValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Görev başlığı boş olamaz.")
                .MaximumLength(200).WithMessage("Görev başlığı 200 karakterden uzun olamaz.");

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Açıklama 2000 karakterden uzun olamaz.");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Bitiş tarihi geçmiş olamaz.")
                .When(x => x.DueDate.HasValue);
        }
    }
}
