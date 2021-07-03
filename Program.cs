using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        public static JsonSerializerSettings JsonSerializationSettingExport = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
        static async Task Main(string[] args)
        {
            Console.WriteLine("Getting OpenCellId Details");

            var Cell_MMC = "639";
            var Cell_MNC = "07";
            var Cell_LAC = "11101";
            var Cell_ID = "1364";


            OpenCellIdCoordinates _checkOpenCellId = null;
            GeoCoder geoCodeHelper = new GeoCoder();
            if (String.IsNullOrEmpty(Cell_MMC) || String.IsNullOrEmpty(Cell_MNC) || String.IsNullOrEmpty(Cell_LAC) || String.IsNullOrEmpty(Cell_ID))
                _checkOpenCellId = null;
            else
                _checkOpenCellId = await geoCodeHelper.GetOpenCellIdLocation(Cell_MMC, Cell_MNC, Cell_LAC, Cell_ID);

            var cellAttributes = JsonConvert.SerializeObject(_checkOpenCellId, Formatting.None, Program.JsonSerializationSettingExport);

            Console.WriteLine(cellAttributes);

            Console.ReadLine();

        }

    }
}
