using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "asesores",
                columns: table => new
                {
                    Id_Asesor = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AniosExperiencia = table.Column<int>(type: "int", nullable: false),
                    Especialidad = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Idiomas = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FechaNac = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaReg = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asesores", x => x.Id_Asesor);
                });

            migrationBuilder.CreateTable(
                name: "autos",
                columns: table => new
                {
                    Id_Auto = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Anio = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaReg = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_autos", x => x.Id_Auto);
                });

            migrationBuilder.CreateTable(
                name: "estadoUsado",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaReg = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estadoUsado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "horarios",
                columns: table => new
                {
                    Id_Horario = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hora = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaReg = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_horarios", x => x.Id_Horario);
                });

            migrationBuilder.CreateTable(
                name: "nuevos",
                columns: table => new
                {
                    Id_Nuevo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Auto = table.Column<long>(type: "bigint", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Edicion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaReg = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nuevos", x => x.Id_Nuevo);
                    table.ForeignKey(
                        name: "FK_nuevos_autos_Id_Auto",
                        column: x => x.Id_Auto,
                        principalTable: "autos",
                        principalColumn: "Id_Auto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usados",
                columns: table => new
                {
                    Id_Usado = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Auto = table.Column<long>(type: "bigint", nullable: false),
                    NombreVendedor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstadoUsadoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaReg = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usados", x => x.Id_Usado);
                    table.ForeignKey(
                        name: "FK_usados_autos_Id_Auto",
                        column: x => x.Id_Auto,
                        principalTable: "autos",
                        principalColumn: "Id_Auto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usados_estadoUsado_EstadoUsadoId",
                        column: x => x.EstadoUsadoId,
                        principalTable: "estadoUsado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "citas",
                columns: table => new
                {
                    Id_Cita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsesorId_Asesor = table.Column<long>(type: "bigint", nullable: false),
                    HorarioId_Horario = table.Column<long>(type: "bigint", nullable: false),
                    AutoId_Auto = table.Column<long>(type: "bigint", nullable: false),
                    NombresCliente = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaReg = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_citas", x => x.Id_Cita);
                    table.ForeignKey(
                        name: "FK_citas_asesores_AsesorId_Asesor",
                        column: x => x.AsesorId_Asesor,
                        principalTable: "asesores",
                        principalColumn: "Id_Asesor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_citas_autos_AutoId_Auto",
                        column: x => x.AutoId_Auto,
                        principalTable: "autos",
                        principalColumn: "Id_Auto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_citas_horarios_HorarioId_Horario",
                        column: x => x.HorarioId_Horario,
                        principalTable: "horarios",
                        principalColumn: "Id_Horario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_citas_AsesorId_Asesor",
                table: "citas",
                column: "AsesorId_Asesor");

            migrationBuilder.CreateIndex(
                name: "IX_citas_AutoId_Auto",
                table: "citas",
                column: "AutoId_Auto");

            migrationBuilder.CreateIndex(
                name: "IX_citas_HorarioId_Horario",
                table: "citas",
                column: "HorarioId_Horario");

            migrationBuilder.CreateIndex(
                name: "IX_nuevos_Id_Auto",
                table: "nuevos",
                column: "Id_Auto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usados_EstadoUsadoId",
                table: "usados",
                column: "EstadoUsadoId");

            migrationBuilder.CreateIndex(
                name: "IX_usados_Id_Auto",
                table: "usados",
                column: "Id_Auto",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "citas");

            migrationBuilder.DropTable(
                name: "nuevos");

            migrationBuilder.DropTable(
                name: "usados");

            migrationBuilder.DropTable(
                name: "asesores");

            migrationBuilder.DropTable(
                name: "horarios");

            migrationBuilder.DropTable(
                name: "autos");

            migrationBuilder.DropTable(
                name: "estadoUsado");
        }
    }
}
