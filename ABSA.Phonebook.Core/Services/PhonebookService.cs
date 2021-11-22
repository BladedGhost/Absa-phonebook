using ABSA.PB.Core.Interfaces;
using ABSA.PB.Data.Interfaces;
using ABSA.PB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ABSA.PB.Core.Services
{
    public class PhonebookService : IPhonebookService
    {
        private readonly IRepository<Phonebook> _phonebookRepo;
        private readonly IRepository<Entry> _entryRepo;
        private readonly IUnitOfWork _unitOfWork;

        public PhonebookService(IRepository<Phonebook> phonebookRepo, IRepository<Entry> entryRepo, IUnitOfWork unitOfWork)
        {
            _phonebookRepo = phonebookRepo;
            _entryRepo = entryRepo;
            _unitOfWork = unitOfWork;
        }

        public IRepository<Entry> EntryReppo { get; }

        public async Task<ResultModel<Phonebook>> Create(Phonebook phonebook)
        {
            ResultModel<Phonebook> result = new ResultModel<Phonebook>();
            try
            {
                await _phonebookRepo.AddAsync(phonebook);
                await _unitOfWork.SaveAsync();
                result.Data = phonebook;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.ErrorMessage = ex.Message;
                result.Exception = ex;
            }
            return result;
        }

        public async Task<ResultModel<bool>> Delete(Guid id)
        {
            ResultModel<bool> result = new ResultModel<bool>();
            try
            {
                var item = await _phonebookRepo.GetByIdAsync(id);
                _phonebookRepo.Remove(item);
                var entries = _entryRepo.Get(x => x.PhoneBookID == id);
                _entryRepo.RemoveRange(entries);

                await _unitOfWork.SaveAsync();
                result.Data = true;
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.ErrorMessage = ex.Message;
                result.Exception = ex;
            }
            return result;
        }

        public async Task<ResultModel<Phonebook>> Get(Guid id)
        {
            ResultModel<Phonebook> result = new ResultModel<Phonebook>();
            try
            {
                result.Data = await _phonebookRepo.Get(x => x.ID == id).AsQueryable().Include(x => x.Entries).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.ErrorMessage = ex.Message;
                result.Exception = ex;
            }
            return result;
        }

        public ResultModel<IEnumerable<Phonebook>> Get()
        {
            ResultModel<IEnumerable<Phonebook>> result = new ResultModel<IEnumerable<Phonebook>>();
            try
            {
                result.Data = _phonebookRepo.GetAll().AsQueryable().Include(x => x.Entries);
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.ErrorMessage = ex.Message;
                result.Exception = ex;
            }
            return result;
        }

        public ResultModel<IEnumerable<Phonebook>> Get(string name)
        {
            name = name.ToLower();
            ResultModel<IEnumerable<Phonebook>> result = new ResultModel<IEnumerable<Phonebook>>();
            try
            {
                result.Data = _phonebookRepo.GetAll().AsQueryable().Include(x => x.Entries).Where(x => x.Name.ToLower().Contains(name) || x.Entries.Any(a => a.Name.ToLower().Contains(name)));
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.ErrorMessage = ex.Message;
                result.Exception = ex;
            }
            return result;
        }

        public async Task<ResultModel<Phonebook>> Update(Phonebook phonebook)
        {
            ResultModel<Phonebook> result = new ResultModel<Phonebook>();
            try
            {
                var item = _phonebookRepo.Get(x => x.ID == phonebook.ID).AsQueryable().Include(x => x.Entries).FirstOrDefault();
                foreach (var entry in item.Entries)
                {
                    var updateEntry = phonebook.Entries.SingleOrDefault(x => x.ID == entry.ID);
                    entry.Name = updateEntry.Name;
                    entry.PhoneNumber = updateEntry.PhoneNumber;
                    _entryRepo.Update(entry);
                }
                foreach (var entry in phonebook.Entries.Where(x => !item.Entries.Any(a => a.ID == x.ID)))
                {
                    await _entryRepo.AddAsync(entry);
                }
                item.Name = phonebook.Name;
                _phonebookRepo.Update(item);
                await _unitOfWork.SaveAsync();
                result.Data = item;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.ErrorMessage = ex.Message;
                result.Exception = ex;
            }
            return result;
        }
    }
}