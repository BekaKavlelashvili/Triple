using System;
using System.Collections.Generic;
using System.Text;

namespace Triple.Application.Dtos.Shared
{
    public class ImageDto
    {
        public string MimeType { get; set; }

        public string OriginalFileName { get; set; }

        public string UniqueFileName { get; set; }

        public string Path { get; set; }

        public string Url { get; set; }
    }
}
