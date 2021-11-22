using ABSA.PB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ABSA.PB.Core.Interfaces
{
    public interface IPhonebookService
    {
        Task<ResultModel<Phonebook>> Get(Guid id);

        ResultModel<IEnumerable<Phonebook>> Get();

        ResultModel<IEnumerable<Phonebook>> Get(string name);

        Task<ResultModel<Phonebook>> Create(Phonebook phonebook);

        Task<ResultModel<Phonebook>> Update(Phonebook phonebook);

        Task<ResultModel<bool>> Delete(Guid id);
    }
}