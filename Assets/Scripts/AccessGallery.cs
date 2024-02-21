using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using System.Collections;

namespace AccessGallery
{
    class ConnectGSheets
    {
        static readonly string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static readonly string ApplicationName = "GSheets to Unity Accessor";

        public static Gallery Main()
        {
            GoogleCredential credential;

            string jsonCredentials = SingletonGallery.Instance.GetJSONCredentials();
            credential = GoogleCredential.FromJson(jsonCredentials).CreateScoped(Scopes);
            Debug.Log(credential.ToString());

            /*using (var stream =
                new FileStream(Application.streamingAssetsPath + "/credentials.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
                Console.WriteLine("Credential saved" + credential.ToString());
            }*/

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = "1VEIONwFJ0TQzdLZX41bddhHmM1eNxbRyCiBP2KaYNZA";
            String range = "Form Responses 1!A2:I";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            ValueRange response = request.Execute();
            IList<IList<object>> values = response.Values;
            Gallery g = new Gallery
            {
                Artworks = new List<Piece>()
            };
            if (values != null && values.Count > 0)
            {
                Console.WriteLine("Artwork Name");
                foreach (var row in values)
                {
                    Piece art = new Piece
                    {
                        Valid = ((string)row[0]).Equals("TRUE"),
                        ID = (string)row[1],
                        ArtistName = (string)row[3],
                        ArtworkName = (string)row[4],
                        Description = (string)row[5],
                        MediaFormat = (string)row[6],
                        Tags = ((string)row[7]).Replace(" ", string.Empty).Split(','),
                        UploadArtwork = (string)row[8],
                        Art = null
                    };
                    g.Artworks.Add(art);
                }
                /*string JSONresult = JsonConvert.SerializeObject(g);
                string path = "Assets/Artwork/gallery.json";
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(JSONresult.ToString());
                    tw.Close();
                }*/
                return g;
            }
            else
            {
                Console.WriteLine("No data found.");
                return null;
            }
        }
    }
}