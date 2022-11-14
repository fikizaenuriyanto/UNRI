using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNRI.Models.jabatan
{
    public class data_jabatan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_jabatan { get; set; }
        public string nm_jabatan { get; set; }
    }
}
