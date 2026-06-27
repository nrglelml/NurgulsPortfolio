using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Container
{
    public static class Extensions
    {
        public static void ContainerDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAboutMeService, AboutMeManager>();
            services.AddScoped<IAboutMeDal, EfAboutMeDal>();

            services.AddScoped<ICertificateService, CertificateManager>();
            services.AddScoped<ICertificateDal, EfCertificateDal>();

            services.AddScoped<IContactService, ContactManager>();
            services.AddScoped<IContactDal, EfContactDal>();

            services.AddScoped<IContactMeService, ContactMeManager>();
            services.AddScoped<IContactMeDal, EfContactMeDal>();

            services.AddScoped<ICvFileService, CvFileManager>();
            services.AddScoped<ICvFileDal, EfCvFileDal>();


            services.AddScoped<IEducationService, EducationManager>();
            services.AddScoped<IEducationDal, EfEducationDal>();

            services.AddScoped<IExperienceService, ExperienceManager>();
            services.AddScoped<IExperienceDal, EfExperienceDal>();

            services.AddScoped<IinterestService, InterestManager>();
            services.AddScoped<IinterestDal, EfInterestDal>();

            services.AddScoped<IServiceDal, EfServiceDal>();
            services.AddScoped<IMyService, MyServiceManager>();

            services.AddScoped<IProjectImageDal, EfProjectImageDal>();
            services.AddScoped<IProjectImageService, ProjectImageManager>();

            services.AddScoped<IProjectDal, EfProjectDal>();
            services.AddScoped<IProjectService, ProjectManager>();

            services.AddScoped<ISkillDal, EfSkillDal>();
            services.AddScoped<ISkillService, SkillManager>();

            services.AddScoped<ISocialMediaDal, EfSocialMediaDal>();
            services.AddScoped<ISocialMediaService, SocialMediaManager>();

            services.AddScoped<ITestimonialDal, EfTestimonialDal>();
            services.AddScoped<ITestimonialService, TestimonialManager>();
        }
    }
}
