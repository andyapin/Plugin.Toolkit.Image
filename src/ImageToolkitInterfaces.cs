using Plugin.Toolkit.Image.Enums;

namespace Plugin.Toolkit.Image
{
    /// <summary>
    /// Interface for the ImageToolkit. This interface defines the methods that any class 
    /// implementing the ImageToolkit must have.
    /// </summary>
    public interface ImageToolkitInterfaces
    {
        void FromUrl(string url, CacheType cacheType = CacheType.None);
        void FromLocal(string imageName);
        void FromBase64(string base64);
    }
}
