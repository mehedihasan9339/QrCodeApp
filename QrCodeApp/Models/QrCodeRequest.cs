namespace QrCodeApp.Models
{
    public class QrCodeRequest
    {
        public string Type { get; set; }  // e.g., url, vcard, wifi, email, sms, text, event, geolocation, social, login
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

        // For Email
        public string EmailAddr { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }

        // For SMS
        public string SmsNumber { get; set; }
        public string SmsMessage { get; set; }

        // For Text Message
        public string TextMessage { get; set; }

        // For Event
        public string EventTitle { get; set; }
        public string EventDate { get; set; }
        public string EventLocation { get; set; }
        public string EventDescription { get; set; }

        // For Geolocation
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Label { get; set; }

        // For Social Media
        public string SocialUrl { get; set; }

        // For Website Login
        public string LoginUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Filename { get; set; }
    }
}
