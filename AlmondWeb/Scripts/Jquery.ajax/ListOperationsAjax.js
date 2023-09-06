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
            InfoUserwithToastr(result, "Home", "FillTableForListOperations", listName + " listesi başarıyla oluşturuldu.", "Hata meydana geldi. 3 saniye içerisinde sayfa yeniden yüklenecek.")
            //TODO: sayfa 3 saniye içinde yenilenmesi gerekiyor.
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
                InfoUserwithToastr(result, "Home", "FillTableForListOperations", data + " listesi başarıyla silindi.", "Liste silinemedi.")
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
                InfoUserwithToastr(result, "Home", "FillTableForListOperations", listNme + " listesi başarıyla güncellendi.", "Liste güncelleme sırasında bir hata meyda geldi.")
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
function RemoveSavedList(listId, profileID) {
    $.ajax({
        method: "POST",
        url: 'Home/RemoveSavedList',
        data: { listid: listId, profileId: profileID },
        success: function (result) {
            InfoUserwithToastr(result, "Home", "SavedListTablePartial", "Liste kaldırıldı", "Liste kaldırma sırasında bir hata meydana geldi.")
        }, complete: function () {
            $("#closebtn12").click();
        }
    });
}

