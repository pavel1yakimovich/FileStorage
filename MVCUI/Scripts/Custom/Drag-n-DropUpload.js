﻿$(document).ready(function () {

    var dropZone = $('#dropZone'),
        maxFileSize = 20000000;

    // browser support?
    if (typeof (window.FileReader) == 'undefined') {
        dropZone.text('It is not supported by your browser');
        dropZone.addClass('error');
    }

    // ondragover
    dropZone[0].ondragover = function () {
        dropZone.addClass('hover');
        return false;
    };

    // ondragleave
    dropZone[0].ondragleave = function () {
        dropZone.removeClass('hover');
        return false;
    };

    // drop
    dropZone[0].ondrop = function (event) {
        event.preventDefault();
        dropZone.removeClass('hover');
        dropZone.addClass('drop');

        var file = event.dataTransfer.files[0];

        // check size
        if (file.size > maxFileSize) {
            dropZone.text('Too big file!');
            dropZone.addClass('error');
            return false;
        }

        // create request
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener('progress', uploadProgress, false);
        xhr.onreadystatechange = stateChange;
        xhr.open('POST', '/File/Upload');
        xhr.setRequestHeader('X-FILE-NAME', file.name);
        xhr.setRequestHeader('X-FILE-TYPE', file.type);
        xhr.setRequestHeader('X-FILE-ISPUBLIC', $("#IsPublic").is(':checked'));
        xhr.setRequestHeader('X-FILE-Description', document.getElementById("Description").value);
        xhr.send(file);
    };

    // percentage of uploading
    function uploadProgress(event) {
        var percent = parseInt(event.loaded / event.total * 100);
        dropZone.text('Upload: ' + percent + '%');
    }

    function stateChange(event) {
        if (event.target.readyState == 4) {
            if (event.target.status == 200) {
                dropZone.text('Uploaded!');
            } else {
                dropZone.text('Error');
                dropZone.addClass('error');
            }
        }
    }

});