using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using hurricaneapi.Models;
using Microsoft.VisualBasic.FileIO;

namespace hurricaneapi.Services
{
    public class HurricaneService
    {
        
        public void FetchFiles()
        {
            
        }

        public string GetHurricanes()
        {
            return File.ReadAllText("hurricanes.json");
        }
    }
}