﻿$(document).ready(function () {
    var selectedOption = $("#liste-dropdown option:selected");
    if (selectedOption.val() === "") {
        $("#question").text("Liste Seçiniz.");
        $("#answer").html('<p>Herhangi bir listeniz yoksa Liste İşlemleri/<a style="font-style:italic;color: black;" title="Liste işlemleri sayfasına gider" href="Liste-Yonetimi">Liste Yönetimi</a> \'nden yeni bir liste oluşturabilirsiniz.</p>');
    }
});
function isSelectedList() {
    var selectedOption = $("#liste-dropdown option:selected");
    if (selectedOption.val() === "") {
        return toastrInfo("Liste seçimi yapmanız gerekiyor.")
    }
}

$(document).ready(function () {
    var dropdown = document.getElementById("liste-dropdown");
    dropdown.options[0].disabled = true;
})

let datalist;
let i = 0;
let listId;
let que = $("#question");
let ans = $("#answer");
let mainStage = $("#mainStage");

let listDropdown = document.getElementById("liste-dropdown");
$("#liste-dropdown").change(function () {
    listId = listDropdown.value;
    que.text("");
    ans.text("");
    $.ajax({
        method: "GET",
        url: `Home/GetQuestionAnswerJson/${listId}`, // URL düzgün formatta
        success: function (list) {
            if (list.length != 0) {
                console.log(list);
                datalist = list;
                i = 0; // Yeni liste için indeks sıfırlanıyor
                showQuestionAndAnswer(i); // İlk soru ve cevap gösteriliyor
            } else {
                que.text("");
                return toastrInfo("Listede veri yok. Veri eklemelisiniz.");
            }
        }
    });
});
function isSelectList() {
    var selectedOption = $("#liste-dropdown option:selected");
    if (selectedOption.val() === "") {
        return toastrInfo("Liste seçimi yapmanız gerekiyor.");
    }
}
$("#showAnswer").click(function () {
    isSelectList();
})
$(".puanlar").click(function () {
    isSelectList();
    var puanVal = $(this).val();
    $.ajax({
        method: "POST",
        url: 'Home/UpdatePuan',
        data: { dataId: datalist[i].update_Id, puan: puanVal, listIdPost: listId },
        success: function (result) {
            if (result > 0 && i + 1 < datalist.length) {
                i++; // Bir sonraki soruya geçiliyor
                showQuestionAndAnswer(i); // Yeni soru ve cevap gösteriliyor
            } else {
                return endofList();
            }
        }
    });
});

// Soru ve cevapları gösteren fonksiyon
function showQuestionAndAnswer(index) {
    ans.text("");
    que.text(datalist[index].question);
    $("#showAnswer").click(function () {
        ans.text(datalist[index].answer);
    });
}

function endofList() {
    console.clear();
    datalist = null;
    listDropdown.selectedIndex = 0;
    i = 0;
    toastr.success("İlgili listedeki tüm verileri gözden geçirdiniz.", "Tebrikler", { timeOut: 2500, closeButton: true });
    que.text("Tebrikler 🎉.Listedeki bütün verileri gözden geçirdiniz.");
    ans.text("Devam etmek için Liste Seçiniz.");
}

$(document).ready(function () {
    if (true) {

    }
    $(document).keydown(mainpageShortcuts);
});
let isModalClose;
$('#exampleModal').on('show.bs.modal', function () {
    isModalClose = false;
});

// Modal kapatıldığında
$('#exampleModal').on('hidden.bs.modal', function () {
    isModalClose = true;
});
function mainpageShortcuts(event) {
    if (event.key === '1' && isModalClose) {
        $("#puan1").click();
    }
    if (event.key === '2' && isModalClose) {
        $("#puan2").click();
    }
    if (event.key === '3' && isModalClose) {
        $("#puan3").click();
    }
    if (event.key === '4' && isModalClose) {
        $("#puan4").click();
    }
    if (event.key === '5' && isModalClose) {
        $("#puan5").click();
    }
    if (event.key === ' ' && isModalClose) {
        event.preventDefault();
        $("#showAnswer").click();
    }
}

$(".puanlar").click(function () {
    var puan = $(this).val();
});
