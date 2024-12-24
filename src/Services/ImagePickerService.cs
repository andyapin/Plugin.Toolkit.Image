
using Microsoft.Maui.Graphics.Platform;
using Plugin.Toolkit.Image.Helpers;
using Plugin.Toolkit.Image.Interfaces;
using Plugin.Toolkit.Image.Models;
using SkiaSharp;
using System.Buffers.Text;
using System.IO;

namespace Plugin.Toolkit.Image.Services
{
    public class ImagePickerService
    {
        private static TakePickOptions imageOptions = null;

        private FilePickerFileType PickerFileType()
        {
            var FileType = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.iOS, new[] { "jpeg", "jpg", "png" } }, // UTType values
                        { DevicePlatform.Android, new[] { "image/jpeg", "image/jpg", "image/png" } }, // MIME type
                        { DevicePlatform.WinUI, new[] { ".jpeg", ".jpg", ".png" } }, // file extension
                        { DevicePlatform.macOS, new[] { "jpeg", "jpg", "png" } }, // UTType values
                    });
            return FileType;
        }
        private PickOptions PickerOption()
        {
            string title = "Select image file";
            PickOptions options = new()
            {
                PickerTitle = title,
                FileTypes = PickerFileType(),
            };
            return options;
        }
        private Stream ImageProccessingStream(Stream data)
        {
            MemoryStream memoryStream = null;
            try
            {
                Microsoft.Maui.Graphics.IImage image = PlatformImage.FromStream(data);
                if (image != null)
                {
                    var width = image.Width;
                    var height = image.Height;
                    if (width > height)
                    {
                        // Landscape
                        if (width > imageOptions.Width)
                        {
                            Microsoft.Maui.Graphics.IImage newImage = image.Downsize(imageOptions.Width, true);
                            newImage.Save(memoryStream, Microsoft.Maui.Graphics.ImageFormat.Jpeg);
                            memoryStream.Seek(0, SeekOrigin.Begin);
                        }
                        else
                        {
                            Microsoft.Maui.Graphics.IImage newImage = image;
                            newImage.Save(memoryStream, Microsoft.Maui.Graphics.ImageFormat.Jpeg);
                            memoryStream.Seek(0, SeekOrigin.Begin);
                        }
                    }
                    else
                    {
                        // Portrait or Square
                        if (width > imageOptions.Height)
                        {
                            Microsoft.Maui.Graphics.IImage newImage = image.Downsize(imageOptions.Height, true);
                            newImage.Save(memoryStream, Microsoft.Maui.Graphics.ImageFormat.Jpeg);
                            memoryStream.Seek(0, SeekOrigin.Begin);
                        }
                        else
                        {
                            Microsoft.Maui.Graphics.IImage newImage = image;
                            newImage.Save(memoryStream, Microsoft.Maui.Graphics.ImageFormat.Jpeg);
                            memoryStream.Seek(0, SeekOrigin.Begin);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.Exception(ex);
            }
            return memoryStream;
        }
        private string ImageBase64FromStream(Stream imageSource)
        {
            string base64 = string.Empty;
            MemoryStream memoryStream = null;
            try
            {
                using (Stream stream = imageSource)
                {
                    stream.CopyTo(memoryStream);
                    memoryStream.Position = 0;
                    byte[] imageBytes = memoryStream.ToArray();
                    string base64Image = Convert.ToBase64String(imageBytes);
                    if (base64Image != "" && !string.IsNullOrEmpty(base64Image))
                    {
                        base64 = base64Image;
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.Exception(ex);
            }
            return base64;
        }
        private async Task<Stream> TakeSource()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                Stream photoStream = null;
                Stream Stream = null;
                try
                {
                    var photo = await MediaPicker.Default.CapturePhotoAsync();
                    if (photo != null)
                    {
                        photoStream = await photo.OpenReadAsync();
                        if (imageOptions == null)
                        {
                            return photoStream;
                        }
                        Stream = ImageProccessingStream(photoStream);
                        return Stream;
                    }
                }
                catch (Exception ex)
                {
                    ConsoleHelper.Exception(ex);
                }
                finally
                {
                    if (imageOptions != null)
                    {
                        Stream?.Close();
                    }
                }
            }
            return null;
        }
        private async Task<Stream> PickSource()
        {
            Stream photoStream = null;
            Stream Stream = null;
            try
            {
                var photo = await FilePicker.Default.PickAsync(PickerOption());
                if (photo != null)
                {
                    photoStream = await photo.OpenReadAsync();
                    if (imageOptions == null)
                    {
                        return photoStream;
                    }
                    Stream = ImageProccessingStream(photoStream);
                    return Stream;
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.Exception(ex);
            }
            finally
            { 
                if (imageOptions != null)
                {
                    Stream?.Close();
                }
            }
            return null;
        }

        /// <summary>
        /// Picks an image using the IImagePickerInterface.
        /// <para>
        /// Optional TakePickOptions to configure the image resize process.
        /// </para>
        /// Usage:
        /// <code>
        /// var imageToolkit = new ImageToolkit();
        /// var image = await imageToolkit.ImageRequest.PickImage();
        /// </code>
        /// </summary>
        /// <returns>A Stream containing the picked image.</returns>
        public async Task<Stream> PickImage(TakePickOptions options = null)
        {
            imageOptions = options;
            Stream stream = null;
            try
            {
                stream = await PickSource();
            }
            catch (Exception ex)
            {
                ConsoleHelper.Exception(ex);
            }
            return stream;
        }

        /// <summary>
        /// Picks an image from the device's gallery and returns it as a Base64 encoded string.
        /// <para>
        /// Optional TakePickOptions to configure the image resize process.
        /// </para>
        /// Usage:
        /// <code>
        /// var imageToolkit = new ImageToolkit();
        /// var image = await imageToolkit.ImageRequest.PickImageAsBase64();
        /// </code>
        /// </summary>
        /// <returns>A Task that represents the asynchronous operation. The task result contains the Base64 encoded string of the picked image.</returns>
        public async Task<string> PickImageAsBase64(TakePickOptions options = null)
        {
            imageOptions = options;
            string base64 = string.Empty;
            try
            {
                Stream stream = await PickSource();
                base64 = ImageBase64FromStream(stream) ?? string.Empty;
            }
            catch (Exception ex)
            {
                ConsoleHelper.Exception(ex);
            }
            return base64;
        }

        /// <summary>
        /// Takes an image using the IImagePickerInterface.
        /// <para>
        /// Optional TakePickOptions to configure the image resize process.
        /// </para>
        /// Usage:
        /// <code>
        /// var imageToolkit = new ImageToolkit();
        /// var image = await imageToolkit.ImageRequest.TakeImage();
        /// </code>
        /// </summary>
        /// <returns>A Stream containing the taken image.</returns>
        public async Task<Stream> TakeImage(TakePickOptions options = null)
        {
            imageOptions = options;
            Stream memoryStream = null;
            try
            {
                memoryStream = await TakeSource();
            }
            catch (Exception ex)
            {
                ConsoleHelper.Exception(ex);
            }
            return memoryStream;
        }

        /// <summary>
        /// Takes an image using the IImagePickerInterface and returns it as a Base64 encoded string.
        /// <para>
        /// Optional TakePickOptions to configure the image resize process.
        /// </para>
        /// Usage:
        /// <code>
        /// var imageToolkit = new ImageToolkit();
        /// var image = await imageToolkit.ImageRequest.TakeImageAsBase64();
        /// </code>
        /// </summary>
        /// <returns>A Task that represents the asynchronous operation. The task result contains the Base64 encoded string of the taken image.</returns>
        public async Task<string> TakeImageAsBase64(TakePickOptions options = null)
        {
            imageOptions = options;
            string base64 = string.Empty;
            try
            {
                Stream stream = await TakeSource();
                base64 = ImageBase64FromStream(stream) ?? string.Empty;
            }
            catch (Exception ex)
            {
                ConsoleHelper.Exception(ex);
            }
            return base64;
        }
    }
}
