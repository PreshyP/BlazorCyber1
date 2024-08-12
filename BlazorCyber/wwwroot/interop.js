window.showNotification = function (user, message) {
    if (Notification.permission === "granted") {
        new Notification(user, { body: message });
    } else if (Notification.permission !== "denied") {
        Notification.requestPermission().then(function (permission) {
            if (permission === "granted") {
                new Notification(user, { body: message });
            }
        });
    }
};
