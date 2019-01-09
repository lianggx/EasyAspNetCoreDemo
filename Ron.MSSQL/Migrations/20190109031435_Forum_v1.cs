using Microsoft.EntityFrameworkCore.Migrations;

namespace Ron.MSSQL.Migrations
{
    public partial class Forum_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Topics");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Topics",
                nullable: true);
        }
    }
}
