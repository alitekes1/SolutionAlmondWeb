
let _questionfortextarea = document.getElementById("exampleFormControlTextarea17");
let _answerfortextarea = document.getElementById("exampleFormControlTextarea16");
let _selectList = document.getElementById("liste-dropdown");
let _dataId = document.getElementById("UpdateData_Id");
let submitbtnUpdate = $("#submitbtn");
let updateBtn = $(".btntableDelete");
let changeBtn = document.getElementById("changeQueAnsw");
let submitbtn = document.getElementById("submitbtn");

function sharedListSelected(que, listID, ans, dataID, listName) {// kalem ikonuna tıklanıldığında çalışacak olan tüm fonksiyonlar
    transferdataTotextarea(que, listID, ans, dataID, listName);
    disabledList();
}


function selectListFunc(listId, listName) {

    let options = _selectList.options;

    if (listId > 0) {
        for (var i = 0; i < options.length; i++) {
            if (options[i].value == listId) {
                options[i].selected = true;
            }
        }
    }
    else {
        options[0].selected = true;
    }
}

function transferdataTotextarea(question, listId, answer, dataId, listName) {
    _questionfortextarea.removeAttribute("disabled");
    _answerfortextarea.removeAttribute("disabled");
    _selectList.removeAttribute("disabled");
    submitbtn.removeAttribute("disabled");
    changeBtn.removeAttribute("disabled");

    _questionfortextarea.value = question;
    _answerfortextarea.value = answer;


    selectListFunc(listId, listName);
    _dataId.setAttribute("value", dataId);
};
function disabledList() {
    _selectList.setAttribute("disabled", "true");
    toastrInfo("Kaydedilen listedeki verilerin listesi değiştirilemez.");
}

$("#changeQueAnsw").click(function () {
    let c;
    c = _questionfortextarea.value;
    _questionfortextarea.value = _answerfortextarea.value;
    _answerfortextarea.value = c;

});
function onSuccess() {
    toastrSuccess("Veri başarıyla güncellendi.");
}

submitbtnUpdate.click(function (e) {
    if (updateBtn.hasClass("clicked")) {
    }
    else {
        e.preventDefault();
        toastrFail("Güncellenecek veriyi seçiniz.")
    }
})
updateBtn.click(function () {
    if (updateBtn.hasClass("clicked")) {
    } else {
        updateBtn.addClass("clicked");
    }
});
_selectList.options[0].disabled = true;
