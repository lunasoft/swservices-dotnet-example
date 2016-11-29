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

**Obtener Tokenr**
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
*TimbrarV1 Recive el contenido de una factura en string en Base64, si la factura y el token son correctos devuelve el complemento timbre en un string (TFD), en caso contrario lanza una excepción.*

```cs
using System;
using System.IO;
using System.Net;

namespace ExampleRestApiCalling
{
    class Program
    {
        private const string UrlAuth = "http://swservicestest.sw.com.mx/api/Autenticar";
        private const string UrlStamp = "http://swservicestest.sw.com.mx/api/Timbrar/v1";
        private const string User = "demo";
        private const string Password = "123456789";

        //CFDI en Base64
        const string CFDI = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48Y2ZkaTpDb21wcm9iYW50ZSB4c2k6c2NoZW1hTG9jYXRpb249Imh0dHA6Ly93d3cuc2F0LmdvYi5teC9jZmQvMyBodHRwOi8vd3d3LnNhdC5nb2IubXgvc2l0aW9faW50ZXJuZXQvY2ZkLzMvY2ZkdjMyLnhzZCIgdmVyc2lvbj0iMy4yIiBzZXJpZT0iSEZSRUMiIGZvbGlvPSIxMTg0ODEiIEx1Z2FyRXhwZWRpY2lvbj0iTGVvbiwgR3VhbmFqdWF0bywgTWV4aWNvIiBOdW1DdGFQYWdvPSJOTyBJREVOVElGSUNBRE8iIFRpcG9DYW1iaW89IjEuMDAiIE1vbmVkYT0iTVhQIiBGb2xpb0Zpc2NhbE9yaWc9IjEiIGZlY2hhPSIyMDE2LTExLTE0VDE0OjI3OjU3IiBzZWxsbz0iRDJxZjl3RDhYd05OMWxSWExDYTNmSWpJL0JoMkhwcXZwbjZXRnNadjY1dXNZV1BRcFpxZGZlQXZoV1ZKc1FvT3NZVmVOUkxnWUxrMjQ5N3UyaDZhNTh1N1poZTF5YUxFcmxMdC8yQlpJUEFpcGFDMDhnZTJBa1NoMll5cmZIeGNYZEpQbW5Ga1liamgxbDhPb3NNYXVRZEJUd21PNEV1clpzRURwZHF3eUxnPSIgZm9ybWFEZVBhZ289IkNPTlRBIiBub0NlcnRpZmljYWRvPSIyMDAwMTAwMDAwMDIwMDAwMTQyOCIgY2VydGlmaWNhZG89Ik1JSUVZVENDQTBtZ0F3SUJBZ0lVTWpBd01ERXdNREF3TURBeU1EQXdNREUwTWpnd0RRWUpLb1pJaHZjTkFRRUZCUUF3Z2dGY01Sb3dHQVlEVlFRRERCRkJMa011SURJZ1pHVWdjSEoxWldKaGN6RXZNQzBHQTFVRUNnd21VMlZ5ZG1samFXOGdaR1VnUVdSdGFXNXBjM1J5WVdOcHc3TnVJRlJ5YVdKMWRHRnlhV0V4T0RBMkJnTlZCQXNNTDBGa2JXbHVhWE4wY21GamFjT3piaUJrWlNCVFpXZDFjbWxrWVdRZ1pHVWdiR0VnU1c1bWIzSnRZV05wdzdOdU1Ta3dKd1lKS29aSWh2Y05BUWtCRmhwaGMybHpibVYwUUhCeWRXVmlZWE11YzJGMExtZHZZaTV0ZURFbU1DUUdBMVVFQ1F3ZFFYWXVJRWhwWkdGc1oyOGdOemNzSUVOdmJDNGdSM1ZsY25KbGNtOHhEakFNQmdOVkJCRU1CVEEyTXpBd01Rc3dDUVlEVlFRR0V3Sk5XREVaTUJjR0ExVUVDQXdRUkdsemRISnBkRzhnUm1Wa1pYSmhiREVTTUJBR0ExVUVCd3dKUTI5NWIyRmp3NkZ1TVRRd01nWUpLb1pJaHZjTkFRa0NEQ1ZTWlhOd2IyNXpZV0pzWlRvZ1FYSmhZMlZzYVNCSFlXNWtZWEpoSUVKaGRYUnBjM1JoTUI0WERURXpNRFV3TnpFMk1ERXlPVm9YRFRFM01EVXdOekUyTURFeU9Wb3dnZHN4S1RBbkJnTlZCQU1USUVGRFEwVk5JRk5GVWxaSlEwbFBVeUJGVFZCU1JWTkJVa2xCVEVWVElGTkRNU2t3SndZRFZRUXBFeUJCUTBORlRTQlRSVkpXU1VOSlQxTWdSVTFRVWtWVFFWSkpRVXhGVXlCVFF6RXBNQ2NHQTFVRUNoTWdRVU5EUlUwZ1UwVlNWa2xEU1U5VElFVk5VRkpGVTBGU1NVRk1SVk1nVTBNeEpUQWpCZ05WQkMwVEhFRkJRVEF4TURFd01VRkJRU0F2SUVoRlIxUTNOakV3TURNMFV6SXhIakFjQmdOVkJBVVRGU0F2SUVoRlIxUTNOakV3TUROTlJFWk9VMUl3T0RFUk1BOEdBMVVFQ3hNSWNISnZaSFZqZEc4d2daOHdEUVlKS29aSWh2Y05BUUVCQlFBRGdZMEFNSUdKQW9HQkFLUy9iZVVWeTZFM2FPRGFOdUxkMlMzUFhhUXJlMHRHeG1ZVGVVeGE1NXgydC83OTE5dHRnT3BLRjZoUEY1S3ZsWWg0enRxUXFQNHlFVitIakg3eXkvMmQvK2U3dCtKNjFqVHJiZExxVDNXRDArczVmQ0w2Sk9yRjRocXkvL0VHZGZ2WWZ0ZEdSTnJaSCtkQWpXV21sMlMvaHJOOWFVeHJhUzVxcU8xYjdidGxBZ01CQUFHakhUQWJNQXdHQTFVZEV3RUIvd1FDTUFBd0N3WURWUjBQQkFRREFnYkFNQTBHQ1NxR1NJYjNEUUVCQlFVQUE0SUJBUUFDUFhBV1pYMkR1S2laVnYzNVJTMVdGS2dUMnViVU85QytieWZaYXBWNlp6WU5PaUE0S21wa3FIVS9ia1pIcUtqUitSNTlob1loVmRuK0NsVUlsaVpmMkNoSGg4czBhMHZCUk5KM0lIZkExYWtXZHpvY1laTFhqejNtMEVyMzFCWSt1UzNxV1V0UHNPTkdWRHlaTDZJVUJCVWxGb2VjUWhQOUFPMzllcjh6SWJlVTJiME1NQkp4Q3Q0dmJES0Z2VDlpM1YwUHVvbytrbW1rZjE1RDJyQkdSK2RyZDhIOFlnOFRER0ZLZjJ6S21Sc2dUN25JZW91NldwZllwNTcwV0l2TEpRWStmc01wMzM0RDA1VXA1eWtZU0F4VUdhMzBSZFV6QTRyeE41aFQrVzl3aFdWR0Q4OFREMzNOdzU1dU5SVWNSTzNaVVZIbWRXUkcrR2pobGZzRCIgY29uZGljaW9uZXNEZVBhZ289IkNPTlRBIiBzdWJUb3RhbD0iMjkzMC44MiIgdG90YWw9IjI5MzAuODIiIG1ldG9kb0RlUGFnbz0iQ09OVEFETyIgdGlwb0RlQ29tcHJvYmFudGU9ImluZ3Jlc28iIHhtbG5zOmNmZGk9Imh0dHA6Ly93d3cuc2F0LmdvYi5teC9jZmQvMyIgeG1sbnM6eHNpPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYS1pbnN0YW5jZSI+PGNmZGk6RW1pc29yIHJmYz0iQUFBMDEwMTAxQUFBIiBub21icmU9IkVtaXNvck5vbWJyZSI+PGNmZGk6RG9taWNpbGlvRmlzY2FsIGNhbGxlPSJFbWlzb3JDYWxsZSIgbm9FeHRlcmlvcj0iMSIgbm9JbnRlcmlvcj0iMiIgY29sb25pYT0iRW1pc29yQ29sb25pYSIgbG9jYWxpZGFkPSJFbWlzb3JMb2NhbGlkYWQiIHJlZmVyZW5jaWE9IkVtaXNvclJlZmVyZW5jaWEiIG11bmljaXBpbz0iRW1pc29yTXVuaWNpcGlvIiBlc3RhZG89IkVtaXNvckVzdGFkbyIgcGFpcz0iRW1pc29yUGFpcyIgY29kaWdvUG9zdGFsPSI0NTEyMyIgLz48Y2ZkaTpSZWdpbWVuRmlzY2FsIFJlZ2ltZW49IkVtaXNvclJlZ2ltZW5GaXNjYWwiIC8+PC9jZmRpOkVtaXNvcj48Y2ZkaTpSZWNlcHRvciByZmM9IlhBWFgwMTAxMDEwMDAiIG5vbWJyZT0iQUFBIEFBQS4gQUFBIj48Y2ZkaTpEb21pY2lsaW8gY2FsbGU9Ik1FWlFVSVRBTiAjIDYxNSBJTkZPTkFWSVQgUEFSUklMTEEsQ0VMQVlBLEdVQU5BSlVBVE8sIE1FWElDTyIgcGFpcz0iTWV4aWNvIiAvPjwvY2ZkaTpSZWNlcHRvcj48Y2ZkaTpDb25jZXB0b3M+PGNmZGk6Q29uY2VwdG8gY2FudGlkYWQ9IjEiIHVuaWRhZD0iTm8gQXBsaWNhIiBkZXNjcmlwY2lvbj0iTkEiIHZhbG9yVW5pdGFyaW89IjI5MzAuODIiIGltcG9ydGU9IjI5MzAuODIiIC8+PC9jZmRpOkNvbmNlcHRvcz48Y2ZkaTpJbXB1ZXN0b3MgLz48L2NmZGk6Q29tcHJvYmFudGU+";
        
        const string TOKEN = ""; //Si se tiene un token infinito colocarlo aqui
        
        public static void Main(string[] args)
        {
            string result = Timbrar();
            Console.WriteLine(result);
            Console.ReadLine();
        }
        private static string Timbrar()
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
*TimbrarV2 es muy similar a TimbrarV1, con la diferencia de que el resultado en lugar de ser solo el complemento timbre (TFD), es la factura ya timbrada (CFDI mas TFD).*

```cs
using System;
using System.IO;
using System.Net;

