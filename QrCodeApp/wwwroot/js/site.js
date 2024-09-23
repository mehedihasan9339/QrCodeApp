//$('#qrForm').on('submit', function (e) {
//    e.preventDefault();

//    const qrTypeValue = $('#qrType').val();
//    const color = $('#color').val();
//    let requestData = { type: qrTypeValue, color: color };

//    // Add data based on selected QR code type
//    if (qrTypeValue === 'url') {
//        requestData.url = $('#url').val();
//    } else if (qrTypeValue === 'vcard') {
//        requestData.name = $('#name').val();
//        requestData.phone = $('#phone').val();
//        requestData.email = $('#email').val();
//        requestData.web = $('#web').val();
//        requestData.address = $('#address').val();
//        requestData.dob = $('#dob').val();
//    } else if (qrTypeValue === 'wifi') {
//        requestData.ssid = $('#ssid').val();
//        requestData.key = $('#key').val();
//    }

//    console.log('Request Data:', requestData); // Log the request data

//    $.ajax({
//        url: '/api/QrCode/Generate',
//        type: 'POST',
//        contentType: 'application/json',
//        data: JSON.stringify(requestData),
//        success: function (blob) {
//            const qrImage = document.getElementById('qrImage');
//            const url = URL.createObjectURL(blob);
//            qrImage.src = url;
//        },
//        error: function (xhr, status, error) {
//            console.error('Error in generating QR code:', error);
//        }
//    });
//});
