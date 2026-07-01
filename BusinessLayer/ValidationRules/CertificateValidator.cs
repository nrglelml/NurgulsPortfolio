using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
    public class CertificateValidator:AbstractValidator<Certificate>
    {
        public CertificateValidator()
        {
            RuleFor(x => x.Issuer).NotEmpty().WithMessage("Issuer kısmı boş bırakılamaz");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Kategori kısmı boş bırakılamaz");
            RuleFor(x => x.CredentialURL).NotEmpty().WithMessage("Sertifika URL kısmı boş bırakılamaz");
            RuleFor(x => x.CredentialId).NotEmpty().WithMessage("Sertifika ID kısmı boş bırakılamaz");
        }
    }
}
