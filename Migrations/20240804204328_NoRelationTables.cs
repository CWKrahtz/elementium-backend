using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace elementium_backend.Migrations
{
    /// <inheritdoc />
    public partial class NoRelationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transaction_users_UserId",
                table: "transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_transaction",
                table: "transaction");

            migrationBuilder.DropIndex(
                name: "IX_transaction_UserId",
                table: "transaction");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "transaction");

            migrationBuilder.RenameTable(
                name: "transaction",
                newName: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Transaction_type",
                table: "Transactions",
                newName: "TransactionType");

            migrationBuilder.RenameColumn(
                name: "To_account_id",
                table: "Transactions",
                newName: "ToAccountId");

            migrationBuilder.RenameColumn(
                name: "From_account_id",
                table: "Transactions",
                newName: "Timestamp");

            migrationBuilder.AddColumn<string>(
                name: "FromAccountId",
                table: "Transactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "TransactionId");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Balance = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AccountStatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "AuthenticationLogs",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LoginTime = table.Column<int>(type: "integer", nullable: false),
                    LogoutTime = table.Column<int>(type: "integer", nullable: false),
                    IpAddress = table.Column<int>(type: "integer", nullable: false),
                    DeviceInfo = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthenticationLogs", x => x.LogId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AuthenticationLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FromAccountId",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "transaction");

            migrationBuilder.RenameColumn(
                name: "TransactionType",
                table: "transaction",
                newName: "Transaction_type");

            migrationBuilder.RenameColumn(
                name: "ToAccountId",
                table: "transaction",
                newName: "To_account_id");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "transaction",
                newName: "From_account_id");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "transaction",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_transaction",
                table: "transaction",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_UserId",
                table: "transaction",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_transaction_users_UserId",
                table: "transaction",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId");
        }
    }
}
