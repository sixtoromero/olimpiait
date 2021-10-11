using olimpiait.factura.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olimpiait.factura.repository.Interfaces
{
    public interface IFacturaElectronicaRepository
    {
        Response<decimal> RespuestasFacturas(IEnumerable<FacturaElectronicaModel> model);
        Response<FacturaElectronicaModel> ValidacionFacturaElectronica(FacturaElectronicaModel model);
    }
}
