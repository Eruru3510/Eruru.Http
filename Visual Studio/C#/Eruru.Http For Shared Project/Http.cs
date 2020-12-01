using System;
using System.IO;
using System.Net;
using System.Text;

namespace Eruru.Http {

	public class Http {

		public int BufferLength { get; set; } = 1024;
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
			MemoryStream memoryStream = new MemoryStream ();
			Request (information, memoryStream);
			return Encoding.UTF8.GetString (memoryStream.ToArray ());
		}
		public void Request (HttpRequestInformation information, Stream responseStream) {
			if (information is null) {
				throw new ArgumentNullException (nameof (information));
			}
			if (responseStream is null) {
				throw new ArgumentNullException (nameof (responseStream));
			}
			StringBuilder stringBuilder = new StringBuilder (information.Url);
			if (information.QueryStringParameters != null) {
				stringBuilder.Append ('?');
				stringBuilder.Append (information.QueryStringParameters);
			}
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create (stringBuilder.ToString ());
			httpWebRequest.Method = information.Type.ToString ();
			httpWebRequest.CookieContainer = CookieContainer;
			information.Request?.Invoke (httpWebRequest);
			if (information.FormData != null) {
				using (information.FormData) {
					using (Stream stream = httpWebRequest.GetRequestStream ()) {
						byte[] buffer = new byte[BufferLength];
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
					byte[] buffer = new byte[BufferLength];
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
			Request (information, memoryStream);
			return memoryStream.ToArray ();
		}

	}

}