namespace ExampleRestApiCalling
{
    class Program
    {
        private const string UrlAuth = "http://swservicestest.sw.com.mx/api/Autenticar";
        private const string UrlStamp = "http://swservicestest.sw.com.mx/api/Timbrar/v2";
        private const string User = "demo";
        private const string Password = "123456789";

        //CFDI en Base64
        const string CFDI = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48Y2ZkaTpDb21wcm9iYW50ZSB4c2k6c2NoZW1hTG9jYXRpb249Imh0dHA6Ly93d3cuc2F0LmdvYi5teC9jZmQvMyBodHRwOi8vd3d3LnNhdC5nb2IubXgvc2l0aW9faW50ZXJuZXQvY2ZkLzMvY2ZkdjMyLnhzZCIgdmVyc2lvbj0iMy4yIiBzZXJpZT0iSEZSRUMiIGZvbGlvPSIxMTg0ODEiIEx1Z2FyRXhwZWRpY2lvbj0iTGVvbiwgR3VhbmFqdWF0bywgTWV4aWNvIiBOdW1DdGFQYWdvPSJOTyBJREVOVElGSUNBRE8iIFRpcG9DYW1iaW89IjEuMDAiIE1vbmVkYT0iTVhQIiBGb2xpb0Zpc2NhbE9yaWc9IjEiIGZlY2hhPSIyMDE2LTExLTE0VDE0OjI3OjU3IiBzZWxsbz0iRDJxZjl3RDhYd05OMWxSWExDYTNmSWpJL0JoMkhwcXZwbjZXRnNadjY1dXNZV1BRcFpxZGZlQXZoV1ZKc1FvT3NZVmVOUkxnWUxrMjQ5N3UyaDZhNTh1N1poZTF5YUxFcmxMdC8yQlpJUEFpcGFDMDhnZTJBa1NoMll5cmZIeGNYZEpQbW5Ga1liamgxbDhPb3NNYXVRZEJUd21PNEV1clpzRURwZHF3eUxnPSIgZm9ybWFEZVBhZ289IkNPTlRBIiBub0NlcnRpZmljYWRvPSIyMDAwMTAwMDAwMDIwMDAwMTQyOCIgY2VydGlmaWNhZG89Ik1JSUVZVENDQTBtZ0F3SUJBZ0lVTWpBd01ERXdNREF3TURBeU1EQXdNREUwTWpnd0RRWUpLb1pJaHZjTkFRRUZCUUF3Z2dGY01Sb3dHQVlEVlFRRERCRkJMa011SURJZ1pHVWdjSEoxWldKaGN6RXZNQzBHQTFVRUNnd21VMlZ5ZG1samFXOGdaR1VnUVdSdGFXNXBjM1J5WVdOcHc3TnVJRlJ5YVdKMWRHRnlhV0V4T0RBMkJnTlZCQXNNTDBGa2JXbHVhWE4wY21GamFjT3piaUJrWlNCVFpXZDFjbWxrWVdRZ1pHVWdiR0VnU1c1bWIzSnRZV05wdzdOdU1Ta3dKd1lKS29aSWh2Y05BUWtCRmhwaGMybHpibVYwUUhCeWRXVmlZWE11YzJGMExtZHZZaTV0ZURFbU1DUUdBMVVFQ1F3ZFFYWXVJRWhwWkdGc1oyOGdOemNzSUVOdmJDNGdSM1ZsY25KbGNtOHhEakFNQmdOVkJCRU1CVEEyTXpBd01Rc3dDUVlEVlFRR0V3Sk5XREVaTUJjR0ExVUVDQXdRUkdsemRISnBkRzhnUm1Wa1pYSmhiREVTTUJBR0ExVUVCd3dKUTI5NWIyRmp3NkZ1TVRRd01nWUpLb1pJaHZjTkFRa0NEQ1ZTWlhOd2IyNXpZV0pzWlRvZ1FYSmhZMlZzYVNCSFlXNWtZWEpoSUVKaGRYUnBjM1JoTUI0WERURXpNRFV3TnpFMk1ERXlPVm9YRFRFM01EVXdOekUyTURFeU9Wb3dnZHN4S1RBbkJnTlZCQU1USUVGRFEwVk5JRk5GVWxaSlEwbFBVeUJGVFZCU1JWTkJVa2xCVEVWVElGTkRNU2t3SndZRFZRUXBFeUJCUTBORlRTQlRSVkpXU1VOSlQxTWdSVTFRVWtWVFFWSkpRVXhGVXlCVFF6RXBNQ2NHQTFVRUNoTWdRVU5EUlUwZ1UwVlNWa2xEU1U5VElFVk5VRkpGVTBGU1NVRk1SVk1nVTBNeEpUQWpCZ05WQkMwVEhFRkJRVEF4TURFd01VRkJRU0F2SUVoRlIxUTNOakV3TURNMFV6SXhIakFjQmdOVkJBVVRGU0F2SUVoRlIxUTNOakV3TUROTlJFWk9VMUl3T0RFUk1BOEdBMVVFQ3hNSWNISnZaSFZqZEc4d2daOHdEUVlKS29aSWh2Y05BUUVCQlFBRGdZMEFNSUdKQW9HQkFLUy9iZVVWeTZFM2FPRGFOdUxkMlMzUFhhUXJlMHRHeG1ZVGVVeGE1NXgydC83OTE5dHRnT3BLRjZoUEY1S3ZsWWg0enRxUXFQNHlFVitIakg3eXkvMmQvK2U3dCtKNjFqVHJiZExxVDNXRDArczVmQ0w2Sk9yRjRocXkvL0VHZGZ2WWZ0ZEdSTnJaSCtkQWpXV21sMlMvaHJOOWFVeHJhUzVxcU8xYjdidGxBZ01CQUFHakhUQWJNQXdHQTFVZEV3RUIvd1FDTUFBd0N3WURWUjBQQkFRREFnYkFNQTBHQ1NxR1NJYjNEUUVCQlFVQUE0SUJBUUFDUFhBV1pYMkR1S2laVnYzNVJTMVdGS2dUMnViVU85QytieWZaYXBWNlp6WU5PaUE0S21wa3FIVS9ia1pIcUtqUitSNTlob1loVmRuK0NsVUlsaVpmMkNoSGg4czBhMHZCUk5KM0lIZkExYWtXZHpvY1laTFhqejNtMEVyMzFCWSt1UzNxV1V0UHNPTkdWRHlaTDZJVUJCVWxGb2VjUWhQOUFPMzllcjh6SWJlVTJiME1NQkp4Q3Q0dmJES0Z2VDlpM1YwUHVvbytrbW1rZjE1RDJyQkdSK2RyZDhIOFlnOFRER0ZLZjJ6S21Sc2dUN25JZW91NldwZllwNTcwV0l2TEpRWStmc01wMzM0RDA1VXA1eWtZU0F4VUdhMzBSZFV6QTRyeE41aFQrVzl3aFdWR0Q4OFREMzNOdzU1dU5SVWNSTzNaVVZIbWRXUkcrR2pobGZzRCIgY29uZGljaW9uZXNEZVBhZ289IkNPTlRBIiBzdWJUb3RhbD0iMjkzMC44MiIgdG90YWw9IjI5MzAuODIiIG1ldG9kb0RlUGFnbz0iQ09OVEFETyIgdGlwb0RlQ29tcHJvYmFudGU9ImluZ3Jlc28iIHhtbG5zOmNmZGk9Imh0dHA6Ly93d3cuc2F0LmdvYi5teC9jZmQvMyIgeG1sbnM6eHNpPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYS1pbnN0YW5jZSI+PGNmZGk6RW1pc29yIHJmYz0iQUFBMDEwMTAxQUFBIiBub21icmU9IkVtaXNvck5vbWJyZSI+PGNmZGk6RG9taWNpbGlvRmlzY2FsIGNhbGxlPSJFbWlzb3JDYWxsZSIgbm9FeHRlcmlvcj0iMSIgbm9JbnRlcmlvcj0iMiIgY29sb25pYT0iRW1pc29yQ29sb25pYSIgbG9jYWxpZGFkPSJFbWlzb3JMb2NhbGlkYWQiIHJlZmVyZW5jaWE9IkVtaXNvclJlZmVyZW5jaWEiIG11bmljaXBpbz0iRW1pc29yTXVuaWNpcGlvIiBlc3RhZG89IkVtaXNvckVzdGFkbyIgcGFpcz0iRW1pc29yUGFpcyIgY29kaWdvUG9zdGFsPSI0NTEyMyIgLz48Y2ZkaTpSZWdpbWVuRmlzY2FsIFJlZ2ltZW49IkVtaXNvclJlZ2ltZW5GaXNjYWwiIC8+PC9jZmRpOkVtaXNvcj48Y2ZkaTpSZWNlcHRvciByZmM9IlhBWFgwMTAxMDEwMDAiIG5vbWJyZT0iQUFBIEFBQS4gQUFBIj48Y2ZkaTpEb21pY2lsaW8gY2FsbGU9Ik1FWlFVSVRBTiAjIDYxNSBJTkZPTkFWSVQgUEFSUklMTEEsQ0VMQVlBLEdVQU5BSlVBVE8sIE1FWElDTyIgcGFpcz0iTWV4aWNvIiAvPjwvY2ZkaTpSZWNlcHRvcj48Y2ZkaTpDb25jZXB0b3M+PGNmZGk6Q29uY2VwdG8gY2FudGlkYWQ9IjEiIHVuaWRhZD0iTm8gQXBsaWNhIiBkZXNjcmlwY2lvbj0iTkEiIHZhbG9yVW5pdGFyaW89IjI5MzAuODIiIGltcG9ydGU9IjI5MzAuODIiIC8+PC9jZmRpOkNvbmNlcHRvcz48Y2ZkaTpJbXB1ZXN0b3MgLz48L2NmZGk6Q29tcHJvYmFudGU+";
        
