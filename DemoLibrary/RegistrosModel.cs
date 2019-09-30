using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public class RegistrosModel
    {
        public int id { get; set; }
        public string placa { get; set; }
        public int tipo { get; set; }
        public DateTime data_entrada { get; set; }
        public DateTime data_saida{ get; set; }
        public int total_pagar { get; set; }
        public int impresso { get; set; }
    }
}
