//using QRCoder;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Reglas
//{
//    public class GeneradorQR : IGeneradorQR
//    {
//        public string GenerarQR(string texto)
//        {
//            // Crear el generador QR
//            using (var qrGenerator = new QRCodeGenerator())
//            {
//                // Generar los datos del QR con el texto proporcionado
//                var qrCodeData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);

//                // Crear la representación gráfica del QR
//                using (var qrCode = new QRCode(qrCodeData))
//                {
//                    // Generar la imagen del QR como un array de bytes
//                    var qrCodeImage = qrCode.GetGraphic(20);  // 20 es el tamaño de cada módulo del QR

//                    // Convertir la imagen a una cadena Base64
//                    var base64String = Convert.ToBase64String(qrCodeImage);

//                    return base64String; // Retorna la cadena Base64 del QR
//                }
//            }
//        }
//    }
//}
