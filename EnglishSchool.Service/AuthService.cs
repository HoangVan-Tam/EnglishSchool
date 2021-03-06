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

namespace EnglishSchool.Service
{
    public interface IAuthService
    {
        ResponseService<StudentLoginReponseDTO> StudentLogin(StudentLoginDTO account);
        ResponseService<string> StudentChangePassword(ChangePasswordDTO account);
        ResponseService<EmployeeLoginDTO> EmployeeLogin(LoginDTO account);
        ResponseService<string> EmployeeChangePassword(ChangePasswordDTO account);
        ResponseService<string> Login(LoginDTO account);
        ResponseService<EmployeeLoginDTO> AdminLogin(LoginDTO account);
        ResponseService<FullInfoStudentDTO> StudentInfo(string userId);
        ResponseService<EmployeeDTO> EmployeeInfo(string userId);
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




        public ResponseService<string> EmployeeChangePassword(ChangePasswordDTO account)
        {
            var response = new ResponseService<string>();
            var temp = _repository._employee.GetSingleByCondition(p => p.userId == account.userName);
            if (temp != null)
            {
                if (BCrypt.Net.BCrypt.Verify(account.oldPassword, temp.password) == true)
                {
                    try
                    {
                        temp.password = BCrypt.Net.BCrypt.HashPassword(account.newPassword);
                        _repository._employee.Update(temp);
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
        //Chang Password


        public ResponseService<string> StudentChangePassword(ChangePasswordDTO account)
        {
            var response = new ResponseService<string>();
            var temp = _repository._student.GetSingleByCondition(p => p.studentId == account.userName);
            if (temp != null)
            {
                if (BCrypt.Net.BCrypt.Verify(account.oldPassword, temp.password) == true)
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
                if (temp.role == "Admin")
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

        public ResponseService<FullInfoStudentDTO> StudentInfo(string userId)
        {
            var response = new ResponseService<FullInfoStudentDTO>();
            try
            {
                var result = _repository._student.GetAllInfoById(userId);
                response.result = _mapper.Map<Student, FullInfoStudentDTO>(result);
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;

        }

        public ResponseService<EmployeeDTO> EmployeeInfo(string userId)
        {
            var response = new ResponseService<EmployeeDTO>();
            try
            {
                var result = _repository._employee.GetSingleByCondition(p => p.userId == userId);
                var temp = _repository._class.GetAllInfoCoursForTeacher(userId).Select(p=>p.courses.salary).ToList();  
                response.result = _mapper.Map<Employee, EmployeeDTO>(result);
                foreach(var item in temp)
                {
                    response.result.salary = response.result.salary + item;
                }
                response.result.totalCourse = temp.Count();
                response.result.departmentName = _repository._department.GetSingleByCondition(p => p.id == response.result.departmentId).name;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public ResponseService<EmployeeLoginDTO> EmployeeLogin(LoginDTO account)
        {
            var response = new ResponseService<EmployeeLoginDTO>();
            var temp = _repository._employee.GetSingleByCondition(p => p.userId == account.userID);
            if (temp != null)
            {
                if (temp.role == "Teacher")
                {
                    if (temp.status == true)
                    {
                        if (BCrypt.Net.BCrypt.Verify(account.password, temp.password))
                        {
                            response.result = _mapper.Map<Employee, EmployeeLoginDTO>(temp);
                            /*var temp1 = _repository._class.GetMulti(p => p.teacherId == temp.userId).Select(p => p.salary).ToList();
                            foreach (var item in temp1)
                            {
                                response.result.salary = response.result.salary + item;
                            }
                            response.result.totalCourse = temp1.Count();*/
                            response.result.departmentName = _repository._department.GetSingleByCondition(p => p.id == response.result.departmentId).name;
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
    }
}
