using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Eruru.Http {

	public class HttpParameterCollection : IEnumerable<KeyValuePair<string, object>> {

		public object this[string key] {

			get => Parameters[key];

			set => Parameters[key] = value;

		}
		public int Count {

			get => Parameters.Count;

		}

		readonly Dictionary<string, object> Parameters = new Dictionary<string, object> ();

		public HttpParameterCollection () {

		}
		public HttpParameterCollection (string text) {
			string[] pairs = text.Split ('&');
			foreach (string pair in pairs) {
				string[] datas = pair.Split ('=');
				Parameters[datas[0]] = datas.Length == 1 ? null : datas[1];
			}
		}

		public void Add (string key, object value) {
			Parameters[key] = value;
		}

		public override string ToString () {
			StringBuilder stringBuilder = new StringBuilder ();
			int i = 0;
			foreach (var parameter in Parameters) {
				if (i > 0) {
					stringBuilder.Append ('&');
				}
				stringBuilder.Append (parameter.Key);
				if (parameter.Value is null) {
					continue;
				}
				stringBuilder.Append ($"={parameter.Value}");
				i++;
			}
			return stringBuilder.ToString ();
		}

		public static implicit operator string (HttpParameterCollection parameters) {
			return parameters.ToString ();
		}
		public static implicit operator HttpParameterCollection (string text) {
			return new HttpParameterCollection (text);
		}

		#region IEnumerable<KeyValuePair<string, object>>

		public IEnumerator<KeyValuePair<string, object>> GetEnumerator () {
			return Parameters.GetEnumerator ();
		}

		IEnumerator IEnumerable.GetEnumerator () {
			return Parameters.GetEnumerator ();
		}

		#endregion

	}

}