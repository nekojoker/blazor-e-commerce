using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorEC.Server.Migrations
{
    public partial class AddProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Title", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "PlayFab の勉強を始めるにあたって、どこからどういった順番で学習をしていくのかがわからないと思います。いろんな機能がある中で、難しい機能から始めてしまうと、理解が難しくやめてしまう原因にもなります。そこで本書では、理解がしやすい以下の機能をピックアップして、入門編としました。", "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/1.png", "猫でもわかるPlayFab入門", 1000m },
                    { 2, "PlayFabを使いこなす上で自動化まわりの機能の理解は欠かせません。しかしながら、自動化まわりの機能は便利ですが学習コストが高く、なかなか手を出しにくい分野であることも事実です。", "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/2.png", "猫でもわかるPlayFab自動化編", 1100m },
                    { 3, "フレンド機能やドロップテーブルなど、ソーシャルまわりの機能の使い方を１冊にまとめました。", "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/3.png", "猫でもわかるPlayFabソーシャル編", 1200m },
                    { 4, "本書は、PlayFabを使って実際にリリースしたい人に向けて執筆しました。公式ドキュメントからは読み取りにくい内容についても、独自に調べて解説をしています。", "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/4.png", "猫でもわかるPlayFab運用編", 1300m },
                    { 5, "「世の中に情報が出ていないのであれば、自分で試してまとめるしかない」と思い、本書の執筆にいたりました。本書を読むことで、次の内容を理解することができます。", "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/9.png", "猫でもわかるPlayFabUGC編", 1400m },
                    { 6, "Blazorの基礎を勉強できます。", "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/7.png", "猫でもわかるBlazor入門", 1500m },
                    { 7, "Blazorで認証付きのCRUDアプリがつくれるようになります。", "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/8.png", "猫でもわかるBlazor実践編", 1600m },
                    { 8, "Backendlessの無料プランを開放することができます。", "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/5.png", "猫でもわかるBackendless導入編", 1700m },
                    { 9, "ノーコードでTODOアプリが作れます。", "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/6.png", "猫でもわかるAppgyverTodoアプリを作ろう", 1800m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
