using AutoMapper;
using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Data.Repositories;
using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
using EnglishSchool.Model.ResponseService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace EnglishSchool.Service
{
    public interface INewsService : IServiceBase<NewsDTO>
    {
        ResponseService<string> AddAndSave();
        ResponseService<string> Update();
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

        public ResponseService<string> AddAndSave()
        {
            var response = new ResponseService<string>();
            try
            {
                News news = new News();
                news.bodyContent = HttpContext.Current.Request.Form.Get("bodyContent");
                news.headContent = HttpContext.Current.Request.Form.Get("headContent");
                news.title = HttpContext.Current.Request.Form.Get("title");
                var postDateClient = HttpContext.Current.Request.Form.Get("postDateClient");
                news.postDate = Convert.ToDateTime(postDateClient);
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    string path = HttpContext.Current.Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
                    string fileName = "image" + news.postDate.Day.ToString() + news.postDate.Month.ToString() + news.postDate.Year.ToString() +
                        "-" + DateTime.Now.Millisecond.ToString() + news.postDate.Second.ToString() + news.postDate.Minute.ToString() + news.postDate.Hour.ToString() + ".png";
                    postedFile.SaveAs(path + fileName);
                    news.image = fileName;
                }
                _repository._news.Add(news);
                SaveChanges();
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }


        public ResponseService<string> Add(NewsDTO entity)
        {
            throw new NotImplementedException();
        }

        public ResponseService<string> AddAndSave(NewsDTO entity)
        {
            throw new NotImplementedException();
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

        public ResponseService<string> Update()
        {
            var response = new ResponseService<string>();
            try
            {
                int id = Convert.ToInt32(HttpContext.Current.Request.Form.Get("id"));
                var news = _repository._news.GetSingleByCondition(p => p.id == id);
                news.title = HttpContext.Current.Request.Form.Get("title");
                news.bodyContent = HttpContext.Current.Request.Form.Get("bodyContent");
                news.headContent = HttpContext.Current.Request.Form.Get("headContent");
                string Path = HttpContext.Current.Server.MapPath("~/Uploads/");
                if (HttpContext.Current.Request.Files.Count != 0)
                {
                    HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
                    if (news.image == "" || news.image == null)
                    {
                        news.image = "image" + news.postDate.Day.ToString() + news.postDate.Month.ToString() + news.postDate.Year.ToString() +
                        "-" + DateTime.Now.Millisecond.ToString() + news.postDate.Second.ToString() + news.postDate.Minute.ToString() + news.postDate.Hour.ToString() + ".png";
                    }
                    else
                    {
                        news.image = HttpContext.Current.Request.Form.Get("image");
                    }
                    FileInfo file = new FileInfo(Path + news.image);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    news.image = "image" + news.postDate.Day.ToString() + news.postDate.Month.ToString() + news.postDate.Year.ToString() +
                       "-" + DateTime.Now.Millisecond.ToString() + news.postDate.Second.ToString() + news.postDate.Minute.ToString() + news.postDate.Hour.ToString() + ".png";
                    postedFile.SaveAs(Path + news.image);
                }

                _repository._news.Update(news);
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

        public ResponseService<string> Update(NewsDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}