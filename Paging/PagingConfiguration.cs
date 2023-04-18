using System.Linq;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Paging
{
    public class PagingConfiguration<T> : List<T> where T : class
    {
        public int page_index { get; set; }
        public int page_total { get; set; }
        public PagingConfiguration(List<T> list, int count, PageDto page)
        {
            page_index = page.page_index;
            page_total = (int) Math.Ceiling(count/(double)page.page_size);
            this.AddRange(list);
        }

        public bool HasPreviousPage => page_index > 1;
        public bool HasNextPage => page_index < page_total;

        public static async Task<PagingConfiguration<T>> Create (DbSet<T> srouce, PageDto page){
            var myTask = Task.Run(() => srouce.Skip((page.page_index - 1)*page.page_size).Take(page.page_size).ToList());
            var list = await myTask;
            var count = list.Count;
            return new PagingConfiguration<T>(list, count, page);
        }
    }
}