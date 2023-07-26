var table = $("#example");

function transfertoUpdateData(lstnm, id) {
    let listContent = document.getElementById("inputTextUpdateList");
    listContent.value = lstnm;

    document.getElementById("confirmBtnUpdateList").addEventListener("click", function () {
        var listNme = $("#inputTextUpdateList").val();
        $.ajax({
            method: "POST",
            url: '/Home/UpdateList/' + id,
            data: { listName: listNme },//ilk veri contraller da alınancak olan veri ismidir. 2. veri ise verinin değerini tutan değişkendir.
            success: function (result) {
                if (result > 0) {
                    //işlem başarılı tablo tekrardan yüklenecek.
                    ReLoadListData();
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
document.getElementById("confirmBtnCreateList").addEventListener("click", function () {
    var listName = $("#inputTextCreateList").val();
    $.ajax({
        method: "POST",
        url: '/Home/CreateList',
        data: { listNm: listName },
        success: function (result) {
            if (result > 0) {
                //TODO:işlem başarılı toastr çıkacak.
                ReLoadListData();
            } else {
                alert("Hata meydana geldi. 3 saniye içerisinde sayfa yeniden yüklenecek.");//TODO: sayfa 3 saniye içinde yenilenmesi gerekiyor.
            }
        },
        complete: function () {
            $("#closeBtnCreateList").click();
        }
    });
});

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
