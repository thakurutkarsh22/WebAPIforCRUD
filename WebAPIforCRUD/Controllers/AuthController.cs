using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPIforCRUD.Repo;

namespace WebAPIforCRUD.Controllers
{


    public class jsonres
    {
        public string description;
        public string status;
        public string token;
    }

    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {


        [HttpPost("token")]
        public async Task<IActionResult> TokenAsync()
        {
            //string tokenString = "test";
            var header = Request.Headers["Authorization"];
            if (header.ToString().StartsWith("Basic"))
            {
                var credValue = header.ToString().Substring("Basic ".Length).Trim();
                var usernameAndPassenc = Encoding.UTF8.GetString(Convert.FromBase64String(credValue)); //admin:pass
                var usernameAndPass = usernameAndPassenc.Split(':');
                //check in DB username and pass exist

                AuthRepository ap = new AuthRepository();




                if ((await ap.GetUserAsync(usernameAndPass[0], usernameAndPass[1])) != null)
                {
                    var claimsdata = new[] { new Claim(ClaimTypes.Name, usernameAndPass[0]) };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ahbasshfbsahjfbshajbfhjasbfashjbfsajhfvashjfashfbsahfbsahfksdjf"));
                    var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                    var token = new JwtSecurityToken(
                         issuer: "mysite.com",
                         audience: "mysite.com",
                         expires: DateTime.Now.AddMinutes(1),
                         claims: claimsdata,
                         signingCredentials: signInCred
                        );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    //return Ok(tokenString);




                    jsonres jsres = new jsonres();
                    jsres.description = "Token is Succesfully Generated";
                    jsres.status = "Success";
                    jsres.token = tokenString + "";




                    return new JsonResult(jsres);
                }
                else
                {
                    jsonres jsres = new jsonres();
                    jsres.description = "Username or password is incorrect";
                    jsres.status = "Failed";

                    return BadRequest(jsres);
                }
            }
            return BadRequest("wrong request");

            // return View();
        }
    }
}