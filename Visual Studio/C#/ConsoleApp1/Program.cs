using System;
using Eruru.Http;
using Eruru.Json;

namespace ConsoleApp1 {

	class Program {

		static void Main (string[] args) {
			Console.Title = nameof (ConsoleApp1);
			TestPixivic ();
			Console.ReadLine ();
		}

		static void TestPixivic () {
			Http http = new Http ();
			string vid;
			JsonObject response = http.Request ("https://pix.ipv4.host/verificationCode");
			vid = response["data"]["vid"];
			Console.WriteLine (response["message"].String);
			HttpRequestInformation login = new HttpRequestInformation () {
				Type = HttpRequestType.Post,
				QueryStringParameters = {
					{ "vid", vid },
					{ "value", HttpApi.UrlEncode ("abcd中文") },
					{ "abc", 0 }
				},
				FormData = new JsonObject () {
					{ "username", "a" },
					{ "password", "a" }
				}.ToString (),
				Request = httpWebRequest => {
					httpWebRequest.ContentType = "application/json";
				}
			};
			response = http.Request ("https://pix.ipv4.host/users/token", login);
			Console.WriteLine (response["message"].String);
		}

	}

}