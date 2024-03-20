# Displaying Data on a Web Page using .NET application
on current stage let's assume You have your database created, populated and added to your .Net project Sceleton.
Please Navigate to Week4 file and review creation of t-SQL VIEW created to get Hospitals info with Patients count. This View can be used as an example for other pages that would need information from different tables.
## Insert Bootstrap into your web application 
Right mouse cllick on your solution from solution explorer ->  manage NUget Packages for Solution... -> Browse -> type Bootstrap -> select most popular and click "install"
## Add Method to get hospitals from controller: 
 ### Important NOTICE
 If You have added SQL view after You added database to your project you would need to delete folder with your models and places where You added dbcontext so project can be built successfully. And then rerun db scafold command:

 ```powershell
dotnet ef dbcontext scaffold "your connection string" Microsoft.EntityFrameworkCore.SqlServer -o Models
 ```
Next Create new file for new controller in your "Controllers" folder. Call it "HospitalController".
This is the code for HospitalController.cs from my project:
 ```c#
using Microsoft.AspNetCore.Mvc;
using OA_Example_Project.Models;

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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult<IEnumerable<VHospital>> GetHospitals()
        {
            return _context.VHospitals.ToList();
        }
    }
}
 ```

Action "GetHospitals" will give you the list of hospitals from your view.
### Add new page to "Pages" folder
add new folder to "Pages" folder called "Hospital" and then add new View into this folder with the name "index.cshtml"
this is the code of my page: 
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
 ```

### Adding a Java sript file 
Add new .js file with the name "hospital.js" to "js" folder in wwwroot
 this is my code for "hospital.js" file:

```js
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
```

### modify "progeam.cs" file
add "app.MapControllers();" code to "program.cs" file. 
my program.cs file looks next: 

```c#
using Microsoft.EntityFrameworkCore;
using OA_Example_Project.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<OaProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseEndpoints(endpoints =>
//{
//    _ = endpoints.MapControllers(); // This enables attribute routing for controllers.
//});

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.UseStaticFiles();
app.Run();
```

#IMPORTANT!!!
You would need to adjust some parts of your application depending on your tables structure and version of .net framweork that you are using. 
Latest varsion of my application is located in my GitHub repo: [link to Repo](https://github.com/paveltarasenkooa/OA_microintarnship_p24020/tree/main/Project_Example) Feel free to download and review it. 
Please note that my project could not be working on Your PC because it is using my SQL server that does now allow Your IP adress. If You want to be added just let me know.
