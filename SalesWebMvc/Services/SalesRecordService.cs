using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;//criar uma dependecia para acessar o DbContext


        //criar um construtor para que a injeção de dependencia possa ocorrer

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;

        }
        //metodo abaixo faz a busca od registros por data.
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate,DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;//objeto inicial de consulta,construido apartir do link, lembrando que essa consulta não e executada apenas por sua definição. 
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);

            }
            return await result
                .Include(x=> x.Seller)
                .Include(x=> x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;//objeto inicial de consulta,construido apartir do link, lembrando que essa consulta não e executada apenas por sua definição. 
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);

            }
            return await result
                .Include(x=> x.Seller)
                .Include(x=> x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x =>x.Seller.Department)
                .ToListAsync();
                
        }



    }
}

/* Forma Sincrona
 * 
 * 
 public List<SalesRecord> FindByDate(DateTime? minDate,DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;//objeto inicial de consulta,construido apartir do link, lembrando que essa consulta não e executada apenas por sua definição. 
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);

            }
            return result
                .Include(x=>x.Seller)
                .Include(x=> x.Seller.Department)
                .OrderByDescending(x =>x.Date)
                .ToList();
                
        }
     
     */
