const uri = 'api/Genres';
let genres = [];

function getGenres() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayGenres(data))
        .catch(error => console.error('Unable to get genres.', error));
}

function addGenre() {
    const addNameTextbox = document.getElementById('add-name');

    const name = addNameTextbox.value.trim()
   
    if (name === '') {
        alert('Please enter a genre name.');
        return;
    }

    if (genres.some(genre => genre.name.toLowerCase() === name.toLowerCase())) {
        alert('Genre with this name already exists.');
        return;
    }

    const genre = {
        name: name,
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(genre)
    })
        .then(response => response.json())
        .then(() => {
            getGenres();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add genre.', error));
}

function deleteGenre(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getGenres())
        .catch(error => console.error('Unable to delete genre.', error));
}

function displayEditForm(id) {
    const genre = genres.find(genre => genre.id === id);

    document.getElementById('edit-id').value = genre.id;
    document.getElementById('edit-name').value = genre.name;
    document.getElementById('editGenre').style.display = 'block';
}

function updateGenre() {
    const genreId = document.getElementById('edit-id').value;
    const name = document.getElementById('edit-name').value.trim()

    if (name === '') {
        alert('Please enter a genre name.');
        return;
    }

    if (genres.some(genre => genre.name.toLowerCase() === name.toLowerCase())) {
        alert('Genre with this name already exists.');
        return;
    }

    const genre = {
        id: parseInt(genreId, 10),
        name: name,
    };

    fetch(`${uri}/${genreId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(genre)
    })
        .then(() => getGenres())
        .catch(error => console.error('Unable to update genre.', error));

    closeInput();
}

function closeInput() {
    document.getElementById('editGenre').style.display = 'none';
}

function _displayGenres(data) {
    const tBody = document.getElementById('genres');
    tBody.innerHTML = '';

    data.forEach(genre => {
        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(document.createTextNode(genre.name));

        let td2 = tr.insertCell(1);
        let editButton = document.createElement('button');
        editButton.innerText = 'Edit';
        editButton.onclick = function () { displayEditForm(genre.id); };
        td2.appendChild(editButton);

        let td3 = tr.insertCell(2);
        let deleteButton = document.createElement('button');
        deleteButton.innerText = 'Delete';
        deleteButton.onclick = function () { deleteGenre(genre.id); };
        td3.appendChild(deleteButton);
    });

    genres = data;
}
