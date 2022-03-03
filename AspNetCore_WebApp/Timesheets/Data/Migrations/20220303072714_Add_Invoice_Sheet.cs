using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Add_Invoice_Sheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sheet_Contract_ContractId",
                table: "Sheet");

            migrationBuilder.DropForeignKey(
                name: "FK_Sheet_Employees_EmployeeId",
                table: "Sheet");

            migrationBuilder.DropForeignKey(
                name: "FK_Sheet_Invoice_InvoiceId",
                table: "Sheet");

            migrationBuilder.DropForeignKey(
                name: "FK_Sheet_Service_ServiceId",
                table: "Sheet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sheet",
                table: "Sheet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice");

            migrationBuilder.RenameTable(
                name: "Sheet",
                newName: "Sheets");

            migrationBuilder.RenameTable(
                name: "Invoice",
                newName: "Invoices");

            migrationBuilder.RenameIndex(
                name: "IX_Sheet_ServiceId",
                table: "Sheets",
                newName: "IX_Sheets_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Sheet_InvoiceId",
                table: "Sheets",
                newName: "IX_Sheets_InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Sheet_EmployeeId",
                table: "Sheets",
                newName: "IX_Sheets_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Sheet_ContractId",
                table: "Sheets",
                newName: "IX_Sheets_ContractId");

            migrationBuilder.AddColumn<Guid>(
                name: "ContractId",
                table: "Invoices",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "Invoices",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "Invoices",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sheets",
                table: "Sheets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ContractId",
                table: "Invoices",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Contract_ContractId",
                table: "Invoices",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sheets_Contract_ContractId",
                table: "Sheets",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sheets_Employees_EmployeeId",
                table: "Sheets",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sheets_Invoices_InvoiceId",
                table: "Sheets",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sheets_Service_ServiceId",
                table: "Sheets",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Contract_ContractId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Sheets_Contract_ContractId",
                table: "Sheets");

            migrationBuilder.DropForeignKey(
                name: "FK_Sheets_Employees_EmployeeId",
                table: "Sheets");

            migrationBuilder.DropForeignKey(
                name: "FK_Sheets_Invoices_InvoiceId",
                table: "Sheets");

            migrationBuilder.DropForeignKey(
                name: "FK_Sheets_Service_ServiceId",
                table: "Sheets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sheets",
                table: "Sheets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ContractId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "Invoices");

            migrationBuilder.RenameTable(
                name: "Sheets",
                newName: "Sheet");

            migrationBuilder.RenameTable(
                name: "Invoices",
                newName: "Invoice");

            migrationBuilder.RenameIndex(
                name: "IX_Sheets_ServiceId",
                table: "Sheet",
                newName: "IX_Sheet_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Sheets_InvoiceId",
                table: "Sheet",
                newName: "IX_Sheet_InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Sheets_EmployeeId",
                table: "Sheet",
                newName: "IX_Sheet_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Sheets_ContractId",
                table: "Sheet",
                newName: "IX_Sheet_ContractId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sheet",
                table: "Sheet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sheet_Contract_ContractId",
                table: "Sheet",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sheet_Employees_EmployeeId",
                table: "Sheet",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sheet_Invoice_InvoiceId",
                table: "Sheet",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sheet_Service_ServiceId",
                table: "Sheet",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
