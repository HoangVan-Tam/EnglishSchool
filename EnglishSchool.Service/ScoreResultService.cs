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
    public interface IScoreResultService
    {
        ResponseService<List<ScoreResultDTO>> GetListScoreResultByStudentID(string studentId);
        ResponseService<List<ScoreResultDTO>> GetListScoreResultByCourseID(int courseId);
        ResponseService<List<ScoreResultDTO>> GetListScoreResultByCourseDetailId(int courseDetailId);
    }
    public class ScoreResultService : IScoreResultService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ScoreResultService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ResponseService<List<ScoreResultDTO>> GetListScoreResultByStudentID(string studentId)
        {
            throw new NotImplementedException();
        }

        public ResponseService<List<ScoreResultDTO>> GetListScoreResultByCourseID(int courseId)
        {
            throw new NotImplementedException();
        }

        public ResponseService<List<ScoreResultDTO>> GetListScoreResultByCourseDetailId(int courseDetailId)
        {
            var response = new ResponseService<List<ScoreResultDTO>>();
            try
            {
                var result = _repository._scoreResult.GetMulti(p => p.courseDetailId == courseDetailId);
                response.result = _mapper.Map<List<ScoreResult>, List<ScoreResultDTO>>(result);
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }
    }
}
