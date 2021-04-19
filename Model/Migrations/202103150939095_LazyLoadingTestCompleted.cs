namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LazyLoadingTestCompleted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Brand_ID", "dbo.Brands");
            DropIndex("dbo.Products", new[] { "Brand_ID" });
            AlterColumn("dbo.Products", "Brand_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "Brand_ID");
            AddForeignKey("dbo.Products", "Brand_ID", "dbo.Brands", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Brand_ID", "dbo.Brands");
            DropIndex("dbo.Products", new[] { "Brand_ID" });
            AlterColumn("dbo.Products", "Brand_ID", c => c.Int());
            CreateIndex("dbo.Products", "Brand_ID");
            AddForeignKey("dbo.Products", "Brand_ID", "dbo.Brands", "ID");
        }
    }
}
