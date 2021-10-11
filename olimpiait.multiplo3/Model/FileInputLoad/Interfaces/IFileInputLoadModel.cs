using olimpiait.multiplo3.entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace olimpiait.multiplo3.Model.FileInputLoad.Interfaces
{
    public interface IFileInputLoadModel
    {
        Task<Response<string>> fileInputProcess(Stream fileStream, string fileName, string newFileName);
    }
}
