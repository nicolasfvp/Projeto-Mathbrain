using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreApi.Migrations
{
    /// <inheritdoc />
    public partial class MathBrainRankingMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmProcesso",
                columns: table => new
                {
                    IdUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processando = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "UserRanking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MelhorRanking = table.Column<double>(type: "float", nullable: false),
                    UltimoRanking = table.Column<double>(type: "float", nullable: false),
                    QtdAcertos = table.Column<int>(type: "int", nullable: false),
                    MelhorTempo = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRanking", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmProcesso");

            migrationBuilder.DropTable(
                name: "UserRanking");
        }
    }
}
