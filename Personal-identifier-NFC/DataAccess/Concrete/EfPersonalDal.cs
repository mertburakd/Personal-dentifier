using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramwork;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess.Concrete
{
    public class EfPersonalDal : EfEntityRepositoryBase<Personal, PersonalContext>, IPersonalDal
    {
    }
}
