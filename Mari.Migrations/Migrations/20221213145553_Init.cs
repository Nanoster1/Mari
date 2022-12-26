using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mari.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "platform",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_platform", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    isactive = table.Column<bool>(name: "is_active", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "releases",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    versionmajor = table.Column<int>(name: "version_major", type: "integer", nullable: false),
                    versionminor = table.Column<int>(name: "version_minor", type: "integer", nullable: false),
                    versionpatch = table.Column<int>(name: "version_patch", type: "integer", nullable: false),
                    platformid = table.Column<int>(name: "platform_id", type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    completedate = table.Column<DateTimeOffset>(name: "complete_date", type: "timestamp with time zone", nullable: false),
                    updatedate = table.Column<DateTimeOffset>(name: "update_date", type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    mainissue = table.Column<string>(name: "main_issue", type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_releases", x => x.id);
                    table.ForeignKey(
                        name: "fk_releases_platform_platform_temp_id",
                        column: x => x.platformid,
                        principalTable: "platform",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    userid = table.Column<string>(name: "user_id", type: "text", nullable: false),
                    releaseid = table.Column<Guid>(name: "release_id", type: "uuid", nullable: false),
                    createdate = table.Column<DateTimeOffset>(name: "create_date", type: "timestamp with time zone", nullable: false),
                    isredacted = table.Column<bool>(name: "is_redacted", type: "boolean", nullable: false),
                    issystem = table.Column<bool>(name: "is_system", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comments", x => x.id);
                    table.ForeignKey(
                        name: "fk_comments_releases_release_temp_id",
                        column: x => x.releaseid,
                        principalTable: "releases",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comments_users_user_temp_id",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_comments_release_id",
                table: "comments",
                column: "release_id");

            migrationBuilder.CreateIndex(
                name: "ix_comments_user_id",
                table: "comments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_platform_name",
                table: "platform",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_releases_platform_id",
                table: "releases",
                column: "platform_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "releases");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "platform");
        }
    }
}
