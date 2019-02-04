using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace NasaApiGateway
{
    public class NasaApodConsumer
    {
        private string _apodApiBaseUrl = "https://api.nasa.gov/planetary/apod";
        private string _keyString = "dF5CeG0SSA6NzgTvMTooI9PycMK6qlbtbYBaczcw";
        public void RetrieveImageContent(DateTime date, out Apod apod, out byte[] image)
        {
            RetrieveImageToken(date, out apod);

           WebClient webClient = new WebClient();
           byte[] imageData = webClient.DownloadData(apod.url);

            image = imageData;
        }

        public void RetrieveImageToken(DateTime date, out Apod apod)
        {
            WebClient webClient = new WebClient();
            string dateString = date.ToString("yyyy-MM-dd");
            string url = String.Format("{0}?date={1}&api_key={2}", _apodApiBaseUrl, dateString, _keyString);
            byte[] data = webClient.DownloadData(url);

            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Apod));
            apod = (Apod)serializer.ReadObject(stream);
        }
    }
}
