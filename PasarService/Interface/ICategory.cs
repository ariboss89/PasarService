using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PasarService.Models;

namespace PasarService.Interface
{
    public interface ICategory
    {
        List<tb_kategori> GetAllCategory();
        void SaveCategory(tb_kategori ktr);
        void UpdateCategory(tb_kategori ktr);
        void DeleteCategory(int id);
    }
}
