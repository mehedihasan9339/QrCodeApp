using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QrCodeApp.Models;
using QrCodeApp.Services;

namespace QrCodeApp.Controllers
{
    [AllowAnonymous]
    public class QrCodeController : Controller
    {
        private readonly QrCodeService _qrCodeService;
        private readonly IWebHostEnvironment _env;
        public QrCodeController(QrCodeService qrCodeService, IWebHostEnvironment env)
        {
            _qrCodeService = qrCodeService;
            _env = env;
        }

        [HttpPost("/api/GenerateQR")]
        public IActionResult GenerateQR([FromBody] QrCodeRequest request)
        {
            try
            {
                // Log the incoming request data
                Console.WriteLine("Generating QR code with the following data:");
                Console.WriteLine($"Type: {request.Type}, Color: {request.Color}, Filename: {request.Filename}");

                var qrCode = _qrCodeService.GenerateQrCode(request);

                // Check if QR code generation returns a valid response
                if (qrCode == null || qrCode.Length == 0)
                {
                    return BadRequest("Failed to generate QR code");
                }

                // Save QR code to the "QR" folder
                var folderPath = Path.Combine(_env.ContentRootPath, "wwwroot\\QR");
                Directory.CreateDirectory(folderPath); // Create the folder if it doesn't exist
                var filePath = Path.Combine(folderPath, request.Filename);

                // Save the QR code as a PNG file
                System.IO.File.WriteAllBytes(filePath, qrCode);

                return File(qrCode, "image/png");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating QR code: " + ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
