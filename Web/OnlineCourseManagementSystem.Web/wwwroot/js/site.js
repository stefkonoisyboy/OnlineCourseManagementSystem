// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//const btnsReplies = Array.from(document.querySelectorAll('.btn-replies'));

//const collapsesReplies = Array.from(document.querySelectorAll('.collapse-replies')) 

//btnsReplies.forEach(btnReplies => {
//    btnReplies.addEventListener('click', e => {
//        collapsesReplies.forEach(collapseReplies => {
//            const pushedButtonTarget = e.target;
//            const pushedBtnReplies = pushedButtonTarget.dataset['number']

//            const collapseReplies = collapseReplies.dataset['number'];
//            if (pushedBtnReplies == collapseReplies) {
//                collapseReplies.style.visibility = `${visible}`
//            }
//        })
//    })
//})

//var toastElList = [].slice.call(document.querySelectorAll('.toast'))
//var toastList = toastElList.map(function (toastEl) {
//    return new bootstrap.Toast(toastEl, option)
//})



///Button to scrool to the Top

window.onscroll = function () {
    scrollFunction()
}


function scrollFunction() {
    let button = document.getElementById('btn-scrollTop');
    if (document.body.scroll > 20 || document.documentElement.scrollTop > 20) {
        button.style.display = 'block';
    }
    else {
        button.style.display = 'none';
    }
}
button.addEventListener("click", getToTop());

function getToTop() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}

//Btn album
let modalContent = document.getElementById('modal-content');

function openAlbumForm() {
    let modalContent = document.getElementById('modal-content');
    let albumContainer = document.querySelector('.album-container');
    modalContent.style.display = 'block';
    albumContainer.style.opacity = '0.5';
}

function closeAlbumForm() {
    let modalContent = document.getElementById('modal-content');
    let albumContainer = document.querySelector('.album-container');
    modalContent.style.display = 'none';
    albumContainer.style.opacity = '1';
}