        const string TOKEN = ""; //Si se tiene un token infinito colocarlo aqui
        
        public static void Main(string[] args)
        {
            string result = Timbrar();
            Console.WriteLine(result);
            Console.ReadLine();
        }
        private static string Timbrar()
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
*Para esta implementaci&oacute;n es necesario mandar los archivos **.CER**, **.KEY** y **PASSWORD** junto con el **UUID** de la factura que se desea cancelar.*
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

        public static void Main(string[] args)
        {
            string result = Cancelar();
            Console.WriteLine(result);
            Console.ReadLine();
        }
        private static string Cancelar()
        {
            string TOKEN = GetToken(); 

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlCancel);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("token", TOKEN);
            
            //.CER en Base64
            request.Headers.Add("cer", "MIIEYTCCA0mgAwIBAgIUMjAwMDEwMDAwMDAyMDAwMDE0MjgwDQYJKoZIhvcNAQEFBQAwggFcMRowGAYDVQQDDBFBLkMuIDIgZGUgcHJ1ZWJhczEvMC0GA1UECgwmU2VydmljaW8gZGUgQWRtaW5pc3RyYWNpw7NuIFRyaWJ1dGFyaWExODA2BgNVBAsML0FkbWluaXN0cmFjacOzbiBkZSBTZWd1cmlkYWQgZGUgbGEgSW5mb3JtYWNpw7NuMSkwJwYJKoZIhvcNAQkBFhphc2lzbmV0QHBydWViYXMuc2F0LmdvYi5teDEmMCQGA1UECQwdQXYuIEhpZGFsZ28gNzcsIENvbC4gR3VlcnJlcm8xDjAMBgNVBBEMBTA2MzAwMQswCQYDVQQGEwJNWDEZMBcGA1UECAwQRGlzdHJpdG8gRmVkZXJhbDESMBAGA1UEBwwJQ295b2Fjw6FuMTQwMgYJKoZIhvcNAQkCDCVSZXNwb25zYWJsZTogQXJhY2VsaSBHYW5kYXJhIEJhdXRpc3RhMB4XDTEzMDUwNzE2MDEyOVoXDTE3MDUwNzE2MDEyOVowgdsxKTAnBgNVBAMTIEFDQ0VNIFNFUlZJQ0lPUyBFTVBSRVNBUklBTEVTIFNDMSkwJwYDVQQpEyBBQ0NFTSBTRVJWSUNJT1MgRU1QUkVTQVJJQUxFUyBTQzEpMCcGA1UEChMgQUNDRU0gU0VSVklDSU9TIEVNUFJFU0FSSUFMRVMgU0MxJTAjBgNVBC0THEFBQTAxMDEwMUFBQSAvIEhFR1Q3NjEwMDM0UzIxHjAcBgNVBAUTFSAvIEhFR1Q3NjEwMDNNREZOU1IwODERMA8GA1UECxMIcHJvZHVjdG8wgZ8wDQYJKoZIhvcNAQEBBQADgY0AMIGJAoGBAKS/beUVy6E3aODaNuLd2S3PXaQre0tGxmYTeUxa55x2t/7919ttgOpKF6hPF5KvlYh4ztqQqP4yEV+HjH7yy/2d/+e7t+J61jTrbdLqT3WD0+s5fCL6JOrF4hqy//EGdfvYftdGRNrZH+dAjWWml2S/hrN9aUxraS5qqO1b7btlAgMBAAGjHTAbMAwGA1UdEwEB/wQCMAAwCwYDVR0PBAQDAgbAMA0GCSqGSIb3DQEBBQUAA4IBAQACPXAWZX2DuKiZVv35RS1WFKgT2ubUO9C+byfZapV6ZzYNOiA4KmpkqHU/bkZHqKjR+R59hoYhVdn+ClUIliZf2ChHh8s0a0vBRNJ3IHfA1akWdzocYZLXjz3m0Er31BY+uS3qWUtPsONGVDyZL6IUBBUlFoecQhP9AO39er8zIbeU2b0MMBJxCt4vbDKFvT9i3V0Puoo+kmmkf15D2rBGR+drd8H8Yg8TDGFKf2zKmRsgT7nIeou6WpfYp570WIvLJQY+fsMp334D05Up5ykYSAxUGa30RdUzA4rxN5hT+W9whWVGD88TD33Nw55uNRUcRO3ZUVHmdWRG+GjhlfsD");
            
