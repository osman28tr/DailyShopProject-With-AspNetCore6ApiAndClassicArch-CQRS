using Azure.Core;

namespace DailyShop.API.Helpers
{
    public class ImageHelper
    {
        public string GetImage(string schema,HostString host,PathString pathBase,string imageName)
        {
            return String.Format("{0}://{1}{2}/wwwroot/ProductImages/{3}", schema, host,
                pathBase, imageName);
        }
    }
}
