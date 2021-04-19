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
    public interface IQuestionService : IServiceBase<QuestionDTO>
    {
        ResponseService<IList<QuestionDTO>> GetListQuestion();
    }
    public class QuestionService : IQuestionService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public QuestionService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ResponseService<string> Add(QuestionDTO entity)
        {
            throw new NotImplementedException();
        }

        public ResponseService<string> AddAndSave(QuestionDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                _repository._question.Add(_mapper.Map<QuestionDTO, Question>(entity));
                SaveChanges();
                response.result = "Add Question Successfully";
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
            var response = new ResponseService<string>();
            try
            {
                var temp = _repository._question.GetSingleByCondition(p => p.questionId == id);
                _repository._question.Delete(temp);
                SaveChanges();
                response.result = "Detele Question Successfully";
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public ResponseService<List<QuestionDTO>> GetAll()
        {
            var response = new ResponseService<List<QuestionDTO>>();
            try
            {
                response.result=_mapper.Map<List<Question>,List<QuestionDTO>>(_repository._question.GetAll());
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public ResponseService<QuestionDTO> GetById(int id)
        {
            var response = new ResponseService<QuestionDTO>();
            try
            {
                response.result = _mapper.Map<Question, QuestionDTO>(_repository._question.GetSingleByCondition(p => p.questionId == id));
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

        public ResponseService<string> Update(QuestionDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                _repository._question.Update(_mapper.Map<QuestionDTO, Question>(entity));
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

        public ResponseService<IList<QuestionDTO>> GetListQuestion()
        {
            var response = new ResponseService<IList<QuestionDTO>>();
            try
            {
                response.result=_mapper.Map<IList<Question>, IList<QuestionDTO>>(_repository._question.getRamdon());
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }
    }
}
