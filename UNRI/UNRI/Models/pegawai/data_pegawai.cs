using System.ComponentModel.DataAnnotations;

namespace UNRI.Models.pegawai
{
    public class data_pegawai
    {
        [Key]
        public string id_pegawai { get; set; }
        public string nm_pegawai { get; set; }
        public int id_jabatan { get; set; }
        public string alamat_kantor { get; set; }
        public string telp_kantor { get; set; }
        public string keterangan { get; set; }
    }
}
