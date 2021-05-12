using System;
using System.Collections.Generic;
using AutoMapper;
using DevExpress.Xpo;
using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Data.Repositories;
using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
using EnglishSchool.Model.ResponseService;

namespace EnglishSchool.Service
{
    public interface INewsService : IServiceBase<NewsDTO>
    {

    }

    public class NewsService : INewsService
    {

        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public NewsService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ResponseService<string> Add(NewsDTO entity)
        {
            throw new NotImplementedException();
        }

        public ResponseService<string> AddAndSave(NewsDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                entity.postDate = Convert.ToDateTime(entity.postDateClient);
                _repository._news.Add(_mapper.Map<NewsDTO,News> (entity));
                SaveChanges();
                response.result = "Add News successfully";
            }
            catch (Exception ex)
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

        public ResponseService<List<NewsDTO>> GetAll()
        {
            var response = new ResponseService<List<NewsDTO>>();
            try
            {
                var result = _repository._news.GetAll();
                response.result = _mapper.Map<List<News>, List<NewsDTO>>(result);
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public ResponseService<NewsDTO> GetById(int id)
        {
            var response = new ResponseService<NewsDTO>();
            try
            {
                response.result = _mapper.Map<News, NewsDTO>(_repository._news.GetSingleByCondition(p => p.id == id));
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

        public ResponseService<string> Update(NewsDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                _repository._news.Update(_mapper.Map<NewsDTO, News>(entity));
                SaveChanges();
                response.result = "Update News successfully";
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