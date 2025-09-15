using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalRApi.Migrations
{
    /// <inheritdoc />
    public partial class mig_edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_visitors",
                table: "visitors");

            migrationBuilder.RenameTable(
                name: "visitors",
                newName: "Visitors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visitors",
                table: "Visitors",
                column: "VisitorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Visitors",
                table: "Visitors");

            migrationBuilder.RenameTable(
                name: "Visitors",
                newName: "visitors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_visitors",
                table: "visitors",
                column: "VisitorID");
        }
    }
}
