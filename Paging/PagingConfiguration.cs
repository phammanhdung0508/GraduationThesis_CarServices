using GraduationThesis_CarServices.Models.DTO.Page;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Paging
{
    public class PagingConfiguration<T> : List<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageTotal { get; set; }
        public PagingConfiguration(List<T> list, int count, PageDto page)
        {
            PageIndex = page.PageIndex;
            PageTotal = (int) Math.Ceiling(count/(double)page.PageSize);
            this.AddRange(list);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < PageTotal;

        public static async Task<PagingConfiguration<T>> Create (DbSet<T> srouce, PageDto page){
            var myTask = Task.Run(() => srouce.Skip((page.PageIndex - 1)*page.PageSize).Take(page.PageSize).ToList());
            var list = await myTask;
            var count = list.Count;
            return new PagingConfiguration<T>(list, count, page);
        }
    }
}