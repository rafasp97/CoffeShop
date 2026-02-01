using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streamline.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameEntityFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_cntact_customer_CustomerId",
                table: "customer_cntact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customer_cntact",
                table: "customer_cntact");

            migrationBuilder.RenameTable(
                name: "customer_cntact",
                newName: "customer_contact");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customer_contact",
                table: "customer_contact",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_customer_contact_customer_CustomerId",
                table: "customer_contact",
                column: "CustomerId",
                principalTable: "customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_contact_customer_CustomerId",
                table: "customer_contact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customer_contact",
                table: "customer_contact");

            migrationBuilder.RenameTable(
                name: "customer_contact",
                newName: "customer_cntact");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customer_cntact",
                table: "customer_cntact",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_customer_cntact_customer_CustomerId",
                table: "customer_cntact",
                column: "CustomerId",
                principalTable: "customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
