using Microsoft.AspNetCore.Identity;
using SacramentMeetingPlanner.Data;
using SacramentMeetingPlanner.Models;
using System;
using System.Linq;

namespace SacramentMeetingPlanner.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SacramentMeetingPlannerContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {

            context.Database.EnsureCreated();

            // Create default roles if not existing
            if (roleManager.FindByNameAsync("Administrator").Result == null)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Administrator"
                };

                roleManager.CreateAsync(role).Wait();
            }
            if (roleManager.FindByNameAsync("Bishopric").Result == null)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Bishopric"
                };

                roleManager.CreateAsync(role).Wait();
            }

            // Create default user if not existing
            // Add default user to default role if existing
            if (userManager.FindByEmailAsync("admin@sacramentplanner.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "admin@sacramentplanner.com",
                    Email = "admin@sacramentplanner.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "5am33P1an!").Result;

                if (result.Succeeded)
                {
                    if (!userManager.IsInRoleAsync(user, "Administrator").Result)
                        userManager.AddToRoleAsync(user, "Administrator").Wait();

                    if (!userManager.IsInRoleAsync(user, "Bishopric").Result)
                        userManager.AddToRoleAsync(user, "Bishopric").Wait();
                }
            }

            if (context.Meetings.Any())
            {
                return;   // DB has been seeded
            }

            // Seed other data
            var meetings = new Meeting[]
            {
                new Meeting {
                    MeetingDate = DateTime.Parse("2019-03-29"),
                    Conducting = "Bishop Regie Mesina",
                    Invocation = "Brother Drazen Lucic",
                    OpeningSong = "#21, \"Come Listen to a Prophet's Voice\"",
                    SacramentSong = "#184, \"Upon the Cross of Calvary\"",
                    IntermediateSong = "Choir, \"We'll Bring The World His Truth/As Sister's in Zion\"",
                    ClosingSong = "#19, \"We Thank Thee o Good for a Prophet\"",
                    Benediction = "Sister Anna Liza Alzona"
                },
                new Meeting {
                    MeetingDate = DateTime.Parse("2019-04-05"),
                    Conducting = "Brother Roy Javier",
                    Invocation = "Sister Cheryl Lucic",
                    OpeningSong = "#2, \"The Spirit of God\"",
                    SacramentSong = "#183, \"In Remembrance of Thy Suffering\"",
                    IntermediateSong = "Choir, \"We'll Bring The World His Truth/As Sister's in Zion\"",
                    ClosingSong = "#92, \"For the Beauty of the Earth\"",
                    Benediction = "Brother Anthony Lawrence Gampon"
                },
            };
            foreach (Meeting m in meetings)
            {
                context.Meetings.Add(m);
            }
            context.SaveChanges();

            var settings = new Setting[]
            {
                new Setting {
                    Name = "Unit Name",
                    Value = "Dubai 1st Ward (Tagalog)"
                },
                new Setting {
                    Name = "Timing",
                    Value = "3PM - 4PM"
                },
                new Setting {
                    Name = "Presiding",
                    Value = "Bishop Reggie Mesina"
                },
            };
            foreach (Setting s in settings)
            {
                context.Settings.Add(s);
            }
            context.SaveChanges();

            var speakers = new Speaker[]
            {
                new Speaker {
                    MeetingID = 1,
                    SpeakerName = "Brother Lance Aaron Mesina",
                    Subject = "Follow the Prophet",
                    Block = Block.FIRST,
                    Order = 1
                },
                new Speaker {
                    MeetingID = 1,
                    SpeakerName = "Brother Wayne Brockbank",
                    Subject = "Ministering",
                    Block = Block.SECOND,
                    Order = 1
                },
                new Speaker {
                    MeetingID = 2,
                    SpeakerName = "Brother Czamuel Alzona",
                    Subject = "Importance of Daily Scipture Study",
                    Block = Block.FIRST,
                    Order = 1
                },
                new Speaker {
                    MeetingID = 2,
                    SpeakerName = "Sister Natalia Cruz Lopez",
                    Subject = "Eternal Family",
                    Block = Block.FIRST,
                    Order = 2
                },
                new Speaker {
                    MeetingID = 2,
                    SpeakerName = "Brother Rafael Semblante",
                    Subject = "Self Reliance",
                    Block = Block.SECOND,
                    Order = 1
                }
            };
            foreach (Speaker s in speakers)
            {
                context.Speakers.Add(s);
            }
            context.SaveChanges();
        }
    }
}