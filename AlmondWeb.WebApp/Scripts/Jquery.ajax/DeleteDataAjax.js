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


        document.getElementById("confirmBtn").addEventListener("click", function () {
            var deleteRow = $(this).closest("tr");
            $.ajax({
                method: "POST",
                url: '/Home/DeleteData/' + id,
                success: function (result) {
                    if (result > 0) {
                        deleteRow.fadeOut(400, function () {
                            deleteRow.remove();
                            //TODO:burada toastr çıkacak.
                        })
                    } else {
                        alert("Hata meydana geldi. 3 saniye içerisinde sayfa yeniden yüklenecek.");//TODO: sayfa 3 saniye içinde yenilenmesi gerekiyor.
                    }
                },
                complete: function () {
                    $("#closeButton").click();
                }
            });
        });

    });
});