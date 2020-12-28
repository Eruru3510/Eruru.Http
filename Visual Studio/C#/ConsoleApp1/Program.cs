using System;
using System.Web;
using Eruru.Http;
using Eruru.Json;

namespace ConsoleApp1 {

	class Program {

		static void Main (string[] args) {
			Console.Title = nameof (ConsoleApp1);
			TestPixivic ();
			Console.ReadLine ();
		}

		static void Test () {
			string raw = "1a$中";
			string encodeA = HttpAPI.UrlEncode (raw);
			string encodeB = HttpUtility.UrlEncode (raw);
			string decodeA = HttpAPI.UrlEncode (encodeB);
			string decodeB = HttpUtility.UrlEncode (encodeB);
			Console.WriteLine (encodeA);
			Console.WriteLine (encodeB);
			Console.WriteLine (decodeA);
			Console.WriteLine (decodeB);
		}

		static void TestPixivic () {
			Http http = new Http ();
			string vid;
			HttpRequestInformation captcha = new HttpRequestInformation () {
				Url = "https://pix.ipv4.host/verificationCode"
			};
			JsonObject response = http.Request (captcha);
			vid = response["data"]["vid"];
			Console.WriteLine (response["message"].String);
			HttpRequestInformation login = new HttpRequestInformation () {
				Url = "https://pix.ipv4.host/users/token",
				Type = HttpRequestType.Post,
				QueryStringParameters = new HttpParameterCollection () {
					{ "vid", vid },
					{ "value", "abcd中文" }
				},
				FormData = new JsonObject () {
					{ "username", "a" },
					{ "password", "a" }
				}.ToString (),
				Request = httpWebRequest => {
					httpWebRequest.ContentType = "application/json";
				}
			};
			response = http.Request (login);
			Console.WriteLine (response["message"].String);
		}

	}

}