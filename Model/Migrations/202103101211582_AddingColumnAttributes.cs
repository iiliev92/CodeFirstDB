namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingColumnAttributes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProducts", "Product_Barcode", "dbo.Products");
            DropIndex("dbo.UserProducts", new[] { "Product_Barcode" });
            DropPrimaryKey("dbo.Products");
            DropPrimaryKey("dbo.UserProducts");
            AlterColumn("dbo.Products", "Barcode", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.UserProducts", "Product_Barcode", c => c.String(nullable: false, maxLength: 20));
            AddPrimaryKey("dbo.Products", "Barcode");
            AddPrimaryKey("dbo.UserProducts", new[] { "User_ID", "Product_Barcode" });
            CreateIndex("dbo.UserProducts", "Product_Barcode");
            AddForeignKey("dbo.UserProducts", "Product_Barcode", "dbo.Products", "Barcode", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProducts", "Product_Barcode", "dbo.Products");
            DropIndex("dbo.UserProducts", new[] { "Product_Barcode" });
            DropPrimaryKey("dbo.UserProducts");
            DropPrimaryKey("dbo.Products");
            AlterColumn("dbo.UserProducts", "Product_Barcode", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Barcode", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.UserProducts", new[] { "User_ID", "Product_Barcode" });
            AddPrimaryKey("dbo.Products", "Barcode");
            CreateIndex("dbo.UserProducts", "Product_Barcode");
            AddForeignKey("dbo.UserProducts", "Product_Barcode", "dbo.Products", "Barcode", cascadeDelete: true);
        }
    }
}
