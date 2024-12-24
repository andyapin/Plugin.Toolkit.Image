using Plugin.Toolkit.Image.Enums;

namespace Plugin.Toolkit.Image.Interfaces
{
    public interface IImageToolkitView
    {
        void FromUrl(string url, CacheType cacheType = CacheType.None);
        void FromLocal(string imageName);
        void FromBase64(string base64);
    }
}
