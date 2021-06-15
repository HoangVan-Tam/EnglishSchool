using AutoMapper;
using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Data.Repositories;
using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
using EnglishSchool.Model.ResponseService;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;

namespace EnglishSchool.Service
{
    public interface IPersonalInformationService : IServiceBase<PersonalInformationDTO>
    {
        ResponseService<PersonalInformationDTO> GetById(string phoneNumber);
    }
    public class PersonalInformationService : IPersonalInformationService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PersonalInformationService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ResponseService<string> AddAndSave(PersonalInformationDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                entity.note = DateTime.Now.ToString();
                _repository._personalInformation.Add(_mapper.Map<PersonalInformationDTO, PersonalInformation>(entity));
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

        public ResponseService<string> Add(PersonalInformationDTO entity)
        {
            throw new NotImplementedException();
        }

        public ResponseService<string> Update(PersonalInformationDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                _repository._personalInformation.Update(_mapper.Map<PersonalInformationDTO, PersonalInformation>(entity));
                SaveChanges();
                response.result = "Update Question Successfully";
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

        public ResponseService<List<PersonalInformationDTO>> GetAll()
        {
            var response = new ResponseService<List<PersonalInformationDTO>>();
            try
            {
                response.result = _mapper.Map<List<PersonalInformation>, List<PersonalInformationDTO>>(_repository._personalInformation.GetAll());
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public ResponseService<PersonalInformationDTO> GetById(string phoneNumber)
        {
            var response = new ResponseService<PersonalInformationDTO>();
            try
            {
                response.result = _mapper.Map<PersonalInformation, PersonalInformationDTO>(_repository._personalInformation.GetSingleByCondition(p => p.phoneNumber == phoneNumber));
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

        public ResponseService<PersonalInformationDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
