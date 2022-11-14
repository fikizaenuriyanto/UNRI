﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UNRI.Models.Connection;
using UNRI.Models.jabatan;
using UNRI.Models.wilayah_kerja;

namespace UNRI.Controllers.wilayah_kerja
{
    [Route("UnitKerja")]

    public class UnitKerjaController : Controller
    {
        private UNRIcontext uNRIcontext;

        public UnitKerjaController(UNRIcontext uNRIcontext)
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
            var dataTable = await uNRIcontext.data_unit_kerja.ToListAsync();

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
        public async Task<IActionResult> OpenUpdated([FromHeader] int idUnit)
        {
            var dataUpdate = await uNRIcontext.data_unit_kerja.FindAsync(idUnit);

            return PartialView("Updated", dataUpdate);
        }

        [HttpPost]
        [Route("CreatedData")]
        public async Task<IActionResult> CreatedData([FromBody] data_unit_kerja _unit)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            Dictionary<string, object> metadata = new Dictionary<string, object>();

            try
            {
                uNRIcontext.data_unit_kerja.Add(_unit);
                await uNRIcontext.SaveChangesAsync();

                response.Add("result", "true");
                response.Add("data", _unit);

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
        public async Task<IActionResult> UpdatedData([FromBody] data_unit_kerja _unit)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            Dictionary<string, object> metadata = new Dictionary<string, object>();

            try
            {
                uNRIcontext.data_unit_kerja.Update(_unit);
                await uNRIcontext.SaveChangesAsync();

                response.Add("result", "true");
                response.Add("data", _unit);

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
        public async Task<IActionResult> DeletedData([FromHeader] int idUnit)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            Dictionary<string, object> metadata = new Dictionary<string, object>();

            try
            {
                var dataSearch = await uNRIcontext.data_unit_kerja.FindAsync(idUnit);

                if (dataSearch == null)
                {
                    metadata.Add("message", "Data fakultas dengan id unit kerja " + idUnit + " tidak ditemukan.");
                    metadata.Add("code", 202);

                    return NotFound(new { metadata = metadata });
                }
                else
                {
                    uNRIcontext.data_unit_kerja.Remove(dataSearch);
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
