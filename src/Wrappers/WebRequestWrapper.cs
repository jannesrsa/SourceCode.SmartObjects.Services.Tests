using System.Net;

namespace SourceCode.SmartObjects.Services.Tests.Wrappers
{
    internal class WebRequestWrapper
    {
        public virtual HttpWebResponse GetHttpResponse(WebRequest request)
        {
            return (HttpWebResponse)request.GetResponse();
        }
    }
}