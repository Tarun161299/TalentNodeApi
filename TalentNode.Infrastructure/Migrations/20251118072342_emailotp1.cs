using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentNode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class emailotp1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailTemplate",
                table: "EmailTemplate");

            migrationBuilder.RenameTable(
                name: "EmailTemplate",
                newName: "EmailTemplates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailTemplates",
                table: "EmailTemplates",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EmailOTP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailOTP", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailOTP");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailTemplates",
                table: "EmailTemplates");

            migrationBuilder.RenameTable(
                name: "EmailTemplates",
                newName: "EmailTemplate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailTemplate",
                table: "EmailTemplate",
                column: "Id");
        }
    }
}
