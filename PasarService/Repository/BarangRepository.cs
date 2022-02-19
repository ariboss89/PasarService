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
    public class BarangRepository : IBarang
    {
        private MySqlConnection con;
        private MySqlCommand com;
        private Db_Connection db;

        public List<tb_barang> GetAllBarang()
        {
            con = new MySqlConnection();
            db = new Db_Connection();

            var connStr = con.ConnectionString = db.ConnectionString();

            con.Open();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                _connection.Open();
                MySqlCommand _command = new MySqlCommand("SELECT *FROM tb_barang", _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(_command);
                DataTable results = new DataTable();

                adapter.Fill(results);
                _connection.Close();

                if (results == null)
                {
                    return null;
                }

                var srlJson = JsonConvert.SerializeObject(results);

                return JsonConvert.DeserializeObject<List<tb_barang>>(srlJson);
            }
        }

        public void SaveBarang(tb_barang brg)
        {
            con = new MySqlConnection();
            db = new Db_Connection();
            int rowInserted;

            var connStr = con.ConnectionString = db.ConnectionString();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "INSERT INTO tb_barang (nama_barang, harga, gambar, stok, minstok) Values (@nama, @harga, @gambar, '0', @minstok)";
                sqlCmd.Connection = _connection;

                sqlCmd.Parameters.AddWithValue("@nama", brg.nama_barang);
                sqlCmd.Parameters.AddWithValue("@harga", brg.harga);
                sqlCmd.Parameters.AddWithValue("@gambar", brg.gambar);
                sqlCmd.Parameters.AddWithValue("@minstok", brg.stok);

                _connection.Open();
                rowInserted = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void UpdateBarang(tb_barang brg)
        {
            con = new MySqlConnection();
            db = new Db_Connection();

            var connStr = con.ConnectionString = db.ConnectionString();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "UPDATE tb_barang SET nama_kategori=@nama, harga=@harga, gambar=@gambar, minstok=@minstok  WHERE Id= @id";
                sqlCmd.Connection = _connection;

                sqlCmd.Parameters.AddWithValue("@nama", brg.nama_barang);
                sqlCmd.Parameters.AddWithValue("@harga", brg.harga);
                sqlCmd.Parameters.AddWithValue("@gambar", brg.gambar);
                sqlCmd.Parameters.AddWithValue("@minstok", brg.minstok);
                sqlCmd.Parameters.AddWithValue("@id", brg.Id);
                _connection.Open();
                int rowUpdatd = sqlCmd.ExecuteNonQuery();

                _connection.Close();
            }
        }

        public void UpdateStok(tb_barang brg)
        {
            con = new MySqlConnection();
            db = new Db_Connection();

            var connStr = con.ConnectionString = db.ConnectionString();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "UPDATE tb_barang SET stok=@stok  WHERE Id= @id";
                sqlCmd.Connection = _connection;

                sqlCmd.Parameters.AddWithValue("@id", brg.Id);
                _connection.Open();
                int rowUpdatd = sqlCmd.ExecuteNonQuery();

                _connection.Close();
            }
        }

        void IBarang.DeleteBarang(int id)
        {
            con = new MySqlConnection();
            db = new Db_Connection();

            var connStr = con.ConnectionString = db.ConnectionString();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "delete from tb_barang where Id = " + id + "";
                sqlCmd.Connection = _connection;
                _connection.Open();
                int rowDeleted = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
