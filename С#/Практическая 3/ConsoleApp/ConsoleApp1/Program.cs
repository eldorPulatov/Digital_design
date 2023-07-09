using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        string url = "https://localhost:7190/api/Text";

        string inputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt");
        string text = File.ReadAllText(inputFile);

        Console.WriteLine(text);

        //    ServicePointManager.ServerCertificateValidationCallback += ValidateCertificate;

        //    using (HttpClient client = new HttpClient())
        //    {
        //        var response = client.PostAsJsonAsync(url, text).Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = response.Content.ReadAsStringAsync().Result;
        //            Console.WriteLine(result);
        //        }
        //        else
        //        {
        //            Console.WriteLine("Error: " + response.StatusCode);
        //        }
        //    }
        //}

        //private static bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        //{
        //    // верификации, которая всегда принимает сертификаты
        //    return true;
        //}
    }
}
