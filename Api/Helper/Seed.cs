using Api.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helper
{
    /*seeding intital temporay data*/
    public static class Seed
    {

        public static void DbIntitalize(DataDbContext context)
        {
            if (!context.User.Any())
            {
                context.User.Add(new User
                {
                    Email = "subrata@gmail.com",
                    FullName = "Subrata Roy",
                    BirthDate = DateTime.Now,
                    Password = "1234",
                    PasswordHash = null
                });
            }

            if (!context.Todo.Any())
            {
                context.Todo
                    .Add(new Todo
                    {
                        Title = "My Task",
                        Date = DateTime.Now,
                        Description = "N/A",
                        From = "10.00 AM",
                        To = "12.00 AM",
                        Location = "Dhaka Bangladesh",
                        NotifyBy = "Email",
                        NotifyTime = "20 Minutes",
                        Teal = "Color",
                        UserId = 1
                    });
            }
            context.SaveChanges();
        }

    }
}
