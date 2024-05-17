const songUri = 'api/Songs';
const albumsUri = 'api/Albums';
const performersUri = 'api/Performers';
const songPerformersUri = 'api/SongPerformers'
let songs = [];
let albums = [];
let performers = [];
let songPerformers = [];

const albumDropdown = document.getElementById('album-dropdown');
const performerDropdown = document.getElementById('performer-dropdown');


function getSongs() {
    return fetch(songUri)
        .then(response => response.json())
        .catch(error => {
            console.error('Unable to get songs.', error);
            throw error; 
        });
}

function getAlbums() {
    return fetch(albumsUri)
        .then(response => response.json())
        .catch(error => {
            console.error('Unable to get albums.', error);
            throw error;
        });
}

function getPerformers() {
    return fetch(performersUri)
        .then(response => response.json())
        .catch(error => {
            console.error('Unable to get performers.', error);
            throw error;
        });
}

function getSongPerformers() {
    return fetch(songPerformersUri)
        .then(response => response.json())
        .catch(error => {
            console.error('Unable to get songPerformers.', error);
            throw error;
        });
}

function getThingsReady() {
    Promise.all([getSongs(), getAlbums(), getPerformers(), getSongPerformers()])
        .then(([songs, albums, performers, songPerformers]) => {
            console.log(`Overall songs count: ${songs.length}`);
            console.log(`Overall albums count: ${albums.length}`);
            console.log(`Overall performers count: ${performers.length}`);
            console.log(`Overall songPerformers count: ${songPerformers.length}`);
            //albumDropdown.
            //fillDropBoxes(albums, performers);
            albumDropdown.innerHTML = '<option value="" disabled selected>Select Album</option>';
            performerDropdown.innerHTML = '<option value="" disabled selected>Select Performer</option>';
            populateDropdown(albumDropdown, albums);
            populateDropdown(performerDropdown, performers);
            _displaySongs(songs, albums, performers, songPerformers);
        })
        .catch(error => console.error('Error while fetching songs and albums:', error));
}



function addSong() {
    const addNameTextbox = document.getElementById('add-song-name');
    const addDurationTextbox = document.getElementById('add-duration');
    const addIsFavoriteCheckbox = document.getElementById('add-is-favorite');
    const addIsExplicitCheckbox = document.getElementById('add-is-explicit');
    const addReleaseYearTextbox = document.getElementById('add-release-year');

    const name = addNameTextbox.value.trim();
    const albumId = parseInt(document.getElementById('album-dropdown').value.trim(), 10);
    const performerId = parseInt(document.getElementById('performer-dropdown').value.trim(), 10);
    const duration = parseInt(addDurationTextbox.value.trim(), 10);
    const isFavorite = addIsFavoriteCheckbox.checked;
    const isExplicit = addIsExplicitCheckbox.checked;
    const releaseYear = parseInt(addReleaseYearTextbox.value.trim(), 10);

    const song = {
        name: name,
        albumId: albumId,
        duration: duration,
        isFavorite: isFavorite,
        isExplicit: isExplicit,
        releaseYear: releaseYear
    };

    songValidation(song);

    fetch(songUri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(song)
    })
        .then(response => response.json())
        .then(newSong => {
            console.log('Added song:', newSong);

            // Now create the song-performer relationship
            const songPerformer = {
                songId: newSong.id,
                performerId: performerId
            };

            return fetch(songPerformersUri, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(songPerformer)
            });
        })
        .then(response => response.json())
        .then(() => {
            console.log('Added song-performer relationship');
            getThingsReady();

            // Clear form fields
            addNameTextbox.value = '';
            addDurationTextbox.value = '';
            addIsFavoriteCheckbox.checked = false;
            addIsExplicitCheckbox.checked = false;
            addReleaseYearTextbox.value = '';
            albumDropdown.selectedIndex = 0;
            performerDropdown.selectedIndex = 0;
        })
        .catch(error => console.error('Unable to add song or song-performer relationship.', error));
}


function deleteSong(id) {

    fetch(`${songUri}/${id}`, {
        method: 'DELETE'
    })
        //.then(() => getSongs())
        .then(() => getThingsReady())
        .catch(error => console.error('Unable to delete song.', error));
}

