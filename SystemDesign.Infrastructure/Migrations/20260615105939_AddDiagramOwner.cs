using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemDesign.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDiagramOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Diagrams",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Diagrams_OwnerId",
                table: "Diagrams",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Diagrams_OwnerId",
                table: "Diagrams");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Diagrams");
        }
    }
}
