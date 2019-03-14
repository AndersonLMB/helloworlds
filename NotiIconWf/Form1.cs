using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotiIconWf
{
    public partial class Form1 : Form
    {
        private NotifyIcon notifyIcon;
        private ContextMenu contextMenu;
        private MenuItem menuItem;
        private IContainer container;

        public Form1()
        {
            InitializeComponent();

            this.container = new Container();
            contextMenu = new ContextMenu();
            menuItem = new MenuItem();



            contextMenu.MenuItems.Add(menuItem);
            menuItem.Index = 0;
            menuItem.Text = "E&xit";


            notifyIcon = new NotifyIcon(this.container);
            notifyIcon.Icon = new Icon("printer.ico");
            notifyIcon.ContextMenu = this.contextMenu;
            notifyIcon.Visible = true;
            var self = this;
            notifyIcon.DoubleClick += new EventHandler((s, e) =>
            {
                //if (this.WindowState == FormWindowState.Minimized)
                //    WindowState = FormWindowState.Normal;
                //Activate();
            });
            Visible = false;



        }





        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //base.OnFormClosing(e);


            switch (e.CloseReason)
            {
                case CloseReason.None:
                    break;
                case CloseReason.WindowsShutDown:
                    break;
                case CloseReason.MdiFormClosing:
                    break;
                case CloseReason.UserClosing:
                    break;
                case CloseReason.TaskManagerClosing:
                    break;
                case CloseReason.FormOwnerClosing:
                    break;
                case CloseReason.ApplicationExitCall:
                    break;
                default:
                    break;
            }


        }





    }
}
