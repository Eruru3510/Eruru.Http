using System;
using System.IO;
using System.Net;
using System.Text;

namespace Eruru.Http {

	public class Http {

		public int BufferSize { get; set; } = 1024;
		public Encoding Encoding { get; set; } = Encoding.UTF8;
		public CookieContainer CookieContainer {

			get {
				if (_CookieContainer is null) {
					_CookieContainer = new CookieContainer ();
				}
				return _CookieContainer;
			}

			set => _CookieContainer = value;

		}

		CookieContainer _CookieContainer;

		public Http () {
			ServicePointManager.SecurityProtocol = (SecurityProtocolType)(48 | 192 | 768 | 3072);
		}

		public string Request (HttpRequestInformation information) {
			if (information is null) {
				throw new ArgumentNullException (nameof (information));
			}
			return Request (null, information);
		}
		public string Request (string url, HttpRequestInformation information) {
			if (information is null) {
				throw new ArgumentNullException (nameof (information));
			}
			MemoryStream memoryStream = new MemoryStream ();
			Request (url, information, memoryStream);
			return Encoding.UTF8.GetString (memoryStream.ToArray ());
		}
		public void Request (HttpRequestInformation information, Stream responseStream) {
			if (information is null) {
				throw new ArgumentNullException (nameof (information));
			}
			if (responseStream is null) {
				throw new ArgumentNullException (nameof (responseStream));
			}
			Request (null, information, responseStream);
		}
		public void Request (string url, HttpRequestInformation information, Stream responseStream) {
			if (information is null) {
				throw new ArgumentNullException (nameof (information));
			}
			if (responseStream is null) {
				throw new ArgumentNullException (nameof (responseStream));
			}
			if (url is null) {
				url = information.Url;
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
			if (information.QueryStringParameters != null) {
				stringBuilder.Append (hasParameter ? '&' : '?');
				stringBuilder.Append (information.QueryStringParameters);
			}
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create (stringBuilder.ToString ());
			httpWebRequest.Method = information.Type.ToString ();
			httpWebRequest.CookieContainer = CookieContainer;
			information.Request?.Invoke (httpWebRequest);
			if (information.FormData != null) {
				using (information.FormData) {
					using (Stream stream = httpWebRequest.GetRequestStream ()) {
						byte[] buffer = new byte[BufferSize];
						while (true) {
							int length = information.FormData.Stream.Read (buffer, 0, buffer.Length);
							if (length > 0) {
								stream.Write (buffer, 0, length);
							}
							break;
						}
					}
				}
			}
			using (HttpWebResponse httpWebResponse = HttpAPI.GetResponse (httpWebRequest)) {
				information.Response?.Invoke (httpWebResponse);
				using (Stream stream = httpWebResponse.GetResponseStream ()) {
					byte[] buffer = new byte[BufferSize];
					while (true) {
						int length = stream.Read (buffer, 0, buffer.Length);
						if (length <= 0) {
							information.Responsing?.Invoke (true, responseStream);
							break;
						}
						responseStream.Write (buffer, 0, length);
						if (!information.Responsing?.Invoke (false, responseStream) ?? false) {
							break;
						}
					}
				}
			}
		}

		public byte[] RequestBytes (HttpRequestInformation information) {
			if (information is null) {
				throw new ArgumentNullException (nameof (information));
			}
			MemoryStream memoryStream = new MemoryStream ();
			Request (null, information, memoryStream);
			return memoryStream.ToArray ();
		}
		public byte[] RequestBytes (string url, HttpRequestInformation information) {
			if (information is null) {
				throw new ArgumentNullException (nameof (information));
			}
			MemoryStream memoryStream = new MemoryStream ();
			Request (url, information, memoryStream);
			return memoryStream.ToArray ();
		}

	}

}