using Autofac;
using Repository.Data.Repositorys;
using FoodApp.Core.interfaces;
using FoodApp.Services;
using AutoMapper;
using FoodApp.Core.ViewModle.Profiles;

namespace FoodApp.Api.config
{
    public class autofac : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
           builder.RegisterGeneric(typeof(Repository<>)).
            As(typeof(IRepository<>)).
            InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(categoryService).Assembly).
                  Where(p => p.Name.EndsWith("Service")).
                  AsImplementedInterfaces().
                  InstancePerLifetimeScope();

            //builder.RegisterType<UserManager<ApplicationUser>>().AsSelf().InstancePerLifetimeScope();
            //builder.RegisterType<SignInManager<ApplicationUser>>().AsSelf().InstancePerLifetimeScope();

//            builder.Register(ctx => new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile<categoryProfile>(); // ✅ Add your profile here
//            }))
//.AsSelf()
//.SingleInstance();

//            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper())
//                .As<IMapper>()
//                .InstancePerLifetimeScope();



        }
    }

    }

