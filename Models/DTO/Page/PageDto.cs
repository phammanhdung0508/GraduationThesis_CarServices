#nullable disable
using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Page
{
    public class PageDto
    {
        public string page_search {get; set;}
        [DefaultValue(1)]
        public int page_index { get; set; }
        [DefaultValue(10)]
        public int page_size { get; set; }
    }
}