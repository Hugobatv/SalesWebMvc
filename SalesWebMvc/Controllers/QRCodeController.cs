using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QRCoder;
using SalesWebMvc.Models;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SalesWebMvc.Controllers
{
    public class QRCodeController : Controller
    {
        public IActionResult Index()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Index(string Text)
        {
             
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(Text, QRCodeGenerator.ECCLevel.Q);
                QRCode qRCode = new QRCode(qRCodeData);
                using (Bitmap bitMap = qRCode.GetGraphic(20))
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCode = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult QRDetails()
        {
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();

            var conteudo = new Seller()
            {
                Name = "Hugo batista",
                Email = "hbatista@hotmail.com"

            };

            var json = JsonConvert.SerializeObject(conteudo);
            QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(json, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            var bitmapBytes = BitmapToBytes(qrCodeImage);

            return File(bitmapBytes, "image/jpeg");
        }

        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                return stream.ToArray();
            }
        }



    }
}

