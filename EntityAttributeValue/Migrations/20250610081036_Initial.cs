using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityAttributeValue.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttributesInt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributesInt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributesString",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributesString", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityAttributeValuesInt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    AttributeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityAttributeValuesInt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityAttributeValuesInt_AttributesInt_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "AttributesInt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityAttributeValuesInt_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityAttributeValuesString",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    AttributeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityAttributeValuesString", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityAttributeValuesString_AttributesString_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "AttributesString",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityAttributeValuesString_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityAttributeValuesInt_AttributeId",
                table: "EntityAttributeValuesInt",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityAttributeValuesInt_EntityId",
                table: "EntityAttributeValuesInt",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityAttributeValuesString_AttributeId",
                table: "EntityAttributeValuesString",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityAttributeValuesString_EntityId",
                table: "EntityAttributeValuesString",
                column: "EntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityAttributeValuesInt");

            migrationBuilder.DropTable(
                name: "EntityAttributeValuesString");

            migrationBuilder.DropTable(
                name: "AttributesInt");

            migrationBuilder.DropTable(
                name: "AttributesString");

            migrationBuilder.DropTable(
                name: "Entities");
        }
    }
}
