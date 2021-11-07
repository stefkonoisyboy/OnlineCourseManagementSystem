// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
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

//$(() => {
//    let connection = new signalR.HubConnectionBuilder().withUrl("hubs/QAHub").build();
//    console.log("Hello world!");
//    connection.start();

//    connection.on("refreshMessages", function () {
//        console.log("Hi guys!");
//        loadData();
//    });

//    function loadData() {
//        var tr = '';
//        let div = '';

//        $.ajax({
//            url: `/MessageQAs/AllByChannel/12`,
//            method: 'GET',
//            dataType: "json",
//            success: (result) => {
//                $.each(result, (k, v) => {
//                    tr = tr + `<tr>
//                        <td>${v.content}</td>
//                        <td>${v.creatorLastName}</td>
//                        <td>
//                           <h6>  ${v.creatorFirstName}</h6>
//                        </td>
//                    </tr>`;
//                    console.log("What's up!");
//                    console.log(v.content);
//                });

//                $("#tableBody").html(tr);
//            },
//            error: (error) => {
//                console.log(error);
//                console.log("Error");
//            }
//        });
//    }
//});




