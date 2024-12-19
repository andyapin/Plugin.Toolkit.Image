using FFImageLoading.Drawables;
using FFImageLoading.Work;

namespace Plugin.Toolkit.Image.Platforms.Android
{
    internal class ImagesViewTarget : ITarget<SelfDisposingBitmapDrawable, Com.Davemorrissey.Labs.Subscaleview.SubsamplingScaleImageView>
    {
        public ImagesViewTarget(Com.Davemorrissey.Labs.Subscaleview.SubsamplingScaleImageView imageView)
        {
            Control = imageView;
        }

        public Com.Davemorrissey.Labs.Subscaleview.SubsamplingScaleImageView Control { get; }

        public bool IsValid => true;

        public object TargetControl => Control;

        public void Set(IImageLoaderTask task, SelfDisposingBitmapDrawable image, bool animated)
        {
            var source = Com.Davemorrissey.Labs.Subscaleview.ImageSource.CachedBitmap(image.Bitmap);
            Control.SetImage(source);
        }

        public void SetAsEmpty(IImageLoaderTask task)
        {
        }
    }
}
