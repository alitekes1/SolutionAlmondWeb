using AlmondWeb.DataAccessLayer.RepositoryPattern;
using AlmondWeb.DataAccessLayer;
using AlmondWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace AlmondWeb.BusinessLayer
{
    public class ListManager : BaseManager<ListTable>
    {
        public List<ListTable> List(int ownerId)
        {
            return List(x => x.Owner.Id == ownerId && x.isDeleted == false);
        }
    }
}
