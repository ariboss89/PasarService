using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using PasarService.Interface;
using PasarService.Models;
using PasarService.Services;

namespace PasarService.Repository
{
    public class CategoryRepository : ICategory
    {
        private MySqlConnection con;
        private MySqlCommand com;
        private Db_Connection db;

        public List<tb_kategori> GetAllCategory()
        {
            con = new MySqlConnection();
            db = new Db_Connection();

            var connStr = con.ConnectionString = db.ConnectionString();

            con.Open();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                _connection.Open();
                MySqlCommand _command = new MySqlCommand("SELECT *FROM tb_kategori", _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(_command);
                DataTable results = new DataTable();

                adapter.Fill(results);
                _connection.Close();

                if (results == null)
                {
                    return null;
                }

                var srlJson = JsonConvert.SerializeObject(results);

                return JsonConvert.DeserializeObject<List<tb_kategori>>(srlJson);
            }
        }

        public void SaveCategory(tb_kategori ktr)
        {
            con = new MySqlConnection();
            db = new Db_Connection();
            int rowInserted;

            var connStr = con.ConnectionString = db.ConnectionString();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "INSERT INTO tb_kategori (nama_kategori) Values (@nama)";
                sqlCmd.Connection = _connection;

                sqlCmd.Parameters.AddWithValue("@nama", ktr.nama_kategori);
                _connection.Open();
                rowInserted = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void UpdateCategory(tb_kategori ktr)
        {
            con = new MySqlConnection();
            db = new Db_Connection();

            var connStr = con.ConnectionString = db.ConnectionString();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "UPDATE tb_kategori SET nama_kategori = '" + ktr.nama_kategori + "' WHERE id= @id";
                sqlCmd.Connection = _connection;

                sqlCmd.Parameters.AddWithValue("@id", ktr.id);
                _connection.Open();
                int rowUpdatd = sqlCmd.ExecuteNonQuery();

                _connection.Close();
            }
        }

        void ICategory.DeleteCategory(int id)
        {
            con = new MySqlConnection();
            db = new Db_Connection();

            var connStr = con.ConnectionString = db.ConnectionString();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "delete from tb_kategori where id = " + id + "";
                sqlCmd.Connection = _connection;
                _connection.Open();
                int rowDeleted = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
