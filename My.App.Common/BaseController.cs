using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using My.App.Entity.Client;
using System.Web;
using System.Configuration;
using My.App.Config;

namespace My.App.Common
{
    [@Authorize(Enum = AuthorizeEnum.Login)]
    public class BaseController : Controller {
        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <returns></returns>
        public static tbClientUser GetUser() {
            tbClientUser UserInfo = new tbClientUser();
            
            if (System.Web.HttpContext.Current.Session["ClientUser"] != null) {
                UserInfo = System.Web.HttpContext.Current.Session["ClientUser"] as tbClientUser;
            } else {
                UserInfo = new tbClientUser()
                {
                    UserId = 24,
                    NickName = "Andrinuo旗舰店",
                    SessionKey = "41d6428fd300dfe0a717753cf31175a0",
                    PfId = 3,
                    Sex = 1,
                    Birthday = "",
                    Credit = "",
                    Avatar = "",
                    Status = 1,
                    OutId = 12448,
                    UserPsw = ""
                };
               // System.Web.HttpContext.Current.Response.Write("<script>window.location.href='" + Global.Url + "';</script>");
               // System.Web.HttpContext.Current.Response.End();
            }

            return UserInfo;
        }

        /// <summary>
        /// 获取会员编号
        /// </summary>
        /// <returns></returns>
        public static int GetUserId() {
            return GetUser().UserId;
        }
    }

    /// <summary>
    /// 用户授权类
    /// </summary>
    public class Authorize : AuthorizeAttribute {
        /// <summary>
        /// 授权枚举
        /// </summary>
        public AuthorizeEnum Enum { get; set; }

        /// <summary>
        /// 跳转url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 用户授权类无参构造函数
        /// </summary>
        public Authorize() {
            //默认需要登录授权
            this.Enum = AuthorizeEnum.Login;
            //默认跳转地址
            this.Url = Global.Url;
        }

        public override void OnAuthorization(AuthorizationContext filterContext) {
            switch (Enum) {
                case AuthorizeEnum.Login:
                case AuthorizeEnum.Certificate:
                    if (filterContext.HttpContext.Session["ClientUser"] == null) {
                       // System.Web.HttpContext.Current.Response.Write("<script>window.location.href='" + Url + "';</script>");
                        //System.Web.HttpContext.Current.Response.End();
                    }
                break;
            }
        }
    }

    /// <summary>
    /// 授权枚举
    /// </summary>
    public enum AuthorizeEnum {
        /// <summary>
        /// 登录
        /// </summary>
        Login,
        /// <summary>
        /// 认证
        /// </summary>
        Certificate
    }
}
