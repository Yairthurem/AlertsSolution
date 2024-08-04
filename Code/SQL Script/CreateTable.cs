using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightAlerts.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    AlertId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    FlightAbbrevation = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IsReturn = table.Column<bool>(type: "bit", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.AlertId);
                });

            migrationBuilder.CreateIndex(
            name: "IX_Alerts_FlightAbbrevation_DepartureDate_RequestedPrice",
            table: "Alerts",
            columns: new[] { "FlightAbbrevation", "DepartureDate", "RequestedPrice" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerts");
        }
    }
}
