using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class FixBookStoragerelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Storages_BookId",
                table: "Storages");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_BookId",
                table: "Storages",
                column: "BookId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Storages_BookId",
                table: "Storages");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_BookId",
                table: "Storages",
                column: "BookId");
        }
    }
}
