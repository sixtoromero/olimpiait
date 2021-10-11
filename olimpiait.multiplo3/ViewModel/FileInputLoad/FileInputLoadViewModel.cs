using olimpiait.multiplo3.entity;
using olimpiait.multiplo3.General;
using olimpiait.multiplo3.Model.FileInputLoad.Interfaces;
using olimpiait.multiplo3.ViewModel.FileInputLoad.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace olimpiait.multiplo3.ViewModel.FileInputLoad
{
    public class FileInputLoadViewModel : IFileInputLoadViewModel
    {
        private readonly IFileInputLoadModel fileModel;
        private readonly IMostrarMensajes mostrarMensajes;

        public FileInputLoadViewModel(IFileInputLoadModel fileModel,
            IMostrarMensajes mostrarMensajes)
        {
            this.fileModel = fileModel;
            this.mostrarMensajes = mostrarMensajes;
        }

        public async Task<Response<string>> fileInputProcess(Stream fileStream, string fileName, string nameFile)
        {            
            var result = await fileModel.fileInputProcess(fileStream, fileName, nameFile);
            if (result.IsSuccess)
            {
                await mostrarMensajes.MostrarMensajeExitoso(result.Message);
            }
            else
            {                
                await mostrarMensajes.MostrarMensajeError(result.Message);                
            }

            return result;
        }
    }
}
