# Welcome to the DBCommonADO.NET 

This is Reusable Code of Database agnostic lightweight ADO.NET, leveraging the power of **System.Data.Common**.
Unlike Entity Framework, this code is the simplest way for performing **CRUD operations** and Call **Stored Procedures**.

This code can be used in any applications like  **ASP.NET MVC,  ASP.NET Web forms, Webapi, Windows forms, WPF**

### How to use it
1. Include the code file "common_ado.cs" into your project.
2. Import the Namespace "DBCommonADO.NET"
3. create Instance of class "DataConnection" with the parameter as name of the connectionstring you want to use from the config file. That's it.

**DataConnection d=new DataConnection("conn");**

## Now for the CRUD operations

### //Create--Insert
Invoke the method **** passing the "insert statement" as string

**d.DBOperation_Update("insert into tablename--blah blah..");**


### //Read
Invoke the method **DBOperation_Read** passing the "select statement" as string

**d.DBOperation_Update("select * from  tablename--blah blah..");**
The method updates the  state of DataReader **dr**

Iterate through the **d.dr** to get the result set

**while(d.dr.Read())**
**{.....}**

### //Update
Invoke the same old method **DBOperation_Update** passing the "update statement" as string

**d.DBOperation_Update("update  tablename--blah blah..");**

### //Delete
Invoke the same old method **DBOperation_Update** passing the "delete statement" as string

**d.DBOperation_Update("delete from tablename--blah blah..");**

## Calling Stored Procedures
Create  **`List<DataParam> sqlParams = new List<DataParam>()`** - to add all Stored Procedure parameters (input and output)
Invoke the method **DBOperation_ExecuteProcedure** passing the  parameters "Stored Procedure name" as string and
"Stored procedure parameters" as `List<DataParam>`.
The method returns "`List<DataParam>`". 
Iterate through the list to get the result of the output 

              







