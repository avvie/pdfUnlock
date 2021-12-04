using System;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Security;
using System.IO;

namespace pdfUnlock
{
    class Program
    {
        static void Main(string[] args)
        {
            new UnlockPdf(args).Unlock();
        }
    }

    public class UnlockPdf
    {
        private string pathToFile;
        private string password;
        public UnlockPdf(string[] args)
        {
            if(args.Length == 2)
            {
                pathToFile = args[0];
                password = args[1];
            }
            else
            {
                Console.Out.WriteLine("Wrong arguments");
                Console.Out.WriteLine("Usage: pathToFile passwordForDocument");
            }
        }

        public void Unlock()
        {
            if(!File.Exists(pathToFile))
            {
                Console.Out.WriteLine($"File {pathToFile} doesnt exist");

                return;
            }
            
            PdfDocument document;
            Console.Out.WriteLine($"File exists {File.Exists(pathToFile)}");
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            document = PdfReader.Open(pathToFile, password);
            document.SecuritySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.None;
            document.Save($"{Path.Combine(Path.GetDirectoryName(pathToFile), Path.GetFileNameWithoutExtension(pathToFile))}Unprotected.pdf");
        }
    }
}
