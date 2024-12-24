namespace Plugin.Toolkit.Image.Enums
{
    /// <summary>
    /// Enumeration to specify the source of the image.
    /// </summary>
    public enum ImageFrom
    {
        /// <summary>
        /// Image sourced from a URL.
        /// </summary>
        Url,

        /// <summary>
        /// Image sourced from a local file path.
        /// </summary>
        Local,

        /// <summary>
        /// Image sourced from a stream.
        /// </summary>
        Stream,

        /// <summary>
        /// Image sourced from an image object.
        /// </summary>
        ImageSource,

        /// <summary>
        /// Image sourced from a Base64 encoded string.
        /// </summary>
        Base64
    }

}
