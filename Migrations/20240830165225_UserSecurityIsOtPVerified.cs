using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elementium_backend.Migrations
{
    /// <inheritdoc />
    public partial class UserSecurityIsOtPVerified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOtpVerified",
                table: "user_security",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    Email = table.Column<string>(type: "text", nullable: false),
                    Passowrd = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Register",
                columns: table => new
                {
                    Email = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Created_at = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register", x => x.Email);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "Register");

            migrationBuilder.DropColumn(
                name: "IsOtpVerified",
                table: "user_security");
        }
    }
}
