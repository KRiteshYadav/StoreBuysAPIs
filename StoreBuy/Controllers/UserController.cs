using StoreBuy.Domain;
using StoreBuy.Repositories;
using StoreBuy.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using StoreBuy.Utilities;



namespace StoreBuy.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        
        IUserRepository UserRepository = null;
        IOTPRepository OTPRepository = null;
        public UserController(IUserRepository UserRespository,
            IOTPRepository OTPRepository )
        {
            this.OTPRepository = OTPRepository;
            this.UserRepository = UserRespository;
        }

        [HttpGet]
        [Route("GetAllUserDetails")]
        public IEnumerable<Users> GetAllUserDetails()
        {
            try
            {
                return UserRepository.GetAll();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }

        [HttpGet]
        [Route("GetUserById")]
        public Users GetUserById(long UserId)
        {
            try
            {
                return UserRepository.GetById(UserId);
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }


        [HttpGet]
        [Route("UserLogin")]
        public IHttpActionResult UserLogin(long UserId, string UserPassword)
        {
            try
            {
                Users userDetails = UserRepository.GetById(UserId);
                if (userDetails != null)
                {
                    UserPassword = GetHashCode(UserPassword);
                    if (UserPassword.Equals(userDetails.UserPassword))
                    {
                        return Ok(UserId);
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [Route("ChangePassword")]
        public IHttpActionResult ChangePassword(long UserId, string OldPassword, string NewPassword)
        {
            try
            {
               
                if (Utility.ValidatePassword(NewPassword))
                {
                    Users user = UserRepository.GetById(UserId);
                    if (user != null)
                    {
                        OldPassword = GetHashCode(OldPassword);
                        if (OldPassword.Equals(user.UserPassword))
                        {
                            NewPassword = GetHashCode(NewPassword);
                            user.UserPassword = NewPassword;
                        }
                        else
                            return BadRequest();
                        UserRepository.InsertOrUpdate(user);
                        return Ok(UserId);
                    }
                }
                return BadRequest();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }



        [HttpPost]
        [Route("UserRegister")]
        public IHttpActionResult UserRegister([FromBody]Users user)
        {
            try
            {
                var password = user.UserPassword;
                var email = user.Email;
                
                if (Utility.ValidatePassword(password) == true && Utility.ValidateEmail(email) == true)
                {
                    user.UserPassword = GetHashCode(password);
                    user.Phone = GetHashCode(user.Phone);
                    UserRepository.InsertOrUpdate(user);
                    return Ok(user);
                }
                else return BadRequest();
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        
        string GetHashCode(string TextString)
        {
            var Provider = new SHA1CryptoServiceProvider();
            var Encoding = new UnicodeEncoding();
            var ResultaArray = Provider.ComputeHash(Encoding.GetBytes(TextString));
            string HashCode = Convert.ToBase64String(ResultaArray, 0, ResultaArray.Length);
            return HashCode;
        }

        [HttpPut]
        [Route("UpdateUserDetails")]
        public IHttpActionResult UpdateUserDetails([FromBody]Users user)
        {
            try
            {
         
                user.UserPassword = GetHashCode(user.UserPassword);
                user.Phone = GetHashCode(user.Phone);
                UserRepository.InsertOrUpdate(user);
                return Ok(user);
            }
            catch(Exception exe)
            {
                throw exe;
            }
        }

        [HttpGet]
        [Route("ForgotPassword")]
        public HttpResponseMessage ForgotPassword(long UserId)
        {
            Users UserDetails = UserRepository.GetById(UserId);            
            var OTP = Utility.GetRandomNumber();
            OTPValidator OTPToInsert = new OTPValidator();
            OTPToInsert.UserId = UserId;
            OTPToInsert.CurrentOtp = OTP;
            OTPToInsert.DateTime = DateTime.Now;
            OTPRepository.InsertOrUpdate(OTPToInsert);
            string body = Resources.OTPBody + OTP;            
            var IsEmailSent= Utility.SendEmail(UserDetails.Email,body,Resources.OrderSubject);
            if (IsEmailSent)
            {
                return Request.CreateResponse(HttpStatusCode.Found, "Successfully sent");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "User with UserId:" + UserId + " not found");
            }
        }
        [HttpGet]
        [Route("VerifyOTP")]
        public HttpResponseMessage VerifyOTP(long UserId,long OTPReceived)
        {
            DeleteExpiredOTPs();
            var OTP = OTPRepository.GetById(UserId);
            if(OTP==null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "OTP Expired");
            }
            if(OTPReceived==OTP.CurrentOtp)
            {
                return Request.CreateResponse(HttpStatusCode.Found);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Found,"Entered OTP is not valid");
            }

        }

        void DeleteExpiredOTPs()
        {
            var AllOTPs = OTPRepository.GetAll();
            foreach (OTPValidator OTP in AllOTPs)
            {
                DateTime strDate = OTP.DateTime;
                var span = DateTime.Now - strDate;
                if (span.Minutes >=Int32.Parse( Resources.OTPTimeSpan))
                {
                    OTPRepository.Delete(OTP.UserId);
                }
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public IHttpActionResult ResetPassword(long UserId,string NewPassword)
        {
            try
            {               
                
                if (Utility.ValidatePassword(NewPassword))
                {
                    NewPassword = GetHashCode(NewPassword);
                    Users User = UserRepository.GetById(UserId);
                    if (User != null)
                    {
                        User.UserPassword = NewPassword;
                        UserRepository.InsertOrUpdate(User);
                        return Ok(UserId);
                    }
                }
                return BadRequest();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

       
       
    }
}
