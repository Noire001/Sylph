using System.Net;

namespace hurricaneapi.Services
{
    public class HurricaneService
    {
        public void FetchFiles()
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile("https://www.ncei.noaa.gov/data/international-best-track-archive-for-climate-stewardship-ibtracs/v04r00/access/csv/ibtracs.ACTIVE.list.v04r00.csv","file.csv");
                
            }
        }
    }
}