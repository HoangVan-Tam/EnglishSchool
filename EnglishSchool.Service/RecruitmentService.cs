using AutoMapper;
using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Data.Repositories;
using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
using EnglishSchool.Model.ResponseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Service
{
    public interface IRecruitmentService : IServiceBase<RecruitmentDTO>
    {

    }
    public class RecruitmentService : IRecruitmentService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RecruitmentService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        //add 
        public ResponseService<string> AddAndSave(RecruitmentDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                _repository._recruitment.Add(_mapper.Map<RecruitmentDTO, Recruitment>(entity));
                SaveChanges();
                response.result = "Add Successfully";
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }


        //Delete
        public ResponseService<string> Delete(int id)
        {
            var response = new ResponseService<string>();
            try
            {
                var temp = _repository._recruitment.GetSingleByCondition(p => p.id == id);
                _repository._recruitment.Delete(temp);
                SaveChanges();
                response.result = "Delete Successfully";
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }


        //Get all
        public ResponseService<List<RecruitmentDTO>> GetAll()
        {
            var response = new ResponseService<List<RecruitmentDTO>>();
            try
            {
                response.result = _mapper.Map<List<Recruitment>, List<RecruitmentDTO>>(_repository._recruitment.GetAll());
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }


        //Get by id
        public ResponseService<RecruitmentDTO> GetById(int id)
        {
            var response = new ResponseService<RecruitmentDTO>();
            try
            {
                response.result = _mapper.Map<Recruitment, RecruitmentDTO>(_repository._recruitment.GetSingleByCondition(p => p.id == id));
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }


        //Update
        public ResponseService<string> Update(RecruitmentDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                _repository._recruitment.Update(_mapper.Map<RecruitmentDTO, Recruitment>(entity));
                SaveChanges();
                response.result = "Update Successfully";
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public ResponseService<string> Add(RecruitmentDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
