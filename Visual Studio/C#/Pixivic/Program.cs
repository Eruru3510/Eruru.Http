using System;
using System.Net;
using System.Windows.Forms;

namespace Pixivic {
	static class Program {
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main () {
			Application.EnableVisualStyles ();
			Application.SetCompatibleTextRenderingDefault (false);
			Application.Run (new Form_Home ());
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
		}
	}
}
