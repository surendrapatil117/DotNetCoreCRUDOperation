using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCoreCRUDOperation.Data.Migrations
{
    public partial class Initial05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contactno",
                table: "Employees",
                newName: "ContactNo");

            migrationBuilder.CreateTable(
                name: "Employeeautoids",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pincodee = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employeeautoids", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employeeautoids");

            migrationBuilder.RenameColumn(
                name: "ContactNo",
                table: "Employees",
                newName: "Contactno");
        }
    }
}
