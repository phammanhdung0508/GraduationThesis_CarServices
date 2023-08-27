namespace GraduationThesis_CarServices.Models.DTO.Garage
{
    public class LotResponseDto
    {
        public List<LotList>? LotLists {get; set;}
        public int FreeCount {get; set;}
        public int BeingUsedCount {get; set;}
    }
    public class LotList
    {
        public string LotNumber { get; set; } = string.Empty;
        public string IsAssignedFor { get; set; } = string.Empty;
        public string LotStatus { get; set; } = string.Empty;
    }
}