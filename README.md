# C# BOOTCAMP SECTION 501-601
**This repository is based on C# Bootcamp published by Murat Yücedağ on Youtube and is a continuation of the previous 301 & 401 repository. You can browse the details covered in each section from the bottom.**

## 📌 Episode 22: Using Dapper with C# Part 1
We started the first part of the 501 module with **Dapper**. For this new topic, we added a new database and a product table via SQL, into which we entered 5 data in this table. Next we created a new project in Visual Studio and developed a simple form application in the classic format, as is a practical topic.

Since we will work with the **Dapper** topic using **Dtos**, we created two new folders in our project named **Dtos** and **Repositories**. In the Dtos folder, we added a class named **ResultProductDto**, where we wrote all the column headers in the product table we created as a properties. This way we can store the properties created in our table.

After this we added a new class called **CreateProductDto**, whichg we will use to copy the properties we created and place them in the **Dtos** folder. After pasting the copied properties, we removed Id property because it's auto-incremented during the addition process, so it won't be needed.

In the Repositories folder we created a new interface named **IProductRepository**. In this interface we'll work with asynchronous methods. You can see the generated code and its functions below. (You can review what is the asynchronous methods are and how they work in the notes section below.)

    public interface IProductRepository
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task CreateProductAsync(CreateProductDto createProductDto);
        Task UpdateProductAsync(UpdateProductDto updateProductDto);
        Task DeleteProductAsync(int id);
        Task GetByProductIdAsync(int id);
    }

Then we created a new class in Repositories folder named **ProductRepository**, inherited the **IProductRepository** interface in this class and implemented its methods. (By working with interfaces and classes, our goal is to maintain high code readability by making the UI side of the code an area where only methods are called, similar to the **N-Tier Architecture**.)

Finally we right-clicked on the project, selected **Manage Nuget Packages**, went to the **Browse** section in the window that opened, searched for **Dapper** in the search bar and installed the package.

**Notes:**

**Dtos: (Data Transfer Object)** This term refers to a lightweight structure used for transferring data. Unlike a model, DTO's allow us to focus only on the fields we need, rather than working with the entire data structure. They act as intermediaries for mapping our tables and classes in moduler structures and facilitating transitions between layers and folders.

**Async Programming:** In the C# programming language async enables an application to remain responsive while waiting for long-running operations to complete. It allows another task to run simultaneously. For example, imagine you are listing a data on the UI side of your project and the process takes about 20 seconds due to the large amount of data. Async programming is the while waiting for the data to load, you can also perform an update operation in background. In such cases, asynchronous programming allows multiple tasks to execute at the same time, improving efficiency and responsiveness.

**Task:** A structure in the C# programming language that represents the execution of a specific task or operation. Tasks are primarily used in **Async** programming. A task starts a process in the background and completes it while other processes can continue running. Some tasks return a value (as shown in the code at top), while others simply indicate the tasks completion.


## 📌 Episode 23: Using Dapper with C# Part 2
In the second episode of the 501 module, we'll contiune to develop the **Dapper** method that we started in the previous episode. We create a new project instance in the code section of the form we created: **SqlConnection = new SqlConnection("Server=(server name part of your SQL server will go here); initial catalog=(the name of the database we created for Dapper course); integrated security=true");** We don't have to create the connection string un **DatabaseConnectionString** and edit this class to be public and static. The reason we make it static is to access this class without a method.

Then we can create a method called **'public static void SQLDatabaseConnectionString'** and paste the SQL connection part into it. In this way, we can access this connection part by using the name of the method we created. We start using **Dapper** with the listing process. Firstly, we assign a SQL query in the form of **string query = 'Select * From TblProduct;'** to a variable of string type. Then we add the async statement to our method so that we can use async methods in it. **var values = await (await should be here because we are working async programming) connection.QueryAsync<ResultProductDto>(query);** (QueryAsync is a method used to list data in **Dapper**. After adding this method we need to add **ResultProductDto** class, which we created in the less-than symbols and then we need to add query outside of it. The reason for taking the query variable is to map the **ResultProductDto** class to query we wrote.) Finally we assign the values variable to the **DataSource** part of the **DGV** and finish the listing process.

For the methos we will use for the insertion process, we again create an SQL query: **string query = 'Insert Into TblProduct (ProductName, ProductStock, ProductPrice, ProductCategory) values (@productName, @productStock, @productPrice, @productCategory)';** For the assignment of parameters, we first create a variable as **var parameters = new DynamicParameters();** and define **DynamicParameters()** a method in Dapper. Then we complete our assignments for each column as **parameters.Add('@productName', txtProductName.Text);** Finally since we will work asynchronously, we finish the addition process by using query and parameters mapping method in the **ExecuteSync** method in **Dapper** in the form of **await connection.ExecuteAsync(query, parameters);** Similarly, we add the delete and update features and as a final example to the **Dapper** topic, we add the operations showing the total number of books, the most expensive book and number of categories and finish our episode.


## 📌 Episode 24: Using MongoDb with C# Part 1

## 📌 Episode 25: Using MongoDb with C# Part 2
