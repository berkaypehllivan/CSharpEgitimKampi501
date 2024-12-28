using CSharpEgitimKampi601.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public List<Customer> GetAllCustomer()
        {
            var connection = new MongoDbConnection(); //Connection değişkeni oluşturup bir önceki derste oluşturduğumuz MongoDbConnection sınıfındaki constructor metoduna erişim sağlıyoruz.
            var customerCollection = connection.GetCustomersCollection(); //Bu kısımda yukarıdaki bağlantımızın koleksiyonuna erişiyoruz.
            var customers = customerCollection.Find(new BsonDocument()).ToList(); //Bu kısımda da yukarıda oluşan koleksiyondaki verileri hafızaya alıyoruz. 
            List<Customer> customerList = new List<Customer>(); //Bellekte boş bir Customer listesi oluşturuyoruz.
            foreach (var c in customers) //Burada foreach döngüsüyle hafızaya aldığımız customers değişkeninden verileri çekmesini sağlıyoruz.
            {
                customerList.Add(new Customer //Bu kısımda boş oluşturduğumuz Customer listesinin içerisine verilerimizi sırasıyla aktarıyoruz.
                {
                    CustomerId = c["_id"].ToString(),
                    CustomerName = c["CustomerName"].ToString(),
                    CustomerSurname = c["CustomerSurname"].ToString(),
                    CustomerCity = c["CustomerCity"].ToString(),
                    CustomerBalance = decimal.Parse(c["CustomerBalance"].ToString()),
                    CustomerShoppingCount = int.Parse(c["CustomerShoppingCount"].ToString())
                });
            }

            return customerList; //Son olarak bu kısımda da verilerle dolan Customer listesinin dolmuş halinin değerini döndürüyoruz. Bu kısım olmadan bu metod çağırıldığında veriler gelmeyecektir.
        }

        public void DeleteCustomer(string id)
        {
            var connection = new MongoDbConnection(); //Connection değişkeni oluşturup bir önceki derste oluşturduğumuz MongoDbConnection sınıfındaki constructor metoduna erişim sağlıyoruz.
            var customerCollection = connection.GetCustomersCollection(); //Bu kısımda yukarıdaki bağlantımızın koleksiyonuna erişiyoruz.
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id)); //Burada kullanacağımız id değerini filter değişkenine atıyoruz.
            customerCollection.DeleteOne(filter); //Son olarak da bu kısımda MongoDb'de silme işlemini gerçekleştiren metot olan DeleteOne metodunu çağırıp parametre olarak filter değişkenini atıyoruz.
        }

        public void UpdateCustomer(Customer customer)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(customer.CustomerId)); //Burada kullanacağımız id değerini filter değişkenine atıyoruz.

            var updatedValue = Builders<BsonDocument>.Update //Bu kısımda update'in set metodunu kullanarak tek tek verilerin güncellenmesini sağlayan sistemi yazdık.
                .Set("CustomerName", customer.CustomerName)
                .Set("CustomerSurname", customer.CustomerSurname)
                .Set("CustomerCity", customer.CustomerCity)
                .Set("CustomerBalance", customer.CustomerBalance)
                .Set("CustomerShoppingCount", customer.CustomerShoppingCount);

            customerCollection.UpdateOne(filter, updatedValue); //Son olarak burada da UpdateOne metodunun içerisine ilgili id değişkenini ve güncellenen değerleri taşıyan updatedValue değişkenini parametre olarak göndererek güncelleme işlemini gerçekleştirdik.
        }

        public Customer GetCustomerById(string id)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id)); //Burada kullanacağımız id değerini filter değişkenine atıyoruz.

            var result = customerCollection.Find(filter).FirstOrDefault(); //Burada tek bir veri getireceğimiz için FirstOrDefault metodunu kullanarak veriyi result değişkenine gönderdik.

            return new Customer //Son olarak yeni bir Customer bilgisi oluşturup içerisine result'a giden verilerin atamalarını yaptık.
            {
                CustomerId = id,
                CustomerName = result["CustomerName"].ToString(),
                CustomerSurname = result["CustomerSurname"].ToString(),
                CustomerCity = result["CustomerCity"].ToString(),
                CustomerBalance = decimal.Parse(result["CustomerBalance"].ToString()),
                CustomerShoppingCount = int.Parse(result["CustomerShoppingCount"].ToString())
            };
        }
    }
}
