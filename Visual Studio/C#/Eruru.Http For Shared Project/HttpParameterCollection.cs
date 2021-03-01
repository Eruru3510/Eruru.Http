using System;
using System.Collections.Generic;
using System.Text;

namespace Eruru.Http {

	public class HttpParameterCollection : Dictionary<string, string> {

		public new object this[string key] {

			get => base[key];

			set => base[key] = value?.ToString ();

		}

		public HttpParameterCollection () {

		}
		public HttpParameterCollection (string text) {
			if (text is null) {
				throw new ArgumentNullException (nameof (text));
			}
			string[] pairs = text.Split ('&');
			foreach (string pair in pairs) {
				string[] datas = pair.Split ('=');
				this[datas[0]] = datas.Length == 1 ? null : datas[1];
			}
		}

		public void Add (string key, object value) {
			if (key is null) {
				throw new ArgumentNullException (nameof (key));
			}
			this[key] = value;
		}

		public override string ToString () {
			StringBuilder stringBuilder = new StringBuilder ();
			int i = 0;
			foreach (KeyValuePair<string, string> parameter in this) {
				if (i > 0) {
					stringBuilder.Append ('&');
				}
				stringBuilder.Append (parameter.Key);
				if (parameter.Value is null) {
					continue;
				}
				stringBuilder.Append ('=');
				stringBuilder.Append (parameter.Value);
				i++;
			}
			return stringBuilder.ToString ();
		}

		public static implicit operator string (HttpParameterCollection parameters) {
			return parameters?.ToString () ?? string.Empty;
		}
		public static implicit operator HttpParameterCollection (string text) {
			return new HttpParameterCollection (text);
		}

	}

}