using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Entity.Client;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;
using My.App.Entity.Shop;
using Top.Api.Domain;
using My.App.Entity.Order;
using My.App.Business.Service.Entity;
using My.App.Entity.Delivery;
using System.Text.RegularExpressions;

namespace My.App.Business.Service.TaoBao
{
    internal class Api : IApi
    {
        private string Url;

        private string AppKey;

        private string AppSecret;

        /// <summary>
        /// 错误重试次数
        /// </summary>
        private const int ErrorIgnore = 3;

        /// <summary>
        /// 获取字段列表
        /// </summary>
        private string[] ParmList = new string[] { 
            "tid",                                          //淘宝订单编号
            "buyer_nick",                                   //买家昵称
            "shipping_type",                                //配送方式
            "post_fee",                                     //运费
            "total_fee",                                    //商品总金额
            "pay_time",                                     //付款时间
            "payment",                                      //订单总金额
            "created",                                      //下单时间sa
            "commission_fee",                               //交易佣金
            "receiver_name",                                //收货人姓名
            "receiver_state",                               //收货人所在省
            "receiver_city",                                //收货人所在市
            "receiver_district",                            //收货人所在区
            "receiver_address",                             //收货人街道地址
            "receiver_zip",                                 //收货人邮政编码
            "receiver_mobile",                              //收货人手机
            "receiver_phone",                               //收货人电话
            "orders.title",                                 //商品名称
            "orders.pic_path",                              //商品图片
            "orders.price",                                 //商品价格
            "orders.num",                                   //商品数量
            "orders.item_meal_name",                        //商品套餐值
            "orders.sku_properties_name",                   //商品SKU值。颜色，尺寸
            "orders.total_fee",                             //商品总价
            "orders.refund_id",                             //退款编号
            "orders.refund_status",                         //退款状态
            "total_results",                                //订单总条数
            "orders.outer_sku_id",                          //sku商家编码
            "orders.outer_iid",                             //商家编码
            "orders.sku_id",                                //商品的最小库存单位
            "orders.status",                                //订单状态
            "orders.oid",                                   //子订单编号
            "orders.num_iid"                                //商品数字ID
        };

        /// <summary>
        /// 配送方式
        /// </summary>
        private IList<tbLogistics> Logistics = null;

        /// <summary>
        /// 店铺配送方式
        /// </summary>
        private IList<tbShopLogistics> ShopLogistics = null;

        /// <summary>
        /// 订单获取配置信息
        /// </summary>
        private tbOrdersConfig Config = null;

        internal Api() { }

        internal Api(string Url, string AppKey, string AppSecret) {
            this.Url = Url;
            this.AppKey = AppKey;
            this.AppSecret = AppSecret;
        }

        /// <summary>
        /// 获取第三方平台用户授权信息
        /// </summary>
        /// <param name="Code">授权Code</param>
        /// <param name="url">回调Url</param>
        /// <returns></returns>
        public string GetSessionKey(string Code = "", string url = "") { return ""; }

        /// <summary>
        /// 获取淘宝用户信息
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <returns></returns>
        public tbClientUser Select(string SessionKey) {
            tbClientUser User = null;
            try {
                ITopClient client = new DefaultTopClient(Url, AppKey, AppSecret);
                UserSellerGetRequest req = new UserSellerGetRequest();
                req.Fields = "user_id,nick,sex,seller_credit,avatar,status,birthday";
                UserSellerGetResponse response = client.Execute(req, SessionKey);

                if (!response.IsError) {
                    User = new tbClientUser() {
                        Avatar = String.IsNullOrEmpty(response.User.Avatar) ? "" : response.User.Avatar.Trim(),
                        NickName = response.User.Nick,
                        Credit = response.User.SellerCredit.Level.ToString(),
                        PfId = 1,
                        OutId = Convert.ToInt32(response.User.UserId),
                        Sex = String.IsNullOrEmpty(response.User.Sex) ? 1 : response.User.Sex.Trim().ToLower() == "m" ? 1 : 2,
                        Status = response.User.Status.Trim().ToLower() == "normal" ? 1 : response.User.Status.Trim().ToLower() == "inactive" ? 2 : response.User.Status.Trim().ToLower() == "delete" ? 3 : response.User.Status.Trim().ToLower() == "reeze"?4:5,
                        UserPsw = Tools.Encrypt.Encrypting.Encode(response.User.Nick.Trim(), Tools.Encrypt.EncryptMode.Cipher),
                        Birthday = String.IsNullOrEmpty(response.User.Birthday) ? "" : response.User.Birthday.Trim()
                    };
                }
            } catch { }

            return User;
        }

