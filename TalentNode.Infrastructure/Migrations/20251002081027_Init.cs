using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentNode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleMapping",
                table: "UserRoleMapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MdMainModule",
                table: "MdMainModule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "UserRoleMapping");

            migrationBuilder.DropColumn(
                name: "ChildModuleIDs",
                table: "MdMainModule");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "SignupDetails");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "UserRoleMapping",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Roleid",
                table: "MdMainModule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleMapping",
                table: "UserRoleMapping",
                columns: new[] { "UserName", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MdMainModule",
                table: "MdMainModule",
                columns: new[] { "Roleid", "MainModuleID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SignupDetails",
                table: "SignupDetails",
                column: "Id");

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
                name: "PK_UserRoleMapping",
                table: "UserRoleMapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MdMainModule",
                table: "MdMainModule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SignupDetails",
                table: "SignupDetails");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "UserRoleMapping");

            migrationBuilder.DropColumn(
                name: "Roleid",
                table: "MdMainModule");

            migrationBuilder.RenameTable(
                name: "SignupDetails",
                newName: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "UserRoleMapping",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChildModuleIDs",
                table: "MdMainModule",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleMapping",
                table: "UserRoleMapping",
                columns: new[] { "UserName", "RoleName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MdMainModule",
                table: "MdMainModule",
                column: "MainModuleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");
        }
    }
}
