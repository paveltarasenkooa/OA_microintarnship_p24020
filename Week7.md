# Adding new pages to Add/Edit objects from database


## 1. Modifying Routing
Lets modify our routing in controllers and **Prorgam.cs** files to handle additional pages
let's add next lines to our "Prorgam.cs" file: 

```c#
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

```

Then let's remove next code from all out controllers: 
```c#
[Route("api/patient")]
[ApiController]
```
and move this to actions where we need it: 

```c#
 [HttpGet("api/patient/GetPatients")]
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
Then we need to modify our links that we previously used to fetch data from our js files: 

was: 
```js
 var url = '/api/patient?pageIndex=' + pageIndex +'&pageSize=10';
```
need to be: 
```js
 var url = '/api/patient/GetPatients?pageIndex=' + pageIndex +'&pageSize=10';
```

After these changes please test your app. It should work without any errors. 

## 2. Adding Patient "Edit" functionality

### Patient Edit page

Let's add new view into Pages/Shared folder with name "PatientEdit.cshtml" : 
```cshtml
@model OA_Example_Project.Models.Patient

@{
    ViewData["Title"] = "Edit";
}
<a class="btn btn-primary" href="/patient/index" role="button">Back</a>
<form asp-action="Edit">
    <div class="form-group">
        <label for="firstName">First Name</label>
         <input type="text" class="form-control" asp-for="FirstName" id="firstName" name="firstName" placeholder="Enter first name" required>
    </div>
    </br>
    <div class="form-group">
        <label for="firstName">Last Name</label>
        <input type="text" class="form-control" asp-for="LastName" id="lastName" name="lastName" placeholder="Enter last name" required>
    </div>
    </br>
   <div class="form-group">
       <label for="patientDOB">Date of Birth</label>
        <input type="date" class="form-control" asp-for="Dob" id="Dob" name="Dob" required>
   </div>
    <div class="form-group">
        <label for="hospital">Hospital</label>
        <select class="form-control" asp-for="HospitalId" asp-items="@ViewBag.Hospitals" id="hospital" name="HospitalId">
            <option value="">Please select a hospital</option>
        </select>
    </div>
   </br>
  <button type="submit" class="btn btn-primary">Submit</button>
</form>
```

To be able to see this view we need to add new action to our **Patient** controller:

```C#

 public IActionResult Edit(int? id)
 {
     if (id == null)
     {
         return NotFound();
     }

     var patient = _context.Patients.Find(id);
     if (patient == null)
     {
         return NotFound();
     }

     var hospitals = _context.Hospitals.Select(h => new
     {
         h.Id,
         h.Name
     }).ToList();

     ViewBag.Hospitals = new SelectList(hospitals, "Id", "Name");

     return View("PatientEdit",patient);
 }

```
In code snippet above we are using ViewBag to send list of hospitals to the page to be displayed in dropdown.
Action above serves to display page to edit patient.

We also need an action to update Patient object in database: 

```C#
 [HttpPost]
 public async Task<IActionResult> Edit(Patient patient)
 {
     var hospitals = _context.Hospitals.Select(h => new
     {
         h.Id,
         h.Name
     }).ToList();

     ViewBag.Hospitals = new SelectList(hospitals, "Id", "Name");

     if (patient.Id == null )
     {
         return NotFound();
     }
     var dbPatient = _context.Patients.Find(patient.Id);
     dbPatient.FirstName = patient.FirstName;
     dbPatient.LastName = patient.LastName;
     dbPatient.Dob = patient.Dob;
     dbPatient.HospitalId = patient.HospitalId;
     _context.Update(dbPatient);
     _context.SaveChanges();
     return View("PatientEdit",patient);
 }

```
After all code is added we should have new page added and all actions to perform "Edit";

so next our step would be:


### Adding link to Edit Patient from Patients table:

To see this new **Edit** page we need to add links to this page from Table with patients. 

Let's modify "Index.cshtml" page in "Pages/Patient" folder:
```
@page
@model OA_Example_Project.Pages.Patient.IndexModel
@{
    ViewData["Title"] = "Patients";
}

<script src="~/js/patient.js"></script>
<link rel="stylesheet" href="~/css/TableStyle.css" asp-append-version="true" />

