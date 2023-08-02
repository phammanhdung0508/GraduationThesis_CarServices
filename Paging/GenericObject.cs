namespace GraduationThesis_CarServices.Paging
{
    public class GenericObject<T>
    {
        public T list { get; set; }
        public int count { get; set; }

        public GenericObject(T list, int count)
        {
            this.list = list;
            this.count = count;
        }
    }
}