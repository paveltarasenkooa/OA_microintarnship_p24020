document.addEventListener("DOMContentLoaded", function () {

    document.getElementById('spinner').style.display = 'block';

    fetch('/api/hospital')
        .then(response => response.json())
        .then(data => {

            // document.getElementById('hospitalsTable').style.display = 'inline-table';
            populateTable(data);
            document.getElementById('spinner').style.display = 'none';
        })
        .catch(error => console.error('Unable to get hospital data.', error));
});

function populateTable(data) {
    const tableBody = document.getElementById('hospitalsTable').getElementsByTagName('tbody')[0];
    tableBody.innerHTML = ''; // Clear the table body

    // Iterate through the data and append rows to the table body
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