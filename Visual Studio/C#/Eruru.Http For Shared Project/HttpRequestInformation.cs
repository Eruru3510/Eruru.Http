using System;
using System.IO;
using System.Net;

namespace Eruru.Http {

	public class HttpRequestInformation {

		public string Url { get; set; }
		public HttpRequestType Type { get; set; } = HttpRequestType.Get;
		public HttpParameterCollection QueryStringParameters { get; set; } = new HttpParameterCollection ();
		public HttpFormData FormData { get; set; }
		public Action<HttpWebRequest> OnRequest { get; set; }
		public HttpFunc<int, bool> OnRequesting { get; set; }
		public HttpAction OnRequested { get; set; }
		public Action<HttpWebResponse> OnResponse { get; set; }
		public HttpFunc<Stream, bool> OnResponding { get; set; }
		public Action<Stream> OnResponded { get; set; }

	}

}