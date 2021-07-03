using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class GeoCoder
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use.
        private static readonly HttpClient s_client = new HttpClient();

        public async Task<OpenCellIdCoordinates> GetOpenCellIdLocation(string mcc, string mnc, string lac, string id)
        {
            var OpenCellIdCoordinates = new OpenCellIdCoordinates();
            string OpenCellIdUrl = "";

            try
            {
                var taskResult = GetOpenCellId_GetJSONAsync(OpenCellIdUrl, Convert.ToInt32(mcc), Convert.ToInt32(mnc), Convert.ToInt32(lac), Convert.ToInt32(id)).ConfigureAwait(false);
                OpenCellIdCoordinates = await taskResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetOpenCellId_GetJSON:url{OpenCellIdUrl} lat:{mcc} lon:{mnc} imei:{lac}, gpsDate:{id}, Exception:{ex}");
                return OpenCellIdCoordinates;
            }

            return OpenCellIdCoordinates;
        }


        public static async Task<OpenCellIdCoordinates> GetOpenCellId_GetJSONAsync(string serviceurl, int mcc, int mnc, int lac, int cellid)
        {
            string CellIdResult = string.Empty;
            OpenCellIdCoordinates CoordinatesResult = new OpenCellIdCoordinates() { };

            Uri address = new Uri($"{serviceurl}?mcc={mcc}&mnc={mnc}&lac={lac}&cellid={cellid}", UriKind.Absolute);
            try
            {
                //Using HTTP Client
                using (HttpResponseMessage response = await s_client.GetAsync(address))
                using (HttpContent content = response.Content)
                {
                    //Read the returned string.
                    string result = await content.ReadAsStringAsync();
                    CoordinatesResult = JsonConvert.DeserializeObject<OpenCellIdCoordinates>(result);
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"GetOpenCellId_GetJSON:url{serviceurl} lat:{mcc} lon:{mnc} imei:{lac}, gpsDate:{DateTime.Now}, Exception:{ex}");
                CoordinatesResult = null;
            }


            return CoordinatesResult;

        }
    }
}
