using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroAnnuaire.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionsDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrateurs");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborateurs_ServiceId",
                table: "Collaborateurs",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborateurs_SiteId",
                table: "Collaborateurs",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborateurs_Services_ServiceId",
                table: "Collaborateurs",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborateurs_Sites_SiteId",
                table: "Collaborateurs",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborateurs_Services_ServiceId",
                table: "Collaborateurs");

            migrationBuilder.DropForeignKey(
                name: "FK_Collaborateurs_Sites_SiteId",
                table: "Collaborateurs");

            migrationBuilder.DropIndex(
                name: "IX_Collaborateurs_ServiceId",
                table: "Collaborateurs");

            migrationBuilder.DropIndex(
                name: "IX_Collaborateurs_SiteId",
                table: "Collaborateurs");

            migrationBuilder.CreateTable(
                name: "Administrateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollaborateurId = table.Column<int>(type: "int", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrateurs", x => x.Id);
                });
        }
    }
}
