using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDEstadosEF.Models.Context;
using CRUDEstadosEF.Models.Entities;
using CRUDEstadosEF.Models.BL;

namespace CRUDEstadosEF.Controllers
{
    public class EstadosController : Controller
    {
        private readonly BEstados _bEstados;

        public EstadosController()
        {
            _bEstados = new BEstados();
        }

        // GET: Estados
        public async Task<IActionResult> Index()
        {
            var estados = await _bEstados.Consultar();
            return View(estados);   
        }

        // GET: Estados/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            return await Consultar(id);
        }

        // GET: Estados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Estados estados)
        {
            if (ModelState.IsValid)
            {
                var edo = await _bEstados.Agregar(estados);
                return RedirectToAction(nameof(Index));
            }
            return View(estados);
        }

        // GET: Estados/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            return await Consultar(id);
        }

        // POST: Estados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Nombre")] Estados estados)
        {
            if (id != estados.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bEstados.Actualizar(estados);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadosExists(estados.Id))
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
            return View(estados);
        }

        // GET: Estados/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            return await Consultar(id);
        }

        // POST: Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            await _bEstados.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        private bool EstadosExists(short id)
        {
          return (_bEstados.Consultar(id) != null ? true : false);
        }

        private async Task<IActionResult> Consultar(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var estados = await _bEstados.Consultar(id);
            if(estados != null)
            {
                return NotFound();
            }
            return View(estados);
        }
    }
}
