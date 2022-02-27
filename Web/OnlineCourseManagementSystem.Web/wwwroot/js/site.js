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


let _videoTrack = null;
let _activeRoom = null;
let _participants = new Map();
let _dominantSpeaker = null;

async function getVideoDevices() {
    try {
        let devices = await navigator.mediaDevices.enumerateDevices();
        if (devices.every(d => !d.label)) {
            await navigator.mediaDevices.getUserMedia({
                video: true
            });
        }

        devices = await navigator.mediaDevices.enumerateDevices();
        if (devices && devices.length) {
            const deviceResults = [];
            devices.filter(device => device.kind === 'videoinput')
                .forEach(device => {
                    const { deviceId, label } = device;
                    deviceResults.push({ deviceId, label });
                });

            return deviceResults;
        }
    } catch (error) {
        console.log(error);
    }

    return [];
}

async function startVideo(deviceId, selector) {
    const cameraContainer = document.querySelector(selector);
    if (!cameraContainer) {
        return;
    }

    try {
        if (_videoTrack) {
            _videoTrack.detach().forEach(element => element.remove());
        }

        _videoTrack = await Twilio.Video.createLocalVideoTrack({ deviceId });
        const videoEl = _videoTrack.attach();
        cameraContainer.append(videoEl);
    } catch (error) {
        console.log(error);
    }
}

async function createOrJoinRoom(roomName, token) {
    try {
        if (_activeRoom) {
            _activeRoom.disconnect();
        }

        const audioTrack = await Twilio.Video.createLocalAudioTrack();
        const tracks = [audioTrack, _videoTrack];
        _activeRoom = await Twilio.Video.connect(
            token, {
            name: roomName,
            tracks,
            dominantSpeaker: true
        });

        if (_activeRoom) {
            initialize(_activeRoom.participants);
            _activeRoom
                .on('disconnected',
                    room => room.localParticipant.tracks.forEach(
                        publication => detachTrack(publication.track)))
                .on('participantConnected', participant => add(participant))
                .on('participantDisconnected', participant => remove(participant))
                .on('dominantSpeakerChanged', dominantSpeaker => loudest(dominantSpeaker));
        }
    } catch (error) {
        console.error(`Unable to connect to Room: ${error.message}`);
    }

    return !!_activeRoom;
}

function initialize(participants) {
    _participants = participants;
    if (_participants) {
        _participants.forEach(participant => registerParticipantEvents(participant));
    }
}

function add(participant) {
    if (_participants && participant) {
        _participants.set(participant.sid, participant);
        registerParticipantEvents(participant);
    }
}

function remove(participant) {
    if (_participants && _participants.has(participant.sid)) {
        _participants.delete(participant.sid);
    }
}

function loudest(participant) {
    _dominantSpeaker = participant;
}

function registerParticipantEvents(participant) {
    if (participant) {
        participant.tracks.forEach(publication => subscribe(publication));
        participant.on('trackPublished', publication => subscribe(publication));
        participant.on('trackUnpublished',
            publication => {
                if (publication && publication.track) {
                    detachRemoteTrack(publication.track);
                }
            });
    }
}

function subscribe(publication) {
    if (isMemberDefined(publication, 'on')) {
        publication.on('subscribed', track => attachTrack(track));
        publication.on('unsubscribed', track => detachTrack(track));
    }
}

function attachTrack(track) {
    if (isMemberDefined(track, 'attach')) {
        const audioOrVideo = track.attach();
        audioOrVideo.id = track.sid;

        if ('video' === audioOrVideo.tagName.toLowerCase()) {

            if (track.name === "screen") {
                document.getElementById("sharedScreen").appendChild(audioOrVideo);
            } else {
                const responsiveDiv = document.createElement('div');
                responsiveDiv.id = track.sid;
                responsiveDiv.classList.add('embed-responsive');
                responsiveDiv.classList.add('embed-responsive-16by9');

                const responsiveItem = document.createElement('div');
                responsiveItem.classList.add('embed-responsive-item');
                // Similar to.
                // <div class="embed-responsive embed-responsive-16by9">
                //   <div id="camera" class="embed-responsive-item">
                //     <video></video>
                //   </div>
                // </div>
                responsiveItem.appendChild(audioOrVideo);
                responsiveDiv.appendChild(responsiveItem);
                document.getElementById('participants').appendChild(responsiveDiv);
            }
        } else {
            document.getElementById('participants')
                .appendChild(audioOrVideo);
        }
    }
}

