namespace Plugin.Toolkit.Image
{
    public class ImageToolkit
    {
        /// <summary>
        /// Downloads and Show an image from a URL. Default is no caching.
        /// <para></para>
        /// Usage:
        /// <code>
        /// string imageUrl = "https://example.com/image.png";
        /// ImageToolkit.CacheType cacheType = ImageToolkit.CacheType.Memory;
        /// ImageToolkit.FromUrl(imageUrl, cacheType);
        /// </code>
        /// </summary>
        public static void FromUrl(string url, ImageToolkit.CacheType cacheType = ImageToolkit.CacheType.None)
        {
            DependencyService.Get<ImageToolkitInterfaces>().FromUrl(url, cacheType);
        }

        /// <summary>
        /// Show an image from a local file.
        /// <para></para>
        /// Usage:
        /// <code>
        /// string localImageName = "localImage.png";
        /// ImageToolkit.FromLocal(localImageName);
        /// </code>
        /// </summary>
        public static void FromLocal(string imageName)
        {
            DependencyService.Get<ImageToolkitInterfaces>().FromLocal(imageName);
        }

        /// <summary>
        /// Show an image from a Base64 string. Base64 encoded string of the image.
        /// <para></para>
        /// Usage:
        /// <code>
        /// string base64Image = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAA...";
        /// ImageToolkit.FromBase64(base64Image);
        /// </code>
        /// </summary>
        public static void FromBase64(string base64)
        {
            DependencyService.Get<ImageToolkitInterfaces>().FromBase64(base64);
        }

        /// <summary>
        /// Specifies the type of caching to use for images.
        /// </summary>
        public enum CacheType
        {
            /// <summary>
            /// Cache images in memory only.
            /// </summary>
            Memory,

            /// <summary>
            /// Cache images on disk only.
            /// </summary>
            Disk,

            /// <summary>
            /// Cache images in both memory and disk.
            /// </summary>
            All,

            /// <summary>
            /// Do not cache images.
            /// </summary>
            None
        }
    }
}
