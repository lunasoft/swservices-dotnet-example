![NET](http://resources.workable.com/wp-content/uploads/2015/08/Microsoft-dotNET-logo.jpg)

A continuación encontrara los ejemplos para consumir los servicios de SmartWeb; cabe resaltar que todos los ejemplos fueron probados exitosamente con el IDE Visual Studio 2015.

Compatibilidad
-------------

* .Net Framework 4.0 or later 

Dependencias
------------
* [Newtonsoft.Json](http://james.newtonking.com/json)

----------------

Implementaci&oacute;n
---------
#### Aunteticaci&oacute;n #####

**Obtener Token**
```cs
using System;
using System.IO;
using System.Net;

namespace ExampleRestApiCalling
{
    class Program
    {
        private const string UrlAuth = "http://swservicestest.sw.com.mx/api/Autenticar";
        private const string User = "demo";
        private const string Password = "123456789";
        
        public static void Main(string[] args)
        {
            string result = GetToken();
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
    }
}
```
#### Timbrar CFDI V1 #####
**TimbrarV1** Recibe el contenido de una factura en string en **Base64**, si la factura y el token son correctos devuelve el complemento timbre en un string (**TFD**), en caso contrario lanza una excepción.

```cs
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
        private const string User = "demo";
        private const string Password = "123456789";
        const string TOKEN = ""; //Si se tiene un token infinito colocarlo aqui
        
        public static void Main(string[] args)
        {
            string XML = File.ReadAllText("demo.xml");
            byte[] CFDIBYTES = Encoding.UTF8.GetBytes(XML);
            string CFDI = Convert.ToBase64String(CFDIBYTES);
            
            string result = Timbrar(CFDI);
            Console.WriteLine(result);
            Console.ReadLine();
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
    }
}
```
#### Timbrar CFDI V2 #####
**TimbrarV2** es muy similar a **TimbrarV1**, con la diferencia de que el resultado en lugar de ser solo el complemento timbre (**TFD**), es la factura ya timbrada (**CFDI más TFD**).

```cs
using System;
using System.IO;
using System.Net;
using System.Text;

namespace ExampleRestApiCalling
{
    class Program
    {
        private const string UrlAuth = "http://swservicestest.sw.com.mx/api/Autenticar";
        private const string UrlStamp = "http://swservicestest.sw.com.mx/api/Timbrar/v2";
        private const string User = "demo";
        private const string Password = "123456789";
        const string TOKEN = ""; //Si se tiene un token infinito colocarlo aqui
        
        public static void Main(string[] args)
        {
            string XML = File.ReadAllText("demo.xml");
            byte[] CFDIBYTES = Encoding.UTF8.GetBytes(XML);
            string CFDI = Convert.ToBase64String(CFDIBYTES);

            string result = Timbrar(CFDI);
            Console.WriteLine(result);
            Console.ReadLine();
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
    }
}
```   
#### Cancelar CFDI ####
Para esta implementaci&oacute;n es necesario mandar los archivos **CER, KEY y PASSWORD** junto con el **UUID** de la factura que se desea cancelar.

```cs
using System;
using System.IO;
using System.Net;

namespace ExampleRestApiCalling
{
    class Program
    {
        private const string UrlAuth = "http://swservicestest.sw.com.mx/api/Autenticar";
        private const string UrlCancel = "http://swservicestest.sw.com.mx/api/Cancelar";
        private const string User = "demo";
        private const string Password = "123456789";
        const string TOKEN = ""; //Si se tiene un token infinito colocarlo aqui
        
        public static void Main(string[] args)
        {
            byte[] BYTES;

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