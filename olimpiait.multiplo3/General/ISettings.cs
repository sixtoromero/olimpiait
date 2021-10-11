using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace olimpiait.multiplo3.General
{
    public interface ISettings
    {
        Task<string> GetApiUrl();
    }

}
