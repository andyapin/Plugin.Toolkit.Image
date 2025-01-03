﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.Widget;
using FFImageLoading;
using FFImageLoading.Helpers;
using Microsoft.Maui.Controls.Compatibility;
using Plugin.Toolkit.Image.Enums;
using AndroidBase = Android;

namespace Plugin.Toolkit.Image.Platforms.Android
{
    [Activity(Label = "")]
    internal class ImagesActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var uiOptions = SystemUiFlags.HideNavigation |
                            SystemUiFlags.LayoutHideNavigation |
                            SystemUiFlags.LayoutFullscreen |
                            SystemUiFlags.Fullscreen |
                            SystemUiFlags.LayoutStable |
                            SystemUiFlags.ImmersiveSticky;
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
            SetContentView(Resource.Layout.layout_image);
            Intent myIntent = Intent;
            string items = myIntent.GetStringExtra("ImageToolkit") ?? string.Empty;
            string type = myIntent.GetStringExtra("ImageToolkitType") ?? nameof(ImageFrom.Url);
            string cache = myIntent.GetStringExtra("ImageToolkitCacheType") ?? nameof(CacheType.None);
            InitComponents(items, type, cache);
        }

        protected void InitComponents(string imageValue, string type = nameof(ImageFrom.Url), string cacheType = nameof(CacheType.None))
        {
            var ImageView = FindViewById<Com.Davemorrissey.Labs.Subscaleview.SubsamplingScaleImageView>(Resource.Id.galleryImageView);
            ImageView.SetMinimumDpi(10);
            IImageService _imageService = ServiceHelper.GetService<IImageService>();
            var imageTarget = new ImagesViewTarget(ImageView);
            var cache = FFImageLoading.Cache.CacheType.None;
            switch (cacheType)
            {
                case nameof(CacheType.None):
                    cache = FFImageLoading.Cache.CacheType.None;
                    break;
                case nameof(CacheType.Memory):
                    cache = FFImageLoading.Cache.CacheType.Memory;
                    break;
                case nameof(CacheType.Disk):
                    cache = FFImageLoading.Cache.CacheType.Disk;
                    break;
                case nameof(CacheType.All):
                    cache = FFImageLoading.Cache.CacheType.All;
                    break;
            }
            switch (type)
            {
                case nameof(ImageFrom.Url):
                    ImageService.Instance.LoadUrl(imageValue).WithCache(cache).IntoAsync(imageTarget, _imageService);
                    break;
                case nameof(ImageFrom.Base64):
                    ImageService.Instance.LoadBase64String(imageValue).IntoAsync(imageTarget, _imageService);
                    break;
                case nameof(ImageFrom.Local):
                    ImageService.Instance.LoadFileFromApplicationBundle(imageValue).IntoAsync(imageTarget, _imageService);
                    break;
            }
            if (type == nameof(ImageFrom.Url))
            {
                var activityIndicatorView = FindViewById<AndroidBase.Widget.ProgressBar>(Resource.Id.activityIndicator);
                if (activityIndicatorView is not null)
                {
                    if (!ImageView.IsImageLoaded)
                    {
                        activityIndicatorView.Visibility = ViewStates.Visible;
                    }
                    ImageView.ImageLoaded += (s, e) =>
                    {
                        if (ImageView.IsImageLoaded)
                        {
                            activityIndicatorView.Visibility = ViewStates.Gone;
                        }
                    };
                }
            }
            var btnAction = FindViewById<AndroidBase.Widget.ImageButton>(Resource.Id.btnclose);
            if (btnAction is not null)
            {
                btnAction.Click += (o, e) =>
                {
                    Finish();
                };
            }
        }
    }
}
