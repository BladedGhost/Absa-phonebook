using ABSA.PB.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABSA.PB.API
{
    public class APIBase: ControllerBase
    {
        internal IActionResult CreateBadRequest<T>(Exception ex)
        {
            return BadRequest(new ResultModel<T>
            {
                Data = default,
                ErrorMessage = ex.Message
            });
        }
    }
}
