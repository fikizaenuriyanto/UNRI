using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNRI.Models.wilayah_kerja
{
    public class data_unit_kerja
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_unit_kerja { get; set; }
        public string nm_unit_kerja { get; set; }
    }
}
