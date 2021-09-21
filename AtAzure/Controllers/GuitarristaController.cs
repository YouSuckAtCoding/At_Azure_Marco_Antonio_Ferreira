using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using Services.Services;


namespace AtAzure.Controllers
{
    public class GuitarristaController : Controller
    {
        private readonly IGuitarristaService _guitarristaService;
        private readonly IBlobService _blobservice;
        private readonly IConfiguration _configuration;
        private readonly IQueueService _queue;
        
        public GuitarristaController(Context context, IBlobService blobservice, IConfiguration configuration, IQueueService queue, IGuitarristaService guitarristaService)
        {
            _guitarristaService = guitarristaService;
            _blobservice = blobservice;
            _configuration = configuration;
            _queue = queue;

        }

        // GET: Guitarrista
        public async Task<IActionResult> Index()
        {
            return View(await _guitarristaService.GetAll());
        }

        // GET: Guitarrista/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guitarrista = await _guitarristaService.GetById(id.Value);
            //Por queue
            var jsonGuitarrista = JsonConvert.SerializeObject(guitarrista);
            var bytesJsonGuitarrista = UTF8Encoding.UTF8.GetBytes(jsonGuitarrista);
            string jsonGuitarristaBase64 = Convert.ToBase64String(bytesJsonGuitarrista);

            await _queue.SendAsync(jsonGuitarristaBase64);
            if (guitarrista == null)
            {
                return NotFound();
            }

            return View(guitarrista);
        }

        // GET: Guitarrista/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guitarrista/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form, Guitarrista guitarrista)
        {
            if (ModelState.IsValid)
            {
                var file = form.Files.SingleOrDefault();
                var streamfile = file.OpenReadStream();
                var uriImage = await _blobservice.UploadAsync(streamfile);
                guitarrista.ImageUri = uriImage;
                await _guitarristaService.Create(guitarrista);
               
                return RedirectToAction(nameof(Index));
            }
            return View(guitarrista);
        }

        // GET: Guitarrista/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guitarrista = await _guitarristaService.GetById(id.Value);
            if (guitarrista == null)
            {
                return NotFound();
            }
            return View(guitarrista);
        }

        // POST: Guitarrista/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Guitarrista guitarrista)
        {
            if (id != guitarrista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _guitarristaService.Edit(guitarrista);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuitarristaExists(guitarrista.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(guitarrista);
        }

        // GET: Guitarrista/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guitarrista = await _guitarristaService.GetById(id.Value);
            if (guitarrista == null)
            {
                return NotFound();
            }

            return View(guitarrista);
        }

        // POST: Guitarrista/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _guitarristaService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool GuitarristaExists(int id)
        {
           var guitarrista =_guitarristaService.GetById(id);
           var exists = guitarrista != null;
           return exists;

        }
    }
}
