
function kategoriEkle() {
    kategori = new Object();
    kategori.Adi = $("#kategoriAdi").val();
    kategori.URL = $("#kategoriUrl").val();
    kategori.Aktif = $("#kategoriAktif").is(":checked");
    kategori.ParentID = $("#ParentIDs").val();


    $.ajax({
        type: 'POST',
        url: "/Kategori/Ekle",
        data: kategori,
        success: function (data) {
            if (data.Success) {
                bootbox.alert(data.Message, function () {
                    //SAYFAYI YENILEMEK
                    location.reload();
                    
                });
            }
            else {
                bootbox.alert(data.Message, function () {
                    //Hata durumu için yapılacaklar
                });
            }
        }
    });

}


function kategoriDuzenle() {
    kategori = new Object();
    kategori.Adi = $("#Adi").val();
    kategori.URL = $("#URL").val();
    kategori.Aktif = $("#Aktif").is(":checked");
   
    kategori.ParentID = $("#Parent option:selected",).val();
    kategori.KullaniciID = $("#KullaniciID").val();
    kategori.Id = $("#kategoriID").val();

 
    $.ajax({
        type: 'POST',
        url: "/Kategori/Duzenle",
        data: kategori,
        success: function (data) {
            if (data.Success) {
               
                    //SAYFAYI YENILEMEK
                    location.reload();

             

               
            }
            else {
                bootbox.alert(data.Message, function () {
                    //Hata durumu için yapılacaklar
                });
            }
        }
    });
}


function haberEkle() {

    haber = new Object();
    haber.Baslik = $("#baslik").val();
    haber.KisaAciklama = $("#kaciklama").val();
    haber.Aciklama = $("#Aciklama").val();
    haber.KategoriID = $("#katId option:selected").val();
    haber.Aktif = $("#aktif").is(":checked");

   
    $.ajax({
        type: 'POST',
        url: "/Haber/Ekle",
        data: haber,
        dataType: 'json',
        success: function (value) {
            if (value.Success) {
                bootbox.alert(value.Message, function () {
                    location.reload();
                });
            }
            else {
                bootbox.alert(value.Message, function () {
                    //bla bla lba
                });
            }
        }
    });

}



