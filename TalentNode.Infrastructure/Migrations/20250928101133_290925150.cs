using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentNode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _290925150 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MdMainModule",
                table: "MdMainModule");

            migrationBuilder.DropColumn(
                name: "ChildModuleIDs",
                table: "MdMainModule");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MdMainModule",
                table: "MdMainModule",
                columns: new[] { "Roleid", "MainModuleID" });

            migrationBuilder.CreateTable(
                name: "MdRoleModule",
                columns: table => new
                {
                    MainModuleID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModuleID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MdRoleModule", x => new { x.MainModuleID, x.ModuleID });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MdRoleModule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MdMainModule",
                table: "MdMainModule");

            migrationBuilder.AddColumn<string>(
                name: "ChildModuleIDs",
                table: "MdMainModule",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MdMainModule",
                table: "MdMainModule",
                columns: new[] { "Roleid", "ChildModuleIDs", "MainModuleID" });
        }
    }
}
