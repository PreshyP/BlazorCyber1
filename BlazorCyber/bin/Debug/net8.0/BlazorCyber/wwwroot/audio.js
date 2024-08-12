function requestAudioPermission() {
    return navigator.mediaDevices.getUserMedia({ audio: true })
        .then(function (stream) {
            console.log("Audio permission granted.");
            return true;
        })
        .catch(function (err) {
            console.log("Audio permission denied: " + err);
            return false;
        });
}
