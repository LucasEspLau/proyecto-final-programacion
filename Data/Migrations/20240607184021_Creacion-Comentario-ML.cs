using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trabajo_final_grupo_verde.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreacionComentarioML : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comentario",
                table: "t_contacto",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comentario",
                table: "t_contacto");
        }
    }
}
