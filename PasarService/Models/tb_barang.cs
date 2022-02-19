using System;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace PasarService.Models
{
    public class tb_barang
    {
        public int Id { get; set; }
        public string nama_barang { get; set; }
        public int harga { get; set; }
        public byte[] gambar { get; set; }
        public int stok { get; set; }
        public int minstok { get; set; }
    }
}
