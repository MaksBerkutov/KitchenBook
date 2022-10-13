using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;

namespace App1.Services.MySql
{
    using Models;
    using System.Threading.Tasks;

    static class MySql
    {
       
        #region Date Mysql
       

        static readonly  MySqlConnectionStringBuilder  ConString = new MySqlConnectionStringBuilder
            {
                Server = "192.168.1.10",
                Database = "kitchen",
                UserID = "root",
                Password = "root",
                ConnectionTimeout=60,
                Port = 3306,
                AllowZeroDateTime = true
            };
    #endregion
        public static void AddDate(Item item) {
            MySqlConnection conn = new MySqlConnection(
            ConString.ConnectionString);
            conn.Open();
            string com = $"INSERT items(ID, Name, Category, Type, NameKitchen, HTMLText) VALUES ('{item.Id}', '{item.Name}', '{item.Category}', '{item.Type}', " +
                $"'{item.NameKitchen}', '{item.HTMLText}')";
            MySqlCommand command = new MySqlCommand(com, conn);
            command.ExecuteNonQuery();
            command.Clone();
            conn.Clone();



        }
        public static void UpdateDate(string UID , Item Item)
        {
            MySqlConnection conn = new MySqlConnection(
             ConString.ConnectionString);
            conn.Open();
            string com = $"UPDATE items Name='{Item.Name}', Category='{Item.Category}', Type='{Item.Type}', NameKitchen='{Item.NameKitchen}', HTMLText='{Item.HTMLText}' WHERE ID='{UID}'";
            MySqlCommand command = new MySqlCommand(com, conn);
            command.ExecuteNonQuery();
            command.Clone();
            conn.Close();
        }
        public static void DeleteDate(string UID) {
            MySqlConnection conn = new MySqlConnection(
                 ConString.ConnectionString);
            conn.Open();
            string com = $"DELETE  FROM items WHERE ID='{UID}'";
            MySqlCommand command = new MySqlCommand(com, conn);
            command.ExecuteNonQuery();
            command.Clone();
            conn.Close();
        }
        public static List<Item> GetDate()
        {
            List<Item> result = new List<Item>();
            MySqlConnection conn = new MySqlConnection(
           ConString.ConnectionString);
            conn.Open();
            string com = $"SELECT * FROM items";
            MySqlCommand command = new MySqlCommand(com, conn);
   
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    (string ID, string Name, string Category, string Type, string NameKitchen, string HTMLText) tmp;
                    tmp.ID = reader.GetString(0);
                    tmp.Name = reader.GetString(1);
                    tmp.Category = reader.GetString(2);
                    tmp.Type = reader.GetString(3);
                    tmp.NameKitchen = reader.GetString(4);
                    tmp.HTMLText = reader.GetString(5);
                    result.Add(new Item() { Id = tmp.ID, HTMLText = tmp.HTMLText, Category = tmp.Category, Name = tmp.Name, NameKitchen = tmp.NameKitchen, Type = tmp.Type });
                }
            }
            command.Clone();
            reader.Close();
            conn.Close();
            return result;
        }
        public static async void UpdateDateAsync(string UID , Item Item)
        {
            MySqlConnection conn = new MySqlConnection(
             ConString.ConnectionString);
            await conn.OpenAsync();
            string com = $"UPDATE items SET Name='{Item.Name}', Category='{Item.Category}', Type='{Item.Type}', NameKitchen='{Item.NameKitchen}', HTMLText='{Item.HTMLText}' WHERE ID='{UID}'";
            MySqlCommand command = new MySqlCommand(com, conn);
            command.ExecuteNonQuery();
            command.Clone();
            await conn.CloseAsync();
        }
        public static async void AddDateAsync(Item item)
        {
            MySqlConnection conn = new MySqlConnection(
            ConString.ConnectionString);
            await conn.OpenAsync();
            string com = $"INSERT items(ID, Name, Category, Type, NameKitchen, HTMLText) VALUES ('{item.Id}', '{item.Name}', '{item.Category}', '{item.Type}', " +
                $"'{item.NameKitchen}', '{item.HTMLText}')";
            MySqlCommand command = new MySqlCommand(com, conn);
            command.ExecuteNonQuery();
            command.Clone();
            await conn.CloseAsync();
        }
        public static async void DeleteDateAsync(string UID)
        {
            MySqlConnection conn = new MySqlConnection(
             ConString.ConnectionString);
            await conn.OpenAsync();
            string com = $"DELETE  FROM items WHERE ID='{UID}'";
            MySqlCommand command = new MySqlCommand(com, conn);
            command.ExecuteNonQuery();
            command.Clone();
            await conn.CloseAsync();
        }
        public static async Task<List<Item>> GetDateAsync()
        {
            List<Item> result = new List<Item>();
            MySqlConnection conn = new MySqlConnection(
           ConString.ConnectionString);
           await conn.OpenAsync();
            string com = $"SELECT * FROM items";
            MySqlCommand command = new MySqlCommand(com, conn);

            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                (string ID, string Name, string Category, string Type, string NameKitchen, string HTMLText) tmp;
                await reader.ReadAsync();
                tmp.ID = reader.GetString(0);
                tmp.Name = reader.GetString(1);
                tmp.Category = reader.GetString(2);
                tmp.Type = reader.GetString(3);
                tmp.NameKitchen = reader.GetString(4);
                tmp.HTMLText = reader.GetString(5);
                result.Add(new Item() { Id = tmp.ID, HTMLText = tmp.HTMLText, Category = tmp.Category, Name = tmp.Name, NameKitchen = tmp.NameKitchen, Type = tmp.Type });
            }
            command.Clone();
            reader.Close();
            await conn.CloseAsync();
            return result;
        }

    }
}
