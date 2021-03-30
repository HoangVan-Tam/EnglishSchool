using AutoMapper;
using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Data.Repositories;
using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.ResponseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Service
{
    public interface IParentService:IServiceBase<ParentDTO>
    {

    }
    public class ParentService : IParentService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IDbFactory _db;
        public ParentService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper, IDbFactory db)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _db = db;
        }

        public ResponseService<string> AddAndSave(ParentDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                var db = _db.Init();
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        response.success = false;
                        response.message = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public ResponseService<string> Add(ParentDTO entity)
        {
            throw new NotImplementedException();
        }

        public ResponseService<string> Update(ParentDTO entity)
        {
            throw new NotImplementedException();
        }

        public ResponseService<string> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseService<List<ParentDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public ResponseService<ParentDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
