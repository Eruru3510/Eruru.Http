using Eruru.Http;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Pixivic {

	public partial class HomeItemControl : UserControl {

		public HomeItem Item { get; set; }

		public HomeItemControl () {
			InitializeComponent ();
		}

		private void HomeItem_Load (object sender, EventArgs e) {

		}

		public void RefreshImage () {
			new Thread (() => {
				HttpRequestInformation httpRequestInformation = new HttpRequestInformation () {
					Url = Item.ImageUrls[0].Medium.Replace ("i.pximg.net", "o.acgpic.net"),
					OnRequest = httpWebRequest => {
						httpWebRequest.Referer = $"https://pixivic.com/illusts/{Item.Id}";
					}
				};
				MemoryStream memoryStream = Global.Http.RequestMemoryStream (httpRequestInformation);
				Invoke (new Action (() => {
					try {
						PictureBox.Image = Image.FromStream (memoryStream);
					} catch {

					}
				}));
			}) {
				IsBackground = true
			}.Start ();
		}

	}

}