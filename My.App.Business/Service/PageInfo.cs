using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Business.Service
{
    /// <summary>
    /// 页码类
    /// </summary>
    internal class PageInfo
    {
        private long TotalResults;                                                                                          //总条数
        private long TotalPages;                                                                                            //总页数
        private long Page;                                                                                                  //当前页数
        private long PageSize;                                                                                              //每页条数
        private bool IsNext;                                                                                                //是否有下一页
        private static PageInfo ProductPage;                                                                                //单例模式

        /// <summary>
        /// 页码类无参构造函数
        /// </summary>
        private PageInfo() { }

        /// <summary>
        /// 页码类有参构造函数
        /// </summary>
        /// <param name="TotalResults">总条数</param>
        /// <param name="TotalPages">总页数</param>
        /// <param name="Page">当前页数</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="IsNext">是否有下一页</param>
        private PageInfo(long TotalResults, long TotalPages, long Page, long PageSize, bool IsNext) {
            this.TotalResults = TotalResults;
            this.TotalPages = TotalPages;
            this.Page = Page;
            this.PageSize = PageSize;
            this.IsNext = IsNext;
        }

        /// <summary>
        /// 总条数
        /// </summary>
        public long _TotalResults {
            get { return TotalResults; }
            set { TotalResults = value; }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public long _TotalPages {
            get { return TotalPages; }
            set { TotalPages = value; }
        }

        /// <summary>
        /// 当前页数
        /// </summary>
        public long _Page {
            get { return Page; }
            set { Page = value; }
        }

        /// <summary>
        /// 每页条数
        /// </summary>
        public long _PageSize {
            get { return PageSize; }
            set { PageSize = value; }
        }

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool _IsNext {
            get { return IsNext; }
            set { IsNext = value; }
        }

        /// <summary>
        /// 单例模式
        /// </summary>
        public static PageInfo _ProductPage {
            get {
                if (ProductPage == null) {
                    Reset();
                }
                return ProductPage;
            }
            set { ProductPage = value; }
        }

        /// <summary>
        /// 重置页码信息
        /// </summary>
        public static void Reset(long PageSize = 40L) {
            if (ProductPage != null) {
                ProductPage._IsNext = true;
                ProductPage._Page = 1L;
                ProductPage._PageSize = PageSize;
                ProductPage._TotalPages = 0L;
                ProductPage._TotalResults = 0L;
            } else {
                ProductPage = new PageInfo() {
                    _IsNext = true,
                    _Page = 1L,
                    _PageSize = PageSize,
                    _TotalPages = 0L,
                    _TotalResults = 0L
                };
            }
        }

        /// <summary>
        /// 分析页码
        /// </summary>
        /// <param name="_PageInfo">页码类</param>
        public void ResolvePageNumber() {
            PageInfo _PageInfo = this;

            if (_PageInfo._IsNext) {
                long Remainder = _PageInfo._TotalResults % _PageInfo._PageSize;
                long MaxNo = _PageInfo._TotalResults / _PageInfo._PageSize;

                if (Remainder != 0) {
                    MaxNo += 1;
                }

                _PageInfo._TotalPages = MaxNo;

                if (_PageInfo._Page >= MaxNo) {
                    _PageInfo._IsNext = false;
                } else {
                    _PageInfo._Page += 1;
                }
            }
        }
    }
}
