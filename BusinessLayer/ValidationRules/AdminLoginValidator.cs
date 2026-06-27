using DTOLayer;
using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class AdminLoginValidator:AbstractValidator<AdminLoginDTO>
    {
        public AdminLoginValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Lütfen kullanıcı adınızı giriniz.");
            RuleFor(x => x.Password)
              .NotEmpty().WithMessage("Lütfen şifrenizi giriniz.")
              .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");
        }
    }
}
