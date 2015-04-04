using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace TResizeImages
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string duoi = ".png";
        private void button1_Click(object sender, EventArgs e)
        {
            string ofile = textBox1.Text;
            string osize = textBox2.Text;

            string[] sizes = osize.Split('|');
            Image oimgae = Image.FromFile(ofile);

            if (ofile.IndexOf(".png") > 0) duoi = ".png";
            else if (ofile.IndexOf(".jpg") > 0) duoi = ".jpg";
             string realsfilenamr ;
            if(duoi == ".png") realsfilenamr = ofile.Remove(ofile.IndexOf(".png"),4);
            else realsfilenamr = ofile.Remove(ofile.IndexOf(".jpg"), 4);
            
            for (int i = 0; i < sizes.Length; i++)
            {
                string[] sizes___ = sizes[i].Split('x');
                Image iimgae;

                if(checkBox1.Checked == false)
                    iimgae = resizeImage(oimgae, new Size(Int32.Parse(sizes___[0]), Int32.Parse(sizes___[1])));
                else
                    iimgae = resizeImage_contraint(oimgae, new Size(Int32.Parse(sizes___[0]), Int32.Parse(sizes___[1])), oimgae.Size);

                if (duoi == ".png") iimgae.Save(realsfilenamr + "_" + sizes[i] + duoi, ImageFormat.Png);
                else if (duoi == ".jpg") iimgae.Save(realsfilenamr + "_" + sizes[i] + duoi,ImageFormat.Jpeg);
            }
           
        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
           // if(checkbo
            return (Image)(new Bitmap(imgToResize, size));
        }
        public static Image resizeImage_contraint(Image imgToResize, Size destsize,Size first_size)
        {
            // if(checkbo
            //return (Image)(new Bitmap(imgToResize, size));
            Size size_temp = new Size();
            float ratio = destsize.Width*1.0f / destsize.Height;
            float ratio2 = first_size.Width * 1.0f / first_size.Height;
            if (ratio2 > ratio)
            {
                size_temp.Height = first_size.Height;
                size_temp.Width = (int)(size_temp.Height * ratio);
                
            }
            else if (ratio2 < ratio)
            {
                size_temp.Width = first_size.Width;
                size_temp.Height = (int)(size_temp.Width / ratio);
            }
            else
            {
                size_temp = first_size;
            }

            int x = (int)(first_size.Width/2 - size_temp.Width/2);
            int y = (int)(first_size.Height/2 - size_temp.Height/2);
            
            Rectangle r = new Rectangle(x,y,size_temp.Width,size_temp.Height);
            Bitmap bitmp0 = new Bitmap(imgToResize);
            Bitmap croppedImage = bitmp0.Clone(r, bitmp0.PixelFormat);
            Image temp_iamge = (Image)croppedImage;

            return resizeImage(temp_iamge, destsize);
        }
      
    }
}
