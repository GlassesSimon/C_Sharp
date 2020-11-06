using Microsoft.EntityFrameworkCore.Migrations;

namespace Task2Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Books");

            migrationBuilder.CreateTable(
                name: "Book",
                schema: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Genre = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Novelty = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book",
                schema: "Books");
        }
    }
}
