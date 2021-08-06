using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebAPI.Controllers
{
    public class ProductoController : ApiController
    {
        // GET: api/Producto

        [HttpGet]
        [Route("api/Producto/GetAll")]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Producto.GetAllEF();

            if (result.Correct)
            {
                //return Content(HttpStatusCode.OK,result.Objects);
                return Ok(result);

            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result.ErrorMessage);
            }
        }

        // GET: api/Producto/5

        [HttpGet]
        [Route("api/Producto/GetbyId/{IdProducto}")]
        public IHttpActionResult GetById(int IdProducto)
        {
            ML.Result result = BL.Producto.GetByIdEFMVC(IdProducto);

            
            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result.Object);
            }
        }

        // POST: api/Producto
        [HttpPost]
        [Route("api/Producto/Add")]
        public IHttpActionResult Add([FromBody]ML.Producto producto)
        {
            ML.Result result = BL.Producto.AddEF(producto);


            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result.Object);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result.Object);
            }
        }

        // PUT: api/Producto/5

        [HttpPut]
        [Route("api/Producto/Update/{IdProducto}")]
        public IHttpActionResult Update(int IdProducto,[FromBody]ML.Producto producto)
        {
            producto.IdProducto = IdProducto;
            ML.Result result = BL.Producto.UpdateEF(producto);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(); 
            }
        }

        // DELETE: api/Producto/5
        [HttpDelete]
        [Route("api/Producto/{IdProducto}/Delete")]
        public IHttpActionResult Delete(int IdProducto)
        {
            ML.Result result = BL.Producto.DeleteEFMVC(IdProducto);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
