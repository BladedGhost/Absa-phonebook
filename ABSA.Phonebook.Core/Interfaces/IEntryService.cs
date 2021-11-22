using ABSA.PB.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABSA.PB.Core.Interfaces
{
    public interface IEntryService
    {
        Task<ResultModel<Entry>> Get(Guid id);

        ResultModel<IEnumerable<Entry>> Get();

        Task<ResultModel<Entry>> Create(Entry phonebook);

        Task<ResultModel<Entry>> Update(Entry phonebook);

        Task<ResultModel<bool>> Delete(Guid id);
    }
}