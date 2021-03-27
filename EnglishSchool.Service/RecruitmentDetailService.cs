using AutoMapper;
using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Data.Repositories;
using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
using EnglishSchool.Model.ResponseService;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Service
{
    public interface IRecruitmentDetailService : IServiceBase<RecruitmentDetailDTO>
    {
        ResponseService<string> Delete(int departmentId, int recruitmentId);
        ResponseService<RecruitmentDetailDTO> GetByDepartmentIdAndRecruitmentId(int departmentId, int recruitmentId);
    }
    public class RecruitmentDetailService : IRecruitmentDetailService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RecruitmentDetailService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ResponseService<string> AddAndSave(JObject entity)
        {
            var response = new ResponseService<string>();
            try
            {
                _repository._listRecruitmentDetail.Add(_mapper.Map<JObject, RecruitmentDetail>(entity));
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

        public ResponseService<string> Delete(int departmentId, int recruitmentId)
        {
            var response = new ResponseService<string>();
            try
            {
                var temp = _repository._listRecruitmentDetail.GetSingleByCondition(p => p.departmentId == departmentId && p.recruitmentId == recruitmentId);
                _repository._listRecruitmentDetail.Delete(temp);
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

        public ResponseService<List<RecruitmentDetailDTO>> GetAll()
        {
            var response = new ResponseService<List<RecruitmentDetailDTO>>();
            try
            {
                response.result = _mapper.Map<List<RecruitmentDetail>, List<RecruitmentDetailDTO>>(_repository._listRecruitmentDetail.GetAllInFomationOfRecruitment());
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public ResponseService<RecruitmentDetailDTO> GetByDepartmentIdAndRecruitmentId(int departmentId, int recruitmentId)
        {
            var response = new ResponseService<RecruitmentDetailDTO>();
            try
            {
                var temp = _repository._listRecruitmentDetail.GetSingleByCondition(p => p.departmentId == departmentId && p.recruitmentId == recruitmentId);
                response.result = _mapper.Map<RecruitmentDetail, RecruitmentDetailDTO>(temp);
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

        public ResponseService<string> Update(JObject entity)
        {
            var response = new ResponseService<string>();
            try
            {
                _repository._listRecruitmentDetail.Update(_mapper.Map<JObject, RecruitmentDetail>(entity));
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

        public ResponseService<string> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseService<RecruitmentDetailDTO> GetById(int id)
        {
            var response = new ResponseService<RecruitmentDetailDTO>();
            try
            {
                var temp = _repository._listRecruitmentDetail.GetSingleByCondition(p => p.departmentId == id || p.recruitmentId == id);
                response.result = _mapper.Map<RecruitmentDetail, RecruitmentDetailDTO>(temp);
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public ResponseService<string> Add(JObject entity)
        {
            throw new NotImplementedException();
        }
    }
}
