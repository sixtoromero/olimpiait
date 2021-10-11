using olimpiait.factura.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace olimpiait.factura.transversal.Helpers
{
    public class General
    {
        public string EsFacturaValida(FacturaElectronicaModel model)
        {
            if (model.Id < 0)
            {
                return "El Id de la factura no tiene un valor entero válido.\n";
            }

            if (!(Regex.IsMatch(model.Nit.ToString(), @"^[0-9]+$")))
            {
                return "El Nit de la factura no tiene un formato correcto.\n";
            }


            if (model.ValorTotal < 0)
            {
                return "El valor de la factura nunca debe ser negativo.\n";
            }
            
            if (model.PorcentajeIVA < 0 || model.PorcentajeIVA > 100)
            {
                return "El Iva de la factura debe estar entre 0(%) y 100(%)\n";
            }

            return "success";
        }
    }
}
