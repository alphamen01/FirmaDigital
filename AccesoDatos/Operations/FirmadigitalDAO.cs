using AccesoDatos.Context;
using AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Operations
{
    public class FirmadigitalDAO
    {
        private readonly PruebaContext context;
        public FirmadigitalDAO(PruebaContext context)
        {
            this.context = context;
        }

        public List<Firmadigital> seleccionarTodos()
        {
            var firmasDigitales = context.Firmadigitals.ToList<Firmadigital>();
            return firmasDigitales;
        }

        public Pager seleccionarTodosPaginado(int page, int size)
        {
            var pageSize = size;
            if (pageSize < 1)
            {
                pageSize = 1;
            }
            var records = context.Firmadigitals.Count();
            var firmas = context.Firmadigitals
                .Skip((page - 1) * (int)pageSize)
                .Take((int)pageSize)
                .ToList();

            var results = new Pager(records, page, size)
            {
                Firmas = firmas

            };
            return results;

        }

        public Firmadigital seleccionar(int id)
        {
            var firmaDigital = context.Firmadigitals.FirstOrDefault(c => c.IdFirma == id);
            return firmaDigital!;
        }

        public void insertarFirma(Firmadigital firma)
        {
            context.Firmadigitals.Add(firma);
            context.SaveChanges();
        }

        public bool eliminar(int id)
        {
            var firma = seleccionar(id);
            if(firma == null)
            {
                return false;
            }
            else
            {
                context.Firmadigitals.Remove(firma);
                context.SaveChanges();
                return true;
            }
        }
    }
}
