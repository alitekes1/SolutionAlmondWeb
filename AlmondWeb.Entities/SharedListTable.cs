using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlmondWeb.Entities
{
    public class SharedListTable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int profileId { get; set; }
        public int listId { get; set; }
        public bool isPublic { get; set; }
        public int OwnerId { get; set; }
        public virtual ProfileTable profile { get; set; }
        public virtual ListTable List { get; set; }
        public virtual AlmondUserTable Owner { get; set; }
        public virtual List<SharedDataTable> SharedData { get; set; }
    }
}

