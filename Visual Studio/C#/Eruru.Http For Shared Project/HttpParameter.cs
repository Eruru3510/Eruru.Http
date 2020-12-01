using System;

namespace Eruru.Http {

	public class HttpParameter {

		public string Name { get; set; }
		public string Value { get; set; }

		public HttpParameter (string name) {
			Name = name ?? throw new ArgumentNullException (nameof (name));
		}
		public HttpParameter (string name, object value) {
			if (value is null) {
				throw new ArgumentNullException (nameof (value));
			}
			Name = name ?? throw new ArgumentNullException (nameof (name));
			Value = value.ToString ();
		}

	}

}