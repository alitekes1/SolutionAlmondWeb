let _questionfortextarea = document.getElementById("exampleFormControlTextarea17");
let _answerfortextarea = document.getElementById("exampleFormControlTextarea16");
let _dataId = document.getElementById("UpdateData_Id");

function transferdataTotextarea(question, listId, answer, dataId) {
    _questionfortextarea.value = question;
    _answerfortextarea.value = answer;
    if (listId > 0) {

        for (var i = 0; i < options.length; i++) {
            if (options[i].value == listId) {
                options[i].selected = true;
            }
        }
    }
    //else {

    //}
    _dataId.setAttribute("value", dataId);
};
var submitbuton = document.getElementById("submitbtn");
var isSelectedData = document.getElementById("UpdateData_Id");

submitbuton.addEventListener("click", function () {
    if (isSelectedData.value == null) {
        return alert("Hangi veriyi güncellemek istediğinizi tablodaki kalem ikonuna tıklayarak seçiniz.");
    }
});