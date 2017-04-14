using Microsoft.WindowsAzure.MobileServices;
using MicrosoftHouse;
using System.Threading.Tasks;

namespace TaskList.UWP.Services
{
    class UWPLoginProvider : ILoginProvider
    {
        public async Task LoginAsync(MobileServiceClient client)
        {
            await client.LoginAsync("facebook");
        }
    }
}