        /// <summary>
        /// 获取淘宝店铺信息
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="SellerNick">卖家昵称</param>
        /// <returns></returns>
        public tbShopInfo SelectShop(string SessionKey, string SellerNick) {
            tbShopInfo Shop = null;
            try {
                ITopClient client = new DefaultTopClient(Url, AppKey, AppSecret);
                ShopGetRequest req = new ShopGetRequest();
                req.Fields = "sid,cid,title,nick,desc,bulletin,pic_path,created";
                req.Nick = SellerNick;
                ShopGetResponse response = client.Execute(req, SessionKey);
                
                if (!response.IsError) {
                    Shop = new tbShopInfo() { 
                        PfId = 1,
                        VenderId = "",
                        Bulletin = String.IsNullOrEmpty(response.Shop.Bulletin) ? "" : response.Shop.Bulletin.Trim(),
                        CategoryId = Convert.ToInt32(response.Shop.Cid),
                        CategoryName = "",
                        CreateTime = String.IsNullOrEmpty(response.Shop.Created) ? "" : response.Shop.Created.Trim(),
                        InputDate = DateTime.Now,
                        LogoUrl = String.IsNullOrEmpty(response.Shop.PicPath) ? "" : "http://logo.taobao.com/shop-logo" + response.Shop.PicPath.Trim(),
                        NickName = String.IsNullOrEmpty(response.Shop.Nick) ? "" : response.Shop.Nick.Trim(),
                        OutShopId = Convert.ToInt32(response.Shop.Sid),
                        ShopDesc = String.IsNullOrEmpty(response.Shop.Desc) ? "" : response.Shop.Desc.Trim(),
                        ShopName = String.IsNullOrEmpty(response.Shop.Title) ? "" : response.Shop.Title.Trim()
                    };
                }
            } catch { }

            return Shop;
        }

        /// <summary>
        /// 获取淘宝寄件人信息
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <returns></returns>
        public tbSenderInfo SelectSender(string SessionKey) {
            tbSenderInfo Sender = null;

            try {
                ITopClient client = new DefaultTopClient(Url, AppKey, AppSecret);
                LogisticsAddressSearchRequest req = new LogisticsAddressSearchRequest();
                req.Rdef = "send_def";
                LogisticsAddressSearchResponse response = client.Execute(req, SessionKey);
                if (!response.IsError) {
                    if (response.Addresses != null && response.Addresses.Count > 0) {
                        var item = response.Addresses[0];
                        if (item != null) {
                            Sender = new tbSenderInfo() {
                                CompanyName = String.IsNullOrEmpty(item.SellerCompany) ? "" : item.SellerCompany.Trim(),
                                SenderName = String.IsNullOrEmpty(item.ContactName) ? "" : item.ContactName.Trim(),
                                SenderMobile = String.IsNullOrEmpty(item.MobilePhone) ? "" : item.MobilePhone.Trim(),
                                SenderTel = String.IsNullOrEmpty(item.Phone) ? "" : item.Phone.Trim(),
                                Province = String.IsNullOrEmpty(item.Province) ? "" : item.Province.Trim(),
                                City = String.IsNullOrEmpty(item.City) ? "" : item.City.Trim(),
                                District = String.IsNullOrEmpty(item.Country) ? "" : item.Country.Trim(),
                                SenderAdd = String.IsNullOrEmpty(item.Addr) ? "" : item.Addr.Trim(),
                                PostCode = String.IsNullOrEmpty(item.ZipCode) ? "" : item.ZipCode.Trim()
                            };
                        }
                    }
                }
            } catch { }

            return Sender;
        }

        /// <summary>
        /// 同步订单
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="OrderConfig">配置信息</param>
        /// <param name="LogisList">物流信息</param>
        /// <param name="ShopId">店铺编号</param>
        /// <returns></returns>
        public bool Request(string SessionKey, tbOrdersConfig OrderConfig, IList<tbShopLogistics> LogisList, int ShopId) {
            bool IsOk = false;
            try {
                Logistics = null;
                ShopLogistics = LogisList;
                Config = OrderConfig;

                List<string> ExportUserNick = GetTaoBaoUser(SessionKey);
                if (ExportUserNick.Count > 0) {
                    List<tbOrdersInfo> OrderList = GetTaoBaoOrder(ExportUserNick, SessionKey, ShopId);
                    IsOk = OrderList.Count > 0;
                }
            } catch { }

            return IsOk;
        }

        /// <summary>
        /// 同步订单
        /// </summary>
        /// <param name="OrderCodeList">订单编码列表</param>
        /// <param name="StartDate">下单日期</param>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="OrderConfig">配置信息</param>
        /// <param name="LogisList">物流信息</param>
        /// <param name="ShopId">店铺编号</param>
        /// <returns></returns>
        public bool Request(string OrderCodeList, string SessionKey, tbOrdersConfig OrderConfig, IList<tbShopLogistics> LogisList, int ShopId) {
            return false;
        }

        /// <summary>
        /// 订单发货
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="LogisticsId">物流编号</param>
        /// <param name="OutNumber">淘宝订单号</param>
        /// <param name="DeliveryNumber">发货单号</param>
        /// <param name="Sender">寄件人信息</param>
        /// <param name="Consignee">收件人信息</param>
        /// <returns></returns>
        public IDictionary<string, IDictionary<bool, string>> Ship(string SessionKey, int LogisticsId, string OutNumber, string DeliveryNumber, tbSenderInfo Sender = null, tbConsigneeInfo Consignee = null) {
            IDictionary<string, IDictionary<bool, string>> dic = new Dictionary<string, IDictionary<bool, string>>();
            tbLogistics Logistics = Business.Delivery.Logistics.Select(LogisticsId);

            if (String.IsNullOrEmpty(OutNumber)) {
                dic.Add(OutNumber, new Dictionary<bool, string>() { { true, "" } });
            } else{
                string[] strs = OutNumber.Split(',');
                foreach (string str in strs) {
                    StatusTable st = GetOrderStatus(SessionKey, str);
                    if (st.IsError) {
                        dic.Add(str, new Dictionary<bool, string>() { { false, st.Msg } });
                        continue;
                    }

                    //淘宝是否已经发货
                    if (st.IsSuccess) {
                        StatusTable st1 = SelectDelivery(SessionKey, str);
                        dic.Add(str, new Dictionary<bool, string>() { { st1.IsSuccess, st1.Msg } });
                        continue;
                    } else {
                        StatusTable st2 = ToDelivery(SessionKey, str, Logistics.LogisticsCode, DeliveryNumber);
                        dic.Add(str, new Dictionary<bool, string>() { { st2.IsSuccess, st2.Msg } });
                        continue;
                    }
                }
            }
            return dic;
        }

