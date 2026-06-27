using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class TestimonialValidator : AbstractValidator<Testimonials>
    {
        public TestimonialValidator()
        {
 
           RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş bırakılamaz");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık alanı boş bırakılamaz");
            RuleFor(x => x.MessageBody).NotEmpty().WithMessage("Mesaj alanı boş bırakılamaz");
        }
    }
}
