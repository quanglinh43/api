namespace Employee.Model
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int Order { get; set; }
        public Menu( int id, string name, int parentId, int order)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
            Order = order;  
        }
        public List<Menu> Menus { get; set;}

    }
}
