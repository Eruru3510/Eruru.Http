using Eruru.Http;
using Eruru.Json;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Pixivic {

	public partial class Form_Login : Form {

		public Form_Login () {
			InitializeComponent ();
		}

		private void Form1_Load (object sender, EventArgs e) {
			RefreshCaptcha ();
		}

		private void PictureBox_Captcha_Click (object sender, EventArgs e) {
			RefreshCaptcha ();
		}

		private void TextBox_Captcha_KeyDown (object sender, KeyEventArgs e) {
			if (!e.Control && e.KeyCode == Keys.Enter) {
				e.Handled = true;
				Login ();
			}
		}

		private void Button_Login_Click (object sender, EventArgs e) {
			Login ();
		}

		void RefreshCaptcha () {
			TextBox_Captcha.Clear ();
			new Thread (() => {
				string response = Global.Http.Request ("https://pix.ipv4.host/verificationCode");
				JsonObject jsonObject = response;
				JsonObject data = jsonObject["data"];
				Global.Vid = data["vid"];
				Invoke (new Action (() => {
					PictureBox_Captcha.Image = new Bitmap (new MemoryStream (Convert.FromBase64String (data["imageBase64"])));
				}));
			}) {
				IsBackground = true
			}.Start ();
		}

		void Login () {
			HttpRequestInformation httpRequestInformation = new HttpRequestInformation () {
				Url = "https://pix.ipv4.host/users/token",
				Type = HttpRequestType.Post,
				QueryStringParameters = {
					{ "vid", Global.Vid },
					{ "value", TextBox_Captcha.Text }
				},
				FormData = new JsonObject () {
					{ "username", TextBox_Account.Text },
					{ "password", TextBox_Password.Text }
				}.ToString (),
				OnRequest = httpWebRequest => {
					httpWebRequest.ContentType = "application/json";
				}
			};
			new Thread (() => {
				string response = Global.Http.Request (httpRequestInformation);
				JsonObject jsonObject = response;
				if (jsonObject.ContainsKey ("data")) {
					return;
				}
				MessageBox.Show (jsonObject["message"]);
				RefreshCaptcha ();
			}) {
				IsBackground = true
			}.Start ();
		}

	}

}