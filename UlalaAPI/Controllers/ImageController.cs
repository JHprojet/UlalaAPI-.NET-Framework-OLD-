using System;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Web.Http;
using UlalaAPI.Models;
using UlalaAPI.Helper;

namespace UlalaAPI.Controllers
{
    public class ImageController : ApiController
    {
        const string FILE_PATH = @"C:\Users\Les Jujus\Desktop\Projet Ulala\6 - Serveur Images\Images\"; //Emplacement constant de stockage des images

        #region Récupération et mise sur le serveur des images
        /// <summary>
        /// Post API/Image 
        /// </summary>
        /// <param name="IMG">Image a mettre sur le serveur</param>
        public IHttpActionResult Post([FromBody]ImageModel IMG)
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                //Enlever l'en-tête du fichier Base64 si présent
                if (IMG.FileAsBase64.Contains(","))
                {
                    IMG.FileAsBase64 = IMG.FileAsBase64
                      .Substring(IMG.FileAsBase64
                      .IndexOf(",") + 1);
                }
                //Base64 to image
                Image NewImage = ImageHelper.Base64ToImage(IMG.FileAsBase64);

                //Resize
                NewImage = ImageHelper.ResizeImage(NewImage, NewImage.Width / 3, NewImage.Height / 3);

                //Image to Base64
                IMG.FileAsBase64 = ImageHelper.ImageToBase64(NewImage);

                //Conversion du fichier Base64 en ByteArray
                IMG.FileAsByteArray = Convert.FromBase64String(IMG.FileAsBase64);

                //Ecriture du fichier à l'emplacement souhaité
                using (var fs = new FileStream(FILE_PATH + IMG.FileName, FileMode.CreateNew))
                {
                    fs.Write(IMG.FileAsByteArray, 0,
                             IMG.FileAsByteArray.Length);
                }
                return Ok();
            }
            else return Unauthorized();
        }
        #endregion

        
    }
}
