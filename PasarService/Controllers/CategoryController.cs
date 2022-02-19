using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using PasarService.Interface;
using PasarService.Models;
using PasarService.Repository;
using PasarService.Services;

namespace PasarService.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategory categories = new CategoryRepository();

        private MySqlConnection con;
        private MySqlCommand com;
        private Db_Connection db;

        [HttpGet]
        public IEnumerable<tb_kategori> GetAllCategory()
        {
            return categories.GetAllCategory();
        }

        [HttpPost]
        public JsonResult AddCategory(tb_kategori ktr)
        {
            ktr = new tb_kategori()
            {
                nama_kategori = ktr.nama_kategori
            };

            categories.SaveCategory(ktr);

            return new JsonResult("Add Success!");

        }

        [HttpPut]
        public JsonResult UpdateKategori(tb_kategori ktr)
        {
            ktr = new tb_kategori()
            {
                id = ktr.id,
                nama_kategori = ktr.nama_kategori
            };

            categories.UpdateCategory(ktr);

            return new JsonResult("Data Updated!");
        }

        //[HttpDelete("{id}")]
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            categories.DeleteCategory(id);

            return new JsonResult("Delete Success!");
        }

        //[HttpGet]
        //public string ShowSGGID(string requestor)
        ////{
        //con = new MySqlConnection();
        //db = new Db_Connection();

        //var connStr = con.ConnectionString = db.ConnectionString();


        //    string connStr = ConfigurationManager.ConnectionStrings["mySql"].ConnectionString;

        //    using (MySqlConnection _connection = new MySqlConnection(connStr))
        //    {
        //        _connection.Open();
        //        MySqlCommand _command = new MySqlCommand("SELECT *FROM tbl_user WHERE requestor = '" + requestor + "'", _connection);
        //        MySqlDataAdapter adapter = new MySqlDataAdapter(_command);
        //        DataTable results = new DataTable();

        //        adapter.Fill(results);



        //        _connection.Close();

        //        if (results == null)
        //        {
        //            NotFound();
        //        }

        //        var srlJson = JsonConvert.SerializeObject(results);


        //        return JsonConvert.DeserializeObject<List<dt_emrf>>(srlJson);
        //    }
        //}

        //[HttpPost]
        //public void Login([FromUri] string username, string password, string name)
        //{
        //    string connStr = ConfigurationManager.ConnectionStrings["mySql"].ConnectionString;
        //    using (MySqlConnection _connection = new MySqlConnection(connStr))
        //    {
        //        _connection.Open();
        //        MySqlCommand _command = new MySqlCommand("SELECT *FROM tbl_user where username = '" + username + "' and password = '" + password + "' AND name = '"+name+"'", _connection);
        //        MySqlDataAdapter adapter = new MySqlDataAdapter(_command);

        //        _connection.Close();
        //    }
        //}

        //[HttpPost]
        //public void LoginUser(tbl_user usr)
        //{
        //    string connStr = ConfigurationManager.ConnectionStrings["mySql"].ConnectionString;

        //    using (MySqlConnection _connection = new MySqlConnection(connStr))
        //    {
        //        MySqlCommand sqlCmd = new MySqlCommand("SELECT *FROM tbl_user WHERE username=@username AND password=@password AND name=@name", _connection);
        //        //sqlCmd.CommandType = CommandType.Text;
        //        //sqlCmd.CommandText = "SELECT *FROM tbl_admin WHERE username=@username AND pasw=@password";
        //        //sqlCmd.Connection = _connection;

        //        if (sqlCmd != null)
        //        {
        //            sqlCmd.Parameters.AddWithValue("@username", usr.username);
        //            sqlCmd.Parameters.AddWithValue("@password", usr.password);
        //            sqlCmd.Parameters.AddWithValue("@name", usr.name);
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

        //    [HttpPost]
        //    public IHttpActionResult LoginUser(tbl_user usr)
        //    {
        //        string connStr = ConfigurationManager.ConnectionStrings["mySql"].ConnectionString;

        //        if (!ModelState.IsValid)
        //            return BadRequest("Not a valid data");

        //        using (MySqlConnection _connection = new MySqlConnection(connStr))
        //        {
        //            MySqlCommand sqlCmd = new MySqlCommand("SELECT *FROM tbl_user WHERE sggid=@username AND password=@password AND name=@name", _connection);
        //            sqlCmd.Parameters.AddWithValue("@username", usr.username.Trim());
        //            sqlCmd.Parameters.AddWithValue("@password", usr.password);
        //            sqlCmd.Parameters.AddWithValue("@name", usr.name.Trim());
        //            _connection.Open();

        //            var log = sqlCmd.ExecuteScalar();
        //            _connection.Close();

        //            if (log == null)
        //            {
        //                return NotFound();
        //            }
        //            else

        //                return Ok();

        //        }
        //    }
        //}



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
}