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
            if (!(Regex.IsMatch(model.Id.ToString(), @"^[0-9]+$")))
            {
                return "FORMATO_INVALIDO_ID";
            }

            if (!(Regex.IsMatch(model.Nit.ToString(), @"^[0-9]+$")))
            {
                return "FORMATO_INVALIDO_NIT";
            }


            if (!(model.ValorTotal >= 0))
            {
                return "VALOR_NEGATIVO_TOTAL";
            }


            if (!((model.PorcentajeIVA >= 0) && (model.PorcentajeIVA <= 100)))
            {
                return "IVA_INVALIDO";
            }

            return "success";
        }
    }
}
