using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations.StoreIdentity
{
    /// <inheritdoc />
    public partial class Updatee7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Addresses");
        }
    }
}
