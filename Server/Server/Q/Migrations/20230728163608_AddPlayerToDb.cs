using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Q.Migrations
{
    public partial class AddPlayerToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RandomStep",
                table: "TblPlayers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RandomStep",
                table: "TblPlayers");
        }
    }
}
