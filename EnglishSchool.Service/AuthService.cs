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
        ResponseService<ParentLoginResponseDTO> ParentLogin(ParentLoginDTO account);
        ResponseService<StudentLoginReponseDTO> StudentLogin(StudentLoginDTO account);
        ResponseService<string> ParentChangePassword(ChangePasswordDTO account);
        ResponseService<string> StudentChangePassword(ChangePasswordDTO account);
        ResponseService<string> Login(LoginDTO account);
        ResponseService<EmployeeLoginDTO> AdminLogin(LoginDTO account);
        ResponseService<Employee> AdminRegister(ParentLoginDTO account);
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

        //create token
        public string CreateToken(string userName)
        {
            string secretKey = "my_secret_key_12345";
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
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
        public ResponseService<ParentLoginResponseDTO> ParentLogin(ParentLoginDTO account)
        {
            var response = new ResponseService<ParentLoginResponseDTO>();
            var temp = _repository._parent.GetSingleByCondition(p => p.parentId == account.parentId);
            if (temp != null)
            {
                if (temp.status == true)
                {
                    if (BCrypt.Net.BCrypt.Verify(account.password, temp.password))
                    {
                        response.result = _mapper.Map<Parent, ParentLoginResponseDTO>(temp);
                    }
                    else
                    {
                        response.message = "Password is not correct";
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


        //Chang Password
        public ResponseService<string> ParentChangePassword(ChangePasswordDTO account)
        {
            var response = new ResponseService<string>();
            var temp = _repository._parent.GetSingleByCondition(p => p.parentId == account.userName);
            if (temp != null)
            {
                if (BCrypt.Net.BCrypt.Verify(account.newPassword, temp.password) == true)
                {
                    try
                    {
                        temp.password = BCrypt.Net.BCrypt.HashPassword(account.newPassword);
                        _repository._parent.Update(temp);
                        SaveChanges();
                        response.result = "Action successfully";
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


        public ResponseService<string> StudentChangePassword(ChangePasswordDTO account)
        {
            var response = new ResponseService<string>();
            var temp = _repository._student.GetSingleByCondition(p => p.studentId == account.userName);
            if (temp != null)
            {
                if (BCrypt.Net.BCrypt.Verify(account.newPassword, temp.password) == true)
                {
                    try
                    {
                        temp.password = BCrypt.Net.BCrypt.HashPassword(account.newPassword);
                        _repository._student.Update(temp);
                        SaveChanges();
                        response.result = "Action successfully";
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
        public ResponseService<StudentLoginReponseDTO> StudentLogin(StudentLoginDTO student)
        {
            var response = new ResponseService<StudentLoginReponseDTO>();
            var temp = _repository._student.GetSingleByCondition(p => p.studentId == student.studentId);
            if (temp != null)
            {
                if (temp.status == true)
                {
                    if (temp.deactivationDate > DateTime.Now)
                    {
                        if (BCrypt.Net.BCrypt.Verify(student.password, temp.password))
                        {
                            response.result = _mapper.Map<Student, StudentLoginReponseDTO>(temp);
                            response.result.token = CreateToken(temp.studentId);
                        }
                        else
                        {
                            response.message = "Password is not correct";
                            response.success = false;
                        }
                    }
                    else
                    {
                        try
                        {
                            temp.status = false;
                            _repository._student.Update(temp);
                            SaveChanges();
                            response.message = "Account is disable";
                            response.success = false;
                        }
                        catch (Exception ex)
                        {
                            response.message = ex.Message;
                            response.success = false;
                        }
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
                response.message = "Account is not found";
                response.success = false;
            }
            return response;
        }

        public ResponseService<string> Login(LoginDTO account)
        {
            var response = new ResponseService<string>();
            if (_repository._student.GetSingleByCondition(p => p.studentId == account.userID) != null)
            {
                response.result = "Student";
            }
            else if(_repository._parent.GetSingleByCondition(p=>p.parentId==account.userID)!=null)
            {
                response.result = "Parent";
            }
            else
            {
                response.message = "Account is not found";
            }
            return response;
        }


        public ResponseService<EmployeeLoginDTO> AdminLogin(LoginDTO account)
        {
            var response = new ResponseService<EmployeeLoginDTO>();
            var temp = _repository._employee.GetSingleByCondition(p => p.userId == account.userID);
            if (temp != null)
            {
                if(temp.role == "Admin")
                {
                    if (temp.status == true)
                    {
                        if (BCrypt.Net.BCrypt.Verify(account.password, temp.password))
                        {
                            response.result = _mapper.Map<Employee, EmployeeLoginDTO>(temp);
                            response.result.token = CreateToken(account.userID);
                        }
                        else
                        {
                            response.message = "Password is not correct";
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
                    response.message = "Account does not have access";
                    response.success = false;
                }
            }
            else
            {
                response.message = "Account is not found";
                response.success = false;
            }
            return response;
        }

        public ResponseService<Employee> AdminRegister(ParentLoginDTO account)
        {
            throw new NotImplementedException();
        }
    }
}
