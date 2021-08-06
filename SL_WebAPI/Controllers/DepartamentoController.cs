using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;



// SE UTILIZARAN LOS SIGUENTES COMPLEMENTOS 

namespace SL_WebAPI.Controllers
{
    public class DepartamentoController : ApiController
    {
        // GET: api/Departamento
        [HttpGet]
        [Route("api/Departamento/GetAll")]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Departamento.GetAllEF();
            if(result.Correct)
            {
                return Ok(result);
            }
            else                
            {
                return BadRequest(result.ErrorMessage);
            }
           
        }

        // GET: api/Departamento/5
        [HttpGet]
        [Route("api/Departamento/GetById/{IdDepartamento}")]
        public IHttpActionResult GetById(int IdDepartamento)
        {
            ML.Result result = BL.Departamento.GetById(IdDepartamento);
            if(result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Departamento
        [HttpPost]
        [Route("api/Departamento/Add")]
        public IHttpActionResult Add([FromBody]ML.Departamento departamento)
        {
            ML.Result result = BL.Departamento.AddEF(departamento);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        // PUT: api/Departamento/5
        [HttpPut]
        [Route("api/Departamento/Update/{IdDepartamento}")]
        public IHttpActionResult Update(int IdDepartamento,[FromBody]ML.Departamento departamento)
        {
            departamento.IdDepartamento = IdDepartamento;

            
            ML.Result result = BL.Departamento.UpdateEF(departamento);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/Departamento/5
        [HttpDelete]
        [Route("api/Departamento/Delete/{IdDepartamento}")]
        public IHttpActionResult Delete(int IdDepartamento)
        {
            ML.Result result = BL.Departamento.DeleteEF(IdDepartamento);

            if (result.Correct)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
