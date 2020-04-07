using System;
using System.IO;
using System.Web.Http;
using UlalaAPI.Models;

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
            //Enlever l'en-tête du fichier Base64 si présent
            if (IMG.FileAsBase64.Contains(","))
            {
                IMG.FileAsBase64 = IMG.FileAsBase64
                  .Substring(IMG.FileAsBase64
                  .IndexOf(",") + 1);
            }
            //Conversion du fichier Base64 en ByteArray
            IMG.FileAsByteArray = Convert.FromBase64String(IMG.FileAsBase64);
            //Ecriture du fichier à l'emplacement souhaité
            using (var fs = new FileStream(FILE_PATH+IMG.FileName, FileMode.CreateNew))
            {
                fs.Write(IMG.FileAsByteArray, 0,
                         IMG.FileAsByteArray.Length);
            }
            return Ok();
        }
        #endregion
    }
}
