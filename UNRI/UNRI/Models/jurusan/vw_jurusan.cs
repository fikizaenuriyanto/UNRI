using System.ComponentModel.DataAnnotations;

namespace UNRI.Models.jurusan
{
    public class vw_jurusan
    {
        [Key]
        public string id_jurusan { get; set; }
        public string nm_jurusan { get; set; }
        public string id_fakultas { get; set; }
        public string nm_fakultas { get; set; }
    }
}
