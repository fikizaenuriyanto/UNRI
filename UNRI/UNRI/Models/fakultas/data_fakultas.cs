using System.ComponentModel.DataAnnotations;

namespace UNRI.Models.fakultas
{
    public class data_fakultas
    {
        [Key]
        public string id_fakultas { get; set; }
        public string nm_fakultas { get; set; }
    }
}
