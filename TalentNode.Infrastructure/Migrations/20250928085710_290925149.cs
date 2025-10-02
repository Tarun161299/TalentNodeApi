using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentNode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _290925149 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MdMainModule",
                table: "MdMainModule");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MdMainModule",
                table: "MdMainModule",
                columns: new[] { "Roleid", "ChildModuleIDs", "MainModuleID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MdMainModule",
                table: "MdMainModule");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MdMainModule",
                table: "MdMainModule",
                column: "MainModuleID");
        }
    }
}
