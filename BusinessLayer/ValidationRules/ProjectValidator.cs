using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ProjectValidator : AbstractValidator<Project>
    {
        public ProjectValidator()

        {
            RuleFor(x => x.ProjectTitle).NotEmpty().WithMessage("Başlık alanı boş bırakılamaz");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama alanı boş bırakılamaz");
            RuleFor(x => x.ProjectUrl).NotEmpty().WithMessage("URL alanı boş bırakılamaz");
        }
    }
}
