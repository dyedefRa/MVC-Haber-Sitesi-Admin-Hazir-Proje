namespace HaberinMerkezi.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contextEkle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Etiket",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 50),
                        EklenmeTarihi = c.DateTime(),
                        Aktif = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HaberEtiket",
                c => new
                    {
                        HaberID = c.Int(nullable: false),
                        EtiketID = c.Int(nullable: false),
                        Aktif = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.HaberID, t.EtiketID })
                .ForeignKey("dbo.Haber", t => t.HaberID)
                .ForeignKey("dbo.Etiket", t => t.EtiketID)
                .Index(t => t.HaberID)
                .Index(t => t.EtiketID);
            
            CreateTable(
                "dbo.Haber",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 250),
                        KisaAciklama = c.String(maxLength: 250),
                        Aciklama = c.String(),
                        KullaniciID = c.Int(nullable: false),
                        OkunmaSayisi = c.Int(),
                        KategoriID = c.Int(nullable: false),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        Aktif = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kategori", t => t.KategoriID)
                .ForeignKey("dbo.Kullanici", t => t.KullaniciID)
                .Index(t => t.KullaniciID)
                .Index(t => t.KategoriID);
            
            CreateTable(
                "dbo.Kategori",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 150),
                        ParentID = c.Int(),
                        URL = c.String(maxLength: 150),
                        KullaniciID = c.Int(nullable: false),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        Aktif = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kullanici", t => t.KullaniciID, cascadeDelete: true)
                .Index(t => t.KullaniciID);
            
            CreateTable(
                "dbo.Kullanici",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdSoyad = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 50),
                        Parola = c.String(nullable: false, maxLength: 50),
                        RolID = c.Int(nullable: false),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        Aktif = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rol", t => t.RolID)
                .Index(t => t.RolID);
            
            CreateTable(
                "dbo.Rol",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RolAdi = c.String(nullable: false, maxLength: 100),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        Aktif = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Resim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HaberID = c.Int(nullable: false),
                        ResimYol = c.String(nullable: false, maxLength: 250),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        Aktif = c.Boolean(nullable: false),
                        VitrinMi = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Haber", t => t.HaberID)
                .Index(t => t.HaberID);
            
            CreateTable(
                "dbo.Slider",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baslik = c.String(maxLength: 255),
                        URL = c.String(maxLength: 255),
                        Aciklama = c.String(maxLength: 255),
                        ResimURL = c.String(nullable: false, maxLength: 255),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        Aktif = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HaberEtiket", "EtiketID", "dbo.Etiket");
            DropForeignKey("dbo.Resim", "HaberID", "dbo.Haber");
            DropForeignKey("dbo.Kategori", "KullaniciID", "dbo.Kullanici");
            DropForeignKey("dbo.Kullanici", "RolID", "dbo.Rol");
            DropForeignKey("dbo.Haber", "KullaniciID", "dbo.Kullanici");
            DropForeignKey("dbo.Haber", "KategoriID", "dbo.Kategori");
            DropForeignKey("dbo.HaberEtiket", "HaberID", "dbo.Haber");
            DropIndex("dbo.Resim", new[] { "HaberID" });
            DropIndex("dbo.Kullanici", new[] { "RolID" });
            DropIndex("dbo.Kategori", new[] { "KullaniciID" });
            DropIndex("dbo.Haber", new[] { "KategoriID" });
            DropIndex("dbo.Haber", new[] { "KullaniciID" });
            DropIndex("dbo.HaberEtiket", new[] { "EtiketID" });
            DropIndex("dbo.HaberEtiket", new[] { "HaberID" });
            DropTable("dbo.Slider");
            DropTable("dbo.Resim");
            DropTable("dbo.Rol");
            DropTable("dbo.Kullanici");
            DropTable("dbo.Kategori");
            DropTable("dbo.Haber");
            DropTable("dbo.HaberEtiket");
            DropTable("dbo.Etiket");
        }
    }
}
