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
    public interface ITestService
    {
        ResponseService<List<TestDTO>> GetListTestByStudentID(string studentId);
        ResponseService<List<TestDTO>> GetListTestByCourseID(int courseId);
        ResponseService<List<TestDTO>> GetListTestByCourseDetailId(int CourseDetailId);
    }
    public class TestService : ITestService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TestService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ResponseService<List<TestDTO>> GetListTestByStudentID(string studentId)
        {
            throw new NotImplementedException();
        }

        public ResponseService<List<TestDTO>> GetListTestByCourseID(int courseId)
        {
            throw new NotImplementedException();
        }

        public ResponseService<List<TestDTO>> GetListTestByCourseDetailId(int CourseDetailId)
        {
            var response = new ResponseService<List<TestDTO>>();
            try
            {
                var result = _repository._test.GetMulti(p => p.courseDetailId == CourseDetailId);
                response.result = _mapper.Map<List<Test>, List<TestDTO>>(result);
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }
    }
    public ResponseService<List<DetailTestDTO>>GetListQuestionByTestId(int testId)
    {
        var response = new ResponseService<List<DetailTestDTO>>();
        try
        {
            var question = _repository
        }
        catch(Exception ex)
        {
            response.message = ex.Message;
            response.success = false;
        }
        return response;
    }
}
