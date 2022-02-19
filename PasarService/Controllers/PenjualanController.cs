using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using PasarService.Interface;
using PasarService.Models;
using PasarService.Repository;
using PasarService.Services;

namespace PasarService.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PenjualanController : ControllerBase
    {
        private IPenjualan penjualans = new PenjualanRepository();

        private MySqlConnection con;
        private MySqlCommand com;
        private Db_Connection db;

        [HttpGet]
        public IEnumerable<tb_penjualan> GetAllPenjualan()
        {
            return penjualans.GetAllPenjualan();
        }

        [HttpGet]
        public IEnumerable<dt_penjualan> GetDetailPenjualan(string id)
        {
            return penjualans.GetDetailPenjualan(id);
        }

        [HttpPost]
        public JsonResult SavePenjualan(tb_penjualan pnj)
        {
            pnj = new tb_penjualan()
            {
                id_penjualan = pnj.id_penjualan,
                tanggal = pnj.tanggal,
                total_item = pnj.total_item,
                total_bayar = pnj.total_bayar,
                bayar = pnj.bayar,
                kembali = pnj.kembali
            };

            penjualans.SavePenjualan(pnj);

            return new JsonResult("Add Success!");

        }

        [HttpPost]
        public JsonResult SaveDetail(dt_penjualan pnj)
        {
            pnj = new dt_penjualan()
            {
                id_penjualan = pnj.id_penjualan,
                id_barang = pnj.id_barang,
                nama_barang = pnj.nama_barang,
                harga = pnj.harga,
                qty = pnj.qty,
                total = pnj.total
            };

            penjualans.SaveDetail(pnj);

            return new JsonResult("Add Success!");

        }

        [HttpPut]
        public JsonResult UpdatePenjualan(string Id)
        {
            penjualans.UpdateStatus(Id);

            return new JsonResult("Data Updated!");
        }

        [HttpDelete]
        public JsonResult DeletePenjualan(string id)
        {
            penjualans.DeletePenjualan(id);

            return new JsonResult("Delete Success!");
        }
    }
}