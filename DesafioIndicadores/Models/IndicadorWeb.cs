using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioIndicadores.Models
{
    public class IndicadorWeb
    {
        public IndicadorWeb()
        {
            this.indicadores = new List<Indicador>();
            this.tipo = new TipoIndicador();
        }
        public IndicadorWeb(List<Indicador> _indicadores, TipoIndicador _tipo)
        {
            this.indicadores = _indicadores;
            this.tipo = _tipo;
        }
        public List<Indicador> indicadores { get; set; }
        public TipoIndicador tipo { get; set; }
    }
}