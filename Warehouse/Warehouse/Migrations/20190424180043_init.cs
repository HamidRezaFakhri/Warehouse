using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Warehouse.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.EnsureSchema(
                name: "SEC");

            migrationBuilder.CreateTable(
                name: "Store",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<byte>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "SEC",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "SEC",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "SEC",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Remittance",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 100, nullable: false),
                    RemittanceType = table.Column<byte>(nullable: false),
                    InDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    StoreId = table.Column<byte>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remittance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Remittance_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "dbo",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Remittance_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "SEC",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stuff",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stuff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stuff_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "SEC",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RemittanceStuff",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RemittanceId = table.Column<long>(nullable: false),
                    StuffId = table.Column<long>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemittanceStuff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemittanceStuff_Remittance_RemittanceId",
                        column: x => x.RemittanceId,
                        principalSchema: "dbo",
                        principalTable: "Remittance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RemittanceStuff_Stuff_StuffId",
                        column: x => x.StuffId,
                        principalSchema: "dbo",
                        principalTable: "Stuff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Remittance_Code",
                schema: "dbo",
                table: "Remittance",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Remittance_StoreId",
                schema: "dbo",
                table: "Remittance",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Remittance_UserId",
                schema: "dbo",
                table: "Remittance",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceStuff_RemittanceId",
                schema: "dbo",
                table: "RemittanceStuff",
                column: "RemittanceId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceStuff_StuffId",
                schema: "dbo",
                table: "RemittanceStuff",
                column: "StuffId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_Name",
                schema: "dbo",
                table: "Store",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stuff_Code",
                schema: "dbo",
                table: "Stuff",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stuff_Name",
                schema: "dbo",
                table: "Stuff",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stuff_UserId",
                schema: "dbo",
                table: "Stuff",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                schema: "SEC",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                schema: "SEC",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                schema: "SEC",
                table: "User",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RemittanceStuff",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Remittance",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Stuff",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Store",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "SEC");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "SEC");
        }
    }
}
