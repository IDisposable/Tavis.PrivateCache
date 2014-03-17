using System;
using System.Net.Http;

namespace ClientSamples.CachingTools
{
    public class PrimaryCacheKey
    {
        private readonly Uri _uri;
        private readonly HttpMethod _method;


        public PrimaryCacheKey(Uri uri, HttpMethod method)
        {
            _uri = uri;
            _method = method;
            // see http://www.w3.org/Protocols/rfc2616/rfc2616-sec13.html 13.10, really should invalidate all GET keys
            // for the same URI, so perhaps this whole key thing should be structured as yeild-returning a collection
            // of equivalent/related keys.
            if (_method == HttpMethod.Post)  // A response to a POST can be returned to a GET method
            {
                _method = HttpMethod.Get; 
            }
        }

        public override bool Equals(object obj)
        {
            var key2 = (PrimaryCacheKey) obj; 
            return key2._uri == _uri && key2._method == _method;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + _uri.GetHashCode();
            hash = (hash * 7) + _method.GetHashCode();
            return hash;
        }
    }
}
