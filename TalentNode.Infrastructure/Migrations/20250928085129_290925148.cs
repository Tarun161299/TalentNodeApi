using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentNode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _290925148 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleMapping",
                table: "UserRoleMapping");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "UserRoleMapping");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleMapping",
                table: "UserRoleMapping");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "UserRoleMapping");

            migrationBuilder.DropColumn(
                name: "Roleid",
                table: "MdMainModule");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "UserRoleMapping",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleMapping",
                table: "UserRoleMapping",
                columns: new[] { "UserName", "RoleName" });
        }
    }
}
