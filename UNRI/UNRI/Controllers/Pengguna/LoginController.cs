using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UNRI.Models.Connection;

namespace UNRI.Controllers.Pengguna
{
    [Route("Login")]
    public class LoginController : Controller
    {
        private UNRIcontext uNRIcontext;

        public LoginController(UNRIcontext uNRIcontext)
        {
            this.uNRIcontext = uNRIcontext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("getLogin")]
        public async Task<IActionResult> getLogin([FromHeader]string _email, [FromHeader]string _password)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            Dictionary<string, object> metadata = new Dictionary<string, object>();

            try
            {
                var loginPengguna = await uNRIcontext.data_pengguna
                    .Where(b => b.email_pengguna == _email)
                    .Where(b => b.password_pengguna == _password)
                    .ToListAsync();

                //Set the Expiry date of the Cookie.
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddHours(5);

                foreach (var _data in loginPengguna)
                {
                    var account_username = _email;
                    var account_name = _data.nama_pengguna;

                    //Create a Cookie with a suitable Key and add the Cookie to Browser.
                    Response.Cookies.Append("username", account_username, option);
                    Response.Cookies.Append("accountName", account_name, option);
                    HttpContext.Session.SetString("username", account_username);
                }

                response.Add("data", loginPengguna);

                metadata.Add("message", "Data tersedia");
                metadata.Add("code", 200);

                return Ok(new { response = response, metadata = metadata });
            }
            catch(Exception ex)
            {
                metadata.Add("message", ex.Message);
                metadata.Add("code", 201);

                return NotFound(new { metadata = metadata });
            }
        }

        [HttpGet]
        [Route("ReadCookie")]
        public IActionResult ReadCookie()
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            Dictionary<string, object> metadata = new Dictionary<string, object>();

            try
            {
                //read cookie from IHttpContextAccessor  
                string? cookieValueFromUsername = Request.Cookies["username"];
                string? cookieValueFromAccountName = Request.Cookies["accountName"];

                //read cookie from Request object  
                //string cookieValueFromPoliKlinik = Request.Cookies["poliklinik"];

                response.Add("username", cookieValueFromUsername!);
                response.Add("accountName", cookieValueFromAccountName!);

                metadata.Add("message", "Ok");
                metadata.Add("code", 200);

                return Ok(new { response = response, metadata = metadata });
            }
            catch (Exception ex)
            {
                metadata.Add("message", ex.Message);
                metadata.Add("code", 201);

                return NotFound(new { metadata = metadata });
            }
        }

        [HttpGet]
        [Route("DeleteCookies")]
        public IActionResult DeleteCookies()
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            Dictionary<string, object> metadata = new Dictionary<string, object>();

            try
            {
                //Delete the Cookie from Browser.
                Response.Cookies.Delete("username");
                Response.Cookies.Delete("accountName");

                response.Add("result", "Sukses");

                metadata.Add("message", "Ok");
                metadata.Add("code", 200);

                return Ok(new { response = response, metadata = metadata });
            }
            catch (Exception ex)
            {
                metadata.Add("message", ex.Message);
                metadata.Add("code", 201);

                return NotFound(new { metadata = metadata });
            }
        }
    }
}
