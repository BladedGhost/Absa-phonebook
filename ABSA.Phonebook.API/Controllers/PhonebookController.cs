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
    public class PhonebookController : APIBase
    {
        private readonly IPhonebookService _phonebookService;
        public PhonebookController(IPhonebookService phonebookService)
        {
            _phonebookService = phonebookService;
        }
        [HttpGet("")]
        public IActionResult GetPhonebook()
        {
            IActionResult result;

            try
            {
                var data = _phonebookService.Get();
                if (data.Data.Count() < 1)
                    result = NotFound(data);
                else result = Ok(data);
            }
            catch(Exception ex)
            {
                result = CreateBadRequest<Phonebook>(ex);
            }
            return result;
        }

        [HttpGet("search/{name}")]
        public IActionResult GetPhonebook(string name)
        {
            IActionResult result;

            try
            {
                var data = _phonebookService.Get();
                if (data.Data.Count() < 1)
                    result = NotFound(data);
                else result = Ok(data);
            }
            catch (Exception ex)
            {
                result = CreateBadRequest<Phonebook>(ex);
            }
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhonebook(Guid id)
        {
            IActionResult result;

            try
            {
                var data = await _phonebookService.Get(id);
                if (data.Data == null)
                    result = NotFound(data);
                else result = Ok(data);
            }
            catch (Exception ex)
            {
                result = CreateBadRequest<Phonebook>(ex);
            }
            return result;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreatePhonebook([FromBody] Phonebook phonebook)
        {
            IActionResult result;

            try
            {
                var data = await _phonebookService.Create(phonebook);
                if (data.Data == null)
                    result = NotFound(data);
                else result = Ok(data);
            }
            catch (Exception ex)
            {
                result = CreateBadRequest<Phonebook>(ex);
            }
            return result;
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdatePhonebook([FromBody] Phonebook phonebook)
        {
            IActionResult result;

            try
            {
                var data = await _phonebookService.Update(phonebook);
                if (data.Data == null)
                    result = NotFound(data);
                else result = Ok(data);
            }
            catch (Exception ex)
            {
                result = CreateBadRequest<Phonebook>(ex);
            }
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhonebook(Guid id)
        {
            IActionResult result;

            try
            {
                var data = await _phonebookService.Delete(id);
                if (data.Data == false)
                    result = BadRequest(data);
                else result = Ok(data);
            }
            catch (Exception ex)
            {
                result = CreateBadRequest<Phonebook>(ex);
            }
            return result;
        }
    }
}