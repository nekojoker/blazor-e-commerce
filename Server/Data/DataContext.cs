using BlazorEC.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorEC.Server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderParticular> OrderParticulars { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Title = "猫でもわかるPlayFab入門", UnitPrice = 1000, Description = "PlayFab の勉強を始めるにあたって、どこからどういった順番で学習をしていくのかがわからないと思います。いろんな機能がある中で、難しい機能から始めてしまうと、理解が難しくやめてしまう原因にもなります。そこで本書では、理解がしやすい以下の機能をピックアップして、入門編としました。", ImageUrl = "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/1.png" },
            new Product { Id = 2, Title = "猫でもわかるPlayFab自動化編", UnitPrice = 1100, Description = "PlayFabを使いこなす上で自動化まわりの機能の理解は欠かせません。しかしながら、自動化まわりの機能は便利ですが学習コストが高く、なかなか手を出しにくい分野であることも事実です。", ImageUrl = "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/2.png" },
            new Product { Id = 3, Title = "猫でもわかるPlayFabソーシャル編", UnitPrice = 1200, Description = "フレンド機能やドロップテーブルなど、ソーシャルまわりの機能の使い方を１冊にまとめました。", ImageUrl = "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/3.png" },
            new Product { Id = 4, Title = "猫でもわかるPlayFab運用編", UnitPrice = 1300, Description = "本書は、PlayFabを使って実際にリリースしたい人に向けて執筆しました。公式ドキュメントからは読み取りにくい内容についても、独自に調べて解説をしています。", ImageUrl = "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/4.png" },
            new Product { Id = 5, Title = "猫でもわかるPlayFabUGC編", UnitPrice = 1400, Description = "「世の中に情報が出ていないのであれば、自分で試してまとめるしかない」と思い、本書の執筆にいたりました。本書を読むことで、次の内容を理解することができます。", ImageUrl = "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/9.png" },
            new Product { Id = 6, Title = "猫でもわかるBlazor入門", UnitPrice = 1500, Description = "Blazorの基礎を勉強できます。", ImageUrl = "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/7.png" },
            new Product { Id = 7, Title = "猫でもわかるBlazor実践編", UnitPrice = 1600, Description = "Blazorで認証付きのCRUDアプリがつくれるようになります。", ImageUrl = "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/8.png" },
            new Product { Id = 8, Title = "猫でもわかるBackendless導入編", UnitPrice = 1700, Description = "Backendlessの無料プランを開放することができます。", ImageUrl = "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/5.png" },
            new Product { Id = 9, Title = "猫でもわかるAppgyverTodoアプリを作ろう", UnitPrice = 1800, Description = "ノーコードでTODOアプリが作れます。", ImageUrl = "https://raw.githubusercontent.com/nekojoker/sample-contents/main/images/6.png" }
        );
    }
}
