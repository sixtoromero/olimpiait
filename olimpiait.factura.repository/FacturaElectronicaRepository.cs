using Microsoft.Extensions.Logging;
using olimpiait.factura.entity;
using olimpiait.factura.repository.Interfaces;
using olimpiait.factura.transversal.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace olimpiait.factura.repository
{
    public class FacturaElectronicaRepository : IFacturaElectronicaRepository
    {
        private readonly ILogger<FacturaElectronicaRepository> Logger;
        General helperGeneral;

        public FacturaElectronicaRepository(ILogger<FacturaElectronicaRepository> Logger)
        {
            this.Logger = Logger;
        }

        public Response<FacturaElectronicaModel> ValidacionFacturaElectronica(FacturaElectronicaModel model)
        {
            Response<FacturaElectronicaModel> result = new Response<FacturaElectronicaModel>();

            try
            {                
                if (model == null)
                {
                    result.IsSuccess = false;
                    result.Message = "No se han cargado valores a procesar";
                    result.Data = null;
                    return result;
                }

                if (model.Esvalidacion)
                {
                    helperGeneral = new General();
                    var resp = helperGeneral.EsFacturaValida(model);
                    if (resp.Equals("success"))
                    {
                        result.IsSuccess = true;
                        result.Message = "FACTURA_VALIDA";
                        result.Data = model;
                        return result;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = resp;
                        result.Data = null;
                        return result;
                    }

                }
                else
                {
                    result.IsSuccess = true;
                    result.Message = string.Empty;
                    result.Data = model;

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");

                result.IsSuccess = false;
                result.Message = ex.Message;
                result.Data = null;
                return result;

            }
            
        }
    }

}
