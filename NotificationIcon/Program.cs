using System.Windows.Forms;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Drawing.Printing;
using System.Drawing;
using System.Net.WebSockets;
using System.Net.Sockets;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;
using System.IO;

namespace NotificationIcon
{
    class Program
    {
        static void Main(string[] args)
        {


            //using (FileStream fs = new FileStream(@"C:\test\testdatauri.txt", FileMode.Open))
            //{
            //    var data = new byte[fs.Length];


            //    fs.Read(data, 0, (int)fs.Length);



            //    var ms = new MemoryStream(data);
            //    var image = Image.FromStream(ms, true, true);

            //    ;
            //}

            using (StreamReader sr = new StreamReader(@"C:\test\testdatapure.txt"))
            {
                var img = Image.FromFile(@"C:\test\dxt.png");
                var mms = new MemoryStream();
                img.Save(mms, img.RawFormat);
              

                
                var ddata = mms.GetBuffer();
                var sstr = Encoding.UTF8.GetString(ddata);



                var str = sr.ReadToEnd();
                var data = Encoding.UTF8.GetBytes(str);

                var ms = new MemoryStream(data);


                var image = Image.FromStream(ms);
            }













            // var bmpms = new MemoryStream();
            // bmp.Save(bmpms, bmp.RawFormat);

            // var str = Convert.ToBase64String(ms.ToArray());

            // var newUrl = $"data:image/png;base64,{str}";
            // //==================
            // var bytes = Convert.FromBase64String(str);

            // //var wc = new WebClient();

            // //wc.DownloadData(new Uri("http://localhost:5757/p?s=a&c=2"));
            // //   HttpClient client = new HttpClient();
            // ////   client.BaseAddress = new Uri("http://localhost:5757/p?s=a&c=2");
            // ////;
            ////var req= (HttpWebRequest)WebRequest.Create("http://localhost:5757/p?s=a&c=2");
            //// req.Method = "POST";


            // var nimg = Image.FromStream(new MemoryStream(bytes));





            NotifyIcon notifyIcon = new NotifyIcon(new System.ComponentModel.Container());

            var printerIcon = new System.Drawing.Icon("printer.ico");
            notifyIcon.Icon = printerIcon;
            notifyIcon.Visible = true;
            notifyIcon.Text = "SFS Printer";

            notifyIcon.DoubleClick += new System.EventHandler((sender, e) =>
            {

                MessageBox.Show("sdfa");


                ;
            });
            //notifyIcon.Icon = printerIcon;


            //notifyIcon.Visible = true;
            //notifyIcon.Text = "SFSPrinter";


            //ContextMenuStrip cms = new ContextMenuStrip();

            ////ContextMenu cm = new ContextMenu(new MenuItem[] {
            ////    new MenuItem("A"), new MenuItem("B")
            ////});


            //cms.Items.Add("A");
            //cms.Items.Add("B");
            //notifyIcon.ContextMenuStrip = cms;

            //notifyIcon.BalloonTipText = "SFS Printer";
            //notifyIcon.ShowBalloonTip(5000);


            ////var contextMenu = notifyIcon.ContextMenu;
            ////ContextMenuStrip cms = new ContextMenuStrip();
            ////Form form = new Form();
            ////form.Show();






            var listener = new HttpListener();
            listener.Prefixes.Add(String.Format("http://{1}:{0}/", 5757, "localhost"));
            while (true)
            {
                listener.Start();
                var context = listener.GetContext();
                var request = context.Request;
                var response = context.Response;
                var rawUrl = request.RawUrl;
                response.ContentLength64 = 0;
                response.Close();
                listener.Stop();
            }






            //Console.ReadLine();


            //Console.ReadLine();

        }


    }

}
