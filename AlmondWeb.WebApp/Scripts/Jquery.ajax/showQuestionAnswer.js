$(document).ready(function () {
    var selectedOption = $("#liste-dropdown option:selected");
    if (selectedOption.val() === "") {
        $("#question").text("Liste Seçiniz.");
        $("#answer").html('<p>Herhangi bir listeniz yoksa Liste İşlemleri/<a style="font-style:italic;color: black;" title="Liste işlemleri sayfasına gider" href="Liste-Yonetimi">Liste Yönetimi</a> \'nden yeni bir liste oluşturabilirsiniz.</p>');
    }
});

function isSelectedList() {
    var selectedOption = $("#liste-dropdown option:selected");
    if (selectedOption.val() === "") {
        alert("Liste seçimi yapman gerekiyor :)");
        focus("#liste-dropdown");
    }
};
$(document).ready(function () {
    var dropdown = document.getElementById("liste-dropdown");
    dropdown.options[0].disabled = true;
})