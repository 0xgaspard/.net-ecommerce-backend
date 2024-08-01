using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotNetEcommerceBackend.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Role", "Username" },
                values: new object[] { 1, "admin@ecommerce.com", "$2a$11$RfkEEuEg10m9Fgj9OUgA9eCPpRVzGzip60wT4qiUMnfK613tj1t9W", 0, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
