using System.Diagnostics;

namespace Conglomerate.Printing
{
    public class Program
    {
        // This is the main entry point for the application.
        public static void Main(string[] args)
        {
            var program = "C:\\Users\\tryst\\AppData\\Local\\PdfToPrinter\\print.exe";
            var doc = "\"C:\\code\\conglomerate\\Conglomerate\\Conglomerate.Printing\\Documents\\Pdf.pdf\"";
            var printer = "\"Virtual Printer\"";
            var command = $"{doc} {printer}";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = program,
                Arguments = command,
                CreateNoWindow = true,
                ErrorDialog = false,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Normal
            };

            var process = Process.Start(startInfo);
            process.WaitForInputIdle();
            process.CloseMainWindow();
        }
    }
}
