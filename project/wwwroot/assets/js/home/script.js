'use strict'
let bar = document.querySelector(".navbar i")
let close = document.querySelector(".close i")
let navpane = document.querySelector(".nav-pane")
let body = document.querySelector("body")
bar.onclick = function(){
    navpane.style.display = "block"
    body.style.background = "rgb(0,0,0,0.5)"
}
close.onclick = function(){
    navpane.style.display = "none"
    body.style.background = "none"

}
$('.slider').slick({
    dots: true,
    infinite: true,
    speed: 500,
    fade: true,
    cssEase: 'linear',
    nextArrow: $(".nextBtn"),
    prevArrow: $(".prevBtn"),
    autoplay: true,
    autoplaySpeed: 2000,
})    
$('.brands').slick({
    slidesToShow: 6,
    slidesToScroll: 1,
    dots: true,
    // responsive: [
    //     {
    //       breakpoint: 768,
    //       settings: {
    //         arrows: false,
    //         centerMode: true,
    //         centerPadding: '40px',
    //         slidesToShow: 3
    //       }
    //     },
    //     {
    //       breakpoint: 480,
    //       settings: {
    //         arrows: false,
    //         centerMode: true,
    //         centerPadding: '40px',
    //         slidesToShow: 1
    //       }
    //     }
    //   ]
})