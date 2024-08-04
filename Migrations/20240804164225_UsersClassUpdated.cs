using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elementium_backend.Migrations
{
    /// <inheritdoc />
    public partial class UsersClassUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntityFrameworkCoremail",
                table: "UsersInfo",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "UsersInfo",
                newName: "EntityFrameworkCoremail");
        }
    }
}
