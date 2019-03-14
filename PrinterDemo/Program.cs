using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using System.Threading.Tasks;

using System.Management;

namespace PrinterDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile("C:\\test\\dxt.png");

            var priners = GetPrinterNames();
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Landscape = true;

            pd.PrinterSettings.PrinterName = priners.Where(s => s.ToUpper().Contains("pdf".ToUpper())).First();
          
    
            //pd.DefaultPageSettings.Margins = new Margins(1000, 1000, 1000, 10);


            pd.PrintPage += (o, e) =>
            {

                var pageW = e.PageSettings.PaperSize.Width;
                var pageH = e.PageSettings.PaperSize.Height;




                //var margins = e.PageSettings.Margins;
                //margins.Top = 4000;
                //margins.Bottom = 4000;
                //margins.Left = 4000;
                //margins.Right = 4000;
                //var bound = e.PageBounds;
                //e.Graphics.DrawImage(img,     );

                //e.PageSettings.PrinterSettings.PrinterName;
                //e.Graphics.DrawImage(img,   )

                e.Graphics.DrawImage(img, new Point(0, 0));
            };
            pd.Print();


            //Console.ReadLine();

        }

        static List<string> GetPrinterNames()
        {
            List<string> result = new List<string>();

            string query = "SELECT * from Win32_Printer";

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            using (ManagementObjectCollection collection = searcher.Get())
            {
                foreach (var item in collection)
                {
                    foreach (var property in item.Properties)
                    {

                        Console.WriteLine($"{property.Name} {property.Value}");
                        if (property.Name.ToUpper() == "DEVICEID")
                        {


                            result.Add(property.Value.ToString());
                            ;
                        }
                    }
                }

            }
            return result;
            //throw new NotImplementedException();
        }

        static void GetPrinters()
        {

            string query = "SELECT * from Win32_Printer";

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            using (ManagementObjectCollection collection = searcher.Get())
            {
                foreach (var item in collection)
                {
                    Dictionary<string, object> kvps = new Dictionary<string, object>();
                    foreach (var property in item.Properties)
                    {
                        kvps.Add(property.Name, property.Value);
                    }
                    kvps.OrderBy(kvp => kvp.Key).ToList().ForEach((kvp) =>
                    {
                        Console.WriteLine($"{kvp.Key} : {kvp.Value}");
                    });
                    Console.WriteLine("------");
                }
            }
        }
    }
}