function detachTrack(track) {
    if (this.isMemberDefined(track, 'detach')) {
        track.detach()
            .forEach(el => {
                if ('video' === el.tagName.toLowerCase()) {
                    const parent = el.parentElement;
                    if (parent && parent.id !== 'camera') {
                        const grandParent = parent.parentElement;
                        if (grandParent) {
                            grandParent.remove();
                        }
                    }
                } else {
                    el.remove()
                }
            });
    }
}

function isMemberDefined(instance, member) {
    return !!instance && instance[member] !== undefined;
}

async function leaveRoom() {
    try {
        if (_activeRoom) {
            _activeRoom.disconnect();
            _activeRoom = null;
        }

        if (_participants) {
            _participants.clear();
        }
    }
    catch (error) {
        console.error(error);
    }
}

window.videoInterop = {
    getVideoDevices,
    startVideo,
    createOrJoinRoom,
    leaveRoom
};

function muteVideo() {
    var localParticipant = _activeRoom.localParticipant;
    localParticipant.videoTracks.forEach(function (videoTracks) {
        videoTracks.track.disable();

    });
}

function unMuteVideo() {
    var localParticipant = _activeRoom.localParticipant;
    localParticipant.videoTracks.forEach(function (videoTracks) {
        videoTracks.track.enable();
    });
}

function unMuteAudio() {
    var localParticipant = _activeRoom.localParticipant;
    localParticipant.audioTracks.forEach(function (audioTrack) {
        audioTrack.track.enable();
    });
}

function muteAudio() {
    var localParticipant = _activeRoom.localParticipant;
    localParticipant.audioTracks.forEach(function (audioTrack) {
        audioTrack.track.disable();
    });
}

var screenTrack;

function shareScreenHandler() {
    if (!screenTrack) {
        navigator.mediaDevices.getDisplayMedia().then(stream => {
            screenTrack = new Twilio.Video.LocalVideoTrack(stream.getTracks()[0], {name: "screen"});
            _activeRoom.localParticipant.publishTrack(screenTrack);
            screenTrack.mediaStreamTrack.onended = () => { shareScreenHandler() };
        }).catch(() => {
            alert("Could not share the screen");
        })
    }
    else {
        _activeRoom.localParticipant.unpublishTrack(screenTrack);
        screenTrack.stop();
        screenTrack = null;
    }
}

//function zoomTrack(trackElement) {
//    var container = document.getElementById('participants');
//    if (!trackElement.classList.contains('participantZoomed')) {
//        // zoom in
//        container.childNodes.forEach(participant => {
//            if (participant.className.includes('participant')) {
//                if (participant.childNodes[0] === trackElement) {
//                    participant.childNodes[0].classList.add('participantZoomed')
//                }
//                else {
//                    participant.childNodes[0].classList.add('participantHidden')
//                }
//                if (participant.childNodes.length > 1) {
//                    participant.childNodes[1].classList.add('participantHidden');
//                }
//            }
//        });
//    }
//    else {

//        // zoom out
//        container.childNodes.forEach(participant => {
//            if (participant.className.includes('participant')) {
//                if (participant.childNodes[0] === trackElement) {
//                    participant.childNodes[0].classList.remove('participantZoomed')
//                }
//                else {
//                    participant.childNodes[0].classList.remove('participantHidden')
//                }
//                if (participant.childNodes.length > 1) {
//                    participant.childNodes[1].classList.remove('participantHidden');
//                }
//            }
//        });
//    }
//};
