using Microsoft.EntityFrameworkCore.Migrations;

namespace WebLanchesMVC.Migrations
{
    public partial class fillTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("INSERT INTO Categories(Name,Description) VALUES('Normal','Lanche feito com ingredientes normais')");
			migrationBuilder.Sql("INSERT INTO Categories(Name,Description) VALUES('Natural','Lanche feito com ingredientes integrais e naturais')");

			migrationBuilder.Sql("INSERT INTO Lunches(CategoryId,DescriptionShort,DescriptionDetail,InStock,ImageThumbnailURL,ImageURL,IsPreferred,Name,Price) VALUES((SELECT CategoryId FROM Categories Where Name='Normal'),'Pão, hambúrger, ovo, presunto, queijo e batata palha','Delicioso pão de hambúrger com ovo frito; presunto e queijo de primeira qualidade acompanhado com batata palha',1, 'https://www.flickr.com/photos/190560719@N06/50437520096','https://www.flickr.com/photos/190560719@N06/50437520096', 0 ,'Cheese Salada', 12.50)");

            migrationBuilder.Sql("INSERT INTO Lunches(CategoryId,DescriptionShort,DescriptionDetail,InStock,ImageThumbnailURL,ImageURL,IsPreferred,Name,Price) VALUES((SELECT CategoryId FROM Categories Where Name='Normal'),'Pão, presunto, mussarela e tomate','Delicioso pão francês quentinho na chapa com presunto e mussarela bem servidos com tomate preparado com carinho.',1,'https://www.flickr.com/photos/190560719@N06/50437520036','https://www.flickr.com/photos/190560719@N06/50437520036',0,'Misto Quente', 8.00)");

            migrationBuilder.Sql("INSERT INTO Lunches(CategoryId,DescriptionShort,DescriptionDetail,InStock,ImageThumbnailURL,ImageURL,IsPreferred,Name,Price) VALUES((SELECT CategoryId FROM Categories Where Name='Natural'),'Pão, hambúrger, presunto, mussarela e batalha palha','Pão de hambúrger especial com hambúrger de nossa preparação e presunto e mussarela; acompanha batata palha.',2,'https://www.flickr.com/photos/190560719@N06/50436827088','https://www.flickr.com/photos/190560719@N06/50436827088',1,'Cheese Burger', 11.00)");

            migrationBuilder.Sql("INSERT INTO Lunches(CategoryId,DescriptionShort,DescriptionDetail,InStock,ImageThumbnailURL,ImageURL,IsPreferred,Name,Price) VALUES((SELECT CategoryId FROM Categories Where Name='Natural'),'Pão Integral, queijo branco, peito de peru, cenoura, alface, iogurte','Pão integral natural com queijo branco, peito de peru e cenoura ralada com alface picado e iorgurte natural.',2,'https://www.flickr.com/photos/190560719@N06/50437520061','https://www.flickr.com/photos/190560719@N06/50437520061',0,'Lanche Natural Peito Peru', 15.00)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DELETE FROM Categories");
			migrationBuilder.Sql("DELETE FROM Lunches");
        }
    }
}
