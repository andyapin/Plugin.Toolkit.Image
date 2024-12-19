using Android.Content;
using Android.OS;
using Application = Android.App.Application;

namespace Plugin.Toolkit.Image.Platforms.Android
{
    internal class Images : ImageToolkitInterfaces
    {
        public void FromUrl(string url, ImageToolkit.CacheType cacheType = ImageToolkit.CacheType.None)
        {
            Show(url, Enums.ImageSource.ImageFrom.Url, cacheType);
        }

        public void FromLocal(string imagePath)
        {
            Show(imagePath, Enums.ImageSource.ImageFrom.Local);
        }

        public void FromBase64(string base64)
        {
            Show(base64, Enums.ImageSource.ImageFrom.Base64);
        }

        private void Show(string image, Enums.ImageSource.ImageFrom imageType, ImageToolkit.CacheType cacheType = ImageToolkit.CacheType.None)
        {
            Intent intent = new Intent(Application.Context, typeof(ImagesActivity));
            intent.AddFlags(ActivityFlags.NewTask);
            Bundle b = new Bundle();
            b.PutString("ImageToolkit", image);
            b.PutString("ImageToolkitType", imageType.ToString());
            b.PutString("ImageToolkitCacheType", cacheType.ToString());
            intent.PutExtras(b);
            Application.Context.StartActivity(intent);
        }
    }
}
