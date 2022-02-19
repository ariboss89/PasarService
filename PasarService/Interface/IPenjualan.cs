using System;
using System.Collections.Generic;
using PasarService.Models;

namespace PasarService.Interface
{
    public interface IPenjualan
    {
        List<tb_penjualan> GetAllPenjualan();
        void SavePenjualan(tb_penjualan pnj);
        void UpdateStatus(string id);
        void DeletePenjualan(string id);

        List<dt_penjualan> GetDetailPenjualan(string idPenjualan);
        void SaveDetail(dt_penjualan dt);
        void DeleteDetail(int iddetail);
        string IdPenjualan();
    }
}
