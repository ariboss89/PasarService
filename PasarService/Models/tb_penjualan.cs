using System;
namespace PasarService.Models
{
    public class tb_penjualan
    {
        public string id_penjualan { get; set; }
        public DateTime tanggal { get; set; }
        public int total_item { get; set; }
        public int total_bayar { get; set; }
        public int bayar { get; set; }
        public int kembali { get; set; }
    }
}
