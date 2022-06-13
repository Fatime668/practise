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