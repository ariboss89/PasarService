using System;
using System.Collections.Generic;
using PasarService.Models;

namespace PasarService.Interface
{
    public interface IBarang
    {
        List<tb_barang> GetAllBarang();
        void SaveBarang(tb_barang brg);
        void UpdateBarang(tb_barang brg);
        void DeleteBarang(int id);
        void UpdateStok(tb_barang brg);
    }
}
