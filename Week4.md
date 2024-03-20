
# Displaying Data on a Web Page

After setting up your database and backend application, the next step is to present the data to your users through a web page. This step involves creating a simple front-end application that fetches data from the backend and displays it in a table format.

To fetch data from the backend, you'll need to set up a controller in your Spring Boot application. This controller will respond to HTTP requests with data fetched from the database.

### Creating a Controller in Spring Boot

1. **Define a REST Controller:** Use `@RestController` annotation to define a controller. This annotation enables the class to handle HTTP requests.

2. **Mapping Requests:** Use `@RequestMapping` to map URLs to controller methods. `@GetMapping` can be used to handle GET requests specifically.

### Example Controller

Below is an example controller that fetches data from the `Patient` table and returns it as a list of patient objects.

```java
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import java.util.List;
import your.package.model.Patient;
import your.package.repository.PatientRepository;

@RestController
@RequestMapping("/api/patients")
public class PatientController {

    @Autowired
    private PatientRepository patientRepository;

    @GetMapping
    public List<Patient> getAllPatients() {
        return patientRepository.findAll();
    }
}
```

### Connecting the Front-end with the Spring Boot Backend



## Front-end Development

### Front-end Technology Choice

- **Choose a Front-end Framework:** For simplicity and modern development, consider using frameworks such as React, Angular, or Vue.js. This guide will use basic HTML and JavaScript for demonstration, but the concept is applicable to any front-end framework.

### Fetching Data from the Backend

1. **API Endpoint:** Ensure your Spring Web Application exposes REST API endpoints that return the necessary data from your database. For example, an endpoint to fetch all patients might look like `GET /api/patients`.

2. **AJAX Request:** Use JavaScript to make AJAX requests to these endpoints. The Fetch API is a modern approach to perform network requests from the browser.

   Example:
   ```javascript
   fetch('http://localhost:8080/api/patients')
     .then(response => response.json())
     .then(data => {
       console.log(data);
       populateTable(data);
     })
     .catch(error => console.error('Error fetching data: ', error));
   ```

### Displaying Data in a Table

1. **HTML Structure:** Create an HTML table structure in your webpage where the data will be displayed.

   Example:
   ```html
   
   <table id="patientsTable">
     <thead>
       <tr>
         <th>ID</th>
         <th>Name</th>
         <th>Age</th>
         <!-- Add more columns as needed -->
       </tr>
     </thead>
     <tbody>
       <!-- Data rows will be added here dynamically -->
     </tbody>
   </table>
   
   ```

3. **Populate Table with Data:** Use JavaScript to dynamically add rows to the table based on the data fetched from your backend.

   Example:
   ```javascript
   function populateTable(data) {
     const table = document.getElementById('patientsTable').getElementsByTagName('tbody')[0];
     data.forEach(patient => {
       let row = table.insertRow();
       let idCell = row.insertCell(0);
       idCell.textContent = patient.id;

       let nameCell = row.insertCell(1);
       nameCell.textContent = patient.name;

       let ageCell = row.insertCell(2);
       ageCell.textContent = patient.age;

       // Add more cells as needed
     });
   }
   ```

### Styling the Table

- Use CSS to style your table and make it visually appealing. This can include setting borders, padding, font sizes, and more.
-  here are some examples of link with styling instructions and templates:
  - [Creating beautiful HTML tables with CSS](https://dev.to/dcodeyt/creating-beautiful-html-tables-with-css-428l)
  - [50 CSS Table Examples](https://devdevout.com/css/css-tables)


# !!! Important

- To implement navigations between different instances of application let's create 3 pages: for table with Hospitals, page for table with Patient and page for table with Medical Transaction
- For table with hospitals let's add column with count of patient and for patient table let's add column with count of medical transactions. For table with medical transactions let's add column with name of Medical Caregiver.

   
# Displaying Hospital Data with Patient Counts on a Web Page

## Extending the Back-end with a Custom Query

### Creating a Custom Repository Method

1. **Define a Custom Repository Method:** In your `HospitalRepository`, define a method to fetch hospitals along with their patient counts. This might involve using a custom query with JPQL (Java Persistence Query Language) or a native SQL query that joins the `Hospital` and `Patient` tables and groups the result by hospital.

### Example Custom Repository Method

Here's how you might define such a method in your repository interface:

```java
@Query("SELECT new your.package.model.HospitalWithPatientCount(h.id, h.name, COUNT(p.id)) " +
       "FROM Hospital h LEFT JOIN Patient p ON h.id = p.hospitalId " +
       "GROUP BY h.id")
List<HospitalWithPatientCount> findHospitalWithPatientCounts();
```

This query assumes you have a `HospitalWithPatientCount` class that can encapsulate the ID and name of the hospital along with the count of patients.

## Updating the Controller

### Modifying the Hospital Controller

To fetch and return the list of hospitals with their patient counts, modify your `HospitalController` to use the new repository method.

Example modification:

```java
@Autowired
private HospitalRepository hospitalRepository;

@GetMapping("/hospitals")
public List<HospitalWithPatientCount> getAllHospitalsWithPatientCounts() {
    return hospitalRepository.findHospitalWithPatientCounts();
}
```
# Using approach of T-SQL View
Instead of using custom queries from code of your application you can create View on side of sql server where you can collect all the data you need to show on the web page. 
[Here is some documentation](https://www.w3schools.com/sql/sql_view.asp)

here is the View I have created to show Hospitals and count of patients for each hospital and hospital type: 
```sql

CREATE  VIEW [dbo].[V_Hospital]
AS

SELECT 
a.Id as [HositalId],
a.Name as [HospitalName],
ht.Name as [HospitalType],
MAX(P.DOB) as MAXDOB,
COUNT(DISTINCT p.ID) as PatientCount
FROM [dbo].[Hospital] a
JOIN HospitalType ht on a.HopitalTypeId = ht.Id
LEFT JOIN Patient p on p.HospitalId = a.Id
GROUP BY 
a.Id,
a.Name,
ht.Name
GO

```

