$("#publicRadio").click(function () {
    $("#publicRadio").val(1);
    $("#privateRadio").val(0);
});
$("#privateRadio").click(function () {
    $("#publicRadio").val(0);
    $("#privateRadio").val(1);
});
$("#publicRadioUpdate").click(function () {
    $("#publicRadioUpdate").val(1);
    $("#privateRadioUpdate").val(0);
});
$("#privateRadioUpdate").click(function () {
    $("#privateRadioUpdate").val(1);
    $("#publicRadioUpdate").val(0);
});


let idValueforUpdateGlobal;
let idValueforDeleteGlobal;

document.getElementById("confirmBtnCreateList").addEventListener("click", function () {
    var listName = $("#inputTextCreateList").val();
    var listDescription = $("#listDescriptionTextArea").val();
    var listPublic = $("#publicRadio").val();
    var listPrivate = $("#privateRadio").val();

    var listdescription = $("#listDescriptionTextArea");
    var listname = $("#inputTextCreateList");
    $.ajax({
        method: "POST",
        url: '/Home/CreateList',
        data: { listNm: listName, listDesc: listDescription, listPub: listPublic, listPriv: listPrivate },
        success: function (result) {
            InfoUserwithToastr(result, "Home", "MyAllListTable", listName + " listesi başarıyla oluşturuldu.", "Hata meydana geldi. 3 saniye içerisinde sayfa yeniden yüklenecek.")
            listname.val("");
            listdescription.val("");
        },
        complete: function () {
            $("#closeBtnCreateList").click();
        }
    });
});


function transfertoUpdateData(lstnm, idValue, listdescription) {
    let listContent = document.getElementById("inputTextUpdateList");
    let listdescriptionupdate = document.getElementById("ListdescriptionTextAreaUpdate");

    listContent.value = lstnm;
    listdescriptionupdate.value = listdescription;
    idValueforUpdateGlobal = idValue;

    document.getElementById("confirmBtnUpdateList1").addEventListener("click", function () {
        var listNme = $("#inputTextUpdateList").val();
        var listDescription = $("#ListdescriptionTextAreaUpdate").val();
        var listPublic = $("#publicRadioUpdate").val();
        $.ajax({
            method: "POST",
            url: '/Home/UpdateList',
            data: { listName: listNme, id: idValueforUpdateGlobal, listDesc: listDescription, listPub: listPublic },//ilk veri contraller da alınancak olan veri ismidir. 2. veri ise verinin değerini tutan değişkendir.
            success: function (result) {
                InfoUserwithToastr(result, "Home", "MyAllListTable", listNme + " listesi başarıyla güncellendi.", "Liste güncelleme sırasında bir hata meyda geldi.")
            }, complete: function () {
                $("#closeBtnUpdate23").click();
            }
        });
    });
};

const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')//kayıtlı liste olmadığında çalışcak olan tooltip
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))

function transfertoListName(listName) {
    $("#removeSavedListName").text(listName);
}
function transfertoDeleteData(data, idM) {//kendimie ait olan listeyi kaldırırken kullanılır.
    let _list = document.getElementById("_questionLabel");
    _list.innerText = data;
    idValueforDeleteGlobal = idM;

    document.getElementById("confirmBtnDeleteList").addEventListener("click", function () {
        $.ajax({
            method: "POST",
            url: '/Home/RemoveMyList/' + idValueforDeleteGlobal,
            success: function (result) {
                InfoUserwithToastr(result, "Home", "MyAllListTable", "Liste başarıyla silindi.", "Liste silinemedi.")
            },
            complete: function () {
                $("#closeBtnDeleteList").click();
            }
        });
    });
};
function RemoveSavedList(listId, profileID) {//keşfet te kaydetdiğim listeyi kaldırırken kullanılır.
    $.ajax({
        method: "POST",
        url: 'Home/RemoveSavedList',
        data: { listid: listId, profileId: profileID },
        success: function (result) {

            InfoUserwithToastr(result, "Home", "SavedListTablePartial", "Liste kaldırıldı", "Liste kaldırma sırasında bir hata meydana geldi.", "savedlisttable")
        }, complete: function () {
            $("#closebtn12").click();
        }
    });
}

