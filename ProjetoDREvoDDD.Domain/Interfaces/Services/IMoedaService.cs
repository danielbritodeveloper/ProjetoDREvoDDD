using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoDREvoDDD.Domain.Entities;

namespace ProjetoDREvoDDD.Domain.Interfaces.Services
{
    public interface IMoedaService : IServiceBase<Moeda>
    {
        void Update(Moeda item);
    }
}
