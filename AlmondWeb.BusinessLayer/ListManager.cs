using AlmondWeb.BusinessLayer.RepositoryPattern;
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
    public class ListManager
    {
        Repository<ListTable> repo_list = new Repository<ListTable>();
        public List<ListTable> lists()
        {
            return repo_list.List();
        }
    }
}
