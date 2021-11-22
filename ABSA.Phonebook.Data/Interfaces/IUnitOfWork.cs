using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ABSA.PB.Data.Interfaces
{
    public interface IUnitOfWork
    {
        void Save();

        Task SaveAsync();
    }
}