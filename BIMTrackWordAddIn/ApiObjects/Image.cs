using System;

namespace WordAddIn1.ApiObjects
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string ThumbnailUrl { get; set; }
        public DateTime ThumbnailUrlExpiration { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public DateTime UrlExpiration { get; set; }
    }
}