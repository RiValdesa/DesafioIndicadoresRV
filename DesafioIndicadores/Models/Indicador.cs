using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioIndicadores.Models
{
    public class Indicador
    {
        public virtual int Id { get; set; }
        public virtual TipoIndicador tipoIndicador { get; set; }
        public virtual DateTime fecha { get; set; }
        public virtual double valor { get; set; }
    }
}