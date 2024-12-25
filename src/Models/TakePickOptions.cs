using Plugin.Toolkit.Image.Enums;

namespace Plugin.Toolkit.Image.Models
{
    /// <summary>
    /// Represents the options for taking or picking an image.
    /// </summary>
    public class TakePickOptions
    {
        // The width of the image in pixels.
        /// <summary>
        /// Gets or sets the width of the image.
        /// <code>
        /// Default: 
        /// Portrait: 720px;
        /// Landscape: 1280px;
        /// </code>
        /// </summary>
        public int Width { get; set; } = 1280;

        // The height of the image in pixels.
        /// <summary>
        /// Gets or sets the height of the image.
        /// <code>
        /// Default: 
        /// Portrait: 1280px;
        /// Landscape: 720px;
        /// </code>
        /// </summary>
        public int Height { get; set; } = 720;

        // The format of the image.
        /// <summary>
        /// Gets or sets the format of the image.
        /// </summary>
        public ImageFormat Format { get; set; } = ImageFormat.Jpeg;
    }
}
