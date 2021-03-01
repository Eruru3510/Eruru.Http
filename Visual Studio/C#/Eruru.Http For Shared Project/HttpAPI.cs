using System;
using System.IO;
using System.Net;
using System.Text;

namespace Eruru.Http {

	public delegate void HttpAction ();
	public delegate bool HttpResponsingFunc (bool isDone, Stream stream);

	public static class HttpApi {

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
				stringBuilder.Append (IntToHex (bytes[i] >> 4));
				stringBuilder.Append (IntToHex (bytes[i] & 0xf));
			}
			return stringBuilder.ToString ();
		}

		public static string UrlDecode (string url) {
			if (url is null) {
				throw new ArgumentNullException (nameof (url));
			}
			StringBuilder stringBuilder = new StringBuilder ();
			for (int i = 0; i < url.Length; i++) {
				if (url[i] == '%' && i < url.Length - 2) {
					stringBuilder.Append (HexToInt (url[++i]) << 4 + HexToInt (url[++i]));
					continue;
				}
				stringBuilder.Append (url[i]);
			}
			return stringBuilder.ToString ();
		}

		public static bool Equals (string a, string b) {
			return string.Equals (a, b, StringComparison.OrdinalIgnoreCase);
		}

		internal static HttpWebResponse GetResponse (HttpWebRequest httpWebRequest) {
			if (httpWebRequest is null) {
				throw new ArgumentNullException (nameof (httpWebRequest));
			}
			try {
				return (HttpWebResponse)httpWebRequest.GetResponse ();
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

		static int HexToInt (char value) {
			if (value <= '9') {
				return value - '0';
			}
			return value - 'a' + 10;
		}

	}

}