using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioIndicadores.Models
{
    public class FluctuacionDiaria
    {
        public string Fecha { get; set; }
        public string Uf { get; set; }
        public string Ivp { get; set; }
        public string Dolar { get; set; }
        public string Euro { get; set; }
        public string Tpm { get; set; }
        public string Libra_cobre { get; set; }
    }
}