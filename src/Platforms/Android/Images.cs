using Android.Content;
using Android.OS;
using Plugin.Toolkit.Image.Enums;
using Plugin.Toolkit.Image.Interfaces;
using Application = Android.App.Application;

namespace Plugin.Toolkit.Image.Platforms.Android
{
    public class Images : IImageToolkitView
    {
        public void FromUrl(string url, CacheType cacheType = CacheType.None)
        {
            Show(url, ImageFrom.Url, cacheType);
        }

        public void FromLocal(string imagePath)
        {
            Show(imagePath, ImageFrom.Local);
        }

        public void FromBase64(string base64)
        {
            Show(base64, ImageFrom.Base64);
        }

        private void Show(string image, ImageFrom imageType, CacheType cacheType = CacheType.None)
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
