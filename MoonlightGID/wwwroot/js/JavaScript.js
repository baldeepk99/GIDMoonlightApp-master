

function getLocation() {
    navigator.geolocation.getCurrentPosition(successfulLookups, console.log);
}
const successfulLookups = (position) => {
    var checkBox = document.getElementById("currentLocationCheckBox");
    var text = document.getElementById("currentLocationInputBox");
    const latitude = position.coords.latitude;
    const longitude = position.coords.longitude;
    const key = "f33544a35dd049b192f5489f7d26d094";
    let url = `https://api.opencagedata.com/geocode/v1/json?q=${latitude}+${longitude}&key=${key}`;
    fetch(url)
        .then(response => response.json())
        .then(data => {
            let info = data;
            let location = info.results[0].formatted;
            if (checkBox.checked == true) {
                text.value = location;
            } else {
                text.value = "";
            }
        });
    
    
};