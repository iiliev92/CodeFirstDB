namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RootDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Barcode = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Brand_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Barcode)
                .ForeignKey("dbo.Brands", t => t.Brand_ID, cascadeDelete: true)
                .Index(t => t.Brand_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Age = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserProducts",
                c => new
                    {
                        User_ID = c.Int(nullable: false),
                        Product_Barcode = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.User_ID, t.Product_Barcode })
                .ForeignKey("dbo.Users", t => t.User_ID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Barcode, cascadeDelete: true)
                .Index(t => t.User_ID)
                .Index(t => t.Product_Barcode);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProducts", "Product_Barcode", "dbo.Products");
            DropForeignKey("dbo.UserProducts", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Products", "Brand_ID", "dbo.Brands");
            DropIndex("dbo.UserProducts", new[] { "Product_Barcode" });
            DropIndex("dbo.UserProducts", new[] { "User_ID" });
            DropIndex("dbo.Products", new[] { "Brand_ID" });
            DropTable("dbo.UserProducts");
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Brands");
        }
    }
}
