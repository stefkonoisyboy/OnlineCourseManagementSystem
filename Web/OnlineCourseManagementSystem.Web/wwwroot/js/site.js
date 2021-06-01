// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const btnsReplies = Array.from(document.querySelectorAll('.btn-replies'));

const collapsesReplies = Array.from(document.querySelectorAll('.collapse-replies')) 

btnsReplies.forEach(btnReplies => {
    btnReplies.addEventListener('click', e => {
        collapsesReplies.forEach(collapseReplies => {
            const pushedButtonTarget = e.target;
            const pushedBtnReplies = pushedButtonTarget.dataset['number']

            const collapseReplies = collapseReplies.dataset['number'];
            if (pushedBtnReplies == collapseReplies) {
                collapseReplies.style.visibility = `${visible}`
            }
        })
    })
})