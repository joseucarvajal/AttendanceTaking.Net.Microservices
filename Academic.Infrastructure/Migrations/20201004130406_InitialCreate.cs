using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Academic.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "academic");

            migrationBuilder.CreateTable(
                name: "coursegroup",
                schema: "academic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coursegroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "courses",
                schema: "academic",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 15, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    CourseGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_courses_coursegroup_CourseGroupId",
                        column: x => x.CourseGroupId,
                        principalSchema: "academic",
                        principalTable: "coursegroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_courses_CourseGroupId",
                schema: "academic",
                table: "courses",
                column: "CourseGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courses",
                schema: "academic");

            migrationBuilder.DropTable(
                name: "coursegroup",
                schema: "academic");
        }
    }
}
