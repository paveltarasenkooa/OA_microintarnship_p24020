document.addEventListener("DOMContentLoaded", function () {

    document.getElementById('spinner').style.display = 'block';

    let currentPage = 1;
    const pageSize = 10;
    loadPage(currentPage);
    document.getElementById('nextPage').addEventListener('click', function () {
        currentPage++;
        loadPage(currentPage);
    });



    document.getElementById('prevPage').addEventListener('click', function () {
        if (currentPage > 1) {
            currentPage--;
            loadPage(currentPage);
        }
    });
});

function populateTable(data, hospitalId) {
    const tableBody = document.getElementById('patientsTable').getElementsByTagName('tbody')[0];
    tableBody.innerHTML = '';

    var hospitalName = 'all hospitals';
    if (hospitalId) {
        hospitalName = data.hospitalName;
    }
    document.getElementById('hospitalName').innerText = 'Patients for ' + hospitalName;
    data.data.forEach(hospital => {
        let row = tableBody.insertRow();
        let cell1 = row.insertCell(0);
        let cell2 = row.insertCell(1);
        let cell3 = row.insertCell(2);
        let cell4 = row.insertCell(3);

        cell1.innerHTML = hospital.firstName;
        cell2.innerHTML = hospital.lastName;
        cell3.innerHTML = hospital.hospitalName;
        cell4.innerHTML = hospital.medicalTransactionsCount;
    });
}

function loadPage(pageIndex) {
    document.getElementById('spinner').style.display = 'block';
    const searchParams = new URLSearchParams(window.location.search);
    var hospitalId = searchParams.get('hospitalId');
    var url = '/api/patient?pageIndex=' + pageIndex +'&pageSize=10';
    if (hospitalId) {
        url += '&hospitalId=' + hospitalId;
    }
    fetch(url )
        .then(response => response.json())
        .then(data => {
            populateTable(data, hospitalId);
            updatePagingControls(pageIndex, data.totalRecords);
            document.getElementById('spinner').style.display = 'none';
        })
        .catch(error => console.error('Unable to get hospital data.', error));
}



//function populateTable(data) {
//    const tableBody = document.getElementById('patientsTable').getElementsByTagName('tbody')[0];
//    tableBody.innerHTML = '';


//    data.forEach(hospital => {
//        let row = tableBody.insertRow();
//        let cell1 = row.insertCell(0);
//        let cell2 = row.insertCell(1);
//        let cell3 = row.insertCell(2);

//        cell1.innerHTML = hospital.hospitalName;
//        cell2.innerHTML = hospital.hospitalType;
//        cell3.innerHTML = hospital.patientCount;
//    });
//}

function updatePagingControls(currentPage, totalCount) {
    const pageSize = 10;
    const totalPages = Math.ceil(totalCount / pageSize);

    document.getElementById('currentPage').textContent = currentPage;
    document.getElementById('totalPages').textContent = totalPages;

    document.getElementById('prevPage').disabled = currentPage <= 1;
    document.getElementById('nextPage').disabled = currentPage >= totalPages;

}