using FluentValidation.WebApi;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebStoreDomain.Concrete;
using WebStoreUi.Infrastructure;

namespace WebStoreUi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // внедрение зависимостей
            NinjectModule registations = new NinjectRegistrations();
            IKernel ninjectKernel = new StandardKernel(registations);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(ninjectKernel));




            //        FluentValidationModelValidatorProvider.Configure(
            //provider => provider.AddImplicitRequiredValidator = false);


            //        DependencyResolverValidatorFactory validatorFactory =
            //            new DependencyResolverValidatorFactory();

            //        FluentValidationModelValidatorProvider validatorFactoryProvider =
            //            new FluentValidationModelValidatorProvider(validatorFactory);

            //        validatorFactoryProvider.AddImplicitRequiredValidator = false;
            //        ModelValidatorProviders.Providers.Add(validatorFactoryProvider);


            //  DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;



            ModelValidatorProviders.Providers.Clear();
            //  ModelValidatorProviders.Providers.Add(new AttributeValidatorProvider());

        }
    }
}
