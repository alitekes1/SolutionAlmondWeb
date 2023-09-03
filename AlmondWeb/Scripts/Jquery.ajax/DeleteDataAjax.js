document.addEventListener("DOMContentLoaded", function () {
    $("#mydataTable").on("click", ".btntableDelete", function () {

        let _questionLabel = document.getElementById("_questionLabel");
        let _answerLabel = document.getElementById("_answerLabel");

        let question = $(this).data("que");
        let answer = $(this).data("ans");
        let id = $(this).data("ids");

        _questionLabel.innerText = question;
        _answerLabel.innerText = answer;

        document.getElementById("confirmbtnDelete").addEventListener("click", function () {
            $.ajax({
                method: "POST",
                url: '/Home/DeleteData/' + id,
                success: function (result) {
                    if (result > 0) {
                        ReLoadDeleteData();
                        toastr.success(question + " verisi başarıyla silindi.", "İşlem başarılı!", { closeButton: true, timeOut: 1500 });
                    }
                    else {
                        toastr.warning("Veri Eklenemedi.", "İşlem Başarısız", { closeButton: true, timeOut: 1500 })
                    }
                },
                complete: function () {
                    $("#closeButton").click();
                }
            });
        });
    });

    $("#shareddatatableindeletedata").on("click", ".btntableSharedDataDelete", function () {

        let _shareddataquestionLabel = document.getElementById("_shareddataquestionLabel");
        let _shareddataanswerLabel = document.getElementById("_shareddataanswerLabel");

        let shquestion = $(this).data("shareddataque");
        let shanswer = $(this).data("shareddataans");
        let shid = $(this).data("shareddataid");

        _shareddataquestionLabel.innerText = shquestion;
        _shareddataanswerLabel.innerText = shanswer;

        document.getElementById("confirmbtnDeleteSharedData").addEventListener("click", function () {
            $.ajax({
                method: "POST",
                url: '/User/DeleteSharedData',
                data: { id: shid },
                success: function (result) {
                    if (result > 0) {
                        ReLoadDeleteData();
                        toastr.success(shquestion + " verisi başarıyla silindi.", "İşlem Başarılı!", { closeButton: true, timeOut: 1500 });
                    } else {
                        toastr.warning("Veri silinemedi.", "İşlem Başarısız", { closeButton: true, timeOut: 1500 })
                    }
                },
                complete: function () {
                    $("#closeButtonsh").click();
                }
            });
        });
    });

});
function ReLoadDeleteData() {
    let table = $("#table");
    $.ajax({
        method: "GET",
        url: '/Home/FillTableForDelete',
        success: function (tableData) {
            table.empty(); // Tabloyu temizliyoruz

            table.append(tableData);
        }
    });
}