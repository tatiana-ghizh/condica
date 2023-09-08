using CVU.CONDICA.Dto.Enums;
using CVU.CONDICA.Persistence.Context;
using CVU.CONDICA.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace CVU.CONDICA.Server.Config
{
    public static class DataBaseSeeder
    {
        public static void SeedDb(this AppDbContext appDbContext)
        {
            appDbContext.Database.Migrate();

            appDbContext.SaveChanges();

            //if(appDbContext.Positions.Count() == 0)
            //{
            //    var director = new Position
            //    {
            //        Name = "Director"
            //    };

            //    var programmer = new Position
            //    {
            //        Name = "Programator"
            //    };

            //    var qualityAssurance = new Position
            //    {
            //        Name = "QA"
            //    };

            //    appDbContext.Positions.AddRange(director, programmer, qualityAssurance);
            //}

            if (appDbContext.User.Count() == 0)
            {
                var admin = new User
                {
                    Email = "admin@cvu.ro",
                    FirstName = "Admin",
                    LastName = "Template",
                    Birthday = DateTime.Now,
                    PhoneNumber = "1234567890",
                    Idnp= "1234567890",
                    IsActivated = true,
                    Role = Role.Administrator,
                    IsBlocked = false,
                    CreatedAt = new DateTime(2018, 1, 1),
                    LastUpdatedAt = new DateTime(2018, 1, 1),
                    SecurityCode = "",
                    //PositionId = 1,
                    //Parola11a#
                    Password = "AA7K81530367D3n5yedJkG+KnczUiMh7hiMsVwzrvMGFL0s+VfFVYtJM6fIFtOC2Yw==",
                };

                var user1 = new User
                {
                    Email = "programator@cvu.ro",
                    FirstName = "Programator",
                    LastName = "Template",
                    Birthday = DateTime.Now,
                    PhoneNumber = "1234567890",
                    Idnp = "1234567891",
                    IsActivated = true,
                    Role = Role.Member,
                    IsBlocked = false,
                    CreatedAt = new DateTime(2018, 1, 1),
                    LastUpdatedAt = new DateTime(2018, 1, 1),
                    SecurityCode = "",
                    //PositionId = 2,
                    //Parola11a#
                    Password = "AA7K81530367D3n5yedJkG+KnczUiMh7hiMsVwzrvMGFL0s+VfFVYtJM6fIFtOC2Yw==",
                };

                var user2 = new User
                {
                    Email = "qa@cvu.ro",
                    FirstName = "QA",
                    LastName = "Template",
                    Birthday = DateTime.Now,
                    PhoneNumber = "1234567890",
                    Idnp = "1234567892",
                    IsActivated = true,
                    Role = Role.Member,
                    IsBlocked = false,
                    CreatedAt = new DateTime(2018, 1, 1),
                    LastUpdatedAt = new DateTime(2018, 1, 1),
                    SecurityCode = "",
                    //PositionId = 3,
                    //Parola11a#
                    Password = "AA7K81530367D3n5yedJkG+KnczUiMh7hiMsVwzrvMGFL0s+VfFVYtJM6fIFtOC2Yw==",
                };


                appDbContext.User.AddRange(admin, user1, user2);

            }

            appDbContext.SaveChanges();
        }
    }
}
