using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using PasarService.Interface;
using PasarService.Models;
using PasarService.Services;

namespace PasarService.Repository
{
    public class PenjualanRepository : IPenjualan
    {
        private MySqlConnection con;
        private MySqlCommand com;
        private Db_Connection db;

        public void DeleteDetail(int iddetail)
        {
            con = new MySqlConnection();
            db = new Db_Connection();

            var connStr = con.ConnectionString = db.ConnectionString();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "delete from dt_penjualan where iddetail = " + iddetail + "";
                sqlCmd.Connection = _connection;
                _connection.Open();
                int rowDeleted = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void DeletePenjualan(string id)
        {
            con = new MySqlConnection();
            db = new Db_Connection();

            var connStr = con.ConnectionString = db.ConnectionString();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "delete from tb_penjualan where id_penjualan = " + id + "";
                sqlCmd.Connection = _connection;
                _connection.Open();
                int rowDeleted = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public List<tb_penjualan> GetAllPenjualan()
        {
            con = new MySqlConnection();
            db = new Db_Connection();

            var connStr = con.ConnectionString = db.ConnectionString();

            con.Open();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                _connection.Open();
                MySqlCommand _command = new MySqlCommand("SELECT *FROM tb_penjualan", _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(_command);
                DataTable results = new DataTable();

                adapter.Fill(results);
                _connection.Close();

                if (results == null)
                {
                    return null;
                }

                var srlJson = JsonConvert.SerializeObject(results);

                return JsonConvert.DeserializeObject<List<tb_penjualan>>(srlJson);
            }
        }

        public List<dt_penjualan> GetDetailPenjualan(string idPenjualan)
        {
            con = new MySqlConnection();
            db = new Db_Connection();

            var connStr = con.ConnectionString = db.ConnectionString();

            con.Open();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                _connection.Open();
                MySqlCommand _command = new MySqlCommand("SELECT *FROM dt_penjualan WHERE id_penjualan='"+idPenjualan+"'", _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(_command);
                DataTable results = new DataTable();

                adapter.Fill(results);
                _connection.Close();

                if (results == null)
                {
                    return null;
                }

                var srlJson = JsonConvert.SerializeObject(results);

                return JsonConvert.DeserializeObject<List<dt_penjualan>>(srlJson);
            }
        }

        public void SaveDetail(dt_penjualan dt)
        {
            con = new MySqlConnection();
            db = new Db_Connection();
            int rowInserted;

            var connStr = con.ConnectionString = db.ConnectionString();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "INSERT INTO dt_penjualan (id_penjualan, id_barang, nama_barang, harga, qty, total) Values (@id, @idBarang, @nama, @harga, @qty, @total)";
                sqlCmd.Connection = _connection;

                sqlCmd.Parameters.AddWithValue("@id", dt.id_penjualan);
                sqlCmd.Parameters.AddWithValue("@idBarang", dt.id_barang);
                sqlCmd.Parameters.AddWithValue("@nama", dt.nama_barang);
                sqlCmd.Parameters.AddWithValue("@harga", dt.harga);
                sqlCmd.Parameters.AddWithValue("@qty", dt.qty);
                sqlCmd.Parameters.AddWithValue("@total", dt.total);

                _connection.Open();
                rowInserted = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void SavePenjualan(tb_penjualan pnj)
        {
            con = new MySqlConnection();
            db = new Db_Connection();
            int rowInserted;

            var connStr = con.ConnectionString = db.ConnectionString();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "INSERT INTO dt_penjualan (id_penjualan, tanggal, total_item, total_bayar, bayar, kembali, status) Values (@id, @tanggal, @item, @total, @bayar, @kembali)";
                sqlCmd.Connection = _connection;

                sqlCmd.Parameters.AddWithValue("@id", pnj.id_penjualan);
                sqlCmd.Parameters.AddWithValue("@tanggal", pnj.tanggal);
                sqlCmd.Parameters.AddWithValue("@item", pnj.total_item);
                sqlCmd.Parameters.AddWithValue("@total", pnj.total_bayar);
                sqlCmd.Parameters.AddWithValue("@bayar", pnj.bayar);
                sqlCmd.Parameters.AddWithValue("@kembali", pnj.kembali);

                _connection.Open();
                rowInserted = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void UpdateStatus(string id)
        {
            con = new MySqlConnection();
            db = new Db_Connection();
            int rowInserted;

            var connStr = con.ConnectionString = db.ConnectionString();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "UPDATE tb_penjualan SET status='SELESAI' WHERE id_penjualan='"+id+"'";
                sqlCmd.Connection = _connection;

                _connection.Open();
                rowInserted = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public string IdPenjualan()
        {
            string configKey = "";
            int refNo = 0;
            var idPenjualan = "";

            con = new MySqlConnection();
            db = new Db_Connection();

            var connStr = con.ConnectionString = db.ConnectionString();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "SELECT *FROM tb_config ORDER BY config_value DESC";
                sqlCmd.Connection = _connection;
                _connection.Open();
                MySqlDataReader data = sqlCmd.ExecuteReader();

                if (data.Read())
                {
                    configKey = data.GetValue(0).ToString();
                    refNo = Convert.ToInt32(data.GetValue(1));
                }

                refNo++;

                _connection.Close();

                var date = DateTime.Now.ToString("yyyyMMdd");
                idPenjualan = $"PNJ-{date}-{refNo.ToString("D5")}";
            }

            return idPenjualan;
        }

    }
}
