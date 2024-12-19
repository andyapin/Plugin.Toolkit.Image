using FFImageLoading.Maui;
#if ANDROID
using Plugin.Toolkit.Image.Platforms.Android;
#endif

namespace Plugin.Toolkit.Image
{
    public static class Register
    {
        /// <summary>
        /// Registers the Plugin.Toolkit.Image with the Maui app builder.
        /// </summary>
        /// <param name="builder">The Maui app builder instance.</param>
        /// <returns>The Maui app builder instance with the image toolkit registered.</returns>
        public static MauiAppBuilder UseImageToolkit(this MauiAppBuilder builder)
        {
#if ANDROID
            DependencyService.Register<ImageToolkitInterfaces, Images>();
#endif
            builder.UseFFImageLoading();
            return builder;
        }
    }
}
