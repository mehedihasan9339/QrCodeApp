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
                var qrCode = _qrCodeService.GenerateQrCode(request);
                if (qrCode == null || qrCode.Length == 0)
                {
                    return BadRequest("Failed to generate QR code");
                }

                var folderPath = Path.Combine(_env.ContentRootPath, "wwwroot", "QR");
                var filePath = Path.Combine(folderPath, request.Filename);
                System.IO.File.WriteAllBytes(filePath, qrCode);

                return File(qrCode, "image/png");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
