using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RabbitMQ_MicroServices.Transfer.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ToAccount",
                table: "TransferLogs",
                newName: "AccountTo");

            migrationBuilder.RenameColumn(
                name: "FromAccount",
                table: "TransferLogs",
                newName: "AccountFrom");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountTo",
                table: "TransferLogs",
                newName: "ToAccount");

            migrationBuilder.RenameColumn(
                name: "AccountFrom",
                table: "TransferLogs",
                newName: "FromAccount");
        }
    }
}
