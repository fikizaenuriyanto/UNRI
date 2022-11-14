using Microsoft.EntityFrameworkCore;
using UNRI.Models.fakultas;
using UNRI.Models.jabatan;
using UNRI.Models.jurusan;
using UNRI.Models.pegawai;
using UNRI.Models.pengguna;
using UNRI.Models.prodi;
using UNRI.Models.wilayah_kerja;

namespace UNRI.Models.Connection
{
    public class UNRIcontext : DbContext
    {
        public UNRIcontext(DbContextOptions<UNRIcontext> options) : base(options)
        {

        }

        public DbSet<data_fakultas> data_fakultas { get; set; }
        public DbSet<data_jabatan> data_jabatan { get; set; }
        public DbSet<data_jurusan> data_jurusan { get; set; }
        public DbSet<vw_jurusan> vw_jurusan { get; set; }
        public DbSet<data_prodi> data_prodi { get; set; }
        public DbSet<vw_prodi> vw_prodi { get; set; }
        public DbSet<data_unit_kerja> data_unit_kerja { get; set; }
        public DbSet<data_pegawai> data_pegawai { get; set; }
        public DbSet<data_pengguna> data_pengguna { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("dbUNRI");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
