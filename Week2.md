# Working on Database part of an application

## Environment Setup

### Trello account setup
- ✅ Create [Trello](https://trello.com) free account
- ✅ Add 4 columns to the board: ToDo, InProgress, OnReview, Done
- ✅ Fill the ToDo column with tasks for the upcoming week
- ✅ Share trello board with a project leader. My trello email is pavel.tarasenko@openavenuesfoundation.org

### Githup account setup 

- ✅ **GitHub Repository:**
  - Create a GitHub repository for the project to share and collaborate on code. [Creating a new repository on GitHub](https://docs.github.com/en/github/getting-started-with-github/create-a-repo)
  - Share URL to your repo with a project Leader
  - Add link to your trello board to the README.md file in your repository
  - Share URL to your repo with a project Leader
  - 
- ✅ **Install Git:**
  - Git is essential for version control and collaboration. [Git Installation Guide](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git)
  
- ✅ **Git GUI Tools:**
  - Familiarize yourself with a Git Graphical User Interface (GUI) tool. This can help manage your repository more intuitively:
    - [GitKraken](https://www.gitkraken.com/)
    - [SourceTree](https://www.sourcetreeapp.com/)
    - [GitHub Desktop](https://desktop.github.com/)

### MS SQL Managment Studio
- ✅ **Download and install MS SQL Managment Studio:**
  - [Download MSSSMS](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
### Microsoft Azure

- ✅ **Create Free account on Microsoft Azure:**
  - [Create Microsoft Azure Account](https://azure.microsoft.com/en-us/free/search/?ef_id=_k_CjwKCAiA3JCvBhA8EiwA4kujZlcV3PeCVBMhh40DrXbyPy4Q154I2aB_jDyZKF3M13o_YelN6cLf-BoC8rQQAvD_BwE_k_&OCID=AIDcmmfq865whp_SEM__k_CjwKCAiA3JCvBhA8EiwA4kujZlcV3PeCVBMhh40DrXbyPy4Q154I2aB_jDyZKF3M13o_YelN6cLf-BoC8rQQAvD_BwE_k_&gad_source=1&gclid=CjwKCAiA3JCvBhA8EiwA4kujZlcV3PeCVBMhh40DrXbyPy4Q154I2aB_jDyZKF3M13o_YelN6cLf-BoC8rQQAvD_BwE)

- ✅ **Create your database on azure portal:**
  - [Create Database Guide](https://learn.microsoft.com/en-us/azure/azure-sql/database/single-database-create-quickstart?view=azuresql&tabs=azure-portal)

- ✅ **Allow connections to your server from your network:**
  - Go to Created SQL Server -> Networking -> Public network access -> Click "Selected Networks" -> Add Your Cllient IPV4 Adress -> Save
  - Now you are ready to connect to your database from MSSMS from your local PC
  - 
### Create Database Structure
- ✅ **Create next tables in your database:**
  - [Create Table Documentation](https://learn.microsoft.com/en-us/sql/t-sql/statements/create-table-transact-sql?view=sql-server-ver16) 
  - Patient (Personal Patient Data. Please Feel free to add all fields that You think patient can have)
  - Hospital (Information about hospital – Name, Adress, Contact info, Type, etc.)
  -	Hospital_Type
  - Medical_Transaction (RxNumber, Date_Filled, NDC, Days_Supply, Patient_id, Total_Cost, Transaction_Fee, Copay_Amount, Payer_Name, Nurse_Id, Doctor_Id)
  -	Medical_Caregiver (Pesronal and contact information, Type (Doctor, nurse, etc.))
  - Medical_Caregiver_type
  -	Patient_Event (Date_Time, PatientId, Nurse_Id, Description, ect.)
     - All tables should have Primary Keys (Id)
     - Create separate table for Types of entites and add connections to them	
- ✅ **Add Foreign keys to all tables that should have connections:**
  -[Foreign Key Documentation](https://learn.microsoft.com/en-us/sql/relational-databases/tables/create-foreign-key-relationships?view=sql-server-ver16)
- ✅ **Populate tables with data:**
  - Populate all tables except "Medical_Transaction" with data using T-SQL syntax
  - Create .CSV document with same structure as "Medical_Transaction" table. You can use MS Excel for this purpose. Please add not less than 5K records
  - Populate table "Medical_Transaction" using [Using SQL Server Management Studio Import CSV Tools](https://learn.microsoft.com/en-us/sql/relational-databases/import-export/import-flat-file-wizard?view=sql-server-ver16) in Ms SQL Managment Studio or [Bulk Insert](https://learn.microsoft.com/en-us/sql/t-sql/statements/bulk-insert-transact-sql?view=sql-server-ver16)

### Save your work
- ✅ **Create Script of your database and save to your git repository:**
- Generate script of your database and save is as file [Script a database by using the Script option](https://learn.microsoft.com/en-us/sql/ssms/tutorials/scripting-ssms?view=sql-server-ver16#script-a-database-by-using-the-script-option)
- Add script file of the database to your repository 
