const songUri = 'api/Songs';
const albumsUri = 'api/Albums';
let songs = [];
let albums = [];

function getSongs() {
    return fetch(songUri)
        .then(response => response.json())
        .catch(error => {
            console.error('Unable to get songs.', error);
            throw error; // Rethrow the error to catch it in Promise.all()
        });
}

function getAlbums() {
    return fetch(albumsUri)
        .then(response => response.json())
        .catch(error => {
            console.error('Unable to get albums.', error);
            throw error; // Rethrow the error to catch it in Promise.all()
        });
}

function getThingsReady() {
    Promise.all([getSongs(), getAlbums()]) // Wait for both promises to resolve
        .then(([songs, albums]) => {
            console.log(`Overall songs count: ${songs.length}`);
            console.log(`Overall albums count: ${albums.length}`);
            _displaySongs(songs, albums);
        })
        .catch(error => console.error('Error while fetching songs and albums:', error));
}

//if (albums.length !== 0) {
 //   _displaySongs(songs);
//}

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

function _displaySongs(songs, albums) {

    const tBody = document.getElementById('songs');
    tBody.innerHTML = '';

    songs.forEach(song => {
        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(document.createTextNode(song.name));

        let albumName = getAlbumName(song.albumId, albums);
        let td2 = tr.insertCell(1);
        td2.appendChild(document.createTextNode(albumName));

        let td3 = tr.insertCell(2);
        td3.appendChild(document.createTextNode(song.duration));

        let td4 = tr.insertCell(3);
        td4.appendChild(document.createTextNode(song.isFavorite));

        let td5 = tr.insertCell(4);
        td5.appendChild(document.createTextNode(song.isExplicit));

        let td6 = tr.insertCell(5);
        td6.appendChild(document.createTextNode(song.releaseYear));

        let td7 = tr.insertCell(6);
        let editButton = document.createElement('button')
        editButton.innerText = 'Edit';
        editButton.classList.add('edit-button');
        editButton.onclick = function () { editSong(song); };
        td7.appendChild(editButton);

        let deleteButton = document.createElement('button');
        deleteButton.innerText = "Delete";
        deleteButton.classList.add('delete-button');

        deleteButton.onclick = function () { deleteSong(song.id); }
        td7.appendChild(deleteButton)

    });

    songs = songs;

}


function editSong(song) {
    document.getElementById('addSong').style.display = 'none';
    document.getElementById('editSong').style.display = 'block';

    document.getElementById('edit-id').value = song.id;
    document.getElementById('edit-song-name').value = song.name;
    document.getElementById('edit-album-id').value = song.albumId;
    document.getElementById('edit-duration').value = song.duration;
    document.getElementById('edit-release-year').value = song.releaseYear;

    if (song.isFavorite) {
        document.getElementById('edit-is-favorite').checked = true;
    } else {
        document.getElementById('edit-is-favorite').checked = false;
    }

    if (song.isExplicit) {
        document.getElementById('edit-is-explicit').checked = true;
    } else {
        document.getElementById('edit-is-explicit').checked = false;
    }
}

function updateSong() {
    const editIdTextbox = document.getElementById('edit-id');
    const editNameTextbox = document.getElementById('edit-song-name');
    const editAlbumIdTextbox = document.getElementById('edit-album-id');
    const editDurationTextbox = document.getElementById('edit-duration');
    const editIsFavoriteCheckbox = document.getElementById('edit-is-favorite');
    const editIsExplicitCheckbox = document.getElementById('edit-is-explicit');
    const editReleaseYearTextbox = document.getElementById('edit-release-year');

    const id = parseInt(editIdTextbox.value.trim(), 10);
    const name = editNameTextbox.value.trim();
    const albumId = parseInt(editAlbumIdTextbox.value.trim(), 10);
    const duration = parseInt(editDurationTextbox.value.trim(), 10);
    const isFavorite = editIsFavoriteCheckbox.checked;
    const isExplicit = editIsExplicitCheckbox.checked;
    const releaseYear = parseInt(editReleaseYearTextbox.value.trim(), 10);

    if (name === '') {
        alert('Please enter a song name.');
        return;
    }

    const song = {
        id: id,
        name: name,
        albumId: albumId,
        duration: duration,
        isFavorite: isFavorite,
        isExplicit: isExplicit,
        releaseYear: releaseYear
    };

    fetch(`${songUri}/${id}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(song)
    })
        .then(() => {
            getSongs();
            closeInput();
        })
        .catch(error => console.error('Unable to update song.', error));
}


function closeInput() {
    document.getElementById('addSong').style.display = 'block';
    document.getElementById('editSong').style.display = 'none';
}


function getAlbumName(albumId, albums) {
    const album = albums.find(album => album.id === albumId);
    return album ? album.name : "Album not found";
}


function getPerformers(songId) {

}