// See https://aka.ms/new-console-template for more information
using System.Data.SqlClient;
using System.IO.Pipes;
using System.Reflection.PortableExecutable;
using System.Text;
using LearningORM;
using LearningORM.Models;
using LearningORM;

internal class Program
{
    private static void Main(string[] args)
    {
        SqlCommand cmd;
        SqlDataReader dataReader;
        String sql = "";
        List<ProductViewDTO> products = new List<ProductViewDTO>();


        Console.WriteLine("Trying to connect to db");
        SqlConnection cnn = DB.Connect();
        
        cnn.Open();
        
        Console.WriteLine("Connected to database: " + cnn.Database);
        Console.WriteLine("---------------------");

        sql = "Select ProductName from dbo.Product";
        Console.WriteLine("Executing select: \n" + sql + "\n");

        cmd = new SqlCommand(sql, cnn);
        dataReader = cmd.ExecuteReader();

        while (dataReader.Read())
        {
            Product product = new Product();
            product.Name = (string)dataReader["ProductName"];

            Console.WriteLine(product.Name);
        }

        Console.WriteLine("---------------------");
        dataReader.Close();

        sql = "Select * FROM AllProducts";

        Console.WriteLine("Executing view: \n" + sql + "\n");

        cmd = new SqlCommand(sql, cnn);
        dataReader = cmd.ExecuteReader();
        products.Clear();

        while (dataReader.Read())
        {
            ProductViewDTO product = new ProductViewDTO();
            product.Name = dataReader.GetValue(0).ToString();
            product.Description = dataReader.GetValue(1).ToString();
            product.CategoryName = dataReader.GetValue(2).ToString();
            product.Price = (int)dataReader.GetValue(3);

            products.Add(product);
        }

        products.ForEach(p =>
        {
            Console.WriteLine("Name: " + p.Name + " - Description: " + p.Description + " - Category " + p.CategoryName + " - Price: " + p.Price);
        });

        Console.WriteLine("---------------------");
        dataReader.Close();
        

        sql = "GetProductFromName";
        string productName = "Chicken burger";
        cmd = new SqlCommand(sql, cnn);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ProductName", productName));
        products.Clear();

        Console.WriteLine("Executing storedProcedure: \n"  + sql + " - With parameter: " + productName + "\n");

        dataReader = cmd.ExecuteReader();

        while (dataReader.Read())
        {
            ProductViewDTO product = new ProductViewDTO();
            product.Name = dataReader.GetValue(0).ToString();
            product.Description = dataReader.GetValue(1).ToString();
            product.CategoryName = dataReader.GetValue(2).ToString();
            product.Price = (int)dataReader.GetValue(3);

            products.Add(product);
        }

        products.ForEach(p =>
        {
            Console.WriteLine("Name: " + p.Name + " - Description: " + p.Description + " - Category " + p.CategoryName + " - Price: " + p.Price);
        });

        Console.WriteLine("---------------------");
        Console.WriteLine("Closing sql connection");
        cnn.Close();

    }
}