using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streamline.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresses_Customer_CustomerId",
                table: "CustomerAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContacts_Customer_CustomerId",
                table: "CustomerContacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerContacts",
                table: "CustomerContacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerAddresses",
                table: "CustomerAddresses");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "product");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "customer");

            migrationBuilder.RenameTable(
                name: "CustomerContacts",
                newName: "customer_cntact");

            migrationBuilder.RenameTable(
                name: "CustomerAddresses",
                newName: "customer_address");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product",
                table: "product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customer",
                table: "customer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customer_cntact",
                table: "customer_cntact",
                column: "CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customer_address",
                table: "customer_address",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_customer_address_customer_CustomerId",
                table: "customer_address",
                column: "CustomerId",
                principalTable: "customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_customer_cntact_customer_CustomerId",
                table: "customer_cntact",
                column: "CustomerId",
                principalTable: "customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_address_customer_CustomerId",
                table: "customer_address");

            migrationBuilder.DropForeignKey(
                name: "FK_customer_cntact_customer_CustomerId",
                table: "customer_cntact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product",
                table: "product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customer",
                table: "customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customer_cntact",
                table: "customer_cntact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customer_address",
                table: "customer_address");

            migrationBuilder.RenameTable(
                name: "product",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "customer",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "customer_cntact",
                newName: "CustomerContacts");

            migrationBuilder.RenameTable(
                name: "customer_address",
                newName: "CustomerAddresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerContacts",
                table: "CustomerContacts",
                column: "CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerAddresses",
                table: "CustomerAddresses",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresses_Customer_CustomerId",
                table: "CustomerAddresses",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContacts_Customer_CustomerId",
                table: "CustomerContacts",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
