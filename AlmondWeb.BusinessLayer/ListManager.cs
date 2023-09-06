using AlmondWeb.Entities;
using System.Collections.Generic;

namespace AlmondWeb.BusinessLayer
{
    public class ListManager : BaseManager<ListTable>
    {
        public List<ListTable> List(int ownerId)
        {
            return ListwithExpression(x => x.Owner.Id == ownerId && x.isDeleted == false);
        }
    }
}
