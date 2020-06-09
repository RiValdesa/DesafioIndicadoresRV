using DesafioIndicadores.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioIndicadores.Mapping
{
    public class IndicadorMap:ClassMap<Indicador>
    {
        public IndicadorMap()
        {
            Table("indicadores");


            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.fecha);
            Map(x => x.valor);
            References(x => x.tipoIndicador).Column("id_tipo_indicador");
            
        }
    }
}