        /// <summary>
        /// 获取物流公司信息
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <returns></returns>
        public List<DeliveryCompany> GetDeliveryCompany(string SessionKey) { return new List<DeliveryCompany>(); }

        public List<string> GetWayBillCode(string SessionKey, string Num, string Code) { return new List<string>(); }

        /// <summary>
        /// 获取货到付款代收金额
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="OrderNumber">订单编号</param>
        /// <returns></returns>
        public decimal GetCashPrice(string SessionKey, string OrderNumber) { return 0m; }

        /// <summary>
        /// 查询支持起始地到目的地范围的物流公司
        /// </summary>
        /// <param name="service_type">服务类型 可选值：cod(货到付款)、online(在线下单)、 offline(自己联系)、limit(限时物流)</param>
        /// <param name="source_id">物流公司揽货地地区码（必须是区、县一级的）可为空</param>
        /// <param name="target_id">物流公司派送地地区码（必须是区、县一级的）可为空</param>
        /// <returns></returns>
        public List<DeliveryCompany> GetLogistics(string service_type, string source_id, string target_id)
        {
            List<DeliveryCompany> List = new List<DeliveryCompany>();  
            try {
                if (Comm.AreaList == null) {
                    Comm.AreaList = GetAreas();
                }

                string SourceId = "";
                string TargetId = "";
                if (Comm.AreaList != null && Comm.AreaList.Count > 0) {
                    if (!String.IsNullOrEmpty(source_id)) {
                        Area area = Comm.AreaList.FirstOrDefault((e) => { return e.Name.Trim().Equals(source_id.Trim()); });
                        if (area != null) { SourceId = area.Id.ToString(); } else { return List; }
                    }

                    if (!String.IsNullOrEmpty(target_id)) {
                        Area area = Comm.AreaList.FirstOrDefault((e) => { return e.Name.Trim().Equals(target_id.Trim()); });
                        if (area != null) { TargetId = area.Id.ToString(); } else { return List; }
                    }
                } else {
                    return List;
                }

                ITopClient client = new DefaultTopClient(Url, AppKey, AppSecret);
                LogisticsPartnersGetRequest req = new LogisticsPartnersGetRequest();
                req.ServiceType = service_type;
                req.SourceId = SourceId;
                req.TargetId = TargetId;
                req.IsNeedCarriage = false;
                LogisticsPartnersGetResponse response = client.Execute(req);

                if (!response.IsError) {
                    foreach (var item in response.LogisticsPartners) {
                        List.Add(new DeliveryCompany() { 
                            Id=item.Partner.CompanyId.ToString(),
                            Code = item.Partner.CompanyCode,
                            Name = item.Partner.CompanyName,
                            Remark = item.UncoverRemark
                        });
                    }
                }
            } catch { }

            return List;
        }

        /// <summary>
        /// 同步商品
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="ShopId">店铺编号</param>
        /// <returns></returns>
        public bool SyncProduct(string SessionKey, int ShopId) {
            bool IsOk = false;
            IList<My.App.Entity.Product.tbProductAbb> List = new List<My.App.Entity.Product.tbProductAbb>();
            PageInfo.Reset();
            while (PageInfo._ProductPage._IsNext) {
                ITopClient client = new DefaultTopClient(Url, AppKey, AppSecret);
                ItemsOnsaleGetRequest req = new ItemsOnsaleGetRequest();
                req.Fields = "num_iid,title,pic_url,outer_id,num,price";
                req.PageNo = PageInfo._ProductPage._Page;
                req.IsTaobao = true;
                req.PageSize = PageInfo._ProductPage._PageSize;
                ItemsOnsaleGetResponse response = client.Execute(req, SessionKey);

                if (!response.IsError) {
                    foreach (Top.Api.Domain.Item item in response.Items) {
                        List.Add(new My.App.Entity.Product.tbProductAbb() {
                            ShopId = ShopId,
                            ProductName = String.IsNullOrEmpty(item.Title) ? "" : item.Title.Trim(),
                            OutId = item.NumIid.ToString(),
                            Abbreviation ="",
                            Display = true,
                            InputDate = DateTime.Now,
                            ProductImg = String.IsNullOrEmpty(item.PicUrl) ? "" : item.PicUrl.Trim(),
                            ProductUrl = "http://item.taobao.com/item.html?id=" + item.NumIid,
                            ProductNumber = Convert.ToInt32(item.Num),
                            ProductPrice = Convert.ToDecimal(Comm.DataChange(item.Price, TypeChange.DECIMAL))
                        });
                    }
                }

                PageInfo._ProductPage._TotalResults = response.TotalResults;
                PageInfo._ProductPage.ResolvePageNumber();
            }

            IsOk = Comm.InsertProduct(List);
            return IsOk;
        }

