using Eruru.Http;

namespace Pixivic {

	public delegate void Action ();

	class Global {

		public static Http Http = new Http ();
		public static string Vid;

	}

}