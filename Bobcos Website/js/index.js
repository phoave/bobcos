function showDownloadPopup() {
    document.getElementById('download-popup').style.display = 'block';
}

function closeDownloadPopup() {
    document.getElementById('download-popup').style.display = 'none';
}


window.onclick = function(event) {
    if (event.target == document.getElementById('download-popup')) {
        document.getElementById('download-popup').style.display = 'none';
    }
}
