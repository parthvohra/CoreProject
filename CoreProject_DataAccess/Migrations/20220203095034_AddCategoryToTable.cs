using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreProject_DataAccess.Migrations
{
    public partial class AddCategoryToTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into tbl_Category values('Cat 1')");
            migrationBuilder.Sql("insert into tbl_Category values('Cat 2')");
            migrationBuilder.Sql("insert into tbl_Category values('Cat 3')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
