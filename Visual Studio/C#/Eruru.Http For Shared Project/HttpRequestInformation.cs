using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Eruru.Http {

	public class HttpRequestInformation {

		public string Url;
		public HttpRequestType Type = HttpRequestType.Get;
		public HttpParameterCollection QueryStringParameters;
		public HttpFormData FormData;
		public Action<HttpWebRequest> Request;
		public Action<HttpWebResponse> Response;
		public HttpResponsingFunc Responsing;

	}

}