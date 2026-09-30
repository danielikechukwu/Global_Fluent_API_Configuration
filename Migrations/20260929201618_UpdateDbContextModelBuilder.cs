using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_FluentAPIGlobalConfiguration.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbContextModelBuilder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_items_orders_orderid",
                schema: "sales",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "fk_order_items_products_productid",
                schema: "sales",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_customers_customerid",
                schema: "sales",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_orders_customerid",
                schema: "sales",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_order_items_orderid",
                schema: "sales",
                table: "order_items");

            migrationBuilder.DropIndex(
                name: "ix_order_items_productid",
                schema: "sales",
                table: "order_items");

            migrationBuilder.DropColumn(
                name: "customerid",
                schema: "sales",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "orderid",
                schema: "sales",
                table: "order_items");

            migrationBuilder.DropColumn(
                name: "productid",
                schema: "sales",
                table: "order_items");

            migrationBuilder.CreateIndex(
                name: "ix_orders_customer_id",
                schema: "sales",
                table: "orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id",
                schema: "sales",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_product_id",
                schema: "sales",
                table: "order_items",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_items_orders_order_id",
                schema: "sales",
                table: "order_items",
                column: "order_id",
                principalSchema: "sales",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_order_items_products_product_id",
                schema: "sales",
                table: "order_items",
                column: "product_id",
                principalSchema: "sales",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_orders_customers_customer_id",
                schema: "sales",
                table: "orders",
                column: "customer_id",
                principalSchema: "sales",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_items_orders_order_id",
                schema: "sales",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "fk_order_items_products_product_id",
                schema: "sales",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_customers_customer_id",
                schema: "sales",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_orders_customer_id",
                schema: "sales",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_order_items_order_id",
                schema: "sales",
                table: "order_items");

            migrationBuilder.DropIndex(
                name: "ix_order_items_product_id",
                schema: "sales",
                table: "order_items");

            migrationBuilder.AddColumn<int>(
                name: "customerid",
                schema: "sales",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "orderid",
                schema: "sales",
                table: "order_items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "productid",
                schema: "sales",
                table: "order_items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_orders_customerid",
                schema: "sales",
                table: "orders",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_orderid",
                schema: "sales",
                table: "order_items",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_productid",
                schema: "sales",
                table: "order_items",
                column: "productid");

            migrationBuilder.AddForeignKey(
                name: "fk_order_items_orders_orderid",
                schema: "sales",
                table: "order_items",
                column: "orderid",
                principalSchema: "sales",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_order_items_products_productid",
                schema: "sales",
                table: "order_items",
                column: "productid",
                principalSchema: "sales",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_orders_customers_customerid",
                schema: "sales",
                table: "orders",
                column: "customerid",
                principalSchema: "sales",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