function _displaySongs(songs, albums, performers, songPerformers) {

    const tBody = document.getElementById('songs');
    tBody.innerHTML = '';

    songs.forEach(song => {
        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(document.createTextNode(song.name));


        let performerName = getPerformerName(song.id, performers, songPerformers);
        let td2 = tr.insertCell(1);
        td2.appendChild(document.createTextNode(performerName));

        let albumName = getAlbumName(song.albumId, albums);
        let td3 = tr.insertCell(2);
        td3.appendChild(document.createTextNode(albumName));

        let td4 = tr.insertCell(3);
        td4.appendChild(document.createTextNode(song.duration));

        let td5 = tr.insertCell(4);
        td5.appendChild(document.createTextNode(song.isFavorite));

        let td6 = tr.insertCell(5);
        td6.appendChild(document.createTextNode(song.isExplicit));

        let td7 = tr.insertCell(6);
        td7.appendChild(document.createTextNode(song.releaseYear));

        let td8 = tr.insertCell(7);
        let editButton = document.createElement('button')
        editButton.innerText = 'Edit';
        editButton.classList.add('edit-button');
        editButton.onclick = function () { editSong(song); };
        td8.appendChild(editButton);

        let deleteButton = document.createElement('button');
        deleteButton.innerText = "Delete";
        deleteButton.classList.add('delete-button');

        deleteButton.onclick = function () { deleteSong(song.id); }
        td8.appendChild(deleteButton)

    });

    //songs = songs;

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
    //const albumId = parseInt(editAlbumIdTextbox.value.trim(), 10);
    const albumId = parseInt(document.getElementById('album-dropdown').value.trim(), 10);
    const performerId = parseInt(document.getElementById('performer-dropdown').value.trim(), 10);
    const duration = parseInt(editDurationTextbox.value.trim(), 10);
    const isFavorite = editIsFavoriteCheckbox.checked;
    const isExplicit = editIsExplicitCheckbox.checked;
    const releaseYear = parseInt(editReleaseYearTextbox.value.trim(), 10);

    const song = {
        id: id,
        name: name,
        albumId: albumId,
        duration: duration,
        isFavorite: isFavorite,
        isExplicit: isExplicit,
        releaseYear: releaseYear
    };

    songValidation(song)

    fetch(`${songUri}/${id}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(song)
    })
        .then(() => {
            //getSongs();
            getThingsReady();
            closeInput();
        })
        .catch(error => console.error('Unable to update song.', error));
}


function songValidation(song) {
    if (song.name === '') {
        alert('Please enter a song name.');
    }
    if (song.releaseYear < 1200 && song.releaseYear > 2024) {
        alert('Please enter release year from 1200 to 2024.');
    }
    if (song.duration <= 0) {
        alert('Please enter duration greater than 0.');
    }
}



function populateDropdown(dropdown, items) {
    dropdown.innerHTML = '<option value="" disabled selected>Select</option>'; 
    items.forEach(item => {
        const option = document.createElement('option');
        option.value = item.id;
        option.textContent = item.name;
        dropdown.appendChild(option);
    });
}



//function fillDropBoxes(albums, performers) {
    
//    albums.forEach(album => {
//        const option = document.createElement('option');
//        option.value = album.id;
//        option.textContent = album.name;
//        albumDropdown.appendChild(option);
//    });

//    performers.forEach(performer => {
//        const option = document.createElement('option');
//        option.value = performer.id;
//        option.textContent = performer.name;
//        performerDropdown.appendChild(option);
//    });
//}


function closeInput() {
    document.getElementById('addSong').style.display = 'block';
    document.getElementById('editSong').style.display = 'none';
}


function getAlbumName(albumId, albums) {
    const album = albums.find(album => album.id === albumId);
    return album ? album.name : "Album not found";
}


function getPerformerName(songId, performers, songPerformers) {
    const songPerformer = songPerformers.find(sp => sp.songId === songId);

    if (songPerformer) {
        const performer = performers.find(p => p.id === songPerformer.performerId);
        return performer ? performer.name : "Performer not found"
    } else {
        return "Performer not found"
    }
}

