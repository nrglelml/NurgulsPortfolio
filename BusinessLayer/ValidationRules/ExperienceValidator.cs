using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ExperienceValidator : AbstractValidator<Experience>
    {
        public ExperienceValidator()
        {
            RuleFor(x => x.Position).NotEmpty().WithMessage("Pozisyon alanı boş bırakılamaz");
            RuleFor(x => x.Location).NotEmpty().WithMessage("Konum alanı boş bırakılamaz");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Başlangıç tarihi alanı boş bırakılamaz");
        }
    }
}
