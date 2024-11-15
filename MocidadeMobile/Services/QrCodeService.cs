using QRCoder;
using System.IO;
using Microsoft.Maui.Graphics;

namespace MocidadeMobile.Services
{
    public class QrCodeService
    {
        public ImageSource GenerateQrCode(string content)
        {
            //using (var qrGenerator = new QRCodeGenerator())
            //{
            //    var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            //    var qrCode = new QRCode(qrCodeData);
            //    using (var qrCodeImage = qrCode.GetGraphic(20))
            //    {
            //        using (var ms = new MemoryStream())
            //        {
            //            qrCodeImage.Save(ms, ImageFormat.Png);
            //            ms.Seek(0, SeekOrigin.Begin);
            //            return ImageSource.FromStream(() => ms);
            //        }
            //    }
            //}

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                byte[] qrCodeImage = qrCode.GetGraphic(20);

                return ImageSource.FromStream(() => new MemoryStream(qrCodeImage));
            }
        }
    }
}
