using Core.DataAccess;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess.Abstract
{
    public interface IPersonalDal : IEntityRepository<Personal>
    {
    }
}
