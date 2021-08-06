using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//PARA GENERAR unchecked TOKEN sealed INSTALO else SIGUENTE NUGET
//Install-Package System.IdentityModel.Tokens.Jwt -Version 6.11.1
using System.Threading;
using System.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace SL_WebAPI.Controllers
{
    public class UsuarioController : ApiController
    {



        // INICIAR SECCION SIN TOKEN
        //[HttpPost]
        //[Route("api/Usuario/Login")]
        //// POST: api/Usuario
        //public IHttpActionResult Login([FromBody]ML.Usuario usuario)
        //{

        //    ML.Result result = BL.Usuario.UsuarioGetByUserName(usuario);
        //    if (result.Correct)
        //    {
        //        return Content(HttpStatusCode.OK, result);
        //        //return Ok(result);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }

        //}

        // INICIAR SECCION CON TOKEN
        [HttpPost]
        [Route("api/Usuario/GetByUsername")]
        // POST: api/Usuario
        public IHttpActionResult GetByUsername([FromBody]ML.Usuario usuario)
        {

            ML.Result result = BL.Usuario.UsuarioGetByUserName(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }



        [HttpPost]
        [Route("api/Usuario/Authenticate")]
        public IHttpActionResult Authenticate([FromBody]ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.UsuarioGetByUserName(usuario);
            ML.Usuario Usuario = new ML.Usuario();


            if (result.Correct)
            {
                if (usuario.UserName == Usuario.UserName)
                {
                    if (usuario.Password == Usuario.Password)
                    {
                        var token = TokenGenerator.GenerateTokenJwt(usuario.UserName);
                        return Ok(token);
                    }
                    else
                    {
                        result.ErrorMessage = "contraseña incorrecta";
                        return Content(HttpStatusCode.NotFound,result.ErrorMessage);
                    }
                }
                else
                {
                    result.ErrorMessage = "usuario incorrecto";
                    return Content(HttpStatusCode.NotFound, result.ErrorMessage);
                }
            }

            else
            {
                result.ErrorMessage = "no encontrado";
                return Content(HttpStatusCode.NotFound, result.ErrorMessage);
            }
        }

        internal static class TokenGenerator
        {
            public static string GenerateTokenJwt(string username)
            {
                // appsetting for Token JWT
                var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
                var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
                var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
                var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                // create a claimsIdentity
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) });

                // create token to the user
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                    audience: audienceToken,
                    issuer: issuerToken,
                    subject: claimsIdentity,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                    signingCredentials: signingCredentials);

                var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
                return jwtTokenString;
            }
        }



    }
}
