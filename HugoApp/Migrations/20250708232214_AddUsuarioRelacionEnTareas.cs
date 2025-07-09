using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace taskmanager_webservice.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioRelacionEnTareas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tareas",
                table: "Tareas");

            migrationBuilder.RenameTable(
                name: "Tareas",
                newName: "tareas");

            migrationBuilder.RenameColumn(
                name: "correoElectronico",
                table: "usuarios",
                newName: "correoelectronico");

            migrationBuilder.RenameColumn(
                name: "contraseña",
                table: "usuarios",
                newName: "contrasena");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "tareas",
                newName: "titulo");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "tareas",
                newName: "descripcion");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tareas",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "EstaCompletada",
                table: "tareas",
                newName: "esta_completada");

            migrationBuilder.AddColumn<int>(
                name: "usuario_id",
                table: "tareas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tareas",
                table: "tareas",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_tareas_usuario_id",
                table: "tareas",
                column: "usuario_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tareas_usuarios_usuario_id",
                table: "tareas",
                column: "usuario_id",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tareas_usuarios_usuario_id",
                table: "tareas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tareas",
                table: "tareas");

            migrationBuilder.DropIndex(
                name: "IX_tareas_usuario_id",
                table: "tareas");

            migrationBuilder.DropColumn(
                name: "usuario_id",
                table: "tareas");

            migrationBuilder.RenameTable(
                name: "tareas",
                newName: "Tareas");

            migrationBuilder.RenameColumn(
                name: "correoelectronico",
                table: "usuarios",
                newName: "correoElectronico");

            migrationBuilder.RenameColumn(
                name: "contrasena",
                table: "usuarios",
                newName: "contraseña");

            migrationBuilder.RenameColumn(
                name: "titulo",
                table: "Tareas",
                newName: "Titulo");

            migrationBuilder.RenameColumn(
                name: "descripcion",
                table: "Tareas",
                newName: "Descripcion");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Tareas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "esta_completada",
                table: "Tareas",
                newName: "EstaCompletada");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tareas",
                table: "Tareas",
                column: "Id");
        }
    }
}
