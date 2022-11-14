using System.ComponentModel.DataAnnotations;

namespace UNRI.Models.prodi
{
    public class vw_prodi
    {
        [Key]
        public string id_prodi { get; set; }
        public string jenjang { get; set; }
        public string nm_prodi { get; set; }
        public string id_jurusan { get; set; }
        public string nm_jurusan { get; set; }
        public string id_fakultas { get; set; }
        public string nm_fakultas { get; set; }
    }
}
