var table = $("#example");
$(document).ready(function () {
    $("#checkbox1").click(function () {
        if ($(this).is(":checked")) {
            $(this).val(1);
        } else {
            $(this).val(0);
        }
    });
    $("#checkbox2").click(function () {
        if ($(this).is(":checked")) {
            $(this).val(1);
        } else {
            $(this).val(0);
        }
    });
});

document.getElementById("confirmBtnCreateList").addEventListener("click", function () {
    var listName = $("#inputTextCreateList").val();
    var listDescription = $("#listDescriptionTextArea").val();
    var listisPublic = $("#checkbox1").val();
    $.ajax({
        method: "POST",
        url: '/Home/CreateList',
        data: { listNm: listName, listDesc: listDescription, listisPub: listisPublic },
        success: function (result) {
            if (result > 0) {
                //TODO:işlem başarılı toastr çıkacak.
                ReLoadListData();
                toastr.success(listName + " listesi başarıyla oluşturuldu.", "İşlem başarılı!");
            } else {
                alert("Hata meydana geldi. 3 saniye içerisinde sayfa yeniden yüklenecek.");//TODO: sayfa 3 saniye içinde yenilenmesi gerekiyor.
            }
        },
        complete: function () {
            $("#closeBtnCreateList").click();
        }
    });
});
function transfertoUpdateData(lstnm, idValue) {
    let listContent = document.getElementById("inputTextUpdateList");
    listContent.value = lstnm;

    document.getElementById("confirmBtnUpdateList").addEventListener("click", function () {
        var listNme = $("#inputTextUpdateList").val();
        var listDescription = $("#ListdescriptionTextAreaUpdate").val();
        var listisPublic = $("#checkbox2").val();
        $.ajax({
            method: "POST",
            url: '/Home/UpdateList',
            data: { listName: listNme, id: idValue, listDesc: listDescription, listisPub: listisPublic },//ilk veri contraller da alınancak olan veri ismidir. 2. veri ise verinin değerini tutan değişkendir.
            success: function (result) {
                if (result > 0) {
                    ReLoadListData();
                    toastr.success(listNme + " listesi başarıyla güncellendi.", "İşlem başarılı!");
                }
                else {
                    alert("Liste güncelleme sırasında bir hata meyda geldi.");//TODO:sayfa otomatik olarak yenilenecek.
                }
            }, complete: function () {
                $("#closeBtnUpdate23").click();
            }
        });
    });
};

function transfertoDeleteData(data, idM) {
    let _list = document.getElementById("_questionLabel");
    _list.innerText = data;

    document.getElementById("confirmBtnDeleteList").addEventListener("click", function () {
        $.ajax({
            method: "POST",
            url: '/Home/DeleteList/' + idM,
            success: function (result) {
                if (result > 0) {
                    //TODO:işlem başarılı toastr çıkacak.
                    ReLoadListData();
                    toastr.success(data + " listesi başarıyla silindi.", "İşlem başarılı!");
                }
                else {
                    return alert("işlem başarısız:(");
                }
            },
            complete: function () {
                $("#closeBtnDeleteList").click();
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
