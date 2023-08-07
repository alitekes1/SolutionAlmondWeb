namespace AlmondWeb.Entities
{
    public class ProfListTable
    {
        public int profileId { get; set; }
        public int listId { get; set; }
        public bool isPublic { get; set; }
        public ProfileTable profile { get; set; }
        public ListTable List { get; set; }
        public AlmondUserTable Owner { get; set; }
    }
}

