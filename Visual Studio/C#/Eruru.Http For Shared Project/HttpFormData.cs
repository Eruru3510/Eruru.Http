using System;
using System.IO;
using System.Text;

namespace Eruru.Http {

	public class HttpFormData : IDisposable {

		public Stream Stream { get; }

		public HttpFormData (string text) {
			if (text is null) {
				throw new ArgumentNullException (nameof (text));
			}
			Stream = new MemoryStream (Encoding.UTF8.GetBytes (text));
		}
		public HttpFormData (byte[] bytes) {
			if (bytes is null) {
				throw new ArgumentNullException (nameof (bytes));
			}
			Stream = new MemoryStream (bytes);
		}
		public HttpFormData (Stream stream) {
			Stream = stream ?? throw new ArgumentNullException (nameof (stream));
		}

		public static implicit operator HttpFormData (string text) {
			if (text is null) {
				throw new ArgumentNullException (nameof (text));
			}
			return new HttpFormData (text);
		}
		public static implicit operator HttpFormData (byte[] bytes) {
			if (bytes is null) {
				throw new ArgumentNullException (nameof (bytes));
			}
			return new HttpFormData (bytes);
		}
		public static implicit operator HttpFormData (Stream stream) {
			if (stream is null) {
				throw new ArgumentNullException (nameof (stream));
			}
			return new HttpFormData (stream);
		}

		public void Dispose () {
			Stream?.Dispose ();
		}

	}

}