using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class EducationValidator : AbstractValidator<Education>
    {
        public EducationValidator()
        {
            RuleFor(x => x.School).NotEmpty().WithMessage("Okul alanı boş bırakılamaz");
            RuleFor(x => x.Degree).NotEmpty().WithMessage("Derece alanı boş bırakılamaz");
            RuleFor(x => x.GPA).NotEmpty().WithMessage("Not alanı boş bırakılamaz");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Başlangıç tarihi alanı boş bırakılamaz");
        }
    }
}
