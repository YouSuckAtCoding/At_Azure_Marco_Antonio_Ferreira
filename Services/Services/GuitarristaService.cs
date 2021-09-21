using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services.Repository_Interfaces;

namespace Services.Services
{
    public class GuitarristaService : IGuitarristaService
    {
        private readonly IGuitarristaRepository _guitarristaRepository;
        public GuitarristaService(IGuitarristaRepository guitarristaRepository)
        {
            _guitarristaRepository = guitarristaRepository;
        }
        public async Task<Guitarrista> Create(Guitarrista Guitarristas)
        {
            return await _guitarristaRepository.Create(Guitarristas);
        }

        public async Task Delete(int id)
        {
            await _guitarristaRepository.Delete(id);
        }

        public async Task<Guitarrista> Edit(Guitarrista Guitarristas)
        {
            return await _guitarristaRepository.Edit(Guitarristas);
        }

        public async Task<IEnumerable<Guitarrista>> GetAll()
        {
            return await _guitarristaRepository.GetAll();
        }

        public async Task<Guitarrista> GetById(int id)
        {
            return await _guitarristaRepository.GetById(id);
        }

    }
}
