// JavaScript
var images = ['resim1.jpg', 'resim2.jpg', 'resim3.jpg'];
var currentIndex = Math.floor(Math.random() * images.length);
var activeSlide = document.querySelector('.carousel-item');
activeSlide.style.backgroundImage = "url('" + images[currentIndex] + "')";
