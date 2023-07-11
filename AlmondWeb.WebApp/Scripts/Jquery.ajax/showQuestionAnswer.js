$(document).ready(function () {
    var selectedOption = $("#liste-dropdown option:selected");
    if (selectedOption.val() === "") {
        $("#question").text("Liste Seçiniz.");
        $("#answer").html('<p>Herhangi bir listeniz yoksa <a style="font-style:italic;color: black;" title="Liste işlemleri sayfasına gider" href="ListOperations">Liste İşlemleri</a> \'nden yeni bir liste oluşturabilirsiniz.</p>');
    }
});

function isSelectedList() {
    var selectedOption = $("#liste-dropdown option:selected");
    if (selectedOption.val() === "") {
        alert("Liste seçimi yapmanız gerekiyor asas:)");
    }
};
$(document).ready(function () {
    var dropdown = document.getElementById("liste-dropdown");

    // İlk öğeyi devre dışı bırak
    dropdown.options[0].disabled = true;
})