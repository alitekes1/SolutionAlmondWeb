function transferdataTotextarea(question, listId, answer, dataId) {
    let _questionfortextarea = document.getElementById("exampleFormControlTextarea17");
    let _answerfortextarea = document.getElementById("exampleFormControlTextarea16");
    let _selectList = document.getElementById("liste-dropdown");
    let _dataId = document.getElementById("UpdateData_Id");

    _questionfortextarea.value = question;
    _answerfortextarea.value = answer;
    var options = _selectList.options;

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
    _dataId.setAttribute("value", dataId);
};
var submitbuton = document.getElementById("submitbtn");
var isSelectedData = document.getElementById("UpdateData_Id");

submitbuton.addEventListener("click", function () {
    if (isSelectedData.value == null) {
        return alert("Hangi veriyi güncellemek istediğinizi tablodaki kalem ikonuna tıklayarak seçiniz.");
    }
});