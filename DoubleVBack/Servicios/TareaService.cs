﻿using DoubleV.Interfaces;
using DoubleV.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoubleV.Servicios
{
    public class TareaService : ITareaService
    {
        private readonly ApplicationDbContext _context;

        public TareaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CrearTareaAsync(Tarea tarea)
        {
            try
            {
                _context.Tareas.Add(tarea);
                await _context.SaveChangesAsync();
                // Si se guarda correctamente, devuelve true
                return true; 
            }
            catch (DbUpdateException dbEx) 
            {                
                Console.WriteLine("Error de base de datos: " + dbEx.Message); 
                if (dbEx.InnerException != null) 
                {
                    Console.WriteLine("Detalle: " + dbEx.InnerException.Message);
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general: " + ex.Message);
                return false; 
            }           
        }

        public async Task<List<Tarea>> GetAllTareasAsync()
        {
            return await _context.Tareas.Include(t => t.Usuario).ToListAsync();
        }

        public async Task<Tarea> GetTareaByIdAsync(int id)
        {
            return await _context.Tareas.Include(t => t.Usuario)
                .FirstOrDefaultAsync(t => t.TareaId == id);
        }   

        public async Task<Tarea> UpdateTareaAsync(Tarea tarea)
        {
            _context.Entry(tarea).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return tarea;
        }

        public async Task<bool> DeleteTareaAsync(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return false;

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
