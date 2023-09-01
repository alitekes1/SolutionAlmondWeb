// "DOMContentLoaded" olayını dinleyen bir event listener ekleyin.
document.addEventListener("DOMContentLoaded", function () {
    $("#example").on("click", ".btntableDelete", function () {
        let _questionLabel = document.getElementById("_questionLabel");
        let _answerLabel = document.getElementById("_answerLabel");

        let question = $(this).data("que");
        let id = $(this).data("ids");
        let answer = $(this).data("ans");

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
                    } else {
                        toastr.warning("Veri Eklenemedi.", "İşlem Başarısız", { closeButton: true, timeOut: 1500 })
                    }
                },
                complete: function () {
                    $("#closeButton").click();
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
