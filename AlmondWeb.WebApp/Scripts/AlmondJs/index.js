// JavaScript
document.addEventListener('DOMContentLoaded', function () {
    // Resim ayarlamalarý burada yapýlacak
    var Images = ["/Images/background/bg1.jpg", "/Images/background/sea.jpg", "/Images/background/bg3.jpg", "/Images/background/bg4.jpg", "/Images/background/bg6.jpg", "/Images/background/bg10.jpg", "/Images/background/bg8.jpg"];
    var currentIndex = Math.floor(Math.random() * Images.length);
    var activeSlide = document.body;
    activeSlide.style.backgroundImage = "url('" + Images[currentIndex] + "')";
    body.css("background-size", "cover");
});

let _list = document.getElementById("liste-dropdown");
_list.options[0].disabled = true;
