using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using olimpiait.factura.entity;
using olimpiait.factura.repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace olimpiait.factura.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FacturaElectronicaController : ControllerBase
    {
        private readonly IFacturaElectronicaRepository facturaElectronica;

        public FacturaElectronicaController(IFacturaElectronicaRepository facturaElectronica)
        {
            this.facturaElectronica = facturaElectronica;
        }

        [HttpPost]
        public IActionResult RespuestasFacturas(IEnumerable<FacturaElectronicaModel> facturas)
        {
            Response<decimal> resp = new Response<decimal>();
            try
            {
                resp = facturaElectronica.RespuestasFacturas(facturas);
                if (resp.IsSuccess)
                {
                    return Ok(resp);
                }
                else
                {
                    return BadRequest(resp);
                }
            }
            catch (Exception ex)
            {
                resp.IsSuccess = false;
                resp.Message = ex.Message;
                resp.Data = 0;

                return BadRequest(resp);
            }
        }

        [HttpPost]
        public IActionResult ValidacionFactura(FacturaElectronicaModel model)
        {
            Response<FacturaElectronicaModel> resp = new Response<FacturaElectronicaModel>();

            try
            {
                resp = facturaElectronica.ValidacionFacturaElectronica(model);
                if (resp.IsSuccess)
                {
                    return Ok(resp);
                }
                else
                {
                    return BadRequest(resp);
                }
            }
            catch (Exception ex)
            {
                resp.IsSuccess = false;
                resp.Message = ex.Message;
                resp.Data = null;

                return BadRequest(resp);
            }
        }

    }
}
