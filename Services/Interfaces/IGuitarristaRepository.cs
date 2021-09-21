using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository_Interfaces
{
    public interface IGuitarristaRepository
    {
        Task<IEnumerable<Guitarrista>> GetAll();
        Task<Guitarrista> GetById(int id);
        Task<Guitarrista> Create(Guitarrista Guitarristas);
        Task<Guitarrista> Edit(Guitarrista Guitarristas);
        Task Delete(int id);
    }
}
