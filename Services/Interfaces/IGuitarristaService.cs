using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services.Services
{
    public interface IGuitarristaService
    {
        Task<IEnumerable<Guitarrista>> GetAll();
        Task<Guitarrista> GetById(int id);
        Task<Guitarrista> Create(Guitarrista Guitarristas);
        Task<Guitarrista> Edit(Guitarrista Guitarristas);
        Task Delete(int id);
    }
}
