using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoDREvoDDD.Domain.Entities;
using ProjetoDREvoDDD.Domain.Interfaces.Repositories;
using ProjetoDREvoDDD.Domain.Interfaces.Services;

namespace ProjetoDREvoDDD.Domain.Services
{
    public class MoedaService : ServiceBase<Moeda>, IMoedaService
    {
        private readonly IMoedaRepository _rep;

        public MoedaService(IMoedaRepository rep)
            : base(rep)
        {
            this._rep = rep;
        }

        void IMoedaService.Update(Moeda item)
        {
            if (this._rep.Get(i => i.Nome == item.Nome.TrimStart().TrimEnd() &&
                                   i.IdMoeda != item.IdMoeda).Count() > 0)
                throw new Exception("Nome já existe cadastrado!");
            else
                this._rep.Update(item);
        }
    }
}
