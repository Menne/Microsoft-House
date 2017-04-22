using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MicrosoftHouse
{
    public partial class AboutUsPage : ContentPage
    {
        public AboutUsPage()
        {
            InitializeComponent();

            MhMap.MoveToRegion(new MapSpan(new Position(45.481739, 9.183140), 0.0125, 0.0125));

            Position position = new Position(45.481739, 9.183140);
            Pin pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "Microsoft House",
                Address = "Viale Pasubio, 2"
            };
            MhMap.Pins.Add(pin);
        }
    }
}
