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

**Notes:**

**Mapping:** We match the table and the class with each other. This method is called mapping. The layer we will use in this structure is called **DTO**.

**Form Application View:**
![image alt](https://github.com/berkaypehllivan/CSharpEgitimKampi501-601/blob/9404e6f029f55f9889697193347407388c7660f1/Dapper%20Form%20Application.png)

## 📌 Episode 24: Using MongoDb with C# Part 1
With this section, we started the 601 module of the course with the subject of **MongoDB**. We started the section by designing a new form as an interface where can perform **CRUD** operations in the classic format. Then we right-clicked on our project and clicked on '**Manage Nuget Packages**' in the tab opened and in the window that opened, we went to the '**Browse**' section and typed '**MongoDB**' in the search tab and installed the '**MongoDB.Bson**' '**MongoDB.Driver**' packages.

After this process we created a new folder called 'Entities' in our project and created a new class named customer in it. (This class will contain the properties of the customer entity we'll use.) Then we add the options we added to our application as properties, but unlike **MSSQL** we make the value of the id property string, not int. After adding all the necessary properties, we add 2 attributes in the form of **[BsonId]** and **[BsonRepresentation(BsonType.ObjectId)]** in order to use a **MongoDB** on the id. The purpose of the 2 attributes we add is to make the customer id information unique.

After completing our operations in the 'Entites' folder, we added a new folder called 'Services' to our project and created a new class called **MongoDbConnection**. We created a field in the form of **private IMongoDatabase _database** in this class and created our constructor method by type ctor and after that press **Tab** button. In the method we created, we create a variable in the form of **var client = new MongoClient('Connection address in MongoDb Compass application');** and we tell it to connected this local database server and define our connection address in it.

Then, by writing **_database = client.GetDatabase('Db601Customer');** we call the **GetDatabase** method that comes into the client into the _database object we create above and enter the name of the new database to be created in the parameter value, and in this way, when our constructor method is called, it will be create our database. After completing our constructor method, we create a new method as **Public IMongoCollection<BsonDocument> GetCustomersCollection()** and return a value in the form of **return _database.GetCollection<BsonDocument>('Customers');**. The operation done here allows us to create a collection named 'Customer' in our database created above. After finishing our work in this class, we created a new class called **CustomerOperations** in the **Services** folder and wrote the following codes in it:

    public class CustomerOperations
    {
        public void AddCustomer(Customer customer)
        {

            var connection = new MongoDbConnection(); // We've requested a MongoDb connection here.

            var customerCollection = connection.GetCustomersCollection(); // We're connected to our table here.

            var document = new BsonDocument // In this section, we sent the parameters that our entity has. The reason why we do not send the id is that the id value will be automatically incremented, just like in MSSQL.
            {
                {"CustomerName",customer.CustomerName },
                {"CustomerSurname",customer.CustomerSurname },
                {"CustomerCity",customer.CustomerCity },
                {"CustomerBalance",customer.CustomerBalance },
                {"CustomerShoppingCount",customer.CustomerShoppingCount }
            };

            customerCollection.InsertOne(document); // Here we have also carried out the addition process.
        }
    }

Finally to check that the operations are working, we performed the insertion process in our form application bu writing the following codes: (Unlike the classic method, when working on **MongoDb**, we create a variable and write our data into it as shown below.)

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var customer = new Customer()
            {
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerCity = txtCustomerCity.Text,
                CustomerBalance = decimal.Parse(txtCustomerBalance.Text),
                CustomerShoppingCount = int.Parse(txtCustomerShoppingCount.Text)
            };

            customerOperations.AddCustomer(customer);
            MessageBox.Show("Ekleme işlemi başarılı");
        }

**Notes:**

**MongoDb:** MongoDb is an open source, document-oriented NoSQL database management system. Unlike traditional relational databases (RDBMS), it uses its own BSON (documents), which are JSON-like documents, instead of table and row structures. This makes MongoDb particularly well suited for big data, dynamic data structures and rapid prototyping requirements.

**Differents Between MongoDb and MSSQL:**
![image alt](https://github.com/berkaypehllivan/CSharpEgitimKampi501-601/blob/9404e6f029f55f9889697193347407388c7660f1/MongoDb%20MSSQL%20Differents.png)

**BSON:** MongoDb's binary format, similar to JSON (Javascript Object Nonation) but faster and supporting more data types.

**Binary Format:** It's the representation of data at the most basic level that a computer can understand, consisting of 1s and 0s. Unlike text-based formats like JSON, binary formats store and process data in a compressed and fast-processable way.

**Object-Id:** A 12-byte ID that unqiely identifies documents in MongoDb.

## 📌 Episode 25: Using MongoDb with C# Part 2
We will continue the **MongoDb** topic that we started in the last episode in this course. We have only added new data through the form application. We started this section by bringing all the data and listing it. We go to the **CustomerOperations** class and write this codes in it:

    public List<Customer> GetAllCustomer()
        {
            var connection = new MongoDbConnection(); // We create a Connection variable and access the constructor method in the MongoDbConnection class we created in the                                                          previous lesson.
            var customerCollection = connection.GetCustomersCollection(); // In this section we access the collection of our link above.
            var customers = customerCollection.Find(new BsonDocument()).ToList(); // In this section, we memorise the data in the collection created above. 
            List<Customer> customerList = new List<Customer>(); // We create an empty Customer list in memory.
            foreach (var c in customers) // Here, with the foreach loop, we make it pull the data from the customers variable that we have memorised.
            {
                customerList.Add(new Customer // In this section, we transfer our data into the Customer list, which we created empty, respectively.
                {
                    CustomerId = c["_id"].ToString(),
                    CustomerName = c["CustomerName"].ToString(),
                    CustomerSurname = c["CustomerSurname"].ToString(),
                    CustomerCity = c["CustomerCity"].ToString(),
                    CustomerBalance = decimal.Parse(c["CustomerBalance"].ToString()),
                    CustomerShoppingCount = int.Parse(c["CustomerShoppingCount"].ToString())
                });
            }

            return customerList; // Finally, in this section, we return the value of the Customer list filled with data. Without this part, the data will not come when                                         this method is called.
        }

After creating this method, we only need to go to the form application and write 2 lines of code to the list button method. **List<Customer> customers = customerOperations.GetAllCustomer();**. The process in this section is to create a new list called customers, call the **GetAllCustomers** method we created in the **CustomerOperations** class and transfer the **customerList** data in it. In the other line, we send the customers list to the **DataSource** value of the **DGV** in our form and that's it.

For update, delete and fetch operations according to id, we go the **CustomerOperations** class again and create the following methods and end the MongoDb topic with finishing episode 26:

**Update Method Codes:**

        public void UpdateCustomer(Customer customer)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(customer.CustomerId)); // Here we assign the id value we will use to the filter variable.
            var updatedValue = Builders<BsonDocument>.Update // In this section, we have written the system that updates the data one by one using the set method of update.
                .Set("CustomerName", customer.CustomerName)
                .Set("CustomerSurname", customer.CustomerSurname)
                .Set("CustomerCity", customer.CustomerCity)
                .Set("CustomerBalance", customer.CustomerBalance)
                .Set("CustomerShoppingCount", customer.CustomerShoppingCount);

            customerCollection.UpdateOne(filter, updatedValue); // Finally, here, we performed the update process by sending the relevant id variable and the updatedValue                                                                     variable carrying the updated values as parameters into the UpdateOne method.
        }

**Delete Method Codes:**

        public void DeleteCustomer(string id)
        {
            var connection = new MongoDbConnection(); // We create a Connection variable and access the constructor method in the MongoDbConnection class we created in the                                                          previous lesson.
            var customerCollection = connection.GetCustomersCollection(); // In this section we access the collection of our link above.
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id)); // Here we assign the id value we will use to the filter variable.
            customerCollection.DeleteOne(filter); // Finally, in this section, we call the DeleteOne method, which is the method that performs the deletion operation in                                                         MongoDb, and assign the filter variable as a parameter.
        }

**GetById Method Codes:**

    public Customer GetCustomerById(string id)
    {
        var connection = new MongoDbConnection();
        var customerCollection = connection.GetCustomersCollection();
        var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id)); // Here we assign the id value we will use to the filter variable.
        var result = customerCollection.Find(filter).FirstOrDefault(); // Since we will get a single data here, we sent the data to the result variable using the                                                                                     FirstOrDefault method.

        return new Customer // Finally, we created a new Customer information and assigned the data going to the result.
        {
            CustomerId = id,
            CustomerName = result["CustomerName"].ToString(),
            CustomerSurname = result["CustomerSurname"].ToString(),
            CustomerCity = result["CustomerCity"].ToString(),
            CustomerBalance = decimal.Parse(result["CustomerBalance"].ToString()),
            CustomerShoppingCount = int.Parse(result["CustomerShoppingCount"].ToString())
        };
    }
    
**View of the data on MongoDb Compass App:**
![image alt](https://github.com/berkaypehllivan/CSharpEgitimKampi501-601/blob/9404e6f029f55f9889697193347407388c7660f1/MongoDb%20Compass.png)
