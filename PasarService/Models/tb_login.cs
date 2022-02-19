using System;
namespace PasarService.Models
{
    public class tb_login
    {
        public string username { get; set; }
        public string password { get; set; }
        public string nama { get; set; }
        public string alamat { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public int? kontak { get; set; }
    }
}
