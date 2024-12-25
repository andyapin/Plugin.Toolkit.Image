using Microsoft.Maui.Animations;
using Plugin.Toolkit.Image;
using Plugin.Toolkit.Image.Enums;
using Plugin.Toolkit.Image.Models;
using System.Buffers.Text;

namespace sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string imagePath = "dotnet_bot.png";
            ImageToolkit.FromLocal(imagePath);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            string imagePath = "https://images.unsplash.com/photo-1650177043873-ca284558be52";
            ImageToolkit.FromUrl(imagePath, CacheType.Memory);
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            string base64 = "data:image/png;base64,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";
            ImageToolkit.FromBase64(base64);
        }

        ImageToolkit imageToolkit = new ImageToolkit();

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            var imgStream = await imageToolkit.ImageRequest.TakeImage();
            image_sources.Source = ImageSource.FromStream(() => imgStream);
        }

        private async void Button_Clicked_7(object sender, EventArgs e)
        {
            var imgStream = await imageToolkit.ImageRequest.TakeImage(new TakePickOptions()
            {
                Height = 1280,
                Width = 720,
                Format = ImageFormat.Jpeg
            });
            image_sources.Source = ImageSource.FromStream(() => imgStream);
        }

        private async void Button_Clicked_4(object sender, EventArgs e)
        {
            var imgStream = await imageToolkit.ImageRequest.PickImage();
            image_sources.Source = ImageSource.FromStream(() => imgStream);
        }

        private async void Button_Clicked_5(object sender, EventArgs e)
        {
            string base64 = await imageToolkit.ImageRequest.TakeImageAsBase64();
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                image_base64.Text = base64;
            }
            else
            {
                Console.WriteLine(base64);
                await DisplayAlert("See Console", "", "OK");
            }
        }

        private async void Button_Clicked_6(object sender, EventArgs e)
        {
            string base64 = await imageToolkit.ImageRequest.PickImageAsBase64();
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                image_base64.Text = base64;
            }
            else
            {
                Console.WriteLine(base64);
                await DisplayAlert("See Console", "", "OK");
            }
        }
    }

}
