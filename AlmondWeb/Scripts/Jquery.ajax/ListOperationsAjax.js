var table = $("#example");
$("#publicRadio").click(function () {
    $("#publicRadio").val(1);
    $("#privateRadio").val(0);
});
$("#privateRadio").click(function () {
    $("#privateRadio").val(1);
    $("#publicRadio").val(0);
});
$("#publicRadioUpdate").click(function () {
    $("#publicRadioUpdate").val(1);
    $("#privateRadioUpdate").val(0);
});
$("#privateRadioUpdate").click(function () {
    $("#privateRadioUpdate").val(1);
    $("#publicRadioUpdate").val(0);
});

document.getElementById("confirmBtnCreateList").addEventListener("click", function () {
    var listName = $("#inputTextCreateList").val();
    var listDescription = $("#listDescriptionTextArea").val();
    var listPublic = $("#publicRadio").val();
    var listPrivate = $("#privateRadio").val();

    $.ajax({
        method: "POST",
        url: '/Home/CreateList',
        data: { listNm: listName, listDesc: listDescription, listPub: listPublic, listPriv: listPrivate },
        success: function (result) {
            if (result > 0) {
                ReLoadListData();
                toastr.success(listName + " listesi başarıyla oluşturuldu.", "İşlem başarılı!", { closeButton: true, timeOut: 1500 });
            } else {
                toastr.warning("Hata meydana geldi. 3 saniye içerisinde sayfa yeniden yüklenecek.", "İşlem Başarısız", { closeButton: true, timeOut: 1500 });//TODO: sayfa 3 saniye içinde yenilenmesi gerekiyor.
            }
        },
        complete: function () {
            $("#closeBtnCreateList").click();
        }
    });
});

function transfertoDeleteData(data, idM) {
    let _list = document.getElementById("_questionLabel");
    _list.innerText = data;

    document.getElementById("confirmBtnDeleteList").addEventListener("click", function () {
        $.ajax({
            method: "POST",
            url: '/Home/DeleteList/' + idM,
            success: function (result) {
                if (result > 0) {
                    ReLoadListData();
                    toastr.success(data + " listesi başarıyla silindi.", "İşlem başarılı!", { closeButton: true, timeOut: 1500 });
                }
                else {
                    toastr.warning("Liste Silinemedi", "İşlem Başarısız", { closeButton: true, timeOut: 1500 });
                }
            },
            complete: function () {
                $("#closeBtnDeleteList").click();
            }
        });
    });
};

function transfertoUpdateData(lstnm, idValue) {
    let listContent = document.getElementById("inputTextUpdateList");
    listContent.value = lstnm;

    document.getElementById("confirmBtnUpdateList").addEventListener("click", function () {
        var listNme = $("#inputTextUpdateList").val();
        var listDescription = $("#ListdescriptionTextAreaUpdate").val();
        var listPublic = $("#publicRadioUpdate").val();
        var listPrivate = $("#privateRadioUpdate").val();
        $.ajax({
            method: "POST",
            url: '/Home/UpdateList',
            data: { listName: listNme, id: idValue, listDesc: listDescription, listPub: listPublic, listPriv: listPrivate },//ilk veri contraller da alınancak olan veri ismidir. 2. veri ise verinin değerini tutan değişkendir.
            success: function (result) {
                if (result > 0) {
                    ReLoadListData();
                    toastr.success(listNme + " listesi başarıyla güncellendi.", "İşlem başarılı!", { closeButton: true, timeOut: 1500 });
                }
                else {
                    toastr.warning("Liste güncelleme sırasında bir hata meyda geldi.", "İşlem Başarısız", { closeButton: true, timeOut: 1500 });//TODO:sayfa otomatik olarak yenilenecek.
                }
            }, complete: function () {
                $("#closeBtnUpdate23").click();
            }
        });
    });
};

function ReLoadListData() {
    $.ajax({
        method: "GET",
        url: '/Home/FillTableForListOperations',
        success: function (tableData) {
            table.empty(); // Tabloyu temizliyoruz

            table.append(tableData);
        }
    });
}
