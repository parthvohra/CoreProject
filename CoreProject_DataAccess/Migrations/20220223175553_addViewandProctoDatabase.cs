using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreProject_DataAccess.Migrations
{
    public partial class addViewandProctoDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER proc proc_GetBookDetails
                                   @bookId INT 
                                   AS 
                                   Set NOCOUNT ON; 
                                   Select * from Books where Book_Id=@bookId");

            migrationBuilder.Sql(@"CREATE OR ALTER View GetAllBookDetails AS Select m.ISBN,m.Title,m.Price from dbo.Books as m");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP View GetAllBookDetails");
            migrationBuilder.Sql(@"DROP procudere proc_GetBookDetails ");
        }
    }
}
