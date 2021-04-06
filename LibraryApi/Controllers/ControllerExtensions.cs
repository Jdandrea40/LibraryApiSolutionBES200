using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public static class ControllerExtensions
    {
        // _ = discard ("I know i need a param but im not actually using it")
        public static ActionResult Maybe<T>(this ControllerBase _, T obj)
        {
            if (obj == null)
            {
                return new NotFoundResult();
            }
            else
            {
                return new OkObjectResult(obj);
            }
        }
    }
}
