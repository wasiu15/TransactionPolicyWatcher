using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolicyWatcher.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsIntervalChecked",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIntervalChecked",
                table: "Transactions");
        }
    }
}
