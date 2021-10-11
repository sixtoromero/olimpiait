using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using olimpiait.multiplo3.entity;
using olimpiait.multiplo3.repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace olimpiait.multiplo3.api.Controllers.Administrar
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileInputController : ControllerBase
    {

        private readonly IFileInputRepository fileInput;
        private readonly IWebHostEnvironment env;
        private readonly ILogger<FileInputController> logger;

        public FileInputController(IWebHostEnvironment env, ILogger<FileInputController> logger, IFileInputRepository fileInput)
        {
            this.env = env;
            this.logger = logger;
            this.fileInput = fileInput;
        }

        [HttpPost]
        public async Task<IActionResult> fileInputProcess([FromForm] IFormFile File, [FromForm] string nameFile)
        {
            Response<string> response = new Response<string>();
            string fileExt = Path.GetExtension(File.FileName);
            //string nameFile = Guid.NewGuid().ToString() + ".txt";

            try
            {
                if (File != null)
                {
                    if (File.Length > 0)
                    {
                        if (fileExt == ".txt")
                        {
                            string pathDirectory = Path.Combine(env.ContentRootPath, $"Resources\\");
                            //Verificando si la carpeta existe.
                            if (!Directory.Exists(pathDirectory))
                            {
                                Directory.CreateDirectory(pathDirectory);
                            }

                            var uploading = Path.Combine(pathDirectory, nameFile);

                            //var stream = new FileStream(uploading, FileMode.Create);
                            //await File.CopyToAsync(stream);

                            using (var stream = new FileStream(uploading, FileMode.Create))
                            {
                                await File.CopyToAsync(stream);
                            }

                            string pathDirectoryOut = Path.Combine(env.ContentRootPath, $"Resources\\Out\\{nameFile}");

                            var resp = fileInput.fileInputProcess(uploading, pathDirectoryOut);

                            if (resp == "OK")
                            {
                                response.IsSuccess = true;
                                response.Data = nameFile;
                                response.Message = "Archivo procesado exitosamente.";

                                return Ok(response);
                            }else
                            {
                                response.IsSuccess = false;
                                response.Data = null;
                                response.Message = resp;
                                return BadRequest(response);
                            }
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Data = null;
                            response.Message = "Es requerido un documento válido.";
                            return BadRequest(response);
                        }
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Data = null;
                        response.Message = "Es requerido un documento válido.";
                        return BadRequest(response);
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Data = null;
                    response.Message = "Es requerido un documento válido.";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = ex.Message;
                return BadRequest(response);
            }

        }

        [HttpGet]
        public async Task<FileContentResult> GetDownloadFileProcess(string fileName)
        {
            try
            {
                string pathDirectory = Path.Combine(env.ContentRootPath, $"Resources\\Out\\{fileName}");
                var bytes = await System.IO.File.ReadAllBytesAsync(pathDirectory);
                return File(bytes, "application/ocet-stream", Path.GetFileName(pathDirectory));
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
