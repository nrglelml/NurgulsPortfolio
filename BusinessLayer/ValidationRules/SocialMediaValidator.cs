using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class SocialMediaValidator : AbstractValidator<SocialMedia>
    {
        public SocialMediaValidator()
        {
            RuleFor(x => x.MediaName).NotEmpty().WithMessage("İsim alanı boş bırakılamaz");
            RuleFor(x => x.MediaURL).NotEmpty().WithMessage("URL alanı boş bırakılamaz");
        }
    }
}
