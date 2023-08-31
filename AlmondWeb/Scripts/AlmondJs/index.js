document.addEventListener('DOMContentLoaded', function () {
    // Resim ayarlamalarý burada yapýlacak
    var Images = ["/Images/background/bg1.jpg", "/Images/background/bg2.jpg", "/Images/background/bg9.jpg", "/Images/background/bg4.jpg", "/Images/background/bg5.jpg", "/Images/background/bg6.jpg", "/Images/background/bg7.jpg", "/Images/background/bg8.jpg", "/Images/background/bg10.jpg", "/Images/background/bg11.jpg", "/Images/background/bg12.jpg", "/Images/background/bg13.jpg"];
    var currentIndex = Math.floor(Math.random() * Images.length);
    var activeSlide = document.body;
    activeSlide.style.backgroundImage = "url('" + Images[currentIndex] + "')";
    body.css("background-size", "cover");
});

