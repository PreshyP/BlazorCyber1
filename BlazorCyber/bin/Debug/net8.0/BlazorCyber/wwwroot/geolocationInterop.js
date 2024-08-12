// wwwroot/geolocationInterop.js
window.geolocationInterop = {
    getCurrentPosition: function (options) {
        console.log("getCurrentPosition called with options:", options);
        return new Promise((resolve, reject) => {
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    console.log("Geolocation position obtained:", position);
                    resolve({
                        coords: {
                            latitude: position.coords.latitude,
                            longitude: position.coords.longitude,
                            accuracy: position.coords.accuracy,
                            altitude: position.coords.altitude,
                            altitudeAccuracy: position.coords.altitudeAccuracy,
                            heading: position.coords.heading,
                            speed: position.coords.speed
                        },
                        timestamp: position.timestamp
                    });
                },
                (error) => {
                    console.error("Geolocation error:", error);
                    reject(error);
                },
                options
            );
        });
    }
};
