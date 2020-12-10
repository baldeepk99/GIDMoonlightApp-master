
// Function that finds the current location coords and use those data in another function 'successfulLookups'
function getLocation() {
    navigator.geolocation.getCurrentPosition(successfulLookups, console.log);
}

// This function receives the coordinates(lat and long) and use them to get the address string.
const successfulLookups = (position) => {
    var checkBox = document.getElementById("currentLocationCheckBox");
    var text = document.getElementById("currentLocationInputBox");
    const latitude = position.coords.latitude;
    const longitude = position.coords.longitude;
    const key = "f33544a35dd049b192f5489f7d26d094";

    // Using opencagedata api for reverse geocoding the lat and long coords.
    let url = `https://api.opencagedata.com/geocode/v1/json?q=${latitude}+${longitude}&key=${key}`;

    fetch(url)
        .then(response => response.json()) // converting the result in JSON format
        .then(data => {
            let info = data;
            let location = info.results[0].formatted; // Location in String format.

            // auto fill the current location in the location textbox only if the checkbox is checked.
            if (checkBox.checked == true) {
                text.value = location;
            } else {
                text.value = "";
            }
        });
    
    
};