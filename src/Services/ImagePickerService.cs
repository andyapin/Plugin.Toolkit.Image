using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics.Platform;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;
using Plugin.Toolkit.Image.Helpers;
using Plugin.Toolkit.Image.Models;

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
            using (var memoryStream = new MemoryStream())
            {
                try
                {
                    Microsoft.Maui.Graphics.IImage image = PlatformImage.FromStream(data);
                    if (image != null)
                    {
                        var width = image.Width;
                        var height = image.Height;

                        if (width > height)
                        {
                            if (width > imageOptions.Width)
                            {
                                image = image.Downsize(imageOptions.Width);
                            }
                        }
                        else
                        {
                            if (width > imageOptions.Height)
                            {
                                image = image.Downsize(imageOptions.Height);
                            }
                        }

                        if (image != null)
                        {
                            image.Save(memoryStream, imageOptions.Format);
                            memoryStream.Seek(0, SeekOrigin.Begin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ConsoleHelper.Exception(ex);
                }
                return new MemoryStream(memoryStream.ToArray());
            }
        }
        private string ImageBase64FromStream(Stream imageSource)
        {
            try
            {
                using (Stream stream = imageSource)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        memoryStream.Position = 0;
                        byte[] imageBytes = memoryStream.ToArray();
                        string base64Image = Convert.ToBase64String(imageBytes);
                        if (!string.IsNullOrEmpty(base64Image))
                        {
                            return base64Image;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.Exception(ex);
            }
            return string.Empty;
        }
        private async Task<Stream> TakeSource()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                string tempPath = string.Empty;
                Stream photoStream = null;
                try
                {
                    var photo = await MediaPicker.Default.CapturePhotoAsync();
                    if (photo != null)
                    {
                        tempPath = System.IO.Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                        using (FileStream localFileStream = File.OpenWrite(tempPath))
                        {
                            photoStream = await photo.OpenReadAsync();
                        }
                        if (imageOptions == null)
                        {
                            return photoStream;
                        }
                        return ImageProccessingStream(photoStream);
                    }
                }
                catch (Exception ex)
                {
                    ConsoleHelper.Exception(ex);
                }
                finally
                {
                    if (File.Exists(tempPath))
                    {
                        File.Delete(tempPath);
                    }
                }
            }
            return null;
        }
        private async Task<Stream> PickSource()
        {
            Stream photoStream = null;
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
                    return ImageProccessingStream(photoStream);
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.Exception(ex);
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
            try
            {
                Stream stream = await PickSource();
                if (stream != null)
                {
                    return ImageBase64FromStream(stream);
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.Exception(ex);
            }
            return string.Empty;
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
            Stream stream = null;
            try
            {
                stream = await TakeSource();
            }
            catch (Exception ex)
            {
                ConsoleHelper.Exception(ex);
            }
            return stream;
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
            try
            {
                Stream stream = await TakeSource();
                if (stream != null)
                {
                    return ImageBase64FromStream(stream);
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.Exception(ex);
            }
            return string.Empty;
        }
    }
}
