using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace identityWithChristina.Migrations
{
    public partial class IntialIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.RenameColumn(
                name: "Ssn",
                table: "AspNetUsers",
                newName: "SSN");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                newName: "ProfilePictureURL");

            migrationBuilder.AlterDatabase(
                oldCollation: "SQL_Latin1_General_CP1_CI_AS");

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePictureURL",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Associations",
                columns: table => new
                {
                    ASSId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ASSName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AssDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AssAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AssPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AssLogoURL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associations", x => x.ASSId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PhotoURL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    GaveUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TookUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => new { x.GaveUserID, x.TookUserID });
                    table.ForeignKey(
                        name: "GaveFeed",
                        column: x => x.GaveUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "TookFeed",
                        column: x => x.TookUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GeneralFeedbacks",
                columns: table => new
                {
                    FeedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralFeedbacks", x => x.FeedID);
                    table.ForeignKey(
                        name: "SiteFeedbacks",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ShipDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ShipAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TotalPoints = table.Column<int>(type: "int", nullable: false),
                    NumberOfProducts = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "Ordered",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PhotoURL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    OwnerUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ExchangationUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DonationAssID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "Donation",
                        column: x => x.DonationAssID,
                        principalTable: "Associations",
                        principalColumn: "ASSId");
                    table.ForeignKey(
                        name: "Exchange",
                        column: x => x.ExchangationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Categories",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "Owner",
                        column: x => x.OwnerUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OdrerId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PointsPerUnite = table.Column<int>(type: "int", nullable: false),
                    disCount = table.Column<double>(type: "float", nullable: false),
                    NetPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OdrerId, x.ProductId });
                    table.ForeignKey(
                        name: "Details",
                        column: x => x.OdrerId,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_TookUserID",
                table: "Feedbacks",
                column: "TookUserID");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralFeedbacks_UserID",
                table: "GeneralFeedbacks",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID",
                table: "Orders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DonationAssID",
                table: "Products",
                column: "DonationAssID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ExchangationUserID",
                table: "Products",
                column: "ExchangationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OwnerUserID",
                table: "Products",
                column: "OwnerUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "GeneralFeedbacks");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Associations");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SSN",
                table: "AspNetUsers",
                newName: "Ssn");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureURL",
                table: "AspNetUsers",
                newName: "ProfilePictureUrl");

            migrationBuilder.AlterDatabase(
                collation: "SQL_Latin1_General_CP1_CI_AS");

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Dept_Id = table.Column<int>(type: "int", nullable: false),
                    Dept_Desc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Dept_Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Dept_Manager = table.Column<int>(type: "int", nullable: true),
                    Dept_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Manager_hiredate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Dept_Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    St_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dept_Id = table.Column<int>(type: "int", nullable: true),
                    St_super = table.Column<int>(type: "int", nullable: true),
                    St_Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    St_Age = table.Column<int>(type: "int", nullable: true),
                    St_Fname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    St_Lname = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.St_Id);
                    table.ForeignKey(
                        name: "FK_Student_Department",
                        column: x => x.Dept_Id,
                        principalTable: "Department",
                        principalColumn: "Dept_Id");
                    table.ForeignKey(
                        name: "FK_Student_Student",
                        column: x => x.St_super,
                        principalTable: "Student",
                        principalColumn: "St_Id");
                });

            migrationBuilder.CreateIndex(
                name: "i55",
                table: "Student",
                column: "St_Age",
                unique: true,
                filter: "[St_Age] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Dept_Id",
                table: "Student",
                column: "Dept_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_St_super",
                table: "Student",
                column: "St_super");
        }
    }
}
