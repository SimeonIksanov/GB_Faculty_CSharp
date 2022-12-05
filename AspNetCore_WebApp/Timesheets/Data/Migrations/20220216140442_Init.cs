using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Role = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sheet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContractId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ServiceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sheet_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sheet_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sheet_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sheet_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sheet_ContractId",
                table: "Sheet",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Sheet_EmployeeId",
                table: "Sheet",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sheet_InvoiceId",
                table: "Sheet",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Sheet_ServiceId",
                table: "Sheet",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sheet");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Service");
        }
    }
}
