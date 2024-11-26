using DAL.Concrete;
using DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.FileIO;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;



IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json")
    .Build();

string connectionString = configuration.GetConnectionString("TradingCompany") ?? "";
int i = 0;
/*while (true)
{
    Console.WriteLine("Enter your login and password.\nLogin: ");
    string login = Console.ReadLine();
    string salt = null;
    using (SqlConnection con = new SqlConnection(connectionString))
    {
        con.Open();
        using (var command = new SqlCommand("SELECT * FROM tblUsers WHERE login = @login;", con))
        {
            command.Parameters.AddWithValue("@login", login);
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    salt = Convert.ToString(dataReader["salt"]);

                }
            }
        }
    }
    Console.WriteLine("Password: ");
    string password = ReadPasswordHash(salt);
    if (!Authentication(login, password))
    {
        Console.WriteLine("\nInvalid login or password.");
        i++;
        if (i > 3)
        {
            Console.WriteLine("\nAuthentication failed.");
            return;
        }
        continue;
    }
    else
    {
        Console.WriteLine($"\nSuccessfull authentication. Welcome, {login}!");
        break;
    }
    
}


while (true)
{
    Console.WriteLine("Welcome on our site! Go to:\n - Administator (1)\n - Stock manager (2)\n - Financial Manager (3)\n - News (4)\n - Our Contact Information (5)");
    var choice0 = Convert.ToInt32(Console.ReadLine());
    if (choice0 == 1 || choice0 == 2 || choice0 == 4)
    {
        Console.WriteLine("Sorry, this page is temporarily unavailiable...\nClick any button to return to the main page...");
        Console.ReadLine();
        continue;
    }
    else if (choice0 == 5)
    {
        Console.WriteLine("Ivan Franko National University of Lviv\nFaculty of Applied mathematics and Informatics\nOur site: https://ami.lnu.edu.ua\nClick any button to return to the main page...");
        Console.ReadLine();
        continue;
    }
    else if (choice0 > 5 || choice0 <= 0)
    {
        Console.WriteLine("Incorrect option!");
        continue;
    }
    else if (choice0 == 3)
        break;
}
while (true)
{
    Console.WriteLine("Financial management. Look at the options, enter your choice." +
        "\n1. Show all sold goods.\n2. Get sold goods by id.\n" +
        "3. Get ordered goods by id.\n4. Sort ordered goods.\n5. Sort sold goods.\n6. Make a report.\n" +
        "7. Update sold goods by id.\n8. Add a new order.\n9. Delete an order.\nQ to log out.");
    var choice = Console.ReadLine();
    if (string.IsNullOrEmpty(choice) || choice.Length > 1)
    {
        Console.WriteLine("Incorrect option.");
        continue;
    }
    char fchoice = Convert.ToChar(choice.ToLower());
    switch (fchoice)
    {
        case '1':
            GetAllSoldGoods();
            break;
        case '2':
            GetSoldGoodsById();
            break;
        case '3':
            GetOrderedGoodsById();
            break;
        case '4':
            SortOrderedGoods();
            break;
        case '5':
            SortSoldGoods();
            break;
        case '6':
            MakeAReport();
            break;
        case '7':
            UpdateSoldGoods();
            break;
        case '8':
            AddNewOrder();
            break;
        case '9':
            DeleteOrder();
            break;
        case 'q':
            return;
        default:
            Console.WriteLine("Incorrect option.");
            break;
    }
}

void DeleteOrder()
{
    Console.WriteLine("Enter id of the order you want to delete: ");
    int id = Convert.ToInt32(Console.ReadLine());
    OrderGoodsDAL dal = new OrderGoodsDAL(connectionString);
    bool res = dal.DeleteById(id);
    if (res)
        Console.WriteLine("Deleted successfully.");
    else
        Console.WriteLine("Delete failed.");
}

void AddNewOrder()
{
    try
    {
        Console.WriteLine("Enter goods' id: ");
        int goods_id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the cost of the order: ");
        decimal cost = Convert.ToDecimal(Console.ReadLine());
        Console.WriteLine("Enter the amount of the goods: ");
        int amount = Convert.ToInt32(Console.ReadLine());
        OrderGoodsDAL dal = new OrderGoodsDAL(connectionString);
        OrderedGoods og = new OrderedGoods()
        {
            Goods_Id = goods_id,
            Cost_Lost = cost,
            Amount_Get = amount,
            Date = DateTime.Now
        };

        int id = dal.Add(og);
        if (id == null)
            throw new Exception();
        Console.WriteLine($"New id: {id}");
    }
    catch (Exception)
    {
        Console.WriteLine("Incorrect input!");
        return;
    }
}

void UpdateSoldGoods()
{
    Console.WriteLine("Enter id of the sale: ");
    int id = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter id of the goods: ");
    int goods_id = Convert.ToInt32(Console.ReadLine()); 
    Console.WriteLine("Enter the cost of the sale: ");
    decimal cost = Convert.ToDecimal(Console.ReadLine());
    Console.WriteLine("Enter the amount of the goods: ");
    int amount = Convert.ToInt32(Console.ReadLine());
    //Console.WriteLine("Enter the date of the sale (DD-MM-YY HH:mm:ss): ");
    //DateTime date = Convert.ToDateTime(Console.ReadLine());
    SoldGoodsDAL dal = new SoldGoodsDAL(connectionString);

    SoldGoods sg = new SoldGoods()
    {
        Goods_Id = goods_id,
        Cost_Get = cost,
        Amount_Lost = amount,
        Date = DateTime.Now
    };
    bool res = dal.Update(sg, id);
    if (res)
        Console.WriteLine("Updated successfully.");
    else
        Console.WriteLine("Update failed.");
}

void MakeAReport()
{
    var dal1 = new OrderGoodsDAL(connectionString);
    var dal2 = new SoldGoodsDAL(connectionString);
    List<OrderedGoods> og = dal1.GetAll();
    List<SoldGoods> sg = dal2.GetAll();
    double resOrder = 0;
    double resSold = 0;
    foreach(var o in og)
    {
        resOrder += (double)o.Cost_Lost;
    }
    foreach(var s in sg)
    {
         resSold += (double)s.Cost_Get;
    }
    Console.WriteLine($"Money spent: {resOrder}\nMoney received: {resSold}\nGeneral profit: {resSold - resOrder}");
}

void SortSoldGoods()
{
    Console.WriteLine("Enter a column you want to sort data by: \n(id(1), goods_id(2), cost(3), amount(4), date(5)\n");
    int choice = Convert.ToInt32(Console.ReadLine());
    if (choice <= 0 || choice > 5)
    {
        Console.WriteLine("Incorrect option.");
        return;
    }
    var dal = new SoldGoodsDAL(connectionString);
    List<SoldGoods> goods = dal.GetAllSortedBy(choice);
    Console.WriteLine("Sold Goods\nid\tgoods_id\tcost\t\tamount\t\tdate");
    foreach (var sg in goods)
        Console.WriteLine($"{sg.Sell_Id}\t\t{sg.Goods_Id}\t\t{sg.Cost_Get}\t\t{sg.Amount_Lost}\t\t{sg.Date}");
}
void SortOrderedGoods()
{
    Console.WriteLine("Enter a column you want to sort data by: \n(id(1), goods_id(2), cost(3), amount(4), date(5)\n");
    int choice = Convert.ToInt32(Console.ReadLine());
    if (choice <= 0 || choice > 5)
    {
        Console.WriteLine("Incorrect option.");
        return;
    }
    var dal = new OrderGoodsDAL(connectionString);
    List<OrderedGoods> goods = dal.GetAllSortedBy(choice);
    Console.WriteLine("Ordered Goods\nid\tgoods_id\tcost\t\tamount\t\tdate");
    foreach (var og in goods)
        Console.WriteLine($"{og.Order_Id}\t\t{og.Goods_Id}\t\t{og.Cost_Lost}\t\t{og.Amount_Get}\t\t{og.Date}\n");
}

void GetOrderedGoodsById()
{
    Console.WriteLine("Enter id: ");
    int id = Convert.ToInt32(Console.ReadLine());
    var dal = new OrderGoodsDAL(connectionString);
    var og = dal.GetById(id);
    Console.WriteLine("Sold Goods\nid\tgoods_id\tcost\t\tamount\t\tdate");
    Console.WriteLine($"{og.Order_Id}\t\t{og.Goods_Id}\t\t{og.Cost_Lost}\t\t{og.Amount_Get}\t\t{og.Date}\n");
}

void GetSoldGoodsById()
{
    Console.WriteLine("Enter id: ");
    int id = Convert.ToInt32(Console.ReadLine());
    var dal = new SoldGoodsDAL(connectionString);
    var sg = dal.GetById(id);
    if (sg == null)
    {
        Console.WriteLine("Id not found.");
        return;
    }
    Console.WriteLine("Sold Goods\nid\tgoods_id\tcost\t\tamount\t\tdate");
    Console.WriteLine($"{sg.Sell_Id}\t\t{sg.Goods_Id}\t\t{sg.Cost_Get}\t\t{sg.Amount_Lost}\t\t{sg.Date}");
}

void GetAllOrderedGoods()
{
    var dal = new OrderGoodsDAL(connectionString);
    var ogs = dal.GetAll();
    Console.WriteLine("Ordered Goods\nid\tgoods_id\tcost\t\tamount\t\tdate");
    foreach (var og in ogs)
        Console.WriteLine($"{og.Order_Id}\t\t{og.Goods_Id}\t\t{og.Cost_Lost}\t\t{og.Amount_Get}\t\t{og.Date}\n");
}

void GetAllSoldGoods()
{
    var dal = new SoldGoodsDAL(connectionString);
    var sgs = dal.GetAll();
    Console.WriteLine("Sold Goods\nid\tgoods_id\tcost\t\tamount\t\tdate");
    foreach (var sg in sgs)
        Console.WriteLine($"{sg.Sell_Id}\t\t{sg.Goods_Id}\t\t{sg.Cost_Get}\t\t{sg.Amount_Lost}\t\t{sg.Date}\n");
}

bool Authentication(string login, string password)
{
    var dal = new UserDAL(connectionString);
    var users = dal.GetAll();
    string passwd = null;
    string salt = null;
    using (SqlConnection con = new SqlConnection(connectionString))
    {
        con.Open();
        using (var command = new SqlCommand("SELECT * FROM tblUsers WHERE login = @login;", con))
        {
            command.Parameters.AddWithValue("@login", login);
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    passwd = Convert.ToString(dataReader["password"]);
                    salt = Convert.ToString(dataReader["salt"]);

                }
            }
        }
    }
        foreach (var user in users)
    {
        if (user.Login == login && passwd == password)
            return true;
    }
    return false;
}

string ReadPasswordHash(string salt)
{
    var passwd = string.Empty;
    ConsoleKey key;
    do
    {
        var keyInfo = Console.ReadKey(intercept: true);
        key = keyInfo.Key;

        if (key == ConsoleKey.Backspace && passwd.Length > 0)
        {
            Console.Write("\b \b");
            passwd = passwd[0..^1];
        }
        else if (!char.IsControl(keyInfo.KeyChar))
        {
            Console.Write("*");
            passwd += keyInfo.KeyChar;
        }
    } while (key != ConsoleKey.Enter);
    string password = passwd + salt;
    using (SHA256 sha256Hash = SHA256.Create())
    {
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
            builder.Append(bytes[i].ToString("x2"));
        return builder.ToString();
    }
}
*/