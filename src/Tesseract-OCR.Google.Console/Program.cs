using System;
using System.IO;

namespace Tesseract_OCR.Google.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo file = new FileInfo("files\\test-ocr-text.png");

            string content = AlgorithmOCR.GetContent(file);

            Console.WriteLine("Content: {0}", content);            
        }
    }
}