        #region 查询支持起始地到目的地范围的物流公司编码列表
        
        /// <summary>
        /// 查询支持起始地到目的地范围的物流公司编码列表
        /// </summary>
        /// <param name="service_type">服务类型 可选值：cod(货到付款)、online(在线下单)、 offline(自己联系)、limit(限时物流)</param>
        /// <param name="source_id">物流公司揽货地地区码（必须是区、县一级的）可为空</param>
        /// <param name="target_id">物流公司派送地地区码（必须是区、县一级的）可为空</param>
        /// <returns></returns>
        public List<string> GetLogisticsIdList(string service_type, string source_id, string target_id) {
            List<string> List = new List<string>();  
            try {
                if (Comm.AreaList == null) {
                    Comm.AreaList = GetAreas();
                }

                string SourceId = "";
                string TargetId = "";
                if (Comm.AreaList != null && Comm.AreaList.Count > 0) {
                    if (!String.IsNullOrEmpty(source_id)) {
                        Area area = Comm.AreaList.FirstOrDefault((e) => { return e.Name.Trim().Equals(source_id.Trim()); });
                        if (area != null) { SourceId = area.Id.ToString(); } else { return List; }
                    }

                    if (!String.IsNullOrEmpty(target_id)) {
                        Area area = Comm.AreaList.FirstOrDefault((e) => { return e.Name.Trim().Equals(target_id.Trim()); });
                        if (area != null) { TargetId = area.Id.ToString(); } else { return List; }
                    }
                } else {
                    return List;
                }

                ITopClient client = new DefaultTopClient(Url, AppKey, AppSecret);
                LogisticsPartnersGetRequest req = new LogisticsPartnersGetRequest();
                req.ServiceType = service_type;
                req.SourceId = SourceId;
                req.TargetId = TargetId;
                req.IsNeedCarriage = false;
                LogisticsPartnersGetResponse response = client.Execute(req);

                if (!response.IsError) {
                    foreach (var item in response.LogisticsPartners) {
                        List.Add(item.Partner.CompanyCode.Trim());
                    }
                }
            } catch { }

            return List;
        }

        #endregion

        #region 查询地址区域
        
        /// <summary>
        /// 查询地址区域
        /// </summary>
        /// <returns></returns>
        private List<Area> GetAreas() {
            List<Area> AreaList = new List<Area>();
            try {
                ITopClient client = new DefaultTopClient(Url,AppKey,AppSecret);
                AreasGetRequest req = new AreasGetRequest();
                req.Fields = "id,type,name";
                AreasGetResponse response = client.Execute(req);
                if (!response.IsError) {
                    AreaList = response.Areas;
                }
            } catch { }

            return AreaList;
        }

        #endregion

        #region 查询第三方订单状态

        /// <summary>
        /// 查询第三方订单状态
        /// </summary>
        /// <param name="SessionKey">SessionKey</param>
        /// <param name="OutNumber">外部订单号</param>
        /// <returns></returns>
        private StatusTable GetOrderStatus(string SessionKey, string OutNumber) {
            StatusTable st = new StatusTable() { Msg = "" };
            try {
                ITopClient client = new DefaultTopClient(Url, AppKey, AppSecret);
                TradeGetRequest req = new TradeGetRequest();
                req.Fields = "status";
                req.Tid = long.Parse(OutNumber);
                TradeGetResponse response = client.Execute(req, SessionKey);
                bool isok = false;
                if (response.Trade.Status.Equals("WAIT_BUYER_CONFIRM_GOODS") || response.Trade.Status.Equals("TRADE_FINISHED") || response.Trade.Status.Equals("TRADE_CLOSED")) {
                    isok = true;
                }
                st.IsSuccess = isok;
            } catch {
                st.IsError = true;
                st.Msg = "获取淘宝订单的状态失败";
            }

            return st;
        }

        #endregion

        #region 查询第三方平台订单物流信息
        
        /// <summary>
        /// 查询第三方平台订单物流信息
        /// </summary>
        /// <param name="SessionKey">SessionKey</param>
        /// <param name="OutNumber">外部订单号</param>
        /// <returns></returns>
        private StatusTable SelectDelivery(string SessionKey, string OutNumber) {
            StatusTable st = new StatusTable() { Msg = "" };

            try {
                ITopClient client = new DefaultTopClient(Url, AppKey, AppSecret);
                LogisticsOrdersGetRequest req = new LogisticsOrdersGetRequest();
                req.Fields = "out_sid,company_name";
                req.Tid = long.Parse(OutNumber);
                LogisticsOrdersGetResponse response = client.Execute(req, SessionKey);

                if (response.Shippings.Count > 0) {
                    st.IsSuccess = true;
                    string str = "";
                    foreach (Top.Api.Domain.Shipping ship in response.Shippings) {
                        str = ship.CompanyName + " <font color='red'>" + ship.OutSid + "</font>";
                    }
                    st.Msg = "订单已发货,发货信息[" + str + "]";
                } else {
                    st.IsSuccess = false;
                    st.Msg = "未查询到该订单的物流信息";
                }
            } catch {
                st.IsSuccess = false;
                st.IsError = true;
                st.Msg = "查询该订单物流信息失败";
            }

            return st;
        }

