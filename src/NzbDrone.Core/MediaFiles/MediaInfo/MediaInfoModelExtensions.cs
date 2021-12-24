using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NzbDrone.Common.Extensions;

namespace NzbDrone.Core.MediaFiles.MediaInfo
{
    public static class MediaInfoModelExtensions
    {
        // ToDo Need FFProbe Examples
        private static readonly string[] Hdr10Formats = { "SMPTE ST 2086", "HDR10" };
        private static readonly string[] Hdr10PlusFormats = { "SMPTE ST 2094 App 4" };
        private static readonly string[] DolbyVisionFormats = { "Dolby Vision" };

        public static HdrFormat GetHdrFormat(this MediaInfoModel mediaInfo)
        {
            if (mediaInfo.VideoBitDepth < 10)
            {
                return HdrFormat.None;
            }
            else
            {
                // ToDo Work on this (Need JSON Examples)
                if (DolbyVisionFormats.Any(mediaInfo.VideoHdrFormat.ContainsIgnoreCase))
                {
                    if (Hdr10Formats.Any(mediaInfo.VideoHdrFormat.ContainsIgnoreCase))
                    {
                        return HdrFormat.DolbyVisionHdr10;
                    }
                    else
                    {
                        return HdrFormat.DolbyVision;
                    }
                }
                else if (Hdr10Formats.Any(mediaInfo.VideoHdrFormat.ContainsIgnoreCase))
                {
                    return HdrFormat.Hdr10;
                }
                else if (Hdr10PlusFormats.Any(mediaInfo.VideoHdrFormat.ContainsIgnoreCase))
                {
                    return HdrFormat.Hdr10Plus;
                }
            }
        }
    }
}
