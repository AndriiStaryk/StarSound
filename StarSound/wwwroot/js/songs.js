//const songUri = 'api/Songs';
//let songs = [];

//function getSongs() {
//    fetch(songUri)
//        .then(response => response.json())
//        .then(data => _displaySongs(data))
//        .catch(error => console.error('Unable to get songs.', error));
//}

//function addSong() {
//    const addNameTextbox = document.getElementById('add-song-name');
//    const addAlbumIdTextbox = document.getElementById('add-album-id');
//    const addDurationTextbox = document.getElementById('add-duration');
//    const addIsFavoriteCheckbox = document.getElementById('add-is-favorite');
//    const addIsExplicitCheckbox = document.getElementById('add-is-explicit');
//    const addReleaseYearTextbox = document.getElementById('add-release-year');

//    const name = addNameTextbox.value.trim();
//    const albumId = parseInt(addAlbumIdTextbox.value.trim(), 10);
//    const duration = parseInt(addDurationTextbox.value.trim(), 10);
//    const isFavorite = addIsFavoriteCheckbox.checked;
//    const isExplicit = addIsExplicitCheckbox.checked;
//    const releaseYear = parseInt(addReleaseYearTextbox.value.trim(), 10);

//    if (name === '') {
//        alert('Please enter a song name.');
//        return;
//    }

//    const song = {
//        name: name,
//        albumId: albumId,
//        duration: duration,
//        isFavorite: isFavorite,
//        isExplicit: isExplicit,
//        releaseYear: releaseYear
//    };

//    fetch(songUri, {
//        method: 'POST',
//        headers: {
//            'Accept': 'application/json',
//            'Content-Type': 'application/json'
//        },
//        body: JSON.stringify(song)
//    })
//        .then(response => response.json())
//        .then(() => {
//            getSongs();
//            addNameTextbox.value = '';
//            addAlbumIdTextbox.value = '';
//            addDurationTextbox.value = '';
//            addIsFavoriteCheckbox.checked = false;
//            addIsExplicitCheckbox.checked = false;
//            addReleaseYearTextbox.value = '';
//        })
//        .catch(error => console.error('Unable to add song.', error));
//}

//function deleteSong(id) {
//    fetch(`${songUri}/${id}`, {
//        method: 'DELETE'
//    })
//        .then(() => getSongs())
//        .catch(error => console.error('Unable to delete song.', error));
//}

//function _displaySongs(data) {
//    const tBody = document.getElementById('songs');
//    tBody.innerHTML = '';

//    data.forEach(song => {
//        let tr = tBody.insertRow();

//        let td1 = tr.insertCell(0);
//        td1.appendChild(document.createTextNode(song.name));

//        let td2 = tr.insertCell(1);
//        td2.appendChild(document.createTextNode(song.albumId));

//        let td3 = tr.insertCell(2);
//        td3.appendChild(document.createTextNode(song.duration));

//        let td4 = tr.insertCell(3);
//        td4.appendChild(document.createTextNode(song.isFavorite));

//        let td5 = tr.insertCell(4);
//        td5.appendChild(document.createTextNode(song.isExplicit));

//        let td6 = tr.insertCell(5);
//        td6.appendChild(document.createTextNode(song.releaseYear));

//        let td7 = tr.insertCell(6);
//        let deleteButton = document.createElement('button');
//        deleteButton.innerText = 'Delete';
//        deleteButton.onclick = function () { deleteSong(song.id); };
//        td7.appendChild(deleteButton);
//    });

//    songs = data;
//}

//// Call getSongs() to display songs when the page loads
//getSongs();


const songUri = 'api/Songs';
let songs = [];

function getSongs() {
    fetch(songUri)
        .then(response => response.json())
        .then(data => _displaySongs(data))
        .catch(error => console.error('Unable to get songs.', error));
}

function addSong() {
    const addNameTextbox = document.getElementById('add-song-name');
    const addAlbumIdTextbox = document.getElementById('add-album-id');
    const addDurationTextbox = document.getElementById('add-duration');
    const addIsFavoriteCheckbox = document.getElementById('add-is-favorite');
    const addIsExplicitCheckbox = document.getElementById('add-is-explicit');
    const addReleaseYearTextbox = document.getElementById('add-release-year');

    const name = addNameTextbox.value.trim();
    const albumId = parseInt(addAlbumIdTextbox.value.trim(), 10);
    const duration = parseInt(addDurationTextbox.value.trim(), 10);
    const isFavorite = addIsFavoriteCheckbox.checked;
    const isExplicit = addIsExplicitCheckbox.checked;
    const releaseYear = parseInt(addReleaseYearTextbox.value.trim(), 10);

    if (name === '') {
        alert('Please enter a song name.');
        return;
    }

    const song = {
        name: name,
        albumId: albumId,
        duration: duration,
        isFavorite: isFavorite,
        isExplicit: isExplicit,
        releaseYear: releaseYear
    };

    fetch(songUri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(song)
    })
        .then(response => response.json())
        .then(() => {
            getSongs();
            addNameTextbox.value = '';
            addAlbumIdTextbox.value = '';
            addDurationTextbox.value = '';
            addIsFavoriteCheckbox.checked = false;
            addIsExplicitCheckbox.checked = false;
            addReleaseYearTextbox.value = '';
        })
        .catch(error => console.error('Unable to add song.', error));
}

function deleteSong(id) {
    fetch(`${songUri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getSongs())
        .catch(error => console.error('Unable to delete song.', error));
}

function _displaySongs(data) {
    const tBody = document.getElementById('songs');
    tBody.innerHTML = '';

    data.forEach(song => {
        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(document.createTextNode(song.name));

        let td2 = tr.insertCell(1);
        td2.appendChild(document.createTextNode(song.albumId));

        let td3 = tr.insertCell(2);
        td3.appendChild(document.createTextNode(song.duration));

        let td4 = tr.insertCell(3);
        td4.appendChild(document.createTextNode(song.isFavorite));

        let td5 = tr.insertCell(4);
        td5.appendChild(document.createTextNode(song.isExplicit));

        let td6 = tr.insertCell(5);
        td6.appendChild(document.createTextNode(song.releaseYear));

        let td7 = tr.insertCell(6);
        let editButton = document.createElement('button');
        editButton.innerText = 'Edit';
        editButton.classList.add('edit-button');
        editButton.onclick = function () { editSong(song); };
        td7.appendChild(editButton);
    });

    songs = data;
}

function editSong(song) {
    document.getElementById('edit-id').value = song.id;
    document.getElementById('edit-song-name').value = song.name;
    document.getElementById('edit-album-id').value = song.albumId;
    document.getElementById('edit-duration').value = song.duration;
    document.getElementById('edit-is-favorite').checked = song.isFavorite;
    document.getElementById('edit-is-explicit').checked = song.isExplicit;
    document.getElementById('edit-release-year').value = song.releaseYear;

    document.getElementById('add-song-form').style.display = 'none';
    document.getElementById('editSong').style.display = 'block';
}

function updateSong() {
    // Your updateSong function implementation here
}

function closeInput() {
    document.getElementById('add-song-form').style.display = 'block';
    document.getElementById('editSong').style.display = 'none';
}