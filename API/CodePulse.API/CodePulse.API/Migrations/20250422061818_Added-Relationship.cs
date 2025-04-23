using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogPostBlogPostCategory",
                columns: table => new
                {
                    BlogPostCategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogPostsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostBlogPostCategory", x => new { x.BlogPostCategoriesId, x.BlogPostsId });
                    table.ForeignKey(
                        name: "FK_BlogPostBlogPostCategory_BlogPostsCategories_BlogPostCategoriesId",
                        column: x => x.BlogPostCategoriesId,
                        principalTable: "BlogPostsCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostBlogPostCategory_BlogPosts_BlogPostsId",
                        column: x => x.BlogPostsId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostBlogPostCategory_BlogPostsId",
                table: "BlogPostBlogPostCategory",
                column: "BlogPostsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostBlogPostCategory");
        }
    }
}
