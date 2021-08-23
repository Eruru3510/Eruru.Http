using Eruru.Http;
using Eruru.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Pixivic {

	public partial class Form_Home : Form {

		int Page = 1;
		int ItemWidth = 236;
		int ItemSpacing = 10;
		bool IsLoading;
		int[] ColumnHeights = new int[4];
		List<HomeItem> Items = new List<HomeItem> ();

		public Form_Home () {
			InitializeComponent ();
		}

		private void Form_Home_Load (object sender, EventArgs e) {
			LoadMore ();
		}

		private void Form_Home_Activated (object sender, EventArgs e) {
			Panel.AutoScrollPosition = new Point (0, 100);
		}

		private void Panel_Scroll (object sender, ScrollEventArgs e) {
			OnScroll ();
		}

		private void Panel_MouseWheel (object sender, MouseEventArgs e) {
			OnScroll ();
		}

		void OnScroll () {
			if (IsLoading) {
				return;
			}
			Console.WriteLine (Panel.AutoScrollPosition.Y);
			foreach (var height in ColumnHeights) {
				if (Math.Abs (Panel.AutoScrollPosition.Y) + Panel.Height >= height) {
					IsLoading = true;
					Page++;
					LoadMore ();
					break;
				}
			}
		}

		void LoadMore () {
			HttpRequestInformation httpRequestInformation = new HttpRequestInformation () {
				Url = "https://pix.ipv4.host/ranks",
				QueryStringParameters = {
					{ "page", Page },
					{ "date", DateTime.Now.AddDays (-3).ToString ("yyyy-MM-dd") },
					{ "mode", "day" },
					{ "pageSize", 30 }
				}
			};
			new Thread (() => {
				string response = Global.Http.Request (httpRequestInformation);
				Console.WriteLine (response);
				JsonObject jsonObject = response;
				JsonArray data = jsonObject["data"];
				foreach (var current in data) {
					AddItem (JsonConvert.Deserialize<HomeItem> (current));
				}
				IsLoading = false;
			}) {
				IsBackground = true
			}.Start ();
		}

		void AddItem (HomeItem item) {
			Items.Add (item);
			HomeItemControl homeItem = new HomeItemControl () {
				Item = item,
				Width = ItemWidth,
				Height = (int)((float)ItemWidth / item.Width * item.Height)
			};
			homeItem.RefreshImage ();
			Invoke (new Action (() => {
				Panel.Controls.Add (homeItem);
				int minHeightColumn = ColumnHeights[0];
				int minHeightColumnIndex = 0;
				for (int i = 1; i < ColumnHeights.Length; i++) {
					if (ColumnHeights[i] < minHeightColumn) {
						minHeightColumn = ColumnHeights[i];
						minHeightColumnIndex = i;
					}
				}
				homeItem.Left = ItemWidth * minHeightColumnIndex + ItemSpacing * minHeightColumnIndex;
				homeItem.Top = Panel.AutoScrollPosition.Y + ColumnHeights[minHeightColumnIndex];
				ColumnHeights[minHeightColumnIndex] += homeItem.Height + ItemSpacing;
			}));
		}

	}

}