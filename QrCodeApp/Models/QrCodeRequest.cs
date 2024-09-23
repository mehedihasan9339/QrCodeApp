namespace QrCodeApp.Models
{
    public class QrCodeRequest
    {
        public string Type { get; set; }  // e.g., url, vcard, wifi
        public string Color { get; set; } // Hex code for the color

        // For URL
        public string Url { get; set; }

        // For vCard
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string Address { get; set; }
        public string Dob { get; set; }

        // For Wi-Fi
        public string Ssid { get; set; }
        public string Key { get; set; }
        public string Filename { get; set; }
    }
}
