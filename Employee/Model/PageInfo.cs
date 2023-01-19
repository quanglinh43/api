namespace Employee.Model
{
    public class PageInfo<T>
    {
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }
        public IEnumerable<T>? Items { get; set; }
        
    }
}
