using System;
using Eruru.Http;
using Eruru.Json;

namespace ConsoleApp1 {

	class Program {

		static Http Http = new Http ();
		static string Vid;

		static void Main (string[] args) {
			Console.Title = nameof (ConsoleApp1);
			HttpRequestInformation captcha = new HttpRequestInformation () {
				Url = "https://pix.ipv4.host/verificationCode"
			};
			JsonObject response = Http.Request (captcha);
			Vid = response["data"]["vid"];
			Console.WriteLine (response["message"].String);
			HttpRequestInformation login = new HttpRequestInformation () {
				Url = "https://pix.ipv4.host/users/token",
				Type = HttpRequestType.Post,
				QueryStringParameters = new HttpParameterCollection () {
					{ "vid", Vid },
					{ "value", "abcd" }
				},
				FormData = new JsonObject () {
					{ "username", "a" },
					{ "password", "a" }
				}.ToString (),
				Request = httpWebRequest => {
					httpWebRequest.ContentType = "application/json";
				}
			};
			response = Http.Request (login);
			Console.WriteLine (response["message"].String);
			Console.ReadLine ();
		}

	}

}