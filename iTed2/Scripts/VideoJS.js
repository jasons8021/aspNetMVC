function setMyVideo(title, video) {
    if (video == "") {
        $('#TEDtalk').css('display', 'inline-block');
        $('#videoText').css('display', 'none');
    }
    else {
        $('.googleVideo-text').css('display', 'block');
        loadVideo(video);
    }
}

function loadVideo(video) {
    $('#iframe-src').attr('src', video);
    $('#iframe-div').css('display', 'block');
    //$('#iframe-src').animate({ width: "400px", height: "274px" }, 1000);
    $('#iframe-src').animate({ width: "854px", height: "481px" }, 700);
    $('#iframe-src').data('animated', true);
    $('#TEDtalk').css('display', 'none');
    $('#videoText').css('display', 'inline-block');
}