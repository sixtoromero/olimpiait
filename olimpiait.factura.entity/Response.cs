using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olimpiait.factura.entity
{
    public class Response<T>
    {
        /// <summary>
        /// Aquí se recibe el resultado de la operación pedida
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Nos indica si fue exitoso o no el proceso
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Se almacena información si fue exitoso o no la transacción
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Define el estado de la petición realizada con la API
        /// OK = 200
        /// BadRequest = 400
        /// Unauthorized = 401
        /// InternalServerError = 500
        /// </summary>
        public int ReponseCode { get; set; }
    }
}
