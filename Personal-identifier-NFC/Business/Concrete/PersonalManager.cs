using Business.Abstract;
using DataAccess.Abstract;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PersonalManager : IPersonalService
    {
        private readonly IPersonalDal _personalDal;

        public PersonalManager(IPersonalDal personalDal)
        {
            _personalDal = personalDal;
        }

        public void Add(Personal personal)
        {
            _personalDal.Add(personal);
        }
    }
}
