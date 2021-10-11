using Microsoft.Extensions.Logging;
using olimpiait.multiplo3.entity;
using olimpiait.multiplo3.General;
using olimpiait.multiplo3.Model.FileInputLoad.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace olimpiait.multiplo3.Model.FileInputLoad
{
    public class FileInputLoadModel : IFileInputLoadModel
    {
        private readonly ISettings settings;
        private readonly IConexionRest conexionRest;
        private readonly ILogger<string> logger;

        public FileInputLoadModel(ISettings settings, IConexionRest conexionRest, ILogger<string> logger)
        {
            this.settings = settings;
            this.conexionRest = conexionRest;
            this.logger = logger;
        }


        public async Task<Response<string>> fileInputProcess(Stream fileStream, string fileName, string newFileName)
        {
            Response<string> result = new Response<string>();

            try
            {
                var ApiUrl = await settings.GetApiUrl();                

                var formData = new MultipartFormDataContent();
                formData.Add(new StreamContent(fileStream, (int)fileStream.Length), "File", fileName);
                formData.Add(new StringContent(newFileName), "nameFile");

                var httpResponse = await conexionRest.PostFormData<Response<string>>($"{ApiUrl}/FileInput/fileInputProcess", formData);

                if (!httpResponse.Error)
                {
                    result.IsSuccess = true;
                    result.Message = "Archivo cargado correctamente.";                    

                    result.Data = $"{ApiUrl}/FileInput/GetDownloadFileProcess?fileName={newFileName}";
                }
                else
                {
                    logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}");

                    result.IsSuccess = false;
                    result.Message = "Ha ocurrido un error inesperado.";
                    result.Data = null;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                result.Data = null;                
            }

            return result;
        }

    }
}
