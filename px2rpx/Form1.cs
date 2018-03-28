using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace px2rpx
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = @"D:\weprojects\MiniShare\source\pages";
            RouteFolder(path);
        }

        private void RouteFolder(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo subfi in di.GetFiles())
            {
                FixedFile(subfi.FullName);
            }

            foreach (DirectoryInfo subdi in di.GetDirectories())
            {
                RouteFolder(subdi.FullName);
            }
        }

        private void FixedFile(string fullname)
        {
           string context= File.ReadAllText(fullname);
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                int pxlocation = CheckHavePx(context);
                if (pxlocation == -1)
                {
                    break;
                }
                    int lastsplace = 0;
                for(var i= pxlocation; i > 0; i--)
                {
                    if(context[i]==' '|| context[i] == ':' || context[i] == '(')
                    {
                        lastsplace = i+1;
                        break;
                    }
                }
                string  cc=context.Substring(lastsplace-5 , 10);
                string cca = context.Substring(lastsplace, 5);
                string tochange = context.Substring(lastsplace, pxlocation - lastsplace);
                decimal number = Convert.ToDecimal(tochange);
                    decimal doublenumber = number * 2;
                //context = context.Replace(number.ToString() + "px", doublenumber.ToString() + "rpx");
                context = context.Substring(0, lastsplace) + doublenumber.ToString() + "rpx" + context.Substring(pxlocation + 2);
;            }
            File.Delete(fullname);
            File.AppendAllText(fullname, context);

        }

        private int CheckHavePx(string context)
        {
            for (int i = 0; i < 10; i++)
            {
                if (context.IndexOf(i.ToString()+"px") > 0)
                {
                    return context.IndexOf(i.ToString() + "px")+1;
                }
            }
            return -1;
        }
    }
}
