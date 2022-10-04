using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Results;
using DataAccess.Abstract;
using Entities.Models;
using Entities.Models.Dto;
using Entities.Models.Dto.LoginModels;
using Newtonsoft.Json;
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
        private readonly IBaseValuesService _baseValuesService;
        private IMapper _mapper;

        public PersonalManager(IPersonalDal personalDal, IBaseValuesService baseValuesService, IMapper mapper)
        {
            _baseValuesService = baseValuesService;
            _personalDal = personalDal;
            _mapper = mapper;
        }

        public IResult Add(Personal personal)
        {
            _personalDal.Add(personal);
            return new SuccessResult(Messages.PersonalAdded);
        }

        public IResult CreateNewPersonal(Personal personal)
        {
            var basevalues = _baseValuesService.GetAllActive();
            var mapping=_mapper.Map<List<PersonalDataDto>>(basevalues.Data);
            personal.Data=JsonConvert.SerializeObject(mapping);
            _personalDal.Add(personal);
            
            return new SuccessResult(Messages.PersonalCreatedNewUser);
        }

        public IResult Delete(int UserId)
        {
            _personalDal.Delete(new Personal { UserId=UserId});
            return new SuccessResult(Messages.PersonalDeleteUser);
        }

        public IDataResult<Personal> GetUserData(int userId)
        {
            return new SuccessDataResult<Personal>(_personalDal.Get(q=>q.UserId==userId));
        }

        public IDataResult<List<PersonalDataDto>> PersonalDataView(string Slug)
        {
            var personaldata = _personalDal.Get(q=>q.Slug==Slug);
            var map=_mapper.Map<List<PersonalDataDto>>(JsonConvert.DeserializeObject<List<BaseValues>>(personaldata.Data)).Where(q=>q.IsActive).ToList();
            return new SuccessDataResult<List<PersonalDataDto>>(map);
        }

        public bool SlugCheck(string slug)
        {
            return !_personalDal.GetList(q => q.Slug == slug).Any();
        }
    }
}
