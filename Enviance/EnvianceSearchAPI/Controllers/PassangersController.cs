using System.Collections.Generic;
using System.Web.Http;
using IType;
using BLLFactory;
using DTOs;

namespace EnvianceSearchAPI.Controllers
{
    [RoutePrefix("api/v1/passanger")]
    public class PassangersController : ApiController
    {
        [HttpGet]
        [Route("search/{name}")]        
        public IHttpActionResult GetPassangersByName(string name)
        {
            try
            {
                IPassangerBLL objPassangerBLL = PassangerBLLFactory.getPassangerBLLObject();
                List<PassangerDTO> result = objPassangerBLL.SearchPassangerByName(name);
                if (result.Count <= 0)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (System.Exception)
            {
                return BadRequest("Internal Server Error");
            }
            
        }
    }
}
