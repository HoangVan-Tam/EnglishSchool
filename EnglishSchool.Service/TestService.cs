using AutoMapper;
using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Data.Repositories;
using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
using EnglishSchool.Model.ResponseService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishSchool.Service
{
    public interface ITestService
    {
        ResponseService<List<TestDTO>> GetListTestByStudentID(string studentId);
        ResponseService<List<TestDTO>> GetListTestByCourseID(int courseId);
        ResponseService<List<TestDTO>> GetListTestByCourseDetailId(string studentId, int courseId);
        ResponseService<List<DetailTestDTO>> GetListQuestionByTestId(int testId);
        ResponseService<List<QuestionDTO>> GetListQuestion();
        ResponseService<string> Submit(SubmitTestDTO submitTestDTO);
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

        public ResponseService<List<TestDTO>> GetListTestByCourseDetailId(string studentId, int courseId)
        {
            var response = new ResponseService<List<TestDTO>>();
            try
            {
                var courseDetail = _repository._courseDetailOfStudent.GetSingleByCondition(p => p.studentId == studentId && p.courseId == courseId).courseDetailId;
                var result = _repository._test.GetMulti(p => p.courseDetailId == courseDetail);
                foreach (Test test in result)
                {
                    if (test.finishDay < DateTime.Now)
                    {
                        test.status = "Không tham gia";
                    }
                    else if (test.startDay < DateTime.Now && test.finishDay > DateTime.Now)
                    {
                        test.status = "Làm bài";
                    }
                    else if (test.startDay > DateTime.Now)
                    {
                        test.status = "Chưa được làm";
                    }
                }
                response.result = _mapper.Map<List<Test>, List<TestDTO>>(result);
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }
        public ResponseService<List<DetailTestDTO>> GetListQuestionByTestId(int testId)
        {
            var response = new ResponseService<List<DetailTestDTO>>();
            try
            {
                if (_repository._detailTest.GetSingleByCondition(p => p.testId == testId) == null)
                {
                    var questions = _repository._question.getRamdon20();
                    for (int i = 0; i < 20; i++)
                    {
                        DetailTest detailTest = new DetailTest();
                        detailTest.questionId = questions[i].questionId;
                        detailTest.testId = testId;
                        _repository._detailTest.Add(detailTest);
                    }
                    SaveChanges();
                }
                var result = _repository._detailTest.GetAllInFomation(testId);
                response.result = _mapper.Map<List<DetailTest>, List<DetailTestDTO>>(result);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public ResponseService<List<QuestionDTO>> GetListQuestion()
        {
            var response = new ResponseService<List<QuestionDTO>>();
            try
            {
                var result = _repository._question.getRamdon20();
                response.result = _mapper.Map<List<Question>, List<QuestionDTO>>(result);
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }
        public ResponseService<string> Submit(SubmitTestDTO submitTestDTO)
        {
            var response = new ResponseService<string>();
            try
            {
                var test = _repository._test.GetSingleByCondition(p => p.testId == submitTestDTO.testId);
                test.score = submitTestDTO.score;
                test.status = "Đã làm";
                SaveChanges();
                response.result = "Submit sucessfully";
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }
        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
