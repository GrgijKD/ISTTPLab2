const uri = 'api/Computers';
let computers = [];

function displayEditForm(id) {
    const computer = computers.find(c => c.id === id);

    document.getElementById('edit-id').value = computer.id;
    document.getElementById('edit-name').value = computer.name;
    document.getElementById('edit-type').value = computer.type;
    document.getElementById('edit-brand').value = computer.brand;
    document.getElementById('edit-screenSize').value = computer.screenSize;
    document.getElementById('edit-resolution').value = computer.resolution;
    document.getElementById('edit-storage').value = computer.storage;
    document.getElementById('edit-ramId').value = computer.ramId;
    document.getElementById('edit-processorId').value = computer.processorId;
    document.getElementById('edit-gpuId').value = computer.gpuId || '';
    document.getElementById('edit-price').value = computer.price;

    document.getElementById('editComputer').style.display = 'block';
}

function getComputers() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayComputers(data))
        .catch(error => console.error('Unable to get computers.', error));
}

function deleteComputer(id) {
    fetch(`${uri}/${id}`, { method: 'DELETE' })
        .then(() => getComputers())
        .catch(error => console.error('Unable to delete computer.', error));
}

function updateComputer(event) {
    event.preventDefault();

    const form = document.getElementById('edit-form');
    let isValid = true;

    const requiredTextFields = form.querySelectorAll('input[type="text"][required], input[type="number"][required]');
    requiredTextFields.forEach(field => {
        const errorSpan = field.parentElement.querySelector('.error-message');
        if (!field.value.trim()) {
            isValid = false;
            errorSpan.textContent = 'Заповніть це поле';
            field.classList.add('is-invalid');
        } else {
            errorSpan.textContent = '';
            field.classList.remove('is-invalid');
        }
    });

    ['edit-ramId', 'edit-processorId'].forEach(id => {
        const input = document.getElementById(id);
        const errorSpan = input.parentElement.querySelector('.error-message');
        const val = parseInt(input.value, 10);

        if (!val || val <= 0) {
            isValid = false;
            errorSpan.textContent = 'Введіть коректний ID (число більше 0)';
            input.classList.add('is-invalid');
        } else {
            errorSpan.textContent = '';
            input.classList.remove('is-invalid');
        }
    });

    const gpuInput = document.getElementById('edit-gpuId');
    const gpuError = gpuInput.parentElement.querySelector('.error-message');
    const gpuVal = gpuInput.value ? parseInt(gpuInput.value, 10) : null;
    if (gpuInput.value && (!gpuVal || gpuVal <= 0)) {
        isValid = false;
        gpuError.textContent = 'Введіть коректний ID (число більше 0) або залиште порожнім';
        gpuInput.classList.add('is-invalid');
    } else {
        gpuError.textContent = '';
        gpuInput.classList.remove('is-invalid');
    }

    const priceInput = form.querySelector('#edit-price');
    const priceValue = Number(priceInput.value);
    const priceError = priceInput.parentElement.querySelector('.error-message');
    if (!Number.isInteger(priceValue) || priceValue <= 0) {
        isValid = false;
        priceError.textContent = 'Ціна має бути цілим числом більше 0';
        priceInput.classList.add('is-invalid');
    } else {
        priceError.textContent = '';
        priceInput.classList.remove('is-invalid');
    }

    if (!isValid) return;

    const computerId = parseInt(document.getElementById('edit-id').value, 10);

    const computer = {
        id: computerId,
        name: document.getElementById('edit-name').value.trim(),
        type: document.getElementById('edit-type').value.trim(),
        brand: document.getElementById('edit-brand').value.trim(),
        screenSize: document.getElementById('edit-screenSize').value.trim(),
        resolution: document.getElementById('edit-resolution').value.trim(),
        storage: document.getElementById('edit-storage').value.trim(),
        ramId: parseInt(document.getElementById('edit-ramId').value, 10),
        processorId: parseInt(document.getElementById('edit-processorId').value, 10),
        gpuId: gpuVal,
        price: priceValue
    };

    fetch(`${uri}/${computerId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(computer)
    })
        .then(response => {
            if (!response.ok) throw new Error('Помилка оновлення');
        })
        .then(() => {
            getComputers();
            closeEdit();
        })
        .catch(error => {
            console.error('Неможливо оновити.', error);
            alert('Помилка при оновленні комп\'ютера.');
        });
}

function closeEdit() {
    document.getElementById('editComputer').style.display = 'none';
}

function _displayComputers(data) {
    const tBody = document.getElementById('computers');
    tBody.innerHTML = '';

    const button = document.createElement('button');

    data.forEach(computer => {
        let editButton = document.createElement('button');
        editButton.innerText = 'Редагувати';
        editButton.className = 'btn btn-primary';
        editButton.onclick = () => displayEditForm(computer.id);

        let deleteButton = document.createElement('button');
        deleteButton.innerText = 'Видалити';
        deleteButton.className = 'btn btn-primary';
        deleteButton.onclick = () => deleteComputer(computer.id);

        let tr = tBody.insertRow();

        tr.insertCell(0).innerText = computer.name;
        tr.insertCell(1).innerText = computer.type;
        tr.insertCell(2).innerText = computer.brand;
        tr.insertCell(3).innerText = computer.screenSize;
        tr.insertCell(4).innerText = computer.resolution;
        tr.insertCell(5).innerText = computer.storage;
        tr.insertCell(6).innerText = computer.ram?.name || `ID: ${computer.ramId}`;
        tr.insertCell(7).innerText = computer.processor?.name || `ID: ${computer.processorId}`;
        tr.insertCell(8).innerText = computer.gpu?.name || (computer.gpuId ? `ID: ${computer.gpuId}` : '-');
        tr.insertCell(9).innerText = computer.price;

        const actionCell = tr.insertCell(10);
        actionCell.appendChild(editButton);
        actionCell.appendChild(deleteButton);
    });

    computers = data;
}