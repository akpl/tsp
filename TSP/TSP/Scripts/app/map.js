function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 50.061820, lng: 19.936709 },
        zoom: 13
    });

    var locationSearchInput = document.getElementById('location-input');
    var autocomplete = new google.maps.places.Autocomplete(locationSearchInput);
    autocomplete.bindTo('bounds', map);
    autocomplete.addListener('place_changed', function () {
        var place = autocomplete.getPlace();
        var marker = new google.maps.Marker({
            map: map,
            position: place.geometry.location
        });
    });
}