        #endregion

        #region 订单发货
        
        /// <summary>
        /// 订单发货
        /// </summary>
        /// <param name="SessionKey">SessionKey</param>
        /// <param name="OutNumber">订单编号</param>
        /// <param name="CompanyCode">物流编码</param>
        /// <param name="DeliveryNumber">发货单号</param>
        /// <returns></returns>
        private StatusTable ToDelivery(string SessionKey, string OutNumber, string CompanyCode, string DeliveryNumber) {
            StatusTable st = new StatusTable() { Msg = "" };

            if (!String.IsNullOrEmpty(CompanyCode)) {
                ITopClient client = new DefaultTopClient(Url, AppKey, AppSecret);
                LogisticsOfflineSendRequest req = new LogisticsOfflineSendRequest();
                req.Tid = long.Parse(OutNumber);
                req.OutSid = DeliveryNumber;
                req.CompanyCode = CompanyCode;
                LogisticsOfflineSendResponse response = client.Execute(req, SessionKey);
                if (response.Shipping == null) {
                    st.Msg = "解析淘宝发货数据失败，可能该订单状态不允许发货";
                } else { 
                    st.IsSuccess = response.Shipping.IsSuccess;
                    if (!st.IsSuccess) {
                        st.Msg = "淘宝发货失败";
                    }
                }
            } else {
                st.IsSuccess = false;
                st.Msg = "解析淘宝物流公司编码失败";
            }

            return st;
        }

        #endregion

        #region 获取淘宝会员名（已付款）

        /// <summary>
        /// 获取淘宝会员名（已付款）
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <returns>已付款的会员名列表</returns>
        private List<string> GetTaoBaoUser(string SessionKey) {
            List<string> ExportUserNick = new List<string>();

            PageInfo.Reset();
            while (PageInfo._ProductPage._IsNext) {
                int ErrorNumber = 0;
                bool IsOk = false;

                while (ErrorNumber < ErrorIgnore && !IsOk) {
                    try {
                        ITopClient client = new DefaultTopClient(Url, AppKey, AppSecret);
                        TradesSoldGetRequest req = new TradesSoldGetRequest();
                        req.Fields = "buyer_nick,tid";
                        req.Status = "WAIT_SELLER_SEND_GOODS";
                        req.PageNo = PageInfo._ProductPage._Page;
                        req.PageSize = PageInfo._ProductPage._PageSize;
                        TradesSoldGetResponse response = client.Execute(req, SessionKey);

                        foreach (Trade td in response.Trades) {
                            string BuyerNick = td.BuyerNick.Trim();
                            string Tid = td.Tid.ToString();
                            
                            if (!String.IsNullOrEmpty(BuyerNick) && !ExportUserNick.Contains(BuyerNick)) {
                                ExportUserNick.Add(BuyerNick);
                            }
                        }

                        PageInfo._ProductPage._TotalResults = response.TotalResults;

                        IsOk = true;
                    } catch { } finally { ErrorNumber++; }
                }

                PageInfo._ProductPage.ResolvePageNumber();
            }

            return ExportUserNick;
        }

        #endregion

        #region 判断用户名是否有未付款的订单

        /// <summary>
        /// 判断用户名是否有未付款的订单
        /// </summary>
        /// <param name="NickName">淘宝用户名</param>
        /// <param name="SessionKey">用户授权</param>
        /// <returns></returns>
        private bool CheckNickName(string NickName, string SessionKey) {
            int ErrorNumber = 0;
            bool IsOk = false;
            int Count = 0;

            PageInfo.Reset();

            while (ErrorNumber < ErrorIgnore && !IsOk) {
                try {
                    ITopClient client = new DefaultTopClient(Url, AppKey, AppSecret);
                    TradesSoldGetRequest req = new TradesSoldGetRequest();
                    req.Fields = "tid";
                    req.Status = "WAIT_BUYER_PAY";
                    req.PageNo = PageInfo._ProductPage._Page;
                    req.PageSize = PageInfo._ProductPage._PageSize;
                    req.BuyerNick = NickName;

                    TradesSoldGetResponse response = client.Execute(req, SessionKey);

                    Count = (int)response.TotalResults;

                    IsOk = true;
                } catch { } finally { ErrorNumber++; }
            }

            return Count > 0 ? false : true;
        }

        #endregion

        #region 获取淘宝订单（已付款）

        /// <summary>
        /// 获取淘宝订单（已付款）
        /// </summary>
        /// <param name="ExportUserNick">已付款的淘宝会员名列表</param>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="ShopId">店铺编号</param>
        /// <returns></returns>
        private List<tbOrdersInfo> GetTaoBaoOrder(List<string> ExportUserNick, string SessionKey, int ShopId) {
            List<tbOrdersInfo> OrdersList = new List<tbOrdersInfo>();
            foreach (string NickName in ExportUserNick) {
                //判断是否有未付款的订单
                if (Config.ConfigId > 0){
                    if (Config.JudgePay) {
                        if (!CheckNickName(NickName, SessionKey)) {
                            continue;
                        }
                    }
                }

                List<Trade> TradeList = null;

                int ErrorNumber = 0;
                bool IsOk = false;

                PageInfo.Reset();

                while (ErrorNumber < ErrorIgnore && !IsOk) {
                    try {
                        ITopClient client = new DefaultTopClient(Url, AppKey, AppSecret);
                        TradesSoldGetRequest req = new TradesSoldGetRequest();
                        req.Fields = String.Join(",", ParmList);
                        req.BuyerNick = NickName;
                        req.Status = "WAIT_SELLER_SEND_GOODS";
                        req.PageSize = PageInfo._ProductPage._PageSize;
                        TradesSoldGetResponse response = client.Execute(req, SessionKey);

                        TradeList = response.Trades;

                        IsOk = true;
                    } catch { } finally { ErrorNumber++; }
                }

                List<tbOrdersInfo> TempList = ResolveData(TradeList, SessionKey, ShopId);

                if (TempList != null && TempList.Count > 0) {
                    OrdersList.AddRange(TempList);
                    InsertOrders(TempList, SessionKey);
                }
            }

            return OrdersList;
        }

