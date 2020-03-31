using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace StudentActivity.Controllers.EnglishControllers
{
    public class ArabicCertificateController : Controller
    {
        // GET: Certificate
        public ActionResult Index(string language)
        {
            ChangeLanguageFunction(language);
            return View();
        }

        public ActionResult WaterMark(string student_name, string title, string lecturer_name, string date_time, string language)
        {
            ChangeLanguageFunction(language);

            System.Drawing.Image bitmap = (System.Drawing.Image)Bitmap.FromFile(Server.MapPath("/Images/Certificate.png"));
            string sValue = student_name;
            string tValue = title;
            string lValue = lecturer_name;
            string dValue = date_time;
            string file = "Certificate" + ".png";
            //using (Bitmap bitmap = new Bitmap(File.InputStream, false))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    if (student_name != null)
                    {
                        Brush brush = new SolidBrush(Color.Orange);

                        Font font = new Font("Arial", 60, FontStyle.Italic, GraphicsUnit.Pixel);

                        SizeF textSize = new SizeF();
                        textSize = graphics.MeasureString(sValue, font);

                        Point position = new Point(850, 400);

                        // Set format of string.
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;

                        graphics.DrawString(sValue, font, brush, position, drawFormat);
                    }

                    if (title != null)
                    {
                        Brush brush = new SolidBrush(Color.Black);

                        Font font = new Font("Arial", 45, FontStyle.Italic, GraphicsUnit.Pixel);

                        SizeF textSize = new SizeF();
                        textSize = graphics.MeasureString(tValue, font);

                        Point position = new Point(850, 500);

                        // Set format of string.
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;

                        graphics.DrawString(tValue, font, brush, position, drawFormat);
                    }

                    if (lecturer_name != null)
                    {
                        Brush brush = new SolidBrush(Color.Black);

                        Font font = new Font("Arial", 45, FontStyle.Italic, GraphicsUnit.Pixel);

                        SizeF textSize = new SizeF();
                        textSize = graphics.MeasureString(lValue, font);

                        Point position = new Point(850, 600);

                        // Set format of string.
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;

                        graphics.DrawString(lValue, font, brush, position, drawFormat);
                    }

                    if (lecturer_name != null)
                    {
                        Brush brush = new SolidBrush(Color.Black);

                        Font font = new Font("Arial", 45, FontStyle.Italic, GraphicsUnit.Pixel);

                        SizeF textSize = new SizeF();
                        textSize = graphics.MeasureString(dValue, font);

                        Point position = new Point(850, 700);

                        // Set format of string.
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;

                        graphics.DrawString(dValue, font, brush, position, drawFormat);
                    }

                    using (MemoryStream mStream = new MemoryStream())
                    {
                        bitmap.Save(mStream, ImageFormat.Png);
                        mStream.Position = 0;
                        return File(mStream.ToArray(), "image/png", file);
                    }
                }
            }
        }

        public void ChangeLanguageFunction(string language)
        {
            if (!string.IsNullOrEmpty(language))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

                HttpCookie cookie = new HttpCookie("Languages");
                cookie.Value = language;
                Response.Cookies.Add(cookie);
            }

        }
    }
}