            //.KEY en Base64
            request.Headers.Add("key", "MIICxjBABgkqhkiG9w0BBQ0wMzAbBgkqhkiG9w0BBQwwDgQIAgEAAoGBAKQCAgQAMBQGCCqGSIb3DQMHBAgwggJ2AgEAMASCAoD+W0qS7cu6pXUqVr3xMAvfnfTvmdYzPHjXh4IB45m2t66IShzPaQrNzj8qy1BEDaorR/sYMH4yPC+ejIfmP+qLuUCxsNaQSPLc9UYkpMP55LO5f3sq4CKcgoIMKVQchOfvUWzFz16RxjWIkgwZbsd5tHyjYMCMwZZTN05mxdAN+r9HFbGB9XAlElQylf4yWtft4TIBB6xOpiO+lKGXJzycstK+SfFpb7/R4LYaEG8V6ydGfqzDZUe/8oSI2RwO7RNzkWpUblXNuVucP7PcrtQWbq4/0AGwFilTTWTyAnG7xTuDGSuQGH/cs6TEHHP6E6GjO2Qba1VUmC93ZB6ectQC5utCJyQUvwjz3M9lLDuXnNOSwQCf/XUjpH8NFkQgMeoMXtDFsQ0n3J3f987OK3uWXtGMCF8S3Nh4OX0FUnG5zP7+dmS3EDjI4eXUxGmd2igFW8BfdPUbGLsd7K7H6IookHfP/mNsW8IHM7Igfl7EHQ+sbBFN5KHyWQRvLK5swQ8kE3FTY5ka0iR8vKuQ5/D3Zt/IvxY7bEFfhcoPmyN+ZPKcEyqpPUpsgXSFlipsqChrdNQZtLgBVTfs6S4jO6APA5EELQovUNzMGqXd2uQf8lzxypcsRSIwk9fZ0R+ER19DzMIzfr1Dp2FivbyDyrHj4I8gXsDffrW6VOjejv7D5c/rrON8bosOJiXKjXFrrT2GBdTZK/Sm7p6jAGQhlWLtdVmu5WpHMIXeIq1jrEuDM6VsUCMAPUezzvbAW9SZVdUbOnsQMJt7lZxAILOGwkBTFX5w2JsT9Dhbvggbf9d4KRDXGt7ynqVYin9W8+zesckHY2wEQo/TY4DhFkJJ7ap9");
            
            request.Headers.Add("password", "12345678a");
            request.Headers.Add("rfc", "AAA010101AAA");
            request.Headers.Add("uuids", "f16deda0-8d32-4b6b-b905-aab886a12b9d");

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
