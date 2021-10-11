using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace olimpiait.multiplo3.General
{
    public interface IMostrarMensajes
    {
        Task<bool> MostrarMensajeConfirmacion(string titulo, string Mensaje, string Icono);
        Task MostrarMensajeError(string mensaje);
        Task MostrarMensajeInfo(string mensaje);
        Task MostrarMensajeAdvertencia(string mensaje);
        Task MostrarMensajeExitoso(string mensaje);
        Task<string> MostrarMensajeTextoConfirmacion(string titulo, string Mensaje, string Value);
        Task MostrarToast(string icon, string mensaje);
    }
}
