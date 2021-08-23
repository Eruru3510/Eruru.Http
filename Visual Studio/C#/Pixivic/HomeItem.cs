using System.Collections.Generic;

namespace Pixivic {

	public class HomeItem {

		public int Id;
		public string Caption;
		public int Width;
		public int Height;
		public List<ImageUrl> ImageUrls;

		public class ImageUrl {

			public string Large, Medium, Original, SquareMedium;

		}

	}

}