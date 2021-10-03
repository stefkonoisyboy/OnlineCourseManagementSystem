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



///Button to scroll to the Top

//window.onscroll = function () {
//    scrollFunction()
//}


//function scrollFunction() {
//    let button = document.getElementById('btn-scrollTop');
//    if (document.body.scroll > 20 || document.documentElement.scrollTop > 20) {
//        button.style.display = 'block';
//    }
//    else {
//        button.style.display = 'none';
//    }
//}
//button.addEventListener("click", getToTop());

//function getToTop() {
//    document.body.scrollTop = 0;
//    document.documentElement.scrollTop = 0;
//}

//Btn album
//let modalContent = document.getElementById('modal-content');

//function openAlbumForm() {
//    let modalContent = document.getElementById('modal-content');
//    let albumContainer = document.querySelector('.album-container');
//    modalContent.style.display = 'block';
//    albumContainer.style.opacity = '0.5';
//}

//function closeAlbumForm() {
//    let modalContent = document.getElementById('modal-content');
//    let albumContainer = document.querySelector('.album-container');
//    modalContent.style.display = 'none';
//    albumContainer.style.opacity = '1';
//}

function scrollToBottom() {
    try {
        var objDiv = document.getElementById("messages");
        objDiv.scrollTop = objDiv.scrollHeight;
    } catch (e) {
        return;
    }
    
    
}

//service-worker.js

//function showNotification(event) {
//    return new Promise(resolve => {
//        const { body, title, tag } = JSON.parse(event.data.text());

//        self.registration
//            .getNotifications({ tag })
//            .then(existingNotifications => { })
//            .then(() => {
//                const icon = `/path/to/icon`;
//                return self.registration
//                    .showNotification(title, {body, tag, icon })
//            })
//            .then(resolve)
//    })
//}

//self.addEventListener("push", event => {
//    event.waitUntil(
//        showNotification(event)
//    );
//})

//self.addEventListener("notificationclick", event => {
//    event.waitUntil(clients.openWindow('/'));
//})

$().ready(function () {
    $("#show").hover(function () {
        $("#create-text").show("1000");
    }, function () {
        $("#create-text").hide("1000");
    });
});