        #endregion

        #region 解析淘宝订单列表数据

        /// <summary>
        /// 解析淘宝订单列表数据
        /// </summary>
        /// <param name="trades">订单列表（同一用户名）</param>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="ShopId">店铺编号</param>
        /// <returns></returns>
        private List<tbOrdersInfo> ResolveData(List<Trade> trades, string SessionKey, int ShopId) {
            if (!(trades != null && trades.Count > 0)) { return null; }

            List<tbOrdersInfo> OrderList = new List<tbOrdersInfo>();

            foreach (Trade td in trades) {
                if (Business.Order.OrdersInfo.CheckOrder(td.Tid.ToString().Trim(), ShopId)) {
                    continue;
                }

                tbOrdersInfo Order = new tbOrdersInfo();
                tbConsigneeInfo Consignee = new tbConsigneeInfo();
                tbBuyerInfo Buyer = new tbBuyerInfo();
                List<tbOrdersDetail> Details = new List<tbOrdersDetail>();

                if (!String.IsNullOrEmpty(td.PayTime)) {
                    Order.PayDate = Convert.ToDateTime(Comm.DataChange(td.PayTime, TypeChange.DATETIME));//付款时间
                } else {
                    Order.PayDate = DateTime.Now;
                }

                if (Config.ConfigId > 0) {
                    if (Config.PayTime > 0) {
                        //验证付款时间
                        TimeSpan TSpan = DateTime.Now.Subtract(Order.PayDate);
                        int Span = TSpan.Days * 1440 + TSpan.Hours * 60 + TSpan.Minutes;
                        if (Span < Config.PayTime) {
                            OrderList.Clear();
                            break;
                        }
                    }
                }

                Order.CustomerServiceId = 1;
                Order.CashOndelivery = false;
                Order.Invoice = false;
                Order.NickName = td.BuyerNick.Trim();//会员名
                Buyer.NickName = td.BuyerNick.Trim();//会员名
                Order.Commission = Convert.ToDecimal(Comm.DataChange(td.CommissionFee, TypeChange.DECIMAL));
                Order.OrdersOutNumber = td.Tid.ToString().Trim();//淘宝订单号
                Order.Logistics = new tbLogistics();
                string[] LStrs = Comm.Distribution(Logistics, ShopLogistics, td.ShippingType);//解析物流
                Order.IsFree = bool.Parse(LStrs[0]);//是否包邮
                Order.Logistics.LogisticsId = int.Parse(LStrs[1]);//配送方式
                Order.OrdersFreight = Convert.ToInt32(Comm.DataChange(td.PostFee, TypeChange.DECIMAL));//运费
                if (Order.OrdersFreight == 0) { Order.IsFree = true; }
                Order.OrdersProductTotal = Convert.ToDecimal(Comm.DataChange(td.TotalFee, TypeChange.DECIMAL));//商品总金额
                Order.OrdersDate = Convert.ToDateTime(Comm.DataChange(td.Created, TypeChange.DATETIME));//交易创建时间
                Consignee.Name = Comm.DataChange(td.ReceiverName, TypeChange.STRING).ToString();//收件人姓名
                Buyer.BuyerName = Consignee.Name;
                Consignee.Provice = Comm.DataChange(td.ReceiverState, TypeChange.STRING).ToString();//收件人所在省
                Consignee.City = Comm.DataChange(td.ReceiverCity, TypeChange.STRING).ToString();//收件人所在市
                Consignee.District = Comm.DataChange(td.ReceiverDistrict, TypeChange.STRING).ToString();//收件人所在区
                Consignee.ConsigneeAddress = Comm.DataChange(td.ReceiverAddress, TypeChange.STRING).ToString();//收件人地址
                Consignee.PostCode = Comm.DataChange(td.ReceiverZip, TypeChange.STRING).ToString();//收件人邮编
                Consignee.Mobile = Comm.DataChange(td.ReceiverMobile, TypeChange.STRING).ToString();//收件人手机
                Buyer.Mobile = Consignee.Mobile;
                Consignee.Phone = Comm.DataChange(td.ReceiverPhone, TypeChange.STRING).ToString();//收件人电话
                Buyer.Phone = Consignee.Phone;
                Order.Status = new tbOrdersStatus() { OrdersStatusId = 3 };//订单状态(默认买家已付款，等待卖家发货状态)
                decimal Payment = Convert.ToDecimal(Comm.DataChange(td.Payment, TypeChange.DECIMAL));//订单实收金额
                Order.OrdersAccounts = Payment;
                Order.OrdersPaid = Payment;
                Order.IsOrdersReFund = false;//是否有退款
                Order.OrdersDiscount = Order.OrdersPaid - Order.OrdersProductTotal - Order.OrdersFreight;//订单折扣
                Order.OrdersWeight = 0;
                Order.OrdersNotes = "";
                Order.OrdersFlag = "0";
                Order.OrdersInputDate = DateTime.Now;
                Consignee.InputDate = Order.OrdersInputDate;
                string[] DetailString = GetOrderDetail(Order.OrdersOutNumber, SessionKey);//买家留言&卖家备注&备注旗帜&邮件地址
                Order.ServiceNotes = DetailString[1];//客服备注
                Order.ServiceFlag = DetailString[2];//客服备注旗帜样式
                Order.BuyerMsg = DetailString[0];//买家留言
                Buyer.BuyerEmail = DetailString[3];
                Order.CodFee = Convert.ToDecimal(Comm.DataChange(td.CodFee, TypeChange.DECIMAL));//货到付款服务费
                Order.BuyerRemark = "";
                Order.DeliveryDate = "";
                Order.RemarkFlag = "";
                Order.Shop = new tbShopInfo() { ShopId = ShopId };
                foreach (Top.Api.Domain.Order order in td.Orders) {
                    if (Convert.ToInt32(order.Num) <= 0) { continue; }

                    tbOrdersDetail Detail = new tbOrdersDetail();
                    Detail.ProductEncoding = !String.IsNullOrEmpty(order.OuterSkuId) ? order.OuterSkuId.Trim() : "";
                    Detail.ProductName = !String.IsNullOrEmpty(order.Title) ? order.Title.Trim() : "";
                    Detail.ProductSku = !String.IsNullOrEmpty(order.SkuPropertiesName) ? order.SkuPropertiesName.Trim() : "";
                    Detail.ProductId = 0;
                    Detail.ProductProId = 0;
                    Detail.SalesCommissionId = 1;
                    Detail.ProductImg = String.IsNullOrEmpty(order.PicPath) ? "" : order.PicPath.Trim();//商品图片
                    Detail.ProductPrice = Convert.ToDecimal(Comm.DataChange(order.Price, TypeChange.DECIMAL));//商品价格
                    Detail.ProductNumber = (int)order.Num;//商品数量
                    Detail.PackageName = String.IsNullOrEmpty(order.ItemMealName) ? "" : order.ItemMealName;//商品套餐值
                    Detail.OutNumberIId = order.NumIid.ToString();//商品外部编号
                    Detail.SubOrderNumber = order.Oid.ToString();//子订单编号
                    Detail.InputDate = Order.OrdersInputDate;
                    Detail.ProductTotal = Convert.ToDecimal(Comm.DataChange(order.TotalFee, TypeChange.DECIMAL));//商品总价
                    Detail.Details = "";
                    Detail.ProductCost = 0;
                    Detail.OrdersDiscount = Convert.ToDecimal(Comm.DataChange(order.DiscountFee, TypeChange.DECIMAL));//优惠金额;
                    Detail.OrdersAdjust = Convert.ToDecimal(Comm.DataChange(order.AdjustFee, TypeChange.DECIMAL));//手动调整金额;
                    Detail.OrdersAccounts = Detail.ProductTotal;

                    //订单被取消
                    if (order.Status == "TRADE_CLOSED_BY_TAOBAO") {
                        Detail.IsCanceled = true;
                    }

                    //商品退款
                    if (order.RefundId == 0) {
                        Detail.ReFundNumber = "";
                        Detail.IsProductReFund = false;
                        Detail.ReFundStatusId = 0;
                        Detail.ReFundStatusName = ToConvert(order.RefundStatus);
                    } else {
                        Detail.ReFundNumber = order.RefundId.ToString();
                        Detail.IsProductReFund = true;
                        Order.IsOrdersReFund = true;
                        Detail.ReFundStatusId = 0;
                        Detail.ReFundStatusName = "";
                    }

                    
                    Details.Add(Detail);
                }

                Order.Consignee = Consignee;
                Order.Details = Details;
                Order.Buyer = Buyer;

                Comm.MergeOrder(Config, OrderList, Order);
            }

            return OrderList;
        }

