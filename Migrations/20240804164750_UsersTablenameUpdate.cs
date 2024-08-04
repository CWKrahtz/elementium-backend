using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elementium_backend.Migrations
{
    /// <inheritdoc />
    public partial class UsersTablenameUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersInfo",
                table: "UsersInfo");

            migrationBuilder.RenameTable(
                name: "UsersInfo",
                newName: "users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "UsersInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersInfo",
                table: "UsersInfo",
                column: "UserId");
        }
    }
}
