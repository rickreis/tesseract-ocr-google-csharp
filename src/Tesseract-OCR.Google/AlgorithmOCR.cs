using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Tesseract_OCR.Google
{
    public static class AlgorithmOCR
    {
        public static string GetContent(FileInfo file)
        {
            if (null == file || file.Exists == false)
            {
                throw new FileNotFoundException("file");
            }

            try
            {
                string process_path = ConfigurationManager.AppSettings["Tesseract"];

                string temp_path = Environment.GetEnvironmentVariable("temp");

                Process ocrProcess = new Process();

                string temp_file = Path.Combine(temp_path, Guid.NewGuid().ToString());

                ocrProcess.StartInfo.UseShellExecute = true;
                ocrProcess.StartInfo.FileName = process_path;
                ocrProcess.StartInfo.CreateNoWindow = false;
                ocrProcess.StartInfo.Arguments = String.Format(@"{0} {1}", file.FullName, temp_file);

                ocrProcess.Start();

                while (File.Exists(temp_file + ".txt") == false)
                    Thread.Sleep(2000);

                if (File.Exists(temp_file + ".txt"))
                {
                    var stream = File.OpenText(temp_file + ".txt");

                    var textFind = stream.ReadToEnd();

                    textFind = textFind.Replace("\n", "").Trim();

                    return textFind;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return String.Empty;
        }
    }
}
