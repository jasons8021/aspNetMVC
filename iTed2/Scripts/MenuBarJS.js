var timerId;
var apValue;
var maxAp;

function statusChangeCallback(response) {
    if (response.status === 'connected') {
    } else if (response.status === 'not_authorized') {
    } else {
    }
}

function checkLoginState() {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}

window.fbAsyncInit = function () {
    FB.init({
        appId: '566290543472215',
        cookie: true,  // enable cookies to allow the server to access
        // the session
        xfbml: true,  // parse social plugins on this page
        version: 'v2.1' // use version 2.1
    });


    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });

};

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/zh_TW/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));


function Log_Out() {

    FB.getLoginStatus(function (response) {
        if (response && response.status === 'connected') {
            FB.logout(function (response) {
                document.location.reload();
            });
        }
    });

    javascript: document.location.href = 'https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://iweb.csie.ntut.edu.tw/paas17/Account/LogOff';
}

function addOneAp(id) {
    if (parseInt(apValue) < parseInt(maxAp)) {
        $.ajax({
            type: 'GET',
            url: 'https://iweb.csie.ntut.edu.tw/paas17/api/iTed2/addOneAp?id=' + id,
            contentType: 'application/json',
            success: function (response) {
                apValue++;
                $('#ap').html(response);
            },
            error: function (response) {
            }
        });
    }
    else {
        console.log('timer stop');
        clearInterval(timerId);
    }
}
function getUserInfo(id) {
    timerState = "init";
    $.ajax({
        type: 'GET',
        url: 'https://iweb.csie.ntut.edu.tw/paas17/api/iTed2/getUser?id=' + id,
        contentType: 'application/json',
        success: function (response) {
            timerState = "success";
            apValue = response[1];
            maxAp = response[2];
            $('#userName').html(response[0]);
            $('#ap').html(apValue);
            $('#maxAp').html(' / ' + maxAp);
            if (parseInt(apValue) < parseInt(maxAp)) {
                timerId = setInterval(function () { addOneAp(id); }, 3000);
                timerState = "true";
            } else {
                timerState = "false_"+apValue+"/"+maxAp;
            }
        },
        error: function (response) {
            timerState = "error";
        }
    });
}

$(document).ready(function () {
    $('#registerBtn').click(function (e) {
        location.href = '/Account/Register/';
    });
    $('#webLoginBtn').click(function (e) {
        location.href = '/Account/Login/';
    });
});