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
using System.Text.RegularExpressions;
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


        public string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public ResponseService<string> AddAndSave(ParentDTO entity)
        {
            var response = new ResponseService<string>();
            var checkParent = _repository._parent.GetSingleByCondition(p => p.phoneNumber == entity.phoneNumber);
            if (checkParent==null)
            {
                response.success = false;
                response.message = "Parent was created";
                return response;
            }
            var tempId = _repository._student.GetLastStudentId() + 1;
            try
            {
                var db = _db.Init();
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var parent = _mapper.Map<ParentDTO, Parent>(entity);
                        parent.parentId = "par" + "-" + String.Format("{0:D6}", tempId);
                        var firstName = convertToUnSign3(parent.firstName);
                        var lastName = convertToUnSign3(parent.lastName);
                        parent.password = firstName.First().ToString().ToUpper() + firstName.Substring(1).ToLower()
                                                + lastName.First().ToString().ToUpper() + "@" + parent.phoneNumber.Substring(6);
                        parent.status = true;
                        _repository._parent.Add(parent);
                        SaveChanges();
                        var student = _repository._student.GetSingleByCondition(p => p.studentId ==entity.studentId);
                        student.parentId = parent.parentId;
                        _repository._student.Update(student);
                        SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
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
            var response = new ResponseService<string>();
            try
            {
                _repository._parent.Update(_mapper.Map<ParentDTO, Parent>(entity));
                SaveChanges();
                response.result = "Update Parent successfully";
            }
            catch(Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
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
