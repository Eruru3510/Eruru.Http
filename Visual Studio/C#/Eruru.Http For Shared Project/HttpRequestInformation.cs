using System;
using System.Net;

namespace Eruru.Http {

	public class HttpRequestInformation {

		public string Url;
		public HttpRequestType Type = HttpRequestType.Get;
		public HttpParameterCollection QueryStringParameters = new HttpParameterCollection ();
		public HttpFormData FormData;
		public Action<HttpWebRequest> Request;
		public Action<HttpWebResponse> Response;
		public HttpResponsingFunc Responsing;

	}

}