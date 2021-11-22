using ABSA.PB.Core.Interfaces;
using ABSA.PB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABSA.PB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : APIBase
    {
        private readonly IEntryService _entryService;
        public EntryController(IEntryService entryService)
        {
            _entryService = entryService;
        }
        [HttpGet("")]
        public IActionResult GetEntry()
        {
            IActionResult result;

            try
            {
                var data = _entryService.Get();
                if (data.Data.Count() < 1)
                    result = NotFound(data);
                else result = Ok(data);
            }
            catch (Exception ex)
            {
                result = CreateBadRequest<Entry>(ex);
            }
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntry(Guid id)
        {
            IActionResult result;

            try
            {
                var data = await _entryService.Get(id);
                if (data.Data == null)
                    result = NotFound(data);
                else result = Ok(data);
            }
            catch (Exception ex)
            {
                result = CreateBadRequest<Entry>(ex);
            }
            return result;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateEntry([FromBody] Entry Entry)
        {
            IActionResult result;

            try
            {
                var data = await _entryService.Create(Entry);
                if (data.Data == null)
                    result = NotFound(data);
                else result = Ok(data);
            }
            catch (Exception ex)
            {
                result = CreateBadRequest<Entry>(ex);
            }
            return result;
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateEntry([FromBody] Entry Entry)
        {
            IActionResult result;

            try
            {
                var data = await _entryService.Update(Entry);
                if (data.Data == null)
                    result = NotFound(data);
                else result = Ok(data);
            }
            catch (Exception ex)
            {
                result = CreateBadRequest<Entry>(ex);
            }
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry(Guid id)
        {
            IActionResult result;

            try
            {
                var data = await _entryService.Delete(id);
                if (data.Data == false)
                    result = BadRequest(data);
                else result = Ok(data);
            }
            catch (Exception ex)
            {
                result = CreateBadRequest<Entry>(ex);
            }
            return result;
        }
    }
}
