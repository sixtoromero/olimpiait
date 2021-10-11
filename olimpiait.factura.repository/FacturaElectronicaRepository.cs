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

        public Response<decimal> RespuestasFacturas(IEnumerable<FacturaElectronicaModel> facturas)
        {
            Response<decimal> result = new Response<decimal>();
            decimal VlrTotalFactura = 0;
            bool ExisteError = false;
            string Errores = string.Empty;

            try
            {
                helperGeneral = new General();

                if (facturas.ToList().Count == 0 || facturas == null)
                {
                    result.IsSuccess = false;
                    result.Message = "No se han cargado valores a procesar";
                    result.Data = 0;
                    return result;
                }

                //Se itera la colección para validar cada uno de sus registros y calcular el valor de la factura total
                foreach (var item in facturas)
                {
                    //No pueden existir 2 facturas con el mismo Id.
                    var exist = facturas.Where(x => x.Id == item.Id).ToList();
                    if (exist.Count > 1)
                    {
                        ExisteError = true;
                        Errores += "No pueden existir 2 facturas con el mismo Id.\n";
                    }

                    var resp = helperGeneral.EsFacturaValida(item);
                    if (resp.Equals("success"))
                    {
                        VlrTotalFactura = VlrTotalFactura + item.ValorTotal;
                    }
                    else
                    {
                        ExisteError = true;
                        Errores += resp;
                    }
                }

                if (!ExisteError)
                {
                    result.IsSuccess = true;
                    result.Message = "Se ha procesado correctamente el total de todas las facturas";
                    result.Data = VlrTotalFactura;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = Errores;
                    result.Data = 0;
                }
                
                return result;

            }
            catch (Exception ex)
            {
                Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");

                result.IsSuccess = false;
                result.Message = ex.Message;
                result.Data = 0;
                return result;
            }
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
                        result.Message = "La Factura fué validada exitosamente.";
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
