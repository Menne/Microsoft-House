using MicrosoftHouse;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using TaskList.UWP.Services;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(UWPLoginProvider))]
namespace TaskList.UWP.Services
{
    public class UWPLoginProvider : ILoginProvider
    {

        public async Task LoginAsync(MobileServiceClient client)
        {
            await client.LoginAsync("facebook");
        }

        public string GetSyncStore()
        {
            throw new NotImplementedException();
        }
    }
}