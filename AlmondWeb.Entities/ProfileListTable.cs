using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmondWeb.Entities
{
    public class ProfileListTable
    {
        public int profileId { get; set; }
        public int listId { get; set; }
        public ProfileTable profile { get; set; }
        public ListTable list { get; set; }
    }
}
