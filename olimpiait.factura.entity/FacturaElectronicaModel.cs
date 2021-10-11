using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olimpiait.factura.entity
{
    public class FacturaElectronicaModel
    {
        public int Id { get; set; }
        public string Nit { get; set; }
        public string Descripcion { get; set; }
        public decimal ValorTotal { get; set; }
        public int PorcentajeIVA { get; set; }
        public decimal ValorIVA
        {
            get
            {
                return (ValorTotal * PorcentajeIVA) / 100;
            }
        }
        public decimal ValorTotalFactura { get; set; }
        public bool Esvalidacion { get; set; }
    }
}
