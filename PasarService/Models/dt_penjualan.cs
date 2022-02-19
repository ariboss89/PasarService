using System;
namespace PasarService.Models
{
    public class dt_penjualan
    {
        public int iddetail { get; set; }
        public string id_penjualan { get; set; }
        public int id_barang { get; set; }
        public string nama_barang { get; set; }
        public int harga { get; set; }
        public int qty { get; set; }
        public int total { get; set; }
    }
}
