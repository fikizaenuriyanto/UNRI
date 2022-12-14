using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UNRI.Models.Connection;
using UNRI.Models.jurusan;
using UNRI.Models.prodi;

namespace UNRI.Controllers.prodi
{
    [Route("Prodi")]
    public class ProdiController : Controller
    {
        private UNRIcontext uNRIcontext;

        public ProdiController(UNRIcontext uNRIcontext)
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
        [Route("GetDataTable")]
        public async Task<IActionResult> GetDataTable()
        {
            var dataTable = await uNRIcontext.vw_prodi.ToListAsync();

            return PartialView("data_table", dataTable);
        }

        [HttpGet]
        [Route("OpenCreated")]
        public IActionResult OpenCreated()
        {
            return PartialView("Created");
        }

        [HttpGet]
        [Route("OpenUpdated")]
        public async Task<IActionResult> OpenUpdated([FromHeader] string idProdi)
        {
            var dataUpdate = await uNRIcontext.vw_prodi.FindAsync(idProdi);

            return PartialView("Updated", dataUpdate);
        }

        [HttpGet]
        [Route("GetFakultas")]
        public async Task<IActionResult> GetFakultas()
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            Dictionary<string, object> metadata = new Dictionary<string, object>();

            try
            {
                var dataUpdate = await uNRIcontext.data_fakultas.ToListAsync();

                response.Add("Fakultas", dataUpdate);

                metadata.Add("message", "Data berhasil disimpan");
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
        [Route("GetJurusan")]
        public async Task<IActionResult> GetJurusan([FromHeader] string idFakultas)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            Dictionary<string, object> metadata = new Dictionary<string, object>();

            try
            {
                var dataUpdate = await uNRIcontext.data_jurusan
                    .Where(b => b.id_fakultas == idFakultas)
                    .ToListAsync();

                response.Add("Jurusan", dataUpdate);

                metadata.Add("message", "Data berhasil disimpan");
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

        [HttpPost]
        [Route("CreatedData")]
        public async Task<IActionResult> CreatedData([FromBody] data_prodi _prodi)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            Dictionary<string, object> metadata = new Dictionary<string, object>();

            try
            {
                uNRIcontext.data_prodi.Add(_prodi);
                await uNRIcontext.SaveChangesAsync();

                response.Add("result", "true");
                response.Add("data", _prodi);

                metadata.Add("message", "Data berhasil disimpan");
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

        [HttpPut]
        [Route("UpdatedData")]
        public async Task<IActionResult> UpdatedData([FromBody] data_prodi _prodi)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            Dictionary<string, object> metadata = new Dictionary<string, object>();

            try
            {
                uNRIcontext.data_prodi.Update(_prodi);
                await uNRIcontext.SaveChangesAsync();

                response.Add("result", "true");
                response.Add("data", _prodi);

                metadata.Add("message", "Data berhasil disimpan");
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

        [HttpDelete]
        [Route("DeletedData")]
        public async Task<IActionResult> DeletedData([FromHeader] string idProdi)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            Dictionary<string, object> metadata = new Dictionary<string, object>();

            try
            {
                var dataSearch = await uNRIcontext.data_prodi.FindAsync(idProdi);

                if (dataSearch == null)
                {
                    metadata.Add("message", "Data jurusan dengan id prodi " + idProdi + " tidak ditemukan.");
                    metadata.Add("code", 202);

                    return NotFound(new { metadata = metadata });
                }
                else
                {
                    uNRIcontext.data_prodi.Remove(dataSearch);
                    await uNRIcontext.SaveChangesAsync();

                    response.Add("result", "true");

                    metadata.Add("message", "Data berhasil dihapus");
                    metadata.Add("code", 200);

                    return Ok(new { response = response, metadata = metadata });
                }
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
