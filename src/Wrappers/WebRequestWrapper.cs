using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace SourceCode.SmartObjects.Services.Tests.Wrappers
{
    [ExcludeFromCodeCoverage]
    internal class WebRequestWrapper
    {
        public virtual HttpWebResponse GetHttpResponse(WebRequest request)
        {
            return (HttpWebResponse)request.GetResponse();
        }
    }
}