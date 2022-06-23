using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kolokwium2.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationName = table.Column<string>(nullable: false),
                    OrganizationDomain = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationID);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationID = table.Column<int>(nullable: false),
                    MemberName = table.Column<string>(nullable: false),
                    MemberSurname = table.Column<string>(nullable: false),
                    MemberNickName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberID);
                    table.ForeignKey(
                        name: "FK_Members_Organizations_OrganizationID",
                        column: x => x.OrganizationID,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationID = table.Column<int>(nullable: false),
                    TeamName = table.Column<string>(nullable: false),
                    TeamDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamID);
                    table.ForeignKey(
                        name: "FK_Teams_Organizations_OrganizationID",
                        column: x => x.OrganizationID,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileID = table.Column<int>(nullable: false),
                    TeamID = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: false),
                    FileExtension = table.Column<string>(nullable: false),
                    FileSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => new { x.FileID, x.TeamID });
                    table.ForeignKey(
                        name: "FK_Files_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    MemberID = table.Column<int>(nullable: false),
                    TeamID = table.Column<int>(nullable: false),
                    MembershipDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => new { x.MemberID, x.TeamID });
                    table.ForeignKey(
                        name: "FK_Memberships_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Memberships_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_TeamID",
                table: "Files",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Members_OrganizationID",
                table: "Members",
                column: "OrganizationID");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_TeamID",
                table: "Memberships",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_OrganizationID",
                table: "Teams",
                column: "OrganizationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
