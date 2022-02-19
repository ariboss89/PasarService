using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using PasarService.Models;
using PasarService.Services;

namespace PasarService.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private MySqlConnection con;
        private MySqlCommand com;
        private Db_Connection db;

        [HttpGet]
        public IEnumerable<tb_login> GetAll()
        {
            con = new MySqlConnection();
            db = new Db_Connection();

            var connStr = con.ConnectionString = db.ConnectionString();

            con.Open();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                _connection.Open();
                MySqlCommand _command = new MySqlCommand("SELECT a.username, a.nama, a.email, a.role, a.kontak FROM tb_login a", _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(_command);
                DataTable results = new DataTable();

                adapter.Fill(results);
                _connection.Close();

                if (results == null)
                {
                    NotFound();
                }

                var srlJson = JsonConvert.SerializeObject(results);

                return JsonConvert.DeserializeObject<List<tb_login>>(srlJson);
            }

        }

        [HttpPost]
        public void AddData([FromBody]tb_login log)
        {
            con = new MySqlConnection();
            db = new Db_Connection();

            var connStr = con.ConnectionString = db.ConnectionString();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "INSERT INTO tb_login (username, password, nama, alamat, email, role, kontak) Values (@username, md5(@password), @nama, @alamat, @email, @role, @kontak)";
                sqlCmd.Connection = _connection;

                sqlCmd.Parameters.AddWithValue("@username", log.username);
                sqlCmd.Parameters.AddWithValue("@password", log.password);
                sqlCmd.Parameters.AddWithValue("@nama", log.nama);
                sqlCmd.Parameters.AddWithValue("@alamat", log.alamat);
                sqlCmd.Parameters.AddWithValue("@email", log.email);
                sqlCmd.Parameters.AddWithValue("@role", log.role);
                sqlCmd.Parameters.AddWithValue("@kontak", log.kontak);

                _connection.Open();
                int rowInserted = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        [HttpPut]
        public void Update(tb_login log)
        {
            con = new MySqlConnection();
            db = new Db_Connection();

            var connStr = con.ConnectionString = db.ConnectionString();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "UPDATE tb_login SET password = SHA256('" + log.password + "'), nama=@nama, alamat=@alamat, email=@email, kontak=@kontak WHERE username= @username";
                sqlCmd.Connection = _connection;

                sqlCmd.Parameters.AddWithValue("@username", log.username);
                sqlCmd.Parameters.AddWithValue("@password", log.password);
                sqlCmd.Parameters.AddWithValue("@nama", log.nama);
                sqlCmd.Parameters.AddWithValue("@alamat", log.alamat);
                sqlCmd.Parameters.AddWithValue("@email", log.email);
                sqlCmd.Parameters.AddWithValue("@role", log.role);
                sqlCmd.Parameters.AddWithValue("@kontak", log.kontak);

                _connection.Open();
                int rowUpdatd = sqlCmd.ExecuteNonQuery();

                _connection.Close();
            }
        }

        [HttpDelete]
        public void Delete(string username)
        {
            con = new MySqlConnection();
            db = new Db_Connection();

            var connStr = con.ConnectionString = db.ConnectionString();

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "delete from tb_login where username = " + username + "";
                sqlCmd.Connection = _connection;
                _connection.Open();
                int rowDeleted = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        //[HttpPost]
        //public void LoginUser([FromBody]tb_login usr)
        //{
        //    con = new MySqlConnection();
        //    db = new Db_Connection();

        //    var connStr = con.ConnectionString = db.ConnectionString();

        //    using (MySqlConnection _connection = new MySqlConnection(connStr))
        //    {
        //        MySqlCommand sqlCmd = new MySqlCommand("SELECT *FROM tb_login WHERE username=@username AND password= SHA256(@password) OR email=@email AND password=SHA256(@password)", _connection);

        //        //sqlCmd.CommandType = CommandType.Text;
        //        //sqlCmd.CommandText = "SELECT *FROM tbl_admin WHERE username=@username AND pasw=@password";
        //        //sqlCmd.Connection = _connection;

        //        if (sqlCmd != null)
        //        {
        //            sqlCmd.Parameters.AddWithValue("@username", usr.username);
        //            sqlCmd.Parameters.AddWithValue("@password", usr.password);
        //            sqlCmd.Parameters.AddWithValue("@email", usr.email);
        //            _connection.Open();

        //            sqlCmd.ExecuteNonQuery();
        //            _connection.Close();

        //        }
        //        else
        //        {
        //            Console.WriteLine("LOGIN FAILED");
        //        }
        //    }
        //}

        [HttpPost]
        public JsonResult LoginUser(tb_login usr)
        {
            con = new MySqlConnection();
            db = new Db_Connection();

            var connStr = con.ConnectionString = db.ConnectionString();

            if (!ModelState.IsValid)
                return new JsonResult("Not a valid data");

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand("SELECT *FROM tb_login WHERE username=@username AND password= md5(@password) OR email=@email AND password= md5(@password)", _connection);

                sqlCmd.Parameters.AddWithValue("@username", usr.username);
                sqlCmd.Parameters.AddWithValue("@password", usr.password);
                sqlCmd.Parameters.AddWithValue("@email", usr.email);
                _connection.Open();

                var log = sqlCmd.ExecuteScalar();
                _connection.Close();

                if (log == null)
                {
                    return new JsonResult("Username or password wrong!");
                }
                else

                    return new JsonResult("Login Succesfull!" +
                        "");

            }
        }
    }



    //[HttpPost]
    //public Models.Response UserLogin(tbl_admin login)
    //{
    //    string connStr = ConfigurationManager.ConnectionStrings["mySql"].ConnectionString;

    //    using (MySqlConnection _connection = new MySqlConnection(connStr))
    //    {
    //        MySqlCommand manise = new MySqlCommand("SELECT *FROM tbl_admin WHERE username=@username AND pasw=@password", _connection);
    //        manise.Parameters.AddWithValue("@username", login.username);
    //        manise.Parameters.AddWithValue("@password", login.pasw);
    //        _connection.Open();
    //        var log = manise.ExecuteScalar();
    //        _connection.Close();

    //        if (log == null)
    //        {
    //            return new Models.Response { Status = "Invalid", Message = "Invalid User." };
    //        }
    //        else
    //            return new Models.Response { Status = "Success", Message = "Login Successfully" };
    //    }
    //}


    //}
}