<div id="spinner" style="display: none;">

    <img src="/images/zkzg.gif" alt="Loading..." style="max-height:50px" />
</div>
<h1 id="hospitalName"></h1>

<a class="btn btn-primary" href="/patient/Add" role="button">Add Patient</a>
<table id="patientsTable" class="table table-striped">
    <thead>
        <tr>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Hospital Name</th>
            <th>Medical Transactions Count</th>
            <th></th>
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

We also need to modify **patients.js** file "populateTable" function to add links to Patients Edit page: 
```js
function populateTable(data, hospitalId) {
    const tableBody = document.getElementById('patientsTable').getElementsByTagName('tbody')[0];
    tableBody.innerHTML = '';

    var hospitalName = 'all hospitals';
    if (hospitalId) {
        hospitalName = data.hospitalName;
    }
    document.getElementById('hospitalName').innerText = 'Patients for ' + hospitalName;
    data.data.forEach(patient => {
        let row = tableBody.insertRow();
        let cell1 = row.insertCell(0);
        let cell2 = row.insertCell(1);
        let cell3 = row.insertCell(2);
        let cell4 = row.insertCell(3);
        let cell5 = row.insertCell(4);

        cell1.innerHTML = patient.firstName;
        cell2.innerHTML = patient.lastName;
        cell3.innerHTML = patient.hospitalName;
        cell4.innerHTML = patient.medicalTransactionsCount;
        cell5.innerHTML = '<a href="/Patient/Edit/' + patient.patientId + '"> Edit </a>';
    });
}
```
after all this done your patients table should look like this: 

![image](https://github.com/paveltarasenkooa/OA_microintarnship_p24020/assets/162073913/11ac735c-5f4a-45d9-b354-581a911e5c5c)


## 3. Adding Patient "Add" functionality

Let's add new view into Pages/Shared folder with name "PatientAdd.cshtml" : 

```cshtml
@model OA_Example_Project.Models.Patient

@{
    ViewData["Title"] = "Add PAtient";
}
<a class="btn btn-primary" href="/patient/index" role="button">Back</a>
<form asp-action="Add">
    <div class="form-group">
        <label for="firstName">First Name</label>
        <input type="text" class="form-control" asp-for="FirstName" id="firstName" name="firstName" placeholder="Enter first name" required>
    </div>
    </br>
    <div class="form-group">
        <label for="firstName">Last Name</label>
        <input type="text" class="form-control" asp-for="LastName" id="lastName" name="lastName" placeholder="Enter last name" required>
    </div>
    </br>
    <div class="form-group">
        <label for="patientDOB">Date of Birth</label>
        <input type="date" class="form-control" asp-for="Dob" id="Dob" name="Dob" required>
    </div>
    </br>
    <div class="form-group">
        <label for="hospital">Hospital</label>
        <select class="form-control" asp-for="HospitalId" asp-items="@ViewBag.Hospitals" id="hospital" name="HospitalId">
            <option value="">Please select a hospital</option>
        </select>
    </div>
    </br>
    </br>
    <button type="submit" class="btn btn-primary">Submit</button>
</form>
```

To be able to see this view we need to add new action to our Patient controller: 

```C#
public IActionResult Add()
{
    var hospitals = _context.Hospitals.Select(h => new
    {
        h.Id,
        h.Name
    }).ToList();

    ViewBag.Hospitals = new SelectList(hospitals, "Id", "Name");
    return View("PatientAdd");
}
```

After these actions out add patient page should look like this: 
![image](https://github.com/paveltarasenkooa/OA_microintarnship_p24020/assets/162073913/461e7fb5-b45a-4dab-abc3-fca4c93902f3)

### Action to save new patient to database: 

We already added new action to patient controller with name "Add" but the purpose for that action was to display page where uses can add patients. We also need to add another action to handle "Save" of object into the database:
   ```C#
    [HttpPost]
    public async Task<IActionResult> Add(Patient patient)
    {

        _context.Patients.Add(patient);
        _context.SaveChanges();
        return View("PatientEdit", patient);
    }
```

Please rember that you can alwaye refer to the project example that is located in my github repo. 

## IMPORTANT

Please Implement Add/Edit functionality for at least two entities from these 3: Patient, Hospital, Medical Transaction
