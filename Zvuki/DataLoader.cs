using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Zvuki.Models;
using System.IO;

namespace Zvuki
{
    public class DataLoader
    {
        public const string HUMAN_FILE = "human";
        public const string CLIENT_FILE = "client";
        public const string EMPLOYEE_FILE = "employee";

        public static void saveHuman(Human human)
        {
            string json = JsonSerializer.Serialize(human);
            writeInFile(HUMAN_FILE, json, FileMode.OpenOrCreate);
        }
        public static Human getHuman()
        {
            string json = getData(HUMAN_FILE);
            if(json.Length > 0)
            {
                Human human = JsonSerializer.Deserialize<Human>(json);
                return human;
            } 
            return null;
        }


        public static void saveClient(Client client)
        {
            string json = JsonSerializer.Serialize(client);
            writeInFile(CLIENT_FILE, json, FileMode.OpenOrCreate);
        }
        public static Client getClient()
        {
            string json = getData(CLIENT_FILE);
            if (json.Length > 0)
            {
                Client client = JsonSerializer.Deserialize<Client>(json);
                return client;
            }
            return null;
        }


        public static void saveEmployee(Employee employee)
        {
            string json = JsonSerializer.Serialize(employee);
            writeInFile(EMPLOYEE_FILE, json, FileMode.OpenOrCreate);
        }
        public static Employee getEmployee()
        {
            string json = getData(EMPLOYEE_FILE);
            if (json.Length > 0)
            {
                Employee employee = JsonSerializer.Deserialize<Employee>(json);
                return employee;
            }
            return null;
        }

        public static string getData(string path)
        {
            string str = "";
            using (BinaryReader binaryReader = new BinaryReader(
                File.Open(path, FileMode.OpenOrCreate)))
            {
                try
                {
                    while (true)
                    {
                        str += binaryReader.ReadString();
                    }

                }
                catch (Exception e2) { }

            }
            return str;
        }
        public static void writeInFile(string path, string str, FileMode mode)
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(
            File.Open(path, mode)))
            {
                binaryWriter.Write(str);
            }
        }
        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }
    }
}
