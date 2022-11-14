using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UNRI.Migrations
{
    public partial class dbUNRI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "data_fakultas",
                columns: table => new
                {
                    id_fakultas = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nm_fakultas = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_fakultas", x => x.id_fakultas);
                });

            migrationBuilder.CreateTable(
                name: "data_jabatan",
                columns: table => new
                {
                    id_jabatan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nm_jabatan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_jabatan", x => x.id_jabatan);
                });

            migrationBuilder.CreateTable(
                name: "data_jurusan",
                columns: table => new
                {
                    id_jurusan = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nm_jurusan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_fakultas = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_jurusan", x => x.id_jurusan);
                });

            migrationBuilder.CreateTable(
                name: "data_pegawai",
                columns: table => new
                {
                    id_pegawai = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nm_pegawai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_jabatan = table.Column<int>(type: "int", nullable: false),
                    alamat_kantor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telp_kantor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    keterangan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_pegawai", x => x.id_pegawai);
                });

            migrationBuilder.CreateTable(
                name: "data_pengguna",
                columns: table => new
                {
                    email_pengguna = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nama_pengguna = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    alamat_pengguna = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kelamin_pengguna = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telp_pengguna = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password_pengguna = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_pengguna", x => x.email_pengguna);
                });

            migrationBuilder.CreateTable(
                name: "data_prodi",
                columns: table => new
                {
                    id_prodi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    id_jurusan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jenjang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nm_prodi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_prodi", x => x.id_prodi);
                });

            migrationBuilder.CreateTable(
                name: "data_unit_kerja",
                columns: table => new
                {
                    id_unit_kerja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nm_unit_kerja = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_unit_kerja", x => x.id_unit_kerja);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "data_fakultas");

            migrationBuilder.DropTable(
                name: "data_jabatan");

            migrationBuilder.DropTable(
                name: "data_jurusan");

            migrationBuilder.DropTable(
                name: "data_pegawai");

            migrationBuilder.DropTable(
                name: "data_pengguna");

            migrationBuilder.DropTable(
                name: "data_prodi");

            migrationBuilder.DropTable(
                name: "data_unit_kerja");
        }
    }
}
