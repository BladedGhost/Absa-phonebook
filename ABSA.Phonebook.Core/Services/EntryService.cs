using ABSA.PB.Core.Interfaces;
using ABSA.PB.Data.Interfaces;
using ABSA.PB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ABSA.PB.Core.Services
{
    public class EntryService : IEntryService
    {
        private readonly IRepository<Entry> _entryRepo;
        private readonly IUnitOfWork _unitOfWork;

        public EntryService(IRepository<Entry> entryRepo, IUnitOfWork unitOfWork)
        {
            _entryRepo = entryRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultModel<Entry>> Create(Entry Entry)
        {
            ResultModel<Entry> result = new ResultModel<Entry>();
            try
            {
                await _entryRepo.AddAsync(Entry);
                await _unitOfWork.SaveAsync();
                result.Data = Entry;
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
                var item = await _entryRepo.GetByIdAsync(id);
                _entryRepo.Remove(item);
                result.Data = true;
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.ErrorMessage = ex.Message;
                result.Exception = ex;
            }
            return result;
        }

        public async Task<ResultModel<Entry>> Get(Guid id)
        {
            ResultModel<Entry> result = new ResultModel<Entry>();
            try
            {
                result.Data = await _entryRepo.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.ErrorMessage = ex.Message;
                result.Exception = ex;
            }
            return result;
        }

        public ResultModel<IEnumerable<Entry>> Get()
        {
            ResultModel<IEnumerable<Entry>> result = new ResultModel<IEnumerable<Entry>>();
            try
            {
                result.Data = _entryRepo.GetAll();
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.ErrorMessage = ex.Message;
                result.Exception = ex;
            }
            return result;
        }

        public async Task<ResultModel<Entry>> Update(Entry Entry)
        {
            ResultModel<Entry> result = new ResultModel<Entry>();
            try
            {
                var item = await _entryRepo.GetByIdAsync(Entry.ID);
                item.PhoneNumber = Entry.PhoneNumber;
                item.Name = Entry.Name;
                _entryRepo.Update(item);
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