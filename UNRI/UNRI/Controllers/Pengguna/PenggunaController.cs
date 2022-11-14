using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UNRI.Models.Connection;
using UNRI.Models.pengguna;

namespace UNRI.Controllers.Pengguna
{
    [Route("Pengguna")]
    public class PenggunaController : Controller
    {
        private UNRIcontext uNRIcontext;

        public PenggunaController(UNRIcontext uNRIcontext)
        {
            this.uNRIcontext = uNRIcontext;
        }

        public IActionResult Index()
        {
            string? sessionValue = HttpContext.Session.GetString("username");
            string? cookieValueFromUsername = Request.Cookies["username"];
            string? cookieValueFromAccountName = Request.Cookies["accountName"];

            if (sessionValue != null)
            {
                if (cookieValueFromUsername != null)
                {
                    if (cookieValueFromAccountName != null)
                    {
                        ViewBag.nama = cookieValueFromAccountName;

                        return View();
                    }
                    else
                    {
                        return Redirect("~/Login");
                    }
                }
                else
                {
                    return Redirect("~/Login");
                }
            }
            else
            {
                return Redirect("~/Login");
            }
        }

        [HttpGet]
        [Route("getData_Table")]
        public async Task<IActionResult> getData_Table()
        {
            var dataTable = await uNRIcontext.data_pengguna.ToListAsync();

            return PartialView("data_table",dataTable);
        }

        [HttpGet]
        [Route("OpenCreated")]
        public IActionResult OpenCreated()
        {
            return PartialView("Created");
        }

        [HttpGet]
        [Route("OpenUpdated")]
        public async Task<IActionResult> OpenUpdated([FromHeader]string _username)
        {
            var dataUpdate = await uNRIcontext.data_pengguna.FindAsync(_username);

            return PartialView("Updated", dataUpdate);
        }

        [HttpPost]
        [Route("CreatedData")]
        public async Task<IActionResult> CreatedData([FromBody]data_pengguna _pengguna)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            Dictionary<string, object> metadata = new Dictionary<string, object>();

            try
            {
                uNRIcontext.data_pengguna.Add(_pengguna);
                await uNRIcontext.SaveChangesAsync();

                response.Add("result", "true");
                response.Add("data", _pengguna);

                metadata.Add("message", "Data berhasil disimpan");
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

        [HttpPut]
        [Route("UpdatedData")]
        public async Task<IActionResult> UpdatedData([FromBody]data_pengguna _pengguna)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            Dictionary<string, object> metadata = new Dictionary<string, object>();

            try
            {
                uNRIcontext.data_pengguna.Update(_pengguna);
                await uNRIcontext.SaveChangesAsync();

                response.Add("result", "true");
                response.Add("data", _pengguna);

                metadata.Add("message", "Data berhasil disimpan");
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

        [HttpDelete]
        [Route("DeletedData")]
        public async Task<IActionResult> DeletedData([FromHeader]string _username)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            Dictionary<string, object> metadata = new Dictionary<string, object>();

            try
            {
                var dataSearch = await uNRIcontext.data_pengguna.FindAsync(_username);

                if (dataSearch == null)
                {
                    metadata.Add("message", "Data akun dengan E-mail " + _username + " tidak ditemukan.");
                    metadata.Add("code", 202);

                    return NotFound(new { metadata = metadata });
                }
                else
                {
                    uNRIcontext.data_pengguna.Remove(dataSearch);
                    await uNRIcontext.SaveChangesAsync();

                    response.Add("result", "true");

                    metadata.Add("message", "Data berhasil dihapus");
                    metadata.Add("code", 200);

                    return Ok(new { response = response, metadata = metadata });
                }
            }catch(Exception ex)
            {
                metadata.Add("message", ex.Message);
                metadata.Add("code", 201);

                return NotFound(new { metadata = metadata });
            }
        }
    }
}