        #endregion

        #region 根据订单编号查询该笔交易的订单详情

        /// <summary>
        /// 根据订单编号查询该笔交易的订单详情
        /// </summary>
        /// <param name="OutNumber">订单编号</param>
        /// <param name="SessionKey">用户授权</param>
        /// <returns></returns>
        private string[] GetOrderDetail(string OutNumber, string SessionKey) {
            string[] MyReturnStr = new string[4];

            int ErrorNumber = 0;
            bool IsOk = false;

            while (ErrorNumber < ErrorIgnore && !IsOk) {
                try {
                    ITopClient client = new DefaultTopClient(Url, AppKey, AppSecret);
                    TradeFullinfoGetRequest req = new TradeFullinfoGetRequest();
                    req.Fields = "buyer_message,seller_memo,seller_flag,buyer_email";
                    req.Tid = Convert.ToInt64(OutNumber);
                    TradeFullinfoGetResponse response = client.Execute(req, SessionKey);
                    Trade TempTrade = response.Trade;

                    if (!String.IsNullOrEmpty(TempTrade.BuyerMessage)) {
                        MyReturnStr[0] = TempTrade.BuyerMessage.Trim();
                    } else {
                        MyReturnStr[0] = "";
                    }

                    if (!String.IsNullOrEmpty(TempTrade.SellerMemo)) {
                        MyReturnStr[1] = TempTrade.SellerMemo.Trim();
                    } else {
                        MyReturnStr[1] = "";
                    }

                    switch (TempTrade.SellerFlag) {
                        case 0:
                            MyReturnStr[2] = "op_memo_0.png";
                            break;
                        case 1:
                            MyReturnStr[2] = "op_memo_1.png";
                            break;
                        case 2:
                            MyReturnStr[2] = "op_memo_2.png";
                            break;
                        case 3:
                            MyReturnStr[2] = "op_memo_3.png";
                            break;
                        case 4:
                            MyReturnStr[2] = "op_memo_4.png";
                            break;
                        case 5:
                            MyReturnStr[2] = "op_memo_5.png";
                            break;
                        default:
                            MyReturnStr[2] = "op_memo_0.png";
                            break;
                    }

                    if (!String.IsNullOrEmpty(TempTrade.BuyerEmail)) {
                        MyReturnStr[3] = TempTrade.BuyerEmail.ToString().Trim();
                    } else {
                        MyReturnStr[3] = "";
                    }

                    IsOk = true;
                } catch { } finally { ErrorNumber++; }
            }

            return MyReturnStr;
        }

