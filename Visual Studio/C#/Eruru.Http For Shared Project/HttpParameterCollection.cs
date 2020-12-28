using System;
using System.Collections.Generic;
using System.Text;

namespace Eruru.Http {

	public class HttpParameterCollection : List<HttpParameter> {

		public object this[string name] {

			get => GetOrCreate (name);

			set => GetOrCreate (name, value);

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
				Add (datas[0], datas.Length == 1 ? null : datas[1]);
			}
		}
		public HttpParameterCollection (IEnumerable<HttpParameter> parameters) {
			if (parameters is null) {
				throw new ArgumentNullException (nameof (parameters));
			}
			AddRange (parameters);
		}

		public HttpParameter Add (string name, object value) {
			if (name is null) {
				throw new ArgumentNullException (nameof (name));
			}
			HttpParameter parameter = new HttpParameter (name, value);
			Add (parameter);
			return parameter;
		}

		public HttpParameter Get (string name) {
			if (name is null) {
				throw new ArgumentNullException (nameof (name));
			}
			foreach (HttpParameter parameter in this) {
				if (HttpAPI.Equals (parameter.Name, name)) {
					return parameter;
				}
			}
			return null;
		}

		public override string ToString () {
			StringBuilder stringBuilder = new StringBuilder ();
			for (int i = 0; i < Count; i++) {
				if (i > 0) {
					stringBuilder.Append ('&');
				}
				stringBuilder.Append (base[i].Name);
				if (base[i].Value is null) {
					continue;
				}
				stringBuilder.Append ('=');
				stringBuilder.Append (base[i].Value);
			}
			return stringBuilder.ToString ();
		}

		public static implicit operator string (HttpParameterCollection parameters) {
			return parameters?.ToString () ?? string.Empty;
		}
		public static implicit operator HttpParameterCollection (string text) {
			return new HttpParameterCollection (text);
		}

		HttpParameter GetOrCreate (string name, object value = null) {
			HttpParameter parameter = Get (name);
			if (parameter is null) {
				parameter = new HttpParameter (name, value);
			}
			Add (parameter);
			return parameter;
		}

	}

}