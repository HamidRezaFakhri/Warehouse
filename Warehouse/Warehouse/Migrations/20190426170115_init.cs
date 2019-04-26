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
                name: "sec");

            migrationBuilder.CreateTable(
                name: "Logs",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(nullable: true),
                    MessageTemplate = table.Column<string>(nullable: true),
                    Level = table.Column<string>(maxLength: 128, nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Exception = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(type: "xml", nullable: true),
                    LogEvent = table.Column<string>(nullable: true),
                    OtherData = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    InstanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
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
                    InstanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stuff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "sec",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    InstanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "sec",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    InstanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "sec",
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
                    StoreId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    InstanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
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
                        principalSchema: "sec",
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
                    Count = table.Column<int>(nullable: false),
                    InstanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
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
                name: "IX_Logs_TimeStamp",
                schema: "dbo",
                table: "Logs",
                column: "TimeStamp");

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
                name: "IX_RemittanceStuff_StuffId",
                schema: "dbo",
                table: "RemittanceStuff",
                column: "StuffId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceStuff_RemittanceId_StuffId",
                schema: "dbo",
                table: "RemittanceStuff",
                columns: new[] { "RemittanceId", "StuffId" },
                unique: true);

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
                name: "IX_Role_Name",
                schema: "sec",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                schema: "sec",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                schema: "sec",
                table: "User",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs",
                schema: "dbo");

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
                schema: "sec");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "sec");
        }
    }
}