        #endregion

        #region 将订单数据写入数据库

        /// <summary>
        /// 将订单数据写入数据库
        /// </summary>
        /// <param name="OrdersList">订单数据列表</param>
        /// <param name="SessionKey">用户授权</param>
        public void InsertOrders(List<tbOrdersInfo> OrdersList, string SessionKey) {
            try {
                foreach (tbOrdersInfo Order in OrdersList) {
                    Comm.StatusAssign(Config, Order);
                    bool IsOk = Comm.DeliveryAssign(Config, ShopLogistics, Order);
                    if (!IsOk && Config.LogisticsDis && ShopLogistics != null && ShopLogistics.Count > 0) {
                        string target = String.IsNullOrEmpty(Order.Consignee.District) ? Order.Consignee.City : Order.Consignee.District;
                        if (!String.IsNullOrEmpty(target)) {
                            List<string> CompayList = GetLogisticsIdList((Order.CashOndelivery ? "cod" : "online"), "", target.Trim());
                            Comm.RangeDelivery(ShopLogistics, Order, CompayList);
                        }
                    }
                    Comm.AnalyticalInvoice(Config, ShopLogistics, Order);
                    UpdateDeital(Order, SessionKey);
                    Comm.Insert(Order);
                }
            } catch { }
        }

        #endregion

        #region 转换退款状态
        
        /// <summary>
        /// 转换退款状态
        /// </summary>
        /// <param name="EnumName"></param>
        /// <returns></returns>
        private string ToConvert(string EnumName) {
            string StatusName = "";

            if (!String.IsNullOrEmpty(EnumName)) {
                switch (EnumName.Trim().ToUpper()) { 
                    case "WAIT_SELLER_AGREE":
                        StatusName = "买家已经申请退款，等待买家同意";
                        break;
                    case "WAIT_BUYER_RETURN_GOODS":
                        StatusName = "卖家已经同意退款，等待买家退货";
                        break;
                    case "WAIT_SELLER_CONFIRM_GOODS":
                        StatusName = "买家已经退货，等待卖家确认收货";
                        break;
                    case "SELLER_REFUSE_BUYER":
                        StatusName = "卖家拒绝退款";
                        break;
                    case "CLOSED":
                        StatusName = "退款关闭";
                        break;
                    case "SUCCESS":
                        StatusName = "退款成功";
                        break;
                    default: break;
                }
            }

            return StatusName;
        }

        #endregion

        #region 修改订单备注的旗帜

        /// <summary>
        /// 修改订单备注的旗帜
        /// </summary>
        /// <param name="Order">订单信息</param>
        /// <param name="SessionKey">用户授权</param>
        /// <returns></returns>
        private void UpdateDeital(tbOrdersInfo Order, string SessionKey) {
            string[] OutNumber = Order.OrdersOutNumber.Split(',');
            string[] Detail = Regex.Split(Order.ServiceNotes, "%Separation%");
            Order.ServiceNotes = Order.ServiceNotes.Replace("%Separation%", " ");

            if (Config.ConfigId <= 0) { return; }
            if (String.IsNullOrEmpty(Config.Remark) && Config.RemarkFlag == 0) { return; }

            try {
                for (int i = 0; i < OutNumber.Length; i++) {
                    ITopClient client = new DefaultTopClient(Url, AppKey, AppSecret);
                    TradeMemoUpdateRequest req = new TradeMemoUpdateRequest();
                    req.Tid = Convert.ToInt64(OutNumber[i]);

                    if (!String.IsNullOrEmpty(Config.Remark)) {
                        req.Memo = Detail[i] + Config.Remark;
                    }

                    if (Config.RemarkFlag > 0) {
                        req.Flag = Convert.ToInt64(Config.RemarkFlag);
                    }

                    req.Reset = false;
                    TradeMemoUpdateResponse response = client.Execute(req, SessionKey);
                }
            } catch { }
        }

        #endregion
    }
}
