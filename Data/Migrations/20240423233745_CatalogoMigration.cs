using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trabajo_final_grupo_verde.Data.Migrations
{
    /// <inheritdoc />
    public partial class CatalogoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "t_producto",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "t_producto");
        }
    }
}
