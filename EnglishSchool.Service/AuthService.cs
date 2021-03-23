using AutoMapper;
using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Data.Repositories;
using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
using EnglishSchool.Model.ResponseService;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Service
{
    public interface IAuthService
    {
        ResponseService<string> AddAndSave(Account entity);
        ResponseService<string> Add(Account entity);
        ResponseService<List<AccountDTO>> GetAll();
        ResponseService<AccountDTO> Login(Account account);
        ResponseService<AccountDTO> Update(Account account);
        ResponseService<AccountDTO> GetByUserName(string username);
        ResponseService<AccountDTO> ChangePassword(AccountChangePasswordDTO account);
        void SaveChanges();
    }
    public class AuthService : IAuthService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public AuthService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        //add
        public ResponseService<string> AddAndSave(Account entity)
        {
            var response = new ResponseService<string>();
            if (GetByUserName(entity.userName).result != null)
            {
                response.message = "Duplicate Account";
                response.success = false;
            }
            else
            {
                try
                {
                    entity.password = BCrypt.Net.BCrypt.HashPassword(entity.password);
                    _repository._account.Add(entity);
                    SaveChanges();
                    response.result = "Add Account Successfully";
                }
                catch (Exception ex)
                {
                    response.message = ex.Message;
                    response.success = false;
                }
            }
            return response;
        }

        public ResponseService<string> Add(Account entity)
        {
            var response = new ResponseService<string>();
            if (GetByUserName(entity.userName).result != null)
            {
                response.message = "Duplicate Account";
                response.success = false;
            }
            else
            {
                try
                {
                    entity.password = BCrypt.Net.BCrypt.HashPassword(entity.password);
                    _repository._account.Add(entity);
                    response.result = "Add Account Successfully";
                }
                catch (Exception ex)
                {
                    response.message = ex.Message;
                    response.success = false;
                }
            }
            return response;
        }

        //create token
        public string CreateToken(Account account)
        {
            string secretKey = "my_secret_key_12345";
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.userName),
                new Claim(ClaimTypes.Role, account.role.ToString()),
                //new Claim(ClaimTypes.StateOrProvince, account.status)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        //login
        public ResponseService<AccountDTO> Login(Account account)
        {
            var response = new ResponseService<AccountDTO>();
            var temp = _repository._account.GetSingleByCondition(p => p.userName == account.userName);
            if (temp != null)
            {
                if (temp.status == "available")
                {
                    if (temp.role == account.role)
                    {
                        if (BCrypt.Net.BCrypt.Verify(account.password, temp.password))
                        {
                            response.result = new AccountDTO()
                            {
                                token = CreateToken(temp),
                                userName = temp.userName,
                                role = temp.role,
                                status = temp.status
                            };
                        }
                        else
                        {
                            response.message = "Password is not correct";
                            response.success = false;
                        }
                    }
                    else
                    {
                        response.message = "Access denied, dont have role";
                        response.success = false;
                    }
                }
                else
                {
                    response.message = "Account is not available";
                    response.success = false;
                }
            }
            else
            {
                response.message = "";
                response.success = false;
            }
            return response;
        }

        //Get all
        public ResponseService<List<AccountDTO>> GetAll()
        {
            var response = new ResponseService<List<AccountDTO>>();
            try
            {
                response.result = _mapper.Map<List<Account>, List<AccountDTO>>(_repository._account.GetAll());
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        //Update
        public ResponseService<AccountDTO> Update(Account account)
        {
            var response = new ResponseService<AccountDTO>();
            try
            {
                _repository._account.Update(account);
                SaveChanges();
                response = GetByUserName(account.userName);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        //Get by username
        public ResponseService<AccountDTO> GetByUserName(string name)
        {
            var response = new ResponseService<AccountDTO>();
            try
            {
                response.result = _mapper.Map<Account, AccountDTO>(_repository._account.GetSingleByCondition(p => p.userName == name));
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        //Chang Password
        public ResponseService<AccountDTO> ChangePassword(AccountChangePasswordDTO account)
        {
            var response = new ResponseService<AccountDTO>();
            var temp = _repository._account.GetSingleByCondition(p => p.userName == account.userName);
            if (temp != null)
            {
                if (BCrypt.Net.BCrypt.Verify(account.newPassword, temp.password) == true)
                {
                    try
                    {
                        temp.password = BCrypt.Net.BCrypt.HashPassword(account.newPassword);
                        _repository._account.Update(temp);
                        SaveChanges();
                        response = GetByUserName(account.userName);
                    }
                    catch (Exception ex)
                    {
                        response.message = ex.Message;
                        response.success = false;
                    }
                }
                else
                {
                    response.message = "Old Password is not correct";
                    response.success = false;
                }
            }
            else
            {
                response.message = "Username is not found";
                response.success = false;
            }
            return response;
        }
    }
}
