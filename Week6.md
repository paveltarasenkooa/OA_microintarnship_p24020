# Adding cross-page reference and instance filtering.

## 1. Modifying Patient Controller
By this point of time I assume You have 3 controllers created: HospitalController, PatientController and MedicalTransactionController. All these controllers should have same functionality - displaying data to the page in table form. 
In my example I'll be doing changes to Patient controller only. Similar changes would need to be done by you from Medical Transaction controller.

**Adding hospital filter to getPatients action**

Below we are adding new parameters to the action - **hospitalId**. If this parameter is empty then app patients will be displayed

```c#
[HttpGet]
public ActionResult<IEnumerable<VHospital>> GetPatients(int pageIndex = 0, int pageSize = 10, int? hospitalId = null)
{
    var hospitalName = "";            
        var patientsQuery = _context.VPatients.AsQueryable();

    if (hospitalId.HasValue)
    {
        patientsQuery = patientsQuery.Where(p => p.HospitalId == hospitalId.Value);
        hospitalName = _context.Hospitals.FirstOrDefault(x => x.Id == hospitalId.Value).Name;
    }

    var totalRecords = patientsQuery.Count();

    var patients = patientsQuery.Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

   
    var response = new
    {
        Data = patients,
        TotalRecords = totalRecords,
        PageIndex = pageIndex,
        PageSize = pageSize,
        TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize),
        HospitalName = hospitalName
    };
    return Ok(response);
}
```

We will pass this parameter from page with list of hospitals.


## 2. Modifying js script for page with hospitals to display count of patients as link to patients page for selected hospital

Let's modify your hospital page to look like this: 

```js
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
       // IT's a snippet need to be changed!
        cell3.innerHTML = '<a href="/Patient/Index' + '?hospitalId=' + hospital.hositalId +'">' + hospital.patientCount + '</a>';
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
```

## 2. Modifying js script for page with patients to handle filer by hospitalId

In script for patients page we need to pass Hospital parameter to our GetPatients() method. 
Let's also modify page a bit and add header with the name of hospital if filter by hospital Id is applied. In code snippet this element has Id "hospitalName"

```js
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

   // if filter by hospital id is applied that we are displaying name of hospital to the page.

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

    // We are getting hospital Id from query string (url) which was passed from hospitals page.
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



function updatePagingControls(currentPage, totalCount) {
    const pageSize = 10;
    const totalPages = Math.ceil(totalCount / pageSize);

    document.getElementById('currentPage').textContent = currentPage;
    document.getElementById('totalPages').textContent = totalPages;

    document.getElementById('prevPage').disabled = currentPage <= 1;
    document.getElementById('nextPage').disabled = currentPage >= totalPages;

}
```

## 3. Adding placeholder for hospital name for page with patients

Add this header (H1 element) to your page before your "table" tag:
```html
<h1 id="hospitalName"></h1>
```

