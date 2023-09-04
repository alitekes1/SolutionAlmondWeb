mydataId = -9;
shid = -9;
$("#mydataTable").on("click", ".btntableDelete", function () {//kendimin eklediği veriyi silmek isterken çalışır.

    let _questionLabel = document.getElementById("_questionLabel");
    let _answerLabel = document.getElementById("_answerLabel");

    let question = $(this).data("que");
    let answer = $(this).data("ans");
    mydataId = $(this).data("ids");

    _questionLabel.innerText = question;
    _answerLabel.innerText = answer;

});
document.getElementById("confirmbtnDelete").addEventListener("click", function () {
    $.ajax({
        method: "POST",
        url: '/Home/DeleteData',
        data: { id: mydataId },
        success: function (result) {
            if (result > 0) {
                ReLoadDeleteData();
                toastr.success("Veri başarıyla silindi.", "İşlem başarılı!", { closeButton: true, timeOut: 1500 });
            }
            else {
                toastr.warning("Veri silinemedi.", "İşlem Başarısız", { closeButton: true, timeOut: 1500 })
            }
        },
        complete: function () {
            $("#closeButton").click();
        }
    });
});
$("#shareddatatableindeletedata").on("click", ".btntableSharedDataDelete", function () {
    //paylaşılan bir listedeki veriyi ilgili listeden kaldırmak için kullanılır.

    let _shareddataquestionLabel = document.getElementById("_shareddataquestionLabel");
    let _shareddataanswerLabel = document.getElementById("_shareddataanswerLabel");

    let shquestion = $(this).data("shareddataque");
    let shanswer = $(this).data("shareddataans");
    shid = $(this).data("shareddataid");

    _shareddataquestionLabel.innerText = shquestion;
    _shareddataanswerLabel.innerText = shanswer;
});

document.getElementById("confirmbtnDeleteSharedData").addEventListener("click", function () {
    $.ajax({
        method: "POST",
        url: '/User/DeleteSharedData',
        data: { id: shid },
        success: function (result) {
            if (result > 0) {
                ReLoadDeleteData();
                toastr.success("Veri başarıyla silindi.", "İşlem başarılı!", { closeButton: true, timeOut: 1500 });
            } else {
                toastr.warning("Veri silinemedi.", "İşlem Başarısız", { closeButton: true, timeOut: 1500 })
            }
        },
        complete: function () {
            $("#closeButtonsh").click();
        }
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