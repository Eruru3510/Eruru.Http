using System.IO;
using System.Net;
using System.Text;

namespace Eruru.Http {

	public class Http {

		public int BufferSize { get; set; } = 1024;
		public Encoding Encoding { get; set; } = Encoding.UTF8;
		public CookieContainer CookieContainer { get; set; } = new CookieContainer ();

		public Http () {
			ServicePointManager.SecurityProtocol = (SecurityProtocolType)(48 | 192 | 768 | 3072);
		}

		public void Request (string url, HttpRequestInformation information, Stream responseStream) {
			if (url is null) {
				url = information?.Url;
			}
			StringBuilder stringBuilder = new StringBuilder ();
			int questionMarkIndex = url.IndexOf ('?');
			bool hasParameter = false;
			if (questionMarkIndex == -1) {
				stringBuilder.Append (url);
			} else {
				stringBuilder.Append (url.Substring (0, questionMarkIndex));
				if (questionMarkIndex < url.Length - 1) {
					hasParameter = true;
					stringBuilder.Append ($"?{(HttpParameterCollection)url.Substring (questionMarkIndex + 1)}");
				}
			}
			if (information?.QueryStringParameters?.Count > 0) {
				stringBuilder.Append ($"{(hasParameter ? '&' : '?')}{information.QueryStringParameters}");
			}
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create (stringBuilder.ToString ());
			if (information != null) {
				httpWebRequest.Method = information.Type.ToString ();
			}
			httpWebRequest.CookieContainer = CookieContainer;
			information?.OnRequest?.Invoke (httpWebRequest);
			if (information?.FormData != null) {
				using (information.FormData) {
					using (Stream stream = httpWebRequest.GetRequestStream ()) {
						byte[] buffer = new byte[BufferSize];
						while (true) {
							int length = information.FormData.Stream.Read (buffer, 0, buffer.Length);
							if (length == 0) {
								information?.OnRequesting?.Invoke (length);
								information?.OnRequested?.Invoke ();
								break;
							}
							stream.Write (buffer, 0, length);
							if (!information?.OnRequesting?.Invoke (length) ?? false) {
								break;
							}
						}
					}
				}
			}
			using (HttpWebResponse httpWebResponse = HttpApi.GetResponse (httpWebRequest)) {
				information?.OnResponse?.Invoke (httpWebResponse);
				using (Stream stream = httpWebResponse.GetResponseStream ()) {
					byte[] buffer = new byte[BufferSize];
					while (true) {
						int length = stream.Read (buffer, 0, buffer.Length);
						if (length == 0) {
							information?.OnResponding?.Invoke (responseStream);
							information?.OnResponded?.Invoke (responseStream);
							break;
						}
						responseStream.Write (buffer, 0, length);
						if (!information?.OnResponding?.Invoke (responseStream) ?? false) {
							break;
						}
					}
				}
			}
		}
		public void Request (string url, Stream responseStream) {
			Request (url, null, responseStream);
		}
		public void Request (HttpRequestInformation information, Stream responseStream) {
			Request (null, information, responseStream);
		}
		public string Request (string url, HttpRequestInformation information) {
			using (MemoryStream memoryStream = new MemoryStream ()) {
				Request (url, information, memoryStream);
				return Encoding.UTF8.GetString (memoryStream.ToArray ());
			}
		}
		public string Request (HttpRequestInformation information) {
			using (MemoryStream memoryStream = new MemoryStream ()) {
				Request (null, information, memoryStream);
				return Encoding.UTF8.GetString (memoryStream.ToArray ());
			}
		}
		public string Request (string url) {
			using (MemoryStream memoryStream = new MemoryStream ()) {
				Request (url, null, memoryStream);
				return Encoding.UTF8.GetString (memoryStream.ToArray ());
			}
		}

		public byte[] RequestBytes (string url, HttpRequestInformation information) {
			using (MemoryStream memoryStream = new MemoryStream ()) {
				Request (url, information, memoryStream);
				return memoryStream.ToArray ();
			}
		}
		public byte[] RequestBytes (string url) {
			using (MemoryStream memoryStream = new MemoryStream ()) {
				Request (url, null, memoryStream);
				return memoryStream.ToArray ();
			}
		}
		public byte[] RequestBytes (HttpRequestInformation information) {
			using (MemoryStream memoryStream = new MemoryStream ()) {
				Request (null, information, memoryStream);
				return memoryStream.ToArray ();
			}
		}
		public MemoryStream RequestMemoryStream (string url, HttpRequestInformation information) {
			MemoryStream memoryStream = new MemoryStream ();
			Request (url, information, memoryStream);
			return memoryStream;
		}
		public MemoryStream RequestMemoryStream (string url) {
			MemoryStream memoryStream = new MemoryStream ();
			Request (url, null, memoryStream);
			return memoryStream;
		}
		public MemoryStream RequestMemoryStream (HttpRequestInformation information) {
			MemoryStream memoryStream = new MemoryStream ();
			Request (null, information, memoryStream);
			return memoryStream;
		}

	}

}