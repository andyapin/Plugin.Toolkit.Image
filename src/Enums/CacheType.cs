using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Toolkit.Image.Enums
{
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
