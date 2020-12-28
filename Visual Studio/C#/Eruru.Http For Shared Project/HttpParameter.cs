using System;

namespace Eruru.Http {

	public class HttpParameter {

		public string Name { get; set; }
		public string Value { get; set; }

		public HttpParameter (string name) {
			Name = name ?? throw new ArgumentNullException (nameof (name));
		}
		public HttpParameter (string name, object value) {
			Name = name ?? throw new ArgumentNullException (nameof (name));
			if (value is null) {
				return;
			}
			Value = HttpAPI.UrlEncode (HttpAPI.UrlDecode (value.ToString ()));
		}

	}

}