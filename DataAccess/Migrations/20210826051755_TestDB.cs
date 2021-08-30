using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class TestDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PizzeriaHalls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzeriaHalls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<double>(maxLength: 5, nullable: false),
                    IdItemType = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ItemTypes_IdItemType",
                        column: x => x.IdItemType,
                        principalTable: "ItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Name = table.Column<string>(nullable: true),
                    IdPizzeriaHall = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tables_PizzeriaHalls_IdPizzeriaHall",
                        column: x => x.IdPizzeriaHall,
                        principalTable: "PizzeriaHalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    FirstName = table.Column<string>(maxLength: 15, nullable: false),
                    LastName = table.Column<string>(maxLength: 15, nullable: false),
                    IdRole = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendants_Roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    IdTable = table.Column<int>(nullable: false),
                    IdAttendant = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false, defaultValue: true),
                    IsPaid = table.Column<bool>(nullable: false, defaultValue: false),
                    IsCancelled = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Attendants_IdAttendant",
                        column: x => x.IdAttendant,
                        principalTable: "Attendants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Tables_IdTable",
                        column: x => x.IdTable,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    IdOrder = table.Column<int>(nullable: false),
                    IdItem = table.Column<int>(nullable: false),
                    ItemsNumber = table.Column<int>(nullable: false),
                    ItemPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.IdItem, x.IdOrder });
                    table.ForeignKey(
                        name: "FK_OrderItems_Items_IdItem",
                        column: x => x.IdItem,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_IdOrder",
                        column: x => x.IdOrder,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ItemTypes",
                columns: new[] { "Id", "ModifiedAt", "Name" },
                values: new object[] { 1, null, "Pica" });

            migrationBuilder.InsertData(
                table: "PizzeriaHalls",
                columns: new[] { "Id", "ModifiedAt", "Name" },
                values: new object[] { 1, null, "Basta" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ModifiedAt", "Name" },
                values: new object[] { 1, null, "Menadzer" });

            migrationBuilder.InsertData(
                table: "Attendants",
                columns: new[] { "Id", "Email", "FirstName", "IdRole", "LastName", "ModifiedAt", "Password" },
                values: new object[] { 1, "petar_petrovic@gmail.com", "Petar", 1, "Petrovic", null, "attendant01!" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "IdItemType", "Image", "ModifiedAt", "Name", "Price" },
                values: new object[] { 1, 1, null, null, "Cezar", 500.0 });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "IdPizzeriaHall", "ModifiedAt", "Name" },
                values: new object[] { 1, 1, null, "Sto1" });

            migrationBuilder.CreateIndex(
                name: "IX_Attendants_IdRole",
                table: "Attendants",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_Items_IdItemType",
                table: "Items",
                column: "IdItemType");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name",
                table: "Items",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_Name",
                table: "ItemTypes",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_IdOrder",
                table: "OrderItems",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdAttendant",
                table: "Orders",
                column: "IdAttendant");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdTable",
                table: "Orders",
                column: "IdTable");

            migrationBuilder.CreateIndex(
                name: "IX_PizzeriaHalls_Name",
                table: "PizzeriaHalls",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_IdPizzeriaHall",
                table: "Tables",
                column: "IdPizzeriaHall");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_Name",
                table: "Tables",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ItemTypes");

            migrationBuilder.DropTable(
                name: "Attendants");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "PizzeriaHalls");
        }
    }
}
