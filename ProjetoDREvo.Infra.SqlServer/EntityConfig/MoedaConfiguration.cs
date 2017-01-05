using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoDREvoDDD.Domain.Entities;

namespace ProjetoDREvo.Infra.SqlServer.EntityConfig
{
    public class MoedaConfiguration : EntityTypeConfiguration<Moeda>
    {
        public MoedaConfiguration()
        {
            this.HasKey(p => p.IdMoeda);
        }
    }
}
