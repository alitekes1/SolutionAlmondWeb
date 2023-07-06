var images = ['images/background/sea.jpg', 'images/background/bg1.jpg'];
var currentIndex = Math.floor(Math.random() * images.length);
var bgImage = document.body;
bgImage.style.backgroundImage = "url('" + images[currentIndex] + "')";
