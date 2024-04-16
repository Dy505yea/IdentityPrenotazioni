using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityPrenotazioni.Data.Migrations
{
    /// <inheritdoc />
    public partial class Prenotazioni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Enti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    NomiEventi = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Eventi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    EnteId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventi_Enti_EnteId",
                        column: x => x.EnteId,
                        principalTable: "Enti",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Prenotazioni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Prenotato = table.Column<bool>(type: "INTEGER", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: true),
                    Ora = table.Column<string>(type: "TEXT", nullable: true),
                    Cognome = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    NomeEvento = table.Column<string>(type: "TEXT", nullable: true),
                    EventoId = table.Column<int>(type: "INTEGER", nullable: true),
                    NomeEnte = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prenotazioni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prenotazioni_Eventi_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventi",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eventi_EnteId",
                table: "Eventi",
                column: "EnteId");

            migrationBuilder.CreateIndex(
                name: "IX_Prenotazioni_EventoId",
                table: "Prenotazioni",
                column: "EventoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prenotazioni");

            migrationBuilder.DropTable(
                name: "Eventi");

            migrationBuilder.DropTable(
                name: "Enti");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");
        }
    }
}
