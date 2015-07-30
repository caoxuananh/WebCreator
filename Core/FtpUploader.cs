using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace WPFWebCreator
{
    public static class FtpUploader
    {
        public static string UserName;      // username                
        public static string Password;      // password
        public static string FtpServer;     // server ftp link
        public static string SubFolder;     // sub folder
        public static string Message;       // Message about error

        public static void Init(string str1, string str2, string str3, string str4)
        {
            UserName = str1;
            Password = str2;
            FtpServer = str3;
            SubFolder = str4;

            // fix sub folder name, so it can be more easy to use
            if (SubFolder != "")
                SubFolder = "/" + SubFolder;
            FtpServer = "ftp://" + FtpServer;
        }

        public static bool CreateFolder()
        {
            // try to creato sub folder in server
            try
            {
                // create ftp request
                FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(FtpServer + SubFolder);
                // make folder
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                ftpRequest.Credentials = new NetworkCredential(UserName, Password);

                ftpRequest.KeepAlive = false;
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                // get response from server
                FtpWebResponse resp = (FtpWebResponse)ftpRequest.GetResponse();
                resp.Close();
            }
            catch (WebException ex)
            {
                return false;
                Message = ex.Message;
            }
            return true;
        }
                 
        public static bool DoUpload(string filename)
        {                      
            // start request to server
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                try
                {
                    // connect with ftp server, using username and password that input by user.
                    client.Credentials = new System.Net.NetworkCredential(UserName, Password);
                    // upload all file in home folder                    
                    client.UploadFile(FtpServer + SubFolder + "/" + new FileInfo(filename).Name, "STOR", filename);
                }
                catch (WebException ex)
                {
                    // show message of error
                    Message = ex.Message;
                    return false;
                }
                return true;                
            }
        }

    }
}
