using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentNode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Currency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "JobDetails",
                newName: "Currency");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "JobDetails",
                newName: "CurrencyId");
        }
    }
}
