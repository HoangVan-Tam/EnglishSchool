using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using EnglishSchool.Data;
using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Data.Repositories;
using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
using EnglishSchool.Service;
using Microsoft.AspNet.SignalR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(EnglishSchool.App_Start.Startup))]

namespace EnglishSchool.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            ConfigAutoFac(app);
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("my_secret_key_12345"))
                    }
                });
            app.Map("/signalR", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableJSONP = true
                };
                map.RunSignalR(hubConfiguration);
            });
          
        }

        private void ConfigAutoFac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());


            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterType<EnglishSchoolDbContext>().AsSelf().InstancePerRequest();

            builder.RegisterType<RepositoryWrapper>().As<IRepositoryWrapper>().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(DepartmentRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();


            builder.RegisterAssemblyTypes(typeof(DepartmentService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Department, DepartmentDTO>().ReverseMap();
                cfg.CreateMap<Recruitment, RecruitmentDTO>().ReverseMap();
                cfg.CreateMap<ClassInfoForTeacher, ClassUpdateDTO>().ReverseMap();
                cfg.CreateMap<ClassDetailOfStudent, TeacherManageStudentVer2>().ReverseMap();
                cfg.CreateMap<Department, NameOfDepartment>();
                cfg.CreateMap<Student, StudentLoginReponseDTO>().ReverseMap();
                cfg.CreateMap<Student, FullInfoStudentDTO>().ReverseMap();
                cfg.CreateMap<Student, NameOfDepartment>().ReverseMap();
                cfg.CreateMap<Student, NameOfParent>().ReverseMap();

                cfg.CreateMap<Class, ClassDTO>().ReverseMap();


                cfg.CreateMap<Student, NameOfStudent>();
                cfg.CreateMap<Student, InfoStudent>().ReverseMap();
                cfg.CreateMap<Class, NameOfCourse>();
                cfg.CreateMap<ClassDetailOfStudent, ClassDetailOfStudentDTO>().ReverseMap();
                cfg.CreateMap<ClassDetailOfStudent, ListCourseDetailOfStudent>().ReverseMap();
                cfg.CreateMap<ClassDetailOfStudent, ManageStudentDTO>().ReverseMap();

                cfg.CreateMap<Question, QuestionDTO>().ReverseMap();
                cfg.CreateMap<Class, ClassInfoForTeacher>().ReverseMap();
                cfg.CreateMap<News, NewsDTO>().ReverseMap();
                cfg.CreateMap<Class, ClassForStudentDetailDTO>().ReverseMap();
                cfg.CreateMap<Course, CourseDTO>().ReverseMap();
                cfg.CreateMap<PersonalInformation, PersonalInformationDTO>().ReverseMap();              
                cfg.CreateMap<Test, TestDTO>().ReverseMap();
                cfg.CreateMap<Test, Test3DTO>().ReverseMap();
                cfg.CreateMap<Test, Test2DTO>().ReverseMap();
                cfg.CreateMap<Employee, EmployeeDTO>().ReverseMap();
                cfg.CreateMap<Employee, EmployeeLoginDTO>().ReverseMap();
                cfg.CreateMap<ClassDetailOfStudent, TeacherManageStudent>().ReverseMap();
                cfg.CreateMap<DetailTest, DetailTestDTO>().ReverseMap();
                cfg.CreateMap<ClassDetailOfStudent, CourseStudentNoRegister>().ReverseMap();
                cfg.CreateMap<Schedule, ScheduleDTO>().ReverseMap();
                cfg.CreateMap<Class, ClassUpdateDTO>().ReverseMap();
                cfg.CreateMap<ClassDTO, ClassUpdateDTO>().ReverseMap();
                cfg.CreateMap<Attendance, AttendanceOfStudent>().ReverseMap();
                cfg.CreateMap<ClassDetailOfStudent, ListAttendanceStudentOfCourse>().ReverseMap();
            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();


            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}
