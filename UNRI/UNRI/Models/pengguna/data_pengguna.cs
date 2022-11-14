using System.ComponentModel.DataAnnotations;

namespace UNRI.Models.pengguna
{
    public class data_pengguna
    {
        [Key]
        public string email_pengguna { get; set; }
        public string nama_pengguna { get; set; }
        public string alamat_pengguna { get; set; }
        public string kelamin_pengguna { get; set; }
        public string telp_pengguna { get; set; }
        public string password_pengguna { get; set; }
    }
}
