using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Backend.DataObjects;
using Backend.Models;
using Owin;

namespace Backend
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new MobileServiceInitializer());

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    // This middleware is intended to be used locally for debugging. By default, HostName will
                    // only have a value when running in an App Service application.
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }

            app.UseWebApi(config);
        }
    }

	// if a DB doesn't exist creates an istance
    public class MobileServiceInitializer : CreateDatabaseIfNotExists<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
			// Tutte le Stanze

			List<Room> allRooms = new List<Room>
			{

				//Piano Terra - 5 Stanze
				new Room { Id = Guid.NewGuid().ToString(), Name = "EG1", Floor = "0", Seats="10"  },
				new Room { Id = Guid.NewGuid().ToString(), Name = "EG2", Floor = "0", Seats="10"  },
				new Room { Id = Guid.NewGuid().ToString(), Name = "EG3", Floor = "0", Seats="10"  },
				new Room { Id = Guid.NewGuid().ToString(), Name = "EG4", Floor = "0", Seats="10"  },
				new Room { Id = Guid.NewGuid().ToString(), Name = "EG5", Floor = "0", Seats="10"  },

				//Primo Piano - 6 Stanze
				new Room { Id = Guid.NewGuid().ToString(), Name = "I01", Floor = "1", Seats="10"  },
				new Room { Id = Guid.NewGuid().ToString(), Name = "I02", Floor = "1", Seats="10"  },
				new Room { Id = Guid.NewGuid().ToString(), Name = "I03", Floor = "1", Seats="10"  },
				new Room { Id = Guid.NewGuid().ToString(), Name = "I04", Floor = "1", Seats="10"  },
				new Room { Id = Guid.NewGuid().ToString(), Name = "I05", Floor = "1", Seats="10"  },
				new Room { Id = Guid.NewGuid().ToString(), Name = "I06", Floor = "1", Seats="10"  },

				//Secondo Piano - 4 Stanze
				new Room { Id = Guid.NewGuid().ToString(), Name = "L2601", Floor = "2", Seats="10" },
				new Room { Id = Guid.NewGuid().ToString(), Name = "L2602", Floor = "2", Seats="10" },
				new Room { Id = Guid.NewGuid().ToString(), Name = "L2603", Floor = "2", Seats="10" },
				new Room { Id = Guid.NewGuid().ToString(), Name = "L2604", Floor = "2", Seats="10" },

				//Terzo Piano - 5 Stanze
				new Room { Id = Guid.NewGuid().ToString(), Name = "N01", Floor = "3", Seats="10" },
				new Room { Id = Guid.NewGuid().ToString(), Name = "N02", Floor = "3", Seats="10" },
				new Room { Id = Guid.NewGuid().ToString(), Name = "N03", Floor = "3", Seats="10" },
				new Room { Id = Guid.NewGuid().ToString(), Name = "N04", Floor = "3", Seats="10" },
				new Room { Id = Guid.NewGuid().ToString(), Name = "N05", Floor = "3", Seats="10" }

			};

			foreach (Room room in allRooms)
			{
				context.Set<Room>().Add(room);
			}

			// Parcheggio

			/*context.Set<CarPark>().Add(new CarPark { Id = Guid.NewGuid().ToString(), Park = 30 });

			List<TodoItem> todoItems = new List<TodoItem>
            {
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false },
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false }
            };

            foreach (TodoItem todoItem in todoItems)
            {
                context.Set<TodoItem>().Add(todoItem);
            }*/

            base.Seed(context);
        }
    }
}

