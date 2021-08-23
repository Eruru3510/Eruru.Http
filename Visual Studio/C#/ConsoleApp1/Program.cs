using Eruru.Http;
using Eruru.Json;
using System;

namespace ConsoleApp1 {

	class Program {

		static void Main (string[] args) {
			Console.Title = string.Empty;
			Pixivic ();
			Console.ReadLine ();
		}

		static void Pixivic () {
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
				OnRequest = httpWebRequest => {
					httpWebRequest.ContentType = "application/json";
				}
			};
			response = http.Request ("https://pix.ipv4.host/users/token", login);
			Console.WriteLine (response["message"].String);
		}

	}

}