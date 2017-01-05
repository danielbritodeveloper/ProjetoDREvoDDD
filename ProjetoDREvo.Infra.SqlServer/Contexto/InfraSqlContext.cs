using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using ProjetoDREvo.Infra.SqlServer.EntityConfig;
using ProjetoDREvoDDD.Domain.Entities;

namespace Infra_SqlServer.Contexto
{
    public class InfraSqlContext : DbContext
    {

        public InfraSqlContext()
            : base(@"Data Source=.\sqlexpress;Initial Catalog=Teste;User ID=sa; Password=tes145;")

        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Moeda> Moeda { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new MoedaConfiguration());           

            base.OnModelCreating(modelBuilder);
        }

    }
}
