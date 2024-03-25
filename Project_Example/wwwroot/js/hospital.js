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

function populateTable(data) {
    const tableBody = document.getElementById('hospitalsTable').getElementsByTagName('tbody')[0];
    tableBody.innerHTML = ''; 

    
    data.forEach(hospital => {
        let row = tableBody.insertRow();
        let cell1 = row.insertCell(0);
        let cell2 = row.insertCell(1);
        let cell3 = row.insertCell(2);

        cell1.innerHTML = hospital.hospitalName;
        cell2.innerHTML = hospital.hospitalType;
        cell3.innerHTML = hospital.patientCount;
    });
}

function loadPage(pageIndex) {
    document.getElementById('spinner').style.display = 'block';

    fetch(`/api/hospital?pageIndex=${pageIndex}&pageSize=10`)
        .then(response => response.json())
        .then(data => {
            populateTable(data.data); 
            updatePagingControls(pageIndex, data.totalRecords);
            document.getElementById('spinner').style.display = 'none';
        })
        .catch(error => console.error('Unable to get hospital data.', error));
}



function populateTable(data) {
    const tableBody = document.getElementById('hospitalsTable').getElementsByTagName('tbody')[0];
    tableBody.innerHTML = ''; 

   
    data.forEach(hospital => {
        let row = tableBody.insertRow();
        let cell1 = row.insertCell(0);
        let cell2 = row.insertCell(1);
        let cell3 = row.insertCell(2);

        cell1.innerHTML = hospital.hospitalName;
        cell2.innerHTML = hospital.hospitalType;
        cell3.innerHTML = hospital.patientCount;
    });
}

function updatePagingControls(currentPage, totalCount) {
    const pageSize = 10; 
    const totalPages = Math.ceil(totalCount / pageSize);

    document.getElementById('currentPage').textContent = currentPage;
    document.getElementById('totalPages').textContent = totalPages;

    document.getElementById('prevPage').disabled = currentPage <= 1;
    document.getElementById('nextPage').disabled = currentPage >= totalPages; 
    
}