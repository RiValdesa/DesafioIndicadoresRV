using DesafioIndicadores.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioIndicadores.Mapping
{
    public class tipoIndicadorMap:ClassMap<TipoIndicador>
    {
        public tipoIndicadorMap()
        {
            Table("tipo_indicador");

            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Descripcion);
            
        }
    }
}