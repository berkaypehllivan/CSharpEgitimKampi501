using CSharpEgitimKampi601.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampi601.Services
{
    public class CustomerOperations
    {
        public void AddCustomer(Customer customer)
        {

            var connection = new MongoDbConnection(); // Burada MongoDb bağlantı isteğinde bulunduk.

            var customerCollection = connection.GetCustomersCollection(); // Burada tablomuza bağlandık.

            var document = new BsonDocument // Bu kısımda entityimizin sahip olduğu parametreleri gönderdik. Idyi göndermememizin nedeni aynı MSSQL'de olduğu gibi id değerinin otomatik artan olucak olmasıdır.
            {
                {"CustomerName",customer.CustomerName },
                {"CustomerSurname",customer.CustomerSurname },
                {"CustomerCity",customer.CustomerCity },
                {"CustomerBalance",customer.CustomerBalance },
                {"CustomerShoppingCount",customer.CustomerShoppingCount }
            };

            customerCollection.InsertOne(document); //Burada da ekleme işlemini gerçekleştirdik.
        }
    }
}
