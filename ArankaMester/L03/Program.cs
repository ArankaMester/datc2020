using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace L03
{
    class Program
    {
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "GOOGLE Drive API ";
        static DriveService service;
        static void Main(string[] args)
        {
            Initialize();
        }

        static void Initialize(){
           UserCredential credential;

            using (var stream =
                new FileStream("client.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token";

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    Environment.UserName,
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
           
        } static void GetALLFiles(){
              //  var request= (HttpWebRequest) WebRequest.Create("GET https://www.googleapis.com/drive/v3/files?q='root'%20in%20parents");
            }
    }
}
