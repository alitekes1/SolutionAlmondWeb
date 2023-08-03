namespace AlmondWeb.Entities
{
    public class ProfileListTable
    {
        public int profileId { get; set; }
        public int listId { get; set; }
        public ProfileTable profile { get; set; }
        public ListTable List { get; set; }
    }
}
