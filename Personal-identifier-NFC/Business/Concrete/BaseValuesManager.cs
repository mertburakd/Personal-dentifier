using Business.Abstract;
using Business.Constants;
using Core.Results;
using DataAccess.Abstract;
using Entities.Models;

namespace Business.Concrete
{
    public class BaseValuesManager : IBaseValuesService
    {
        private readonly IBaseValuesDal _baseValuesDal;

        public BaseValuesManager(IBaseValuesDal baseValuesDal)
        {
            _baseValuesDal = baseValuesDal;
        }

        public IResult Add(BaseValues baseValues)
        {
            _baseValuesDal.Add(baseValues);
            return new SuccessResult(Messages.ValuesAdded);
        }
    }
}
