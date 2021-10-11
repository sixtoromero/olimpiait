using olimpiait.multiplo3.entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace olimpiait.multiplo3.ViewModel.FileInputLoad.Interfaces
{
    public interface IFileInputLoadViewModel
    {
        Task<Response<string>> fileInputProcess(Stream fileStream, string fileName, string nameFile);
    }
}
