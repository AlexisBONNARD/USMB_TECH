let map;

window.initMap = (lat, lon, adresse) => {

    setTimeout(() => {

        if (!map) {
            map = L.map('map').setView([lat, lon], 15);

            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '© OpenStreetMap'
            }).addTo(map);
        } else {
            map.setView([lat, lon], 15);
        }

        map.invalidateSize();   // 🔥 OBLIGATOIRE EN BLAZOR
        L.marker([lat, lon]).addTo(map)
            .bindPopup(adresse)
            .openPopup();

    }, 100); // laisse le DOM se stabiliser
};
