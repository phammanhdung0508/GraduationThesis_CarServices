using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationThesis_CarServices.Models.DTO.Car
{
    public class DeleteCarDto
    {
        public int CarId { get; set; }
        public int CarStatus {get; set;}
    }
}