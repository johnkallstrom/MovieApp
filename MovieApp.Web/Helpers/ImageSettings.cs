namespace MovieApp.Web.Helpers
{
    public class ImageSettings
    {
        public string BaseUrl { get; set; }
        public string SizeType { get; set; }
        public string FilePath { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public ImageSettings(string baseUrl, string sizeType, string filePath)
        {
            BaseUrl = baseUrl;
            SizeType = sizeType;
            FilePath = filePath;
        }

        public ImageSettings(string baseUrl, int width, int height)
        {
            BaseUrl = baseUrl;
            Width = width;
            Height = height;
        }
    }
}
