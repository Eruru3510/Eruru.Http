using System;
using System.IO;
using System.Net;
using System.Text;

namespace Eruru.Http {

	public delegate void HttpAction ();
	public delegate bool HttpResponsingFunc (bool isDone, Stream stream);

	static class HttpAPI {

		public static string UrlEncode (string url) {
			if (url is null) {
				throw new ArgumentNullException (nameof (url));
			}
			StringBuilder stringBuilder = new StringBuilder ();
			byte[] bytes = Encoding.UTF8.GetBytes (url);
			for (int i = 0; i < bytes.Length; i++) {
				char character = (char)bytes[i];
				if ((character >= '0' && character <= '9') || (character >= 'A' && character <= 'Z') || (character >= 'a' && character <= 'z')) {
					stringBuilder.Append (character);
					continue;
				}
				switch (character) {
					case '+':
					case '!':
					case '\'':
					case '(':
					case ')':
					case '*':
					case '-':
					case '.':
					case '_':
						stringBuilder.Append (character);
						continue;
				}
				stringBuilder.Append ('%');
				stringBuilder.Append (IntToHex (bytes[i] >> 4 & 0xf));
				stringBuilder.Append (IntToHex (bytes[i] & 0xf));
			}
			return stringBuilder.ToString ();
		}

		public static bool Equals (string a, string b) {
			if (a is null) {
				throw new ArgumentNullException (nameof (a));
			}
			if (b is null) {
				throw new ArgumentNullException (nameof (b));
			}
			return string.Equals (a, b, StringComparison.CurrentCultureIgnoreCase);
		}

		public static HttpWebResponse GetResponse (HttpWebRequest webRequest) {
			if (webRequest is null) {
				throw new ArgumentNullException (nameof (webRequest));
			}
			try {
				return (HttpWebResponse)webRequest.GetResponse ();
			} catch (WebException webException) {
				if (webException.Response is null) {
					throw;
				}
				return (HttpWebResponse)webException.Response;
			}
		}

		static char IntToHex (int value) {
			if (value < 10) {
				return (char)('0' + value);
			}
			return (char)('a' + value - 10);
		}

	}

}