﻿using ZXing;
using ZXing.QrCode;
using System.Drawing;
using System.Drawing.Imaging;
using QrCodeApp.Models;

namespace QrCodeApp.Services
{
    public class QrCodeService
    {
        public byte[] GenerateQrCode(QrCodeRequest request)
        {
            try
            {
                string data = GenerateQrCodeData(request);
                return GenerateQrImage(data, request.Color);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GenerateQrCode: " + ex.Message);
                return null;
            }
        }

        private string GenerateQrCodeData(QrCodeRequest request)
        {
            switch (request.Type.ToLower())
            {
                case "url":
                    return request.Url;
                case "vcard":
                    return $"BEGIN:VCARD\n" +
                           $"FN:{request.Name}\n" +
                           $"TEL:{request.Phone}\n" +
                           $"EMAIL:{request.Email}\n" +
                           $"URL:{request.Web}\n" +
                           $"ADR:{request.Address}\n" +
                           $"BDAY:{request.Dob}\n" +
                           $"END:VCARD";
                case "wifi":
                    return $"WIFI:S:{request.Ssid};T:WPA;P:{request.Key};;";
                case "email":
                    return $"mailto:{request.EmailAddr}?subject={request.EmailSubject}&body={request.EmailBody}";
                case "sms":
                    return $"sms:{request.SmsNumber}?body={request.SmsMessage}";
                case "text":
                    return request.TextMessage;
                case "event":
                    return $"BEGIN:VEVENT\n" +
                           $"SUMMARY:{request.EventTitle}\n" +
                           $"DTSTART:{request.EventDate}\n" +
                           $"LOCATION:{request.EventLocation}\n" +
                           $"DESCRIPTION:{request.EventDescription}\n" +
                           $"END:VEVENT";
                case "geolocation":
                    return $"geo:{request.Latitude},{request.Longitude}?q={request.Label}";
                case "social":
                    return request.SocialUrl;
                case "login":
                    return $"Login URL: {request.LoginUrl}, Username: {request.Username}, Password: {request.Password}";
                default:
                    throw new ArgumentException("Invalid QR code type");
            }
        }

        private byte[] GenerateQrImage(string data, string colorHex)
        {
            var qrWriter = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = 300,
                    Width = 300,
                    Margin = 1
                }
            };

            var pixelData = qrWriter.Write(data);
            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb))
            {
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                                 ImageLockMode.WriteOnly,
                                                 PixelFormat.Format32bppRgb);
                try
                {
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }

                Color qrColor = ColorTranslator.FromHtml(colorHex);
                for (int x = 0; x < bitmap.Width; x++)
                {
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        if (bitmap.GetPixel(x, y).R == 0 && bitmap.GetPixel(x, y).G == 0 && bitmap.GetPixel(x, y).B == 0)
                        {
                            bitmap.SetPixel(x, y, qrColor);
                        }
                    }
                }

                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Png);
                    return stream.ToArray();
                }
            }
        }
    }
}
