# Adding Paging to the Hospital Table

This guide outlines the steps to add paging functionality to the Hospital page within the .NET Core project. The process involves modifications to the Razor page, JavaScript, and the ASP.NET Core controller.

## Overview

1. **Controller Modification**: Update the `HospitalController` to support paging parameters and modify the query to return only a specific page of results along with the total count of items.
2. **JavaScript Changes**: Adjust the JavaScript responsible for fetching data to include paging parameters and handle the display of paging controls.
3. **Razor Page Update**: Modify the Razor view to include paging controls and placeholders for current and total pages.

## Detailed Steps

### Step 1: Controller Changes

Update the `HospitalController` to accept paging parameters (`pageIndex` and `pageSize`) and adjust the query to fetch only a subset of records.

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA_Example_Project.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OA_Example_Project.Controllers
{
    [Route("api/hospital")]
    [ApiController]
    public class HospitalController : Controller
    {
        private readonly OaProjectContext _context;

        public HospitalController(OaProjectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VHospital>>> GetHospitals(int pageIndex = 0, int pageSize = 10)
        {
            var query = _context.VHospitals;
            var totalItems = await query.CountAsync();
            var items = await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

            Response.Headers.Add("X-Total-Count", totalItems.ToString());
            return items;
        }
    }
}
```
### Step 2: JavaScript Changes
Modify your JavaScript "hospital.js" to send paging parameters with the request and update the paging controls based on the response.

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
```

### Step 3: Razor Page Update
Add paging controls and placeholders for the current and total pages in your Razor page.
```cshtml
@page
@model OA_Example_Project.Pages.Hospital.IndexModel
@{
    ViewData["Title"] = "Hospitals";
}

<script src="~/js/hospital.js"></script>
<link rel="stylesheet" href="~/css/TableStyle.css" asp-append-version="true" />

<div id="spinner" style="display: none;">
  
    <img src="/images/zkzg.gif" alt="Loading..." style="max-height:50px" />
</div>

<table id="hospitalsTable" class="table table-striped">
    <thead>
        <tr>
            <th>Name</th>
            <th>Type</th>
            <th>Patient Count</th>
        </tr>
    </thead>
    <tbody>
        <!-- Data will be populated here using JavaScript -->
    </tbody>
</table>  
<nav aria-label="Page navigation example">
    <span id="pageInfo" class="mr-2">Page <span id="currentPage">1</span> of <span id="totalPages">?</span></span>
    <ul class="pagination">
        <li class="page-item"><button class="page-link" id="prevPage">Previous</button></li>
        <li class="page-item"><button class="page-link" id="nextPage">Next</button></li>
    </ul>
</nav>


```

# IMPORTANT!!!
Please add paging to all three pages with tables - Hospitals, Patients and Medical Transactions.

Additional link that you can use: [Bootsrap Pagination](https://getbootstrap.com/docs/4.0/components/pagination/)
