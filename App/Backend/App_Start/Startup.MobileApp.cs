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

			//config.MapHttpAttributeRoutes();

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

	public class MobileServiceInitializer : CreateDatabaseIfNotExists<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
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

			context.Set<CarPark>().Add(new CarPark { Id = Guid.NewGuid().ToString(), Park = "30" });

			List<EventLocation> locations = new List<EventLocation>
			{
				new EventLocation { Id = Guid.NewGuid().ToString(), Name = "Atrio", Floor = "0", Seats="100"  },
				new EventLocation { Id = Guid.NewGuid().ToString(), Name = "Aula Magna", Floor = "0", Seats="200"  }

			};

			foreach (EventLocation location in locations)
			{
				context.Set<EventLocation>().Add(location);
			}

			context.SaveChanges();
            base.Seed(context);
        }
    }
}

