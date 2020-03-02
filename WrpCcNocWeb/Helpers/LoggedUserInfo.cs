using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models.UserManagement;

namespace WrpCcNocWeb.Helpers
{
    public class LoggedUserInfo
    {
        private readonly HttpContext context;
        private readonly IHttpContextAccessor _accessor;

        public LoggedUserInfo()
        {

        }

        public LoggedUserInfo(IHttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
        }

        //public LoggedUserInfo(HttpContext _context)
        //{
        //    context = _context;
        //}

        private UserInfo _loggedUserInfo;

        private string _UserID;

        public UserInfo UserInfo
        {
            get
            {
                return _loggedUserInfo;
            }
            set
            {
                _loggedUserInfo = context.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            }
        }

        public string UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = _accessor.HttpContext.Request.Query["UserID"].ToString(); // _accessor.HttpContext.Session.GetString("UserID");
            }
        }
    }
}
