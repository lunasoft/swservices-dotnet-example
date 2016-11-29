using System;
using System.IO;
using System.Net;
using System.Text;

namespace ExampleRestApiCalling
{
    class Program
    {
        private const string UrlAuth = "http://swservicestest.sw.com.mx/api/Autenticar";
        private const string UrlStamp = "http://swservicestest.sw.com.mx/api/Timbrar/v1";
        private const string UrlCancel = "http://swservicestest.sw.com.mx/api/Cancelar";
        private const string User = "demo";
        private const string Password = "123456789";
        const string TOKEN = ""; //Si se tiene un token infinito colocarlo aqui
        
        public static void Main(string[] args)
        {
            byte[] BYTES;

            string XML = File.ReadAllText("demo.xml");
            BYTES = Encoding.UTF8.GetBytes(XML);
            string CFDI = Convert.ToBase64String(BYTES);

            string stamp = Timbrar(CFDI);

            Console.WriteLine(stamp);

            BYTES = File.ReadAllBytes("CSD01_AAA010101AAA.cer");
            string CER = Convert.ToBase64String(BYTES);

            BYTES = File.ReadAllBytes("CSD01_AAA010101AAA.key");
            string KEY = Convert.ToBase64String(BYTES);
            
            string CertPassword = File.ReadAllText("certpassword.txt");
            string rfc = "AAA010101AAA";
            string uuid = "f16deda0-8d32-4b6b-b905-aab886a12b9d";

            string result = Cancelar(CER, KEY, CertPassword, rfc, uuid);

            Console.WriteLine(result);
            Console.ReadLine();
        }
        private static string GetToken()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlAuth);
            request.Method = "Get";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("user", User);
            request.Headers.Add("password", Password);

            request.ContentLength = 0;
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            return result;
        }

        private static string Timbrar(string CFDI)
        {
            string TOKEN = GetToken(); //Quitar si se tiene token infinito

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlStamp);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("token", TOKEN);
            request.Headers.Add("cfdi", CFDI);
            request.ContentLength = 0;
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            return result;
        }
        private static string Cancelar(string cer, string key, string password, string rfc, string uuid)
        {
            string TOKEN = GetToken(); //Quitar si se tiene token infinito

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlCancel);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("token", TOKEN);
            request.Headers.Add("cer", cer);
            request.Headers.Add("key", key);
            request.Headers.Add("password", password);
            request.Headers.Add("rfc", rfc);
            request.Headers.Add("uuids", uuid);

            request.ContentLength = 0;
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            return result;
        }
    }
}
