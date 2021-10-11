using olimpiait.multiplo3.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olimpiait.multiplo3.repository.Interfaces
{
    public interface IFileInputRepository
    {
        string fileInputProcess(string pathRead, string pathOut);
    }
}
