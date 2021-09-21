using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using Models;

using Services.Repository_Interfaces;


namespace Data.Repository
{
    public class GuitarristaRepository : IGuitarristaRepository
    {
        private readonly Context _context;
        public GuitarristaRepository(Context context)
        {
            _context = context;
        }
        public async Task<Guitarrista> Create(Guitarrista Guitarristas)
        {
            var create = _context.Guitarrista.Add(Guitarristas);
            await _context.SaveChangesAsync();
            return (create.Entity);
        }

        public async Task Delete(int id)
        {
            var user = await GetById(id);
            _context.Guitarrista.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Guitarrista> Edit(Guitarrista Guitarristas)
        {
            var create = _context.Guitarrista.Update(Guitarristas);
            await _context.SaveChangesAsync();
            return (create.Entity);
        }

        public async Task<IEnumerable<Guitarrista>> GetAll()
        {
            return await _context.Guitarrista.ToListAsync();
           
        }

        public async Task<Guitarrista> GetById(int id)
        {
            var guitarrista = await _context.Guitarrista
                .OrderBy(x => x.Nome)
                .FirstOrDefaultAsync(m => m.Id == id);

            return guitarrista;
        }
    }
}
