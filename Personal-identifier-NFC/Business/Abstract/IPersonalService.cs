using Core.Results;
using Entities.Models;
using Entities.Models.Dto;
using Entities.Models.Dto.LoginModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPersonalService
    {
        IResult Add(Personal personal);
        IResult CreateNewPersonal(Personal personal);
        bool SlugCheck(string slug);
        IDataResult<List<PersonalDataDto>> PersonalDataView(string Slug);
        IDataResult<Personal> GetUserData(int userId);
    }
}
