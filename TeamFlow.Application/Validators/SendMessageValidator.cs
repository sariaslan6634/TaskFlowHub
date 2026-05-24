using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Message;

namespace TeamFlow.Application.Validators
{
    public class SendMessageValidator : AbstractValidator<SendMessageDto>
    {
        public SendMessageValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Mesaj boş olamaz.")
                .MaximumLength(2000).WithMessage("Mesaj 2000 karakterden uzun olamaz.");

            RuleFor(x => x.SenderId)
                .GreaterThan(0).WithMessage("Geçerli bir gönderen seçiniz.");

            RuleFor(x => x.ReceiverId)
                .GreaterThan(0).WithMessage("Geçerli bir alıcı seçiniz.");
        }
    }
}
