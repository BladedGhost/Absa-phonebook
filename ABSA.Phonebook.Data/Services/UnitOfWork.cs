using ABSA.PB.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ABSA.PB.Data.Services
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly PhonebookDBContext _context;

        public UnitOfWork(PhonebookDBContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}