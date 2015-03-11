using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Entity.Shop;
using My.App.Entity.Order;
using My.App.Entity.Delivery;
using My.App.Business.Service.Entity;
using System.Net;
using System.IO;
using My.App.Entity.Client;
using My.App.Business.Service.YHD.Client;
using My.App.Business.Service.YHD.Util;
using My.App.Business.Service.YHD.Request;
using My.App.Business.Service.YHD.Response;
using System.Text.RegularExpressions;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace My.App.Business.Service.YHD
{
    internal class Api : IApi
    {
        private string Url;

        private string AppKey;

        private string AppSecret;

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

        /// <summary>
        /// 商品SKU信息列表
        /// </summary>
        //private IDictionary<long, Entity.Product> dic = new Dictionary<long, Entity.Product>();

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
        public string GetSessionKey(string Code, string url) {
            string SessionKey = "";

            try {
                List<string> param = new List<string>() { 
                    "client_id=" + AppKey,
                    "client_secret=" + AppSecret,
                    "grant_type=authorization_code",
                    "code=" + Code,
                    "redirect_uri=" + url
                };

                string weburl = "https://member.yhd.com/login/token.do?" + String.Join("&", param);

                HttpWebRequest request = null;

                if (weburl.Contains("https")) {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = (HttpWebRequest)WebRequest.CreateDefault(new Uri(weburl));
                } else {
                    request = (HttpWebRequest)WebRequest.Create(weburl);
                }

                request.Method = "post";
                request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/xaml+xml, application/x-ms-xbap, application/x-ms-application, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET4.0C; .NET4.0E; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; InfoPath.3)";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8)) {
                    string[] Result = reader.ReadToEnd().Replace("\n", "").Replace("/r/n", "").Replace("/t", "").Replace("{", "").Replace("}", "").Replace("\"", "").Split(',');
                    foreach (string str in Result) {
                        string[] strs = str.Split(':');
                        if (strs[0].Trim().ToLower().Equals("accesstoken") && strs.Length > 1) {
                            SessionKey = strs[1].Trim();
                            break;
                        }
                    }
                }
            } catch { }

            return SessionKey;
        }

        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) {
            return true;
        }

        /// <summary>
        /// 获取一号店用户信息
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <returns></returns>
        public App.Entity.Client.tbClientUser Select(string SessionKey) { 
            tbClientUser User = null;
            try {
                IClient client = new DefaultClient(this.AppKey, this.AppSecret, SessionKey, Url);
                ShopGetRequest req = new ShopGetRequest();
                ShopGetResponse response = client.Execute(req);

                if (!response.IsError) {
                    string NickName = String.IsNullOrEmpty(response.Shop.storeNickName) ? response.Shop.storeName.Trim() : response.Shop.storeNickName.Trim();
                    User = new tbClientUser() {
                        Avatar = "",
                        NickName = NickName,
                        Credit = "",
                        PfId = 3,
                        OutId = Convert.ToInt32(response.Shop.storeId),
                        Sex = 1,
                        Status = 1,
                        UserPsw = Tools.Encrypt.Encrypting.Encode(NickName, Tools.Encrypt.EncryptMode.Cipher),
                        Birthday = ""
                    };
                }
            } catch { }

            return User;
        }

        /// <summary>
        /// 获取一号店店铺信息
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="SellerNick">卖家昵称</param>
        /// <returns></returns>
        public App.Entity.Shop.tbShopInfo SelectShop(string SessionKey, string SellerNick = "") {
            tbShopInfo Shop = null;
            try {
                IClient client = new DefaultClient(this.AppKey, this.AppSecret, SessionKey, Url);
                ShopGetRequest req = new ShopGetRequest();
                ShopGetResponse response = client.Execute(req);

                if (!response.IsError) {
                    Shop = new tbShopInfo() { 
                        PfId = 3,
                        VenderId = "",
                        Bulletin = "",
                        CategoryId = String.IsNullOrEmpty(response.Shop.storeOperateCategory) ? 0 : Convert.ToInt32(response.Shop.storeOperateCategory),
                        CategoryName = "",
                        CreateTime = String.IsNullOrEmpty(response.Shop.storeOpenTime) ? "" : response.Shop.storeOpenTime.Trim(),
                        InputDate = DateTime.Now,
                        LogoUrl = String.IsNullOrEmpty(response.Shop.storeAddress) ? "" : response.Shop.storeAddress.Trim(),
                        NickName = String.IsNullOrEmpty(response.Shop.storeNickName) ? "" : response.Shop.storeNickName.Trim(),
                        OutShopId = String.IsNullOrEmpty(response.Shop.storeId) ? 0 : Convert.ToInt32(response.Shop.storeId.Trim()),
                        ShopDesc = "",
                        ShopName = String.IsNullOrEmpty(response.Shop.storeName) ? "" : response.Shop.storeName.Trim()
                    };
                }
            } catch { }

            return Shop;
        }
        
        /// <summary>
        /// 获取一号店寄件人信息
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <returns></returns>
        public tbSenderInfo SelectSender(string SessionKey) {
            return null;
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
            UpdateStatus(SessionKey, ShopId);

            try {
                Logistics = null;
                ShopLogistics = LogisList;
                Config = OrderConfig;
                List<string> PayUser = new List<string>();
                List<Entity.OrderInfo> List = GetPayOrder(PayUser, SessionKey, ShopId);
                if (List != null && List.Count > 0) {
                    List<tbOrdersInfo> OrderList = GetYHDOrder(PayUser, List, SessionKey, ShopId);
                    IsOk = OrderList.Count > 0;
                }
            } catch { }

            return IsOk;
        }

        /// <summary>
        /// 更新系统已有订单的状态
        /// </summary>
        private void UpdateStatus(string SessionKey, int ShopId) {
            try {
                IDictionary<string, string> Nums = Business.Order.OrdersInfo.SelectOrdersNumber(ShopId);
                int PageCount = 50;
                int MaxPage = (int)Math.Ceiling((decimal)Nums.Count / PageCount);

                //需要改变状态的订单编号列表
                IList<string> ChangeIds = new List<string>();

                for (int i = 0; i < MaxPage; i++) {
                    var list = Nums.Skip(i * PageCount).Take(PageCount);

                    IDictionary<string, string> Tmp = new Dictionary<string, string>();
                    IList<string> Ids = new List<string>();
                    foreach (var item in list) {
                        string val= item.Value.Split(',')[0];
                        if (!Tmp.ContainsKey(val)) {
                            Ids.Add(val);
                            Tmp.Add(val, item.Key);
                        }
                    }

                    IClient client = new DefaultClient(AppKey, AppSecret, SessionKey, Url);
                    OrdersDetailRequest req = new OrdersDetailRequest();
                    req.orderCodeList = String.Join(",", Ids);

                    OrdersDetailResponse response = client.Execute(req);

                    foreach (var item in response.Orders) {
                        if (item.Detail.orderStatus == "ORDER_OUT_OF_WH" || item.Detail.orderStatus == "ORDER_RECEIVED" || item.Detail.orderStatus == "ORDER_CANCEL" || item.Detail.orderStatus == "ORDER_FINISH") {
                            ChangeIds.Add(Tmp[item.Detail.orderId.ToString()]);
                        }
                    }
                }

                UpdateStatus(ChangeIds.Distinct().ToList());
            } catch { }
        }

        private void UpdateStatus(IList<string> IdList) {
            try {
                int PageCount = 200;
                int MaxPage = (int)Math.Ceiling((decimal)IdList.Count / PageCount);

                for (int i = 0; i < MaxPage; i++) {
                    var list = IdList.Skip(i * PageCount).Take(PageCount);
                    Business.Order.OrdersInfo.BatchUpdateStatus(String.Join(",", list), 4);
                }
            }
            catch { }
        }

        /// <summary>
        /// 同步订单
        /// </summary>
        /// <param name="OrderCodeList">订单编码列表</param>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="OrderConfig">配置信息</param>
        /// <param name="LogisList">物流信息</param>
        /// <param name="ShopId">店铺编号</param>
        /// <returns></returns>
        public bool Request(string OrderCodeList, string SessionKey, tbOrdersConfig OrderConfig, IList<tbShopLogistics> LogisList, int ShopId) {
           bool IsOk = false;
            try {
                Logistics = null;
                ShopLogistics = LogisList;
                Config = OrderConfig;
                List<string> PayUser = new List<string>();
                List<Entity.OrderInfo> List = GetOrderUser(OrderCodeList, PayUser, SessionKey, ShopId);
                if (List != null && List.Count > 0) {
                    List<tbOrdersInfo> OrderList = GetYHDOrder(PayUser, List, SessionKey, ShopId, false);
                    IsOk = OrderList.Count > 0;
                }
            } catch { }

            return IsOk;
        }

        #region 获取一号店中订单列表

        /// <summary>
        /// 获取一号店中订单列表
        /// </summary>
        /// <param name="OrderCodeList">订单编码列表</param>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="ShopId">店铺编号</param>
        /// <returns></returns>
        public List<Entity.OrderInfo> GetOrderUser(string OrderCodeList, List<string> PayUser, string SessionKey, int ShopId) {
            List<Entity.OrderInfo> OrderList = new List<Entity.OrderInfo>();
            try {
                IClient client = new DefaultClient(AppKey, AppSecret, SessionKey, Url);
                OrdersDetailRequest req = new OrdersDetailRequest();
                req.orderCodeList = OrderCodeList;

                OrdersDetailResponse response = client.Execute(req);

                foreach (Entity.OrderInfo order in response.Orders) {
                    if (PayUser.Count(e => e.Equals(order.Detail.goodReceiverName.Trim())) <= 0) { 
                        bool isExist = Business.Order.OrdersInfo.CheckOrder(order.Detail.orderCode.Trim(), ShopId);
                        if (!isExist) {
                            PayUser.Add(order.Detail.goodReceiverName.Trim());
                            OrderList.Add(order);
                        }
                    }
                }
            } catch {  }

            return OrderList;
        }

        #endregion

        #region 获取一号店中已付款的订单列表

        /// <summary>
        /// 获取一号店中已付款的订单列表
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="ShopId">店铺编号</param>
        /// <returns></returns>
        public List<Entity.OrderInfo> GetPayOrder(List<string> PayUser, string SessionKey, int ShopId) {
            List<Entity.OrderInfo> OrderList = new List<Entity.OrderInfo>();
            try {
                PageInfo.Reset(20L);
                while (PageInfo._ProductPage._IsNext) {
                    IClient client = new DefaultClient(AppKey, AppSecret, SessionKey, Url);
                    OrderSearchRequest req = new OrderSearchRequest();
                    req.orderStatusList = "ORDER_WAIT_SEND";
                    req.startTime = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd HH:mm:ss");
                    req.endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    req.curPage = PageInfo._ProductPage._Page;
                    req.pageRows = PageInfo._ProductPage._PageSize;

                    OrderSearchResponse response = client.Execute(req);
                    
                    if (response.IsError) {
                        break;
                    }

                    List<string> CodeList = new List<string>();

                    foreach (Entity.Order order in response.Orders) {
                        bool isExist = Business.Order.OrdersInfo.CheckOrder(order.orderCode.Trim(), ShopId);
                        if (!isExist) {
                            CodeList.Add(order.orderCode);
                        }
                    }

                    if (CodeList.Count > 0) {
                        OrdersDetailResponse res = GetOrderDetail(SessionKey, String.Join(",", CodeList));
                        if (!res.IsError) {
                            foreach (Entity.OrderInfo order in res.Orders) {
                                if (PayUser.Count(e => e.Equals(order.Detail.goodReceiverName.Trim())) <= 0) {
                                    PayUser.Add(order.Detail.goodReceiverName.Trim());
                                }
                           
                                OrderList.Add(order);
                            }
                        }
                    }

                    PageInfo._ProductPage._TotalResults = response.totalCount;
                    PageInfo._ProductPage.ResolvePageNumber();
                }
            } catch {  }

            return OrderList;
        }

        #endregion

        #region 获取订单

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="PayUser">用户列表</param>
        /// <param name="List">一号店订单列表</param>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="status">是否验证订单状态分配</param>
        /// <returns></returns>
        private List<tbOrdersInfo> GetYHDOrder(List<string> PayUser, List<Entity.OrderInfo> List, string SessionKey, int ShopId, bool status = true) {
            List<tbOrdersInfo> OrdersInfo = new List<tbOrdersInfo>();

            foreach (string NickName in PayUser) {
                List<Entity.OrderInfo> TradeList = List.FindAll(e => e.Detail.goodReceiverName.Trim().Equals(NickName));

                if (TradeList != null && TradeList.Count > 0) {
                    List<tbOrdersInfo> TempList = ResolveData(TradeList, SessionKey, ShopId);
                    if (TempList != null && TempList.Count > 0) {
                        OrdersInfo.AddRange(TempList);
                        InsertOrders(TempList, SessionKey, status);
                    }
                }
            }

            return OrdersInfo;
        }

        #endregion

        #region 解析一号店订单列表数据

        /// <summary>
        /// 解析一号店订单列表数据
        /// </summary>
        /// <param name="orders">一号店订单列表</param>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="ShopId">店铺编号</param>
        /// <returns></returns>
        private List<tbOrdersInfo> ResolveData(List<Entity.OrderInfo> orders, string SessionKey, int ShopId) {
            Dictionary<long, string> AttributeCache = new Dictionary<long, string>();
            if (!(orders != null && orders.Count > 0)) { return null; }
            List<tbOrdersInfo> OrderList = new List<tbOrdersInfo>();

            foreach (Entity.OrderInfo di in orders) {
                tbOrdersInfo Order = new tbOrdersInfo();
                tbConsigneeInfo Consignee = new tbConsigneeInfo();
                tbBuyerInfo Buyer = new tbBuyerInfo();
                List<tbOrdersDetail> Details = new List<tbOrdersDetail>();

                if (!String.IsNullOrEmpty(di.Detail.orderPaymentConfirmDate)) {
                    Order.PayDate = Convert.ToDateTime(Comm.DataChange(di.Detail.orderPaymentConfirmDate, TypeChange.DATETIME));//付款时间
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
                Order.CashOndelivery = di.Detail.payServiceType == 2 || di.Detail.payServiceType == 5 || di.Detail.payServiceType ==12;
                Order.Invoice = di.Detail.orderNeedInvoice > 0;//是否开发票
                Order.NickName = di.Detail.goodReceiverName.Trim();//会员名
                Buyer.NickName = di.Detail.goodReceiverName.Trim();
                Order.Commission = 0;
                Order.OrdersOutNumber = di.Detail.orderCode.Trim();
                Order.Logistics = new tbLogistics();
                string[] LStrs = Comm.Distribution(Logistics, ShopLogistics, "快递");//解析物流
                Order.IsFree = bool.Parse(LStrs[0]);//是否包邮
                Order.Logistics.LogisticsId = int.Parse(LStrs[1]);//配送方式
                Order.OrdersFreight = Convert.ToInt32(di.Detail.orderDeliveryFee);//运费
                if (Order.OrdersFreight == 0) { Order.IsFree = true; }
                Order.OrdersDate = Convert.ToDateTime(Comm.DataChange(di.Detail.orderCreateTime, TypeChange.DATETIME));//交易创建时间
                Consignee.Name = Comm.DataChange(di.Detail.goodReceiverName, TypeChange.STRING).ToString();//收件人姓名
                Buyer.BuyerName = Consignee.Name;
                Consignee.Provice = Comm.DataChange(di.Detail.goodReceiverProvince, TypeChange.STRING).ToString();//收件人所在省
                Consignee.City = Comm.DataChange(di.Detail.goodReceiverCity, TypeChange.STRING).ToString();//收件人所在市
                Consignee.District = Comm.DataChange(di.Detail.goodReceiverCounty, TypeChange.STRING).ToString();//收件人所在区
                Consignee.ConsigneeAddress = Comm.DataChange(di.Detail.goodReceiverAddress, TypeChange.STRING).ToString();//收件人地址
                Consignee.PostCode = String.IsNullOrEmpty(di.Detail.goodReceiverPostCode) ? "" : di.Detail.goodReceiverPostCode;//收件人邮编
                Consignee.Mobile = String.IsNullOrEmpty(di.Detail.goodReceiverMoblie) ? "" : di.Detail.goodReceiverMoblie;//收件人手机
                Buyer.Mobile = Consignee.Mobile;
                Consignee.Phone = String.IsNullOrEmpty(di.Detail.goodReceiverPhone) ? "" : di.Detail.goodReceiverPhone;//收件人电话
                Buyer.Phone = Consignee.Phone;
                int OrdersStatusId = 3;
                switch (di.Detail.orderStatus.Trim().ToUpper()) {
                    case "ORDER_WAIT_PAY":
                        OrdersStatusId = 1;
                        break;
                    case "ORDER_ON_SENDING":
                        OrdersStatusId = 4;
                        break;
                    case "ORDER_RECEIVED":
                    case "ORDER_FINISH":
                        OrdersStatusId = 5;
                        break;
                    case "ORDER_CANCEL":
                        OrdersStatusId = 6;
                        break;
                    default: break;
                }

                Order.Status = new tbOrdersStatus() { OrdersStatusId = OrdersStatusId };//订单状态(默认买家已付款，等待卖家发货状态)
                decimal Payment = di.Detail.orderAmount;//订单实收金额\
                if (Order.CashOndelivery) { //如果货到付款，则获取货到付款实收金额
                    decimal.TryParse(di.Detail.collectOnDeliveryAmount, out Payment);
                }
                //decimal Payment = di.Detail.orderAmount;//订单实收金额
                Order.OrdersAccounts = Payment;
                Order.OrdersPaid = Payment;
                Order.IsOrdersReFund = false;//是否有退款
                Order.OrdersDiscount = 0m;
                Order.OrdersWeight = 0;
                Order.OrdersNotes = "";
                Order.OrdersFlag = "0";
                Order.OrdersInputDate = DateTime.Now;
                Consignee.InputDate = Order.OrdersInputDate;
                Order.ServiceNotes = String.IsNullOrEmpty(di.Detail.merchantRemark) ? "" : di.Detail.merchantRemark;//客服备注
                Order.BuyerMsg = String.IsNullOrEmpty(di.Detail.deliveryRemark) ? "" : di.Detail.deliveryRemark;//买家留言
                string Type = "0";
                Order.ServiceFlag = "op_memo_" + Type + ".png";//客服备注旗帜样式
                Buyer.BuyerEmail = "";//买家Email地址
                Order.CodFee = 0m;//货到付款服务费
                Order.BuyerRemark = "";
                Order.DeliveryDate = "";
                Order.RemarkFlag = "";
                Order.Shop = new tbShopInfo() { ShopId = ShopId };
                Order.OrdersWeight = 0;

                List<long> ProductIdList = new List<long>();

                foreach (Entity.OrderItem order in di.Items) {
                    int ProductNumber = order.orderItemNum;//商品数量
                    if (ProductNumber <= 0) { continue; }

                    tbOrdersDetail Detail = new tbOrdersDetail();

                    Detail.ProductEncoding = !String.IsNullOrEmpty(order.outerId) ? order.outerId.Trim() : "";
                    Detail.ProductName = !String.IsNullOrEmpty(order.productCName) ? order.productCName.Trim() : "";

                    string ProductSku = "";
                    if (AttributeCache.ContainsKey(order.productId)) {
                        ProductSku = AttributeCache[order.productId];
                    } else {
                        ProductSku = GetProductAttribute(SessionKey, order.productId);
                        if (!String.IsNullOrEmpty(ProductSku)) { AttributeCache.Add(order.productId, ProductSku); }
                    }
                    Detail.ProductSku = ProductSku;
                    
                    Detail.ProductId = 0;
                    Detail.ProductProId = 0;
                    Detail.SalesCommissionId = 1;
                    Detail.ProductImg = "";//商品图片
                    Detail.ProductPrice = order.orderItemPrice;//商品价格
                    Detail.ProductNumber = ProductNumber;//商品数量
                    Detail.PackageName = "";//商品套餐值
                    Detail.OutNumberIId = order.productId.ToString();//商品外部编号
                    Detail.SubOrderNumber = order.orderId.ToString();//子订单编号
                    Detail.InputDate = Order.OrdersInputDate;
                    Detail.ProductTotal = Detail.ProductPrice * Detail.ProductNumber;//商品总价
                    Detail.Details = "";
                    Detail.ProductCost = 0;
                    Detail.OrdersDiscount = 0m;//优惠金额;
                    Detail.OrdersAdjust = 0m;//手动调整金额;
                    Detail.OrdersAccounts = Detail.ProductTotal;

                    //商品退款
                    Detail.ReFundNumber = "";
                    Detail.IsProductReFund = false;
                    Detail.ReFundStatusId = 0;
                    Detail.ReFundStatusName = "";
                    Order.OrdersProductTotal += Detail.ProductTotal;//商品总金额
                    Details.Add(Detail);
                    ProductIdList.Add(order.productId);                    
                }

                if (ProductIdList.Count > 0) {
                    Dictionary<long, string> ImgList = GetChildProductsImg(ShopId, SessionKey, ProductIdList);
                    if (ImgList != null && ImgList.Count > 0) {
                        foreach (var item in Details) {
                            if (ImgList.ContainsKey(long.Parse(item.OutNumberIId))) { 
                                string[] strs = ImgList[long.Parse(item.OutNumberIId)].Split(',');
                                item.ProductImg = strs[0];
                                item.Details = strs[1];
                            }
                        }
                    }
                }

                Order.Consignee = Consignee;
                Order.Details = Details;
                Order.Buyer = Buyer;

                Comm.MergeOrder(Config, OrderList, Order);
            }

            return OrderList;
        }

        #endregion

        #region 获取子商品图片信息

        /// <summary>
        /// 获取子商品图片信息
        /// </summary>
        /// <param name="SessionKey">店铺编号</param>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="ProductIdList">子商品编号列表</param>
        /// <param name="dic">子商品图片键值对</param>
        /// <returns></returns>
        private Dictionary<long, string> GetChildProductsImg(int ShopId, string SessionKey, List<long> ProductIdList) {
            Dictionary<long, string> dic = new Dictionary<long, string>();
            try {
                List<My.App.Entity.Product.tbProductAbb> abbList = GetGeneralProduct(ShopId, SessionKey, string.Join(",", ProductIdList));
                if(abbList != null && abbList.Count > 0) {
                    foreach (var item in abbList) {
                        long tmpId = long.Parse(item.OutId);
                        if (!dic.ContainsKey(tmpId)) {
                            dic.Add(tmpId, item.ProductImg + "," + item.ProductUrl);
                        }
                    }
                } else {
                    IClient client = new DefaultClient(AppKey, AppSecret, SessionKey, Url);
                    SerialChildProductsRequest req = new SerialChildProductsRequest();
                    req.productIdList = string.Join(",", ProductIdList);
                    SerialChildProductsResponse response = client.Execute(req);
                    if (!response.IsError) {
                        foreach (var item in response.Products) {
                            if (!dic.ContainsKey(item.productId)) {
                                string Img = String.IsNullOrEmpty(item.prodImg) ? "" : item.prodImg.Trim().Split(',')[0].Split('|')[1];
                                string id = String.IsNullOrEmpty(item.prodDetailUrl) ? "" : item.prodDetailUrl;
                                dic.Add(item.productId, Img + "," + id);
                            }
                        }
                    }
                }
            } catch { }

            return dic;
        }

        #endregion

        #region 根据订单编码列表获取订单详情列表
        
        /// <summary>
        /// 根据订单编码列表获取订单详情列表
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="IdList">订单编码列表</param>
        /// <returns></returns>
        private OrdersDetailResponse GetOrderDetail(string SessionKey, string IdList) {
            IClient client = new DefaultClient(AppKey, AppSecret, SessionKey, Url);
            OrdersDetailRequest req = new OrdersDetailRequest();
            req.orderCodeList = IdList;

            return client.Execute(req);
        }

        #endregion

        #region 将订单数据写入数据库

        /// <summary>
        /// 将订单数据写入数据库
        /// </summary>
        /// <param name="OrdersList">订单数据列表</param>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="status">是否验证状态分配</param>
        public void InsertOrders(List<tbOrdersInfo> OrdersList, string SessionKey, bool status = true) {
            try {
                foreach (tbOrdersInfo Order in OrdersList) {
                    if (status) {
                        Comm.StatusAssign(Config, Order);
                    }
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
        
        #region 查询支持起始地到目的地范围的物流公司编码列表
        
        /// <summary>
        /// 查询支持起始地到目的地范围的物流公司编码列表
        /// </summary>
        /// <param name="service_type">服务类型 可选值：cod(货到付款)、online(在线下单)、 offline(自己联系)、limit(限时物流)</param>
        /// <param name="source_id">物流公司揽货地地区码（必须是区、县一级的）可为空</param>
        /// <param name="target_id">物流公司派送地地区码（必须是区、县一级的）可为空</param>
        /// <returns></returns>
        private List<string> GetLogisticsIdList(string service_type, string source_id, string target_id) {
            List<string> List = null;
            Dictionary<string, string> Params = ApiData.GetApiParams(PlatFormEnum.TaoBao);

            if (Params != null) {
                Service.TaoBao.Api api = new TaoBao.Api(Params["Url"], Params["AppKey"], Params["AppSecret"]);
                List = api.GetLogisticsIdList(service_type, source_id, target_id);
            }

            return List;
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
                    IClient client = new DefaultClient(AppKey, AppSecret, SessionKey, Url);
                    RemarkUpdateRequest req = new RemarkUpdateRequest();
                    req.orderCode = OutNumber[i];
                    req.remark = Detail[i] + Config.Remark;

                    RemarkUpdateResponse response = client.Execute(req);
                }
            } catch { }
        }

        #endregion

        #region 获取商品属性列表

        /// <summary>
        /// 获取商品属性列表
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="ProductId">产品编号列表</param>
        /// <param name="AttributeList">商品属性列表</param>
        /// <returns></returns>
        private string GetProductAttribute(string SessionKey, long ProductId) {
            List<string> Sku = new List<string>();
            try {
                IClient client = new DefaultClient(AppKey, AppSecret, SessionKey, Url);
                ProductAttributeGetRequest req = new ProductAttributeGetRequest();
                req.productId = string.Join(",", ProductId);
                ProductAttributeGetResponse response = client.Execute(req);
                if (!response.IsError) {
                    if (!String.IsNullOrEmpty(response.Attribute.customColorName)) { Sku.Add(response.Attribute.customColorName.Trim()); }
                    if (!String.IsNullOrEmpty(response.Attribute.customSizeName)) { Sku.Add(response.Attribute.customSizeName.Trim()); }
                }
                
            } catch { }

            return String.Join(";", Sku);
        }

        #endregion

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
                    StatusTable st = SelectOrderStatus(SessionKey, str);
                    if (st.IsError) {
                        dic.Add(str, new Dictionary<bool, string>() { { false, st.Msg } });
                        continue;
                    }

                    //一号店是否已经发货
                    if (st.IsSuccess) {
                        dic.Add(str, new Dictionary<bool, string>() { { true, (String.IsNullOrEmpty(st.Msg) ? "一号店已经发货，但是未能获取发货单号." : st.Msg) } });
                        continue;
                    } else {
                        StatusTable st2 = ToDelivery(SessionKey, str, Logistics.LogisticsId, DeliveryNumber);
                        dic.Add(str, new Dictionary<bool, string>() { { st2.IsSuccess, st2.Msg } });
                        continue;
                    }
                }
            }

            return dic;
        }

        #region 查询第三方订单状态
        
        /// <summary>
        /// 查询第三方订单状态
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="OutNumber">外部订单号</param>
        /// <returns></returns>
        private StatusTable SelectOrderStatus(string SessionKey, string OutNumber) {
            StatusTable st = new StatusTable() { Msg = "" };
            try {
                st.IsSuccess = false;
                IClient client = new DefaultClient(AppKey, AppSecret, SessionKey, Url);
                OrdersDetailRequest req = new OrdersDetailRequest();
                req.orderCodeList = OutNumber;

                OrdersDetailResponse response = client.Execute(req);

                if (response.IsError) {
                    st.IsError = true;
                    st.Msg = "获取第三方平台订单的状态失败";
                } else {
                    if (response.Orders != null && response.Orders.Count == 1) {
                        if (!response.Orders[0].Detail.orderStatus.Trim().ToUpper().Equals("ORDER_TRUNED_TO_DO")) {
                            st.IsSuccess = true;
                            if (String.IsNullOrEmpty(response.Orders[0].Detail.merchantExpressNbr)) {
                                st.Msg = "一号店已经发货，但是未能获取发货单号.";
                            } else {
                                st.Msg = "一号店已经发货，发货单号为:" + response.Orders[0].Detail.merchantExpressNbr;
                            }
                        }
                    } else {
                        st.IsError = true;
                        st.Msg = "获取第三方平台订单的状态失败";
                    }
                }
            } catch {
                st.IsError = true;
                st.Msg = "获取第三方平台订单的状态失败";
            }

            return st;
        }

        #endregion

        #region 第三方平台订单发货
        
        /// <summary>
        /// 第三方平台订单发货
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="OutNumber">订单编号</param>
        /// <param name="TransportMode">配送方式编号</param>
        /// <param name="DeliveryNumber">物流单号</param>
        /// <returns></returns>
        private StatusTable ToDelivery(string SessionKey, string OutNumber, int TransportMode, string DeliveryNumber) {
            StatusTable st = new StatusTable() { Msg = "" };
            long CompanyCode = ResolveCode(TransportMode);

            if (CompanyCode != 0) {
                IClient client = new DefaultClient(AppKey, AppSecret, SessionKey, Url);
                OrderShipmentsRequest req = new OrderShipmentsRequest();
                req.orderCode = OutNumber;
                req.expressNbr = DeliveryNumber;
                req.deliverySupplierId = CompanyCode;

                OrderShipmentsResponse response = client.Execute(req);

                if (response.IsError) {
                    st.IsSuccess = false;
                    st.Msg = "第三方平台发货失败";
                } else {
                    if (response.updateCount > 0) {
                        st.IsSuccess = true;
                    } else {
                        st.IsSuccess = false;
                        st.Msg = "第三方平台发货失败";
                    }
                }
            } else {
                st.IsSuccess = false;
                st.Msg = "解析第三方平台物流公司编码失败";
            }

            return st;
        }

        #endregion

        #region 解析一号店物流公司编码
        
        /// <summary>
        /// 解析一号店物流公司编码
        /// </summary>
        /// <param name="TransportMode">配送方式</param>
        /// <returns></returns>
        private int ResolveCode(int TransportMode) {
            int CompanyCode = 1758;

            switch (TransportMode) {
                case 14://国通
                case 12://CCES
                    CompanyCode = 1758;
                    break;
                case 37://申通E物流
                    CompanyCode = 1757;
                    break;
                case 36://圆通速递
                    CompanyCode = 1755;
                    break;
                case 35://中通速递
                case 26://中通速递BJ
                    CompanyCode = 1751;
                    break;
                case 34://汇通快运
                    CompanyCode = 1760;
                    break;
                case 33://顺丰速运
                    CompanyCode = 1756;
                    break;
                case 32://韵达
                    CompanyCode = 1754;
                    break;
                case 31://宅急送
                    CompanyCode = 1752;
                    break;
                case 30://天天快递
                    CompanyCode = 10299;
                    break;
                case 29://EMS
                    CompanyCode = 1759;
                    break;
                case 28://EMS经济快递
                    CompanyCode = 17683;
                    break;
                case 27://邮政国内小包
                    CompanyCode = 20304;
                    break;
                case 25://佳吉快运
                    CompanyCode = 17323;
                    break;
                case 24://全峰快递
                    CompanyCode = 10300;
                    break;
                case 21://德邦物流
                    CompanyCode = 17315;
                    break;
                case 39://德邦快递
                    CompanyCode = 20427;
                    break;
                case 44://联邦快递
                    CompanyCode = 17325;
                    break;
                case 16://全一快递
                    CompanyCode = 17326;
                    break;
                case 45://速尔快递
                    CompanyCode = 17048;
                    break;
                case 47://优速物流
                    CompanyCode = 17331;
                    break;
                case 49://新邦物流
                    CompanyCode = 17330;
                    break;
            }

            return CompanyCode;
        }

        #endregion

        /// <summary>
        /// 获取物流公司信息
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <returns></returns>
        public List<Service.Entity.DeliveryCompany> GetDeliveryCompany(string SessionKey) {
            throw new NotImplementedException();
        }

        public List<string> GetWayBillCode(string SessionKey, string Num, string Code) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取货到付款代收金额
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="OrderNumber">订单编号</param>
        /// <returns></returns>
        public decimal GetCashPrice(string SessionKey, string OrderNumber) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询支持起始地到目的地范围的物流公司
        /// </summary>
        /// <param name="param">参数列表<para>param[0]:AppKey</para><para>param[1]:AppSecret</para><para>param[2]:service_type 服务类型 可选值：cod(货到付款)、online(在线下单)、 offline(自己联系)、limit(限时物流)</para><para>param[3]:source_id 物流公司揽货地地区码（必须是区、县一级的）可为空</para><para>param[4]:target_id 物流公司派送地地区码（必须是区、县一级的）可为空</para></param>
        /// <param name="service_type">服务类型 可选值：cod(货到付款)、online(在线下单)、 offline(自己联系)、limit(限时物流)</param>
        /// <param name="source_id">物流公司揽货地地区码（必须是区、县一级的）可为空</param>
        /// <param name="target_id">物流公司派送地地区码（必须是区、县一级的）可为空</param>
        /// <returns></returns>
        public List<DeliveryCompany> GetLogistics(string service_type, string source_id, string target_id) {
            return new List<DeliveryCompany>();
        }

        /// <summary>
        /// 同步商品
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="ShopId">店铺编号</param>
        /// <returns></returns>
        public bool SyncProduct(string SessionKey, int ShopId) {
            bool IsOk = false;
            List<My.App.Entity.Product.tbProductAbb> List = new List<My.App.Entity.Product.tbProductAbb>();
            
            IDictionary<long, decimal> PriceList = new Dictionary<long, decimal>();

            List<My.App.Entity.Product.tbProductAbb> TmpList = GetGeneralProduct(ShopId, SessionKey);
            if (TmpList != null && TmpList.Count > 0) {
                List.AddRange(TmpList);
                IsOk = true;
            }

            PageInfo.Reset(50L);

            while (PageInfo._ProductPage._IsNext) {
                IClient client = new DefaultClient(AppKey, AppSecret, SessionKey, Url);
                ProductSearchRequest req = new ProductSearchRequest();
                req.canShow = 1;
                req.canSale = 1;
                req.verifyFlg = 2;
                req.curPage = PageInfo._ProductPage._Page;
                req.pageRows = PageInfo._ProductPage._PageSize;

                ProductSearchResponse response = client.Execute(req);

                if (response.IsError) {
                    break;
                }

                List<long> ProductIdList = new List<long>();

                foreach (Entity.SerialProduct item in response.Products) {
                    My.App.Entity.Product.tbProductAbb Abb = new My.App.Entity.Product.tbProductAbb() {
                        ShopId = ShopId,
                        ProductId = Convert.ToInt32(item.productId),
                        ProductName = String.IsNullOrEmpty(item.productCname) ? "" : item.productCname.Trim(),
                        OutId = item.productId.ToString(),
                        Abbreviation = "",
                        Display = true,
                        InputDate = DateTime.Now,
                        ProductImg = String.IsNullOrEmpty(item.prodImg) ? "" : item.prodImg.Trim().Split(',')[0].Split('|')[1],
                        ProductUrl = String.IsNullOrEmpty(item.prodDetailUrl) ? "" : item.prodDetailUrl.Trim(),
                        ProductNumber = 0,
                        ProductPrice = 0
                    };

                    //判断是否包含系列子商品
                    if (String.IsNullOrEmpty(item.prodImg)) {
                        List<Entity.SerialChildProd> Prod = GetProductSkus(item.productId, SessionKey);
                        if (Prod != null && Prod.Count > 0) {
                            for (int i = 0; i < Prod.Count; i++) {
                                Entity.SerialChildProd pro = Prod[i];
                                string img = String.IsNullOrEmpty(pro.prodImg) ? "" : pro.prodImg.Trim().Split(',')[0].Split('|')[1];

                                List.Add(new My.App.Entity.Product.tbProductAbb() {
                                    ShopId = ShopId,
                                    ProductId = Convert.ToInt32(pro.productId),
                                    ProductName = String.IsNullOrEmpty(pro.productCname) ? "" : pro.productCname.Trim(),
                                    OutId = pro.productId.ToString(),
                                    Abbreviation = "",
                                    Display = true,
                                    InputDate = DateTime.Now,
                                    ProductImg = img,
                                    ProductUrl = String.IsNullOrEmpty(pro.prodDetailUrl) ? "" : pro.prodDetailUrl.Trim(),
                                    ProductNumber = 0,
                                    ProductPrice = 0
                                });
                            }
                        }
                    } else {
                        List.Add(Abb);
                    }
                }

                PageInfo._ProductPage._TotalResults = response.totalCount;
                PageInfo._ProductPage.ResolvePageNumber();
            }

            foreach (var item in List) {
                if (PriceList.ContainsKey(item.ProductId)) {
                    item.ProductPrice = PriceList[item.ProductId];
                }
            }

            IsOk = Comm.InsertProduct(List);
            return IsOk;
        }

        #region 获取普通产品信息

        /// <summary>
        /// 获取普通产品信息
        /// </summary>
        /// <param name="ShopId">店铺编号</param>
        /// <param name="SessionKey">授权码</param>
        /// <param name="ProductIdList">商品编号列表</param>
        /// <returns></returns>
        private List<My.App.Entity.Product.tbProductAbb> GetGeneralProduct(int ShopId, string SessionKey, string ProductIdList = "") {
            List<My.App.Entity.Product.tbProductAbb> List = new List<App.Entity.Product.tbProductAbb>();
            
            try {
                PageInfo.Reset(50L);

                while (PageInfo._ProductPage._IsNext) {
                    IClient client = new DefaultClient(AppKey, AppSecret, SessionKey, Url);
                    GeneralProductsSearchRequest req = new GeneralProductsSearchRequest();
                    if (!String.IsNullOrEmpty(ProductIdList)) {
                        req.productIdList = ProductIdList;
                    } else {
                        req.canShow = 1;
                        req.canSale = 1;
                        req.verifyFlg = 2;
                    }
                    req.curPage = PageInfo._ProductPage._Page;
                    req.pageRows = PageInfo._ProductPage._PageSize;

                    GeneralProductsSearchResponse response = client.Execute(req);

                    if (response.IsError) {
                        break;
                    }

                    foreach (Entity.GeneralProduct item in response.Products) {
                        List.Add(new My.App.Entity.Product.tbProductAbb() {
                            ShopId = ShopId,
                            ProductId = Convert.ToInt32(item.productId),
                            ProductName = String.IsNullOrEmpty(item.productCname) ? "" : item.productCname.Trim(),
                            OutId = item.productId.ToString(),
                            Abbreviation = "",
                            Display = true,
                            InputDate = DateTime.Now,
                            ProductImg = String.IsNullOrEmpty(item.prodImg) ? "" : item.prodImg.Trim().Split(',')[0].Split('|')[1],
                            ProductUrl = String.IsNullOrEmpty(item.prodDetailUrl) ? "" : item.prodDetailUrl.Trim(),
                            ProductNumber = 0,
                            ProductPrice = 0
                        });
                    }

                    PageInfo._ProductPage._TotalResults = response.totalCount;
                    PageInfo._ProductPage.ResolvePageNumber();
                }
            } catch { }

            return List;
        }

        #endregion

        #region 获取单个系列产品的子品信息

        /// <summary>
        /// 获取单个系列产品的子品信息
        /// </summary>
        /// <param name="List">产品编号列表</param>
        /// <returns></returns>
        private List<Entity.SerialChildProd> GetProductSkus(long proId, string SessionKey) {
            List<Entity.SerialChildProd> Prod = new List<Entity.SerialChildProd>();
            try {
                IClient client = new DefaultClient(AppKey, AppSecret, SessionKey, Url);
                SerialProductRequest req = new SerialProductRequest();
                req.productId = proId;

                SerialProductResponse response = client.Execute(req);

                if (response.IsError) {
                    return Prod;
                }

                if (response.Products != null && response.Products.Count > 0) {
                    Prod = response.Products;
                }
            } catch { }

            return Prod;
        }

        #endregion

        #region 获取商品价格
        
        /// <summary>
        /// 获取商品价格
        /// </summary>
        /// <param name="SessionKey">用户授权</param>
        /// <param name="ProductIdList">商品编号列表(逗号分隔)</param>
        /// <param name="PriceList">价格键值对</param>
        /// <returns></returns>
        private void GetPrice(string SessionKey, string ProductIdList, IDictionary<long, decimal> PriceList) {
            IClient client = new DefaultClient(AppKey, AppSecret, SessionKey, Url);
            ProductsPriceRequest req = new ProductsPriceRequest();
            req.productIdList = ProductIdList;

            ProductsPriceResponse response = client.Execute(req);

            if (response.IsError) {
                return;
            }

            foreach (Entity.pmPrice sku in response.Prices) {
                if (!PriceList.ContainsKey(sku.productId)) {
                    PriceList.Add(sku.productId, sku.nonMemberPrice);
                }
            }
        }

        #endregion
    }
}
