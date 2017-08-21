using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagement.Services;
using LibraryManagement.Data.Repositories;
using LibraryManagement.Data.Contracts;
using System.Web.Mvc;
using LibraryManagement.Data.Entities;
using System.Web.Http;
using Autofac.Integration.WebApi;
using System.Reflection;

namespace LibraryManagement.App_Start
{
    public static class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(Global).Assembly);
           

            builder.RegisterType<BookService>().As<IBookService>().InstancePerLifetimeScope();
            builder.RegisterType<StudentService>().As<IStudentService>().InstancePerLifetimeScope();
            builder.RegisterType<BorrowService>().As<IBorrowService>().InstancePerLifetimeScope();

            builder.RegisterInstance(BookRepository.Database).As<IBookRepository>().SingleInstance();
            builder.RegisterInstance(BorrowRepository.Database).As<IBorrowRepository>().SingleInstance();
            builder.RegisterInstance(StudentRepository.Database).As<IStudentRepository>().SingleInstance();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            //builder.re

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver =
                 new AutofacWebApiDependencyResolver(container);

            //Demo Data
            Seed();
        }

        private static void Seed()
        {
            //seed books

            BookRepository.Database.Add(new Book()
            {
                BookID = 1,
                Title = "Lost Time"
            });

            BookRepository.Database.Add(new Book()
            {
                BookID = 2,
                Title = "War and Peace"
            });

            BookRepository.Database.Add(new Book()
            {
                BookID = 3,
                Title = "Hamlet"
            });

            BookRepository.Database.Add(new Book()
            {
                BookID = 4,
                Title = "The Brothers"
            });

            BookRepository.Database.Add(new Book()
            {
                BookID = 5,
                Title = " Pride and Prejudice"
            });
          
            //seed students
            StudentRepository.Database.Add(new Student()
            {
              StudentID = 1,
              FirstName = "Peter",
              LastName = "Davis"
            });

            StudentRepository.Database.Add(new Student()
            {
                StudentID = 2,
                FirstName = "Juan",
                LastName = "David"
            });

            StudentRepository.Database.Add(new Student()
            {
                StudentID = 3,
                FirstName = "James",
                LastName = "Wilson"
            });

            StudentRepository.Database.Add(new Student()
            {
                StudentID = 4,
                FirstName = "Tony",
                LastName = "Abott"
            });

            StudentRepository.Database.Add(new Student()
            {
                StudentID = 5,
                FirstName = "Arthur",
                LastName = "Davi"
            });

            //seed borrow
            BorrowRepository.Database.Add(new Borrow()
            {
                StudentID = 1,
                BookID = 2,
                DueDate = DateTime.Now.AddDays(5)
            });

            BorrowRepository.Database.Add(new Borrow()
            {
                StudentID = 1,
                BookID = 1,
                DueDate = DateTime.Now.AddDays(5)
            });

            BorrowRepository.Database.Add(new Borrow()
            {
               StudentID = 2,
               BookID = 5,
               DueDate = DateTime.Now.AddDays(-2)
            });

        }
    }
}