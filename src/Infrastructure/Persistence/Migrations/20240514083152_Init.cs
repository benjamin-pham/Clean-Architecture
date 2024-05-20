using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "User",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                FirstName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                LastName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                Username = table.Column<string>(type: "character varying(250)", unicode: false, maxLength: 250, nullable: false),
                PasswordHash = table.Column<string>(type: "character varying(250)", unicode: false, maxLength: 250, nullable: false),
                Email = table.Column<string>(type: "character varying(250)", unicode: false, maxLength: 250, nullable: false),
                PhoneNumber = table.Column<string>(type: "character varying(250)", unicode: false, maxLength: 250, nullable: true),
                CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                UpdatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                DeletedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_User", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_User_Email",
            table: "User",
            column: "Email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_User_Username",
            table: "User",
            column: "Username",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "User");
    }
}
