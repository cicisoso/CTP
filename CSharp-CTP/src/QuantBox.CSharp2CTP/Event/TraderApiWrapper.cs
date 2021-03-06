﻿using QuantBox.CSharp2CTP.Callback;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantBox.CSharp2CTP.Event
{
    public class TraderApiWrapper : IDisposable
    {
        public event OnConnectHandler OnConnect;
        public event OnDisconnectHandler OnDisconnect;
        public event OnErrRtnOrderActionHandler OnErrRtnOrderAction;
        public event OnErrRtnOrderInsertHandler OnErrRtnOrderInsert;
        public event OnRspErrorHandler OnRspError;
        public event OnRspOrderActionHandler OnRspOrderAction;
        public event OnRspOrderInsertHandler OnRspOrderInsert;
        public event OnRspQryDepthMarketDataHandler OnRspQryDepthMarketData;
        public event OnRspQryInstrumentHandler OnRspQryInstrument;
        public event OnRspQryInstrumentCommissionRateHandler OnRspQryInstrumentCommissionRate;
        public event OnRspQryInstrumentMarginRateHandler OnRspQryInstrumentMarginRate;
        public event OnRspQryInvestorPositionHandler OnRspQryInvestorPosition;
        public event OnRspQryInvestorPositionDetailHandler OnRspQryInvestorPositionDetail;
        public event OnRspQryOrderHandler OnRspQryOrder;
        public event OnRspQryTradeHandler OnRspQryTrade;
        public event OnRspQryTradingAccountHandler OnRspQryTradingAccount;
        public event OnRtnInstrumentStatusHandler OnRtnInstrumentStatus;
        public event OnRtnOrderHandler OnRtnOrder;
        public event OnRtnTradeHandler OnRtnTrade;

        //private readonly fnOnConnect _fnOnConnect_Holder;
        //private readonly fnOnDisconnect _fnOnDisconnect_Holder;
        //private readonly fnOnErrRtnOrderAction _fnOnErrRtnOrderAction_Holder;
        //private readonly fnOnErrRtnOrderInsert _fnOnErrRtnOrderInsert_Holder;
        //private readonly fnOnRspError _fnOnRspError_Holder;
        //private readonly fnOnRspOrderAction _fnOnRspOrderAction_Holder;
        //private readonly fnOnRspOrderInsert _fnOnRspOrderInsert_Holder;
        //private readonly fnOnRspQryDepthMarketData _fnOnRspQryDepthMarketData_Holder;
        //private readonly fnOnRspQryInstrument _fnOnRspQryInstrument_Holder;
        //private readonly fnOnRspQryInstrumentCommissionRate _fnOnRspQryInstrumentCommissionRate_Holder;
        //private readonly fnOnRspQryInstrumentMarginRate _fnOnRspQryInstrumentMarginRate_Holder;
        //private readonly fnOnRspQryInvestorPosition _fnOnRspQryInvestorPosition_Holder;
        //private readonly fnOnRspQryInvestorPositionDetail _fnOnRspQryInvestorPositionDetail_Holder;
        //private readonly fnOnRspQryOrder _fnOnRspQryOrder_Holder;
        //private readonly fnOnRspQryTrade _fnOnRspQryTrade_Holder;
        //private readonly fnOnRspQryTradingAccount _fnOnRspQryTradingAccount_Holder;
        //private readonly fnOnRtnInstrumentStatus _fnOnRtnInstrumentStatus_Holder;
        //private readonly fnOnRtnOrder _fnOnRtnOrder_Holder;
        //private readonly fnOnRtnTrade _fnOnRtnTrade_Holder;

        //private readonly object _lockTd = new object();
        //private readonly object _lockMsgQueue = new object();

        private volatile bool _bTdConnected;
        public bool isConnected { get; private set; }

        private bool disposed;

        private string szPath;
        private string szAddresses;
        private string szBrokerId;
        private string szInvestorId;
        private string szPassword;
        private string szUserProductInfo;
        private string szAuthCode;
        private THOST_TE_RESUME_TYPE nResumeType;

        private MsgQueue m_pMsgQueue;
        private TradeApi m_Api;

        public TraderApiWrapper()
        {
            //_fnOnConnect_Holder = OnConnect_callback;
            //_fnOnDisconnect_Holder = OnDisconnect_callback;
            //_fnOnErrRtnOrderAction_Holder = OnErrRtnOrderAction_callback;
            //_fnOnErrRtnOrderInsert_Holder = OnErrRtnOrderInsert_callback;
            //_fnOnRspError_Holder = OnRspError_callback;
            //_fnOnRspOrderAction_Holder = OnRspOrderAction_callback;
            //_fnOnRspOrderInsert_Holder = OnRspOrderInsert_callback;
            //_fnOnRspQryDepthMarketData_Holder = OnRspQryDepthMarketData_callback;
            //_fnOnRspQryInstrument_Holder = OnRspQryInstrument_callback;
            //_fnOnRspQryInstrumentCommissionRate_Holder = OnRspQryInstrumentCommissionRate_callback;
            //_fnOnRspQryInstrumentMarginRate_Holder = OnRspQryInstrumentMarginRate_callback;
            //_fnOnRspQryInvestorPosition_Holder = OnRspQryInvestorPosition_callback;
            //_fnOnRspQryInvestorPositionDetail_Holder = OnRspQryInvestorPositionDetail_callback;
            //_fnOnRspQryOrder_Holder = OnRspQryOrder_callback;
            //_fnOnRspQryTrade_Holder = OnRspQryTrade_callback;
            //_fnOnRspQryTradingAccount_Holder = OnRspQryTradingAccount_callback;
            //_fnOnRtnInstrumentStatus_Holder = OnRtnInstrumentStatus_callback;
            //_fnOnRtnOrder_Holder = OnRtnOrder_callback;
            //_fnOnRtnTrade_Holder = OnRtnTrade_callback;
            m_pMsgQueue = new MsgQueue();
            m_Api = new TradeApi(m_pMsgQueue);
        }

        //Implement IDisposable.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                Disconnect();
                disposed = true;
            }
            //base.Dispose(disposing);
        }

        // Use C# destructor syntax for finalization code.
        ~TraderApiWrapper()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        public void Connect(string szPath, string szAddresses,
            string szBrokerId, string szInvestorId, string szPassword,
            THOST_TE_RESUME_TYPE nResumeType,
            string szUserProductInfo, string szAuthCode)
        {
            m_Api.Connection = new ConnectionInfo()
            {
                TempPath = szPath,
            };

            m_Api.Front = new FrontInfo()
            {
                BrokerId = szBrokerId,
                TradeAddress = szAddresses,
                UserProductInfo = szUserProductInfo,
                AuthCode = szAuthCode,
            };

            m_Api.Account = new AccountInfo()
            {
                InvestorId = szInvestorId,
                Password = szPassword,
            };

            m_Api.ResumeType = nResumeType;

            Disconnect_TD();
            Connect_TD();
        }

        public void Disconnect()
        {
            Disconnect_TD();
            isConnected = false;
        }

        //建立行情
        private void Connect_TD()
        {
            lock (this)
            {
                m_Api.OnConnect = OnConnect_callback;
                m_Api.OnDisconnect = OnDisconnect_callback;
                m_Api.OnRspError = OnRspError_callback;

                m_Api.OnErrRtnOrderAction = OnErrRtnOrderAction_callback;
                m_Api.OnErrRtnOrderInsert = OnErrRtnOrderInsert_callback;
                m_Api.OnRspOrderAction = OnRspOrderAction_callback;
                m_Api.OnRspOrderInsert = OnRspOrderInsert_callback;
                m_Api.OnRspQryDepthMarketData = OnRspQryDepthMarketData_callback;
                m_Api.OnRspQryInstrument = OnRspQryInstrument_callback;
                m_Api.OnRspQryInstrumentCommissionRate = OnRspQryInstrumentCommissionRate_callback;
                m_Api.OnRspQryInstrumentMarginRate = OnRspQryInstrumentMarginRate_callback;
                m_Api.OnRspQryInvestorPosition = OnRspQryInvestorPosition_callback;
                m_Api.OnRspQryInvestorPositionDetail = OnRspQryInvestorPositionDetail_callback;
                m_Api.OnRspQryOrder = OnRspQryOrder_callback;
                m_Api.OnRspQryTrade = OnRspQryTrade_callback;
                m_Api.OnRspQryTradingAccount = OnRspQryTradingAccount_callback;
                m_Api.OnRtnInstrumentStatus = OnRtnInstrumentStatus_callback;
                m_Api.OnRtnOrder = OnRtnOrder_callback;
                m_Api.OnRtnTrade = OnRtnTrade_callback;

                m_Api.Connect();
            }
        }

        private void Disconnect_TD()
        {
            lock (this)
            {
                m_Api.Disconnect();
                _bTdConnected = false;
            }
        }

        public int SendOrder(
            int OrderRef,
            string szInstrument,
            TThostFtdcDirectionType Direction,
            string szCombOffsetFlag,
            string szCombHedgeFlag,
            int VolumeTotalOriginal,
            double LimitPrice,
            TThostFtdcOrderPriceTypeType OrderPriceType,
            TThostFtdcTimeConditionType TimeCondition,
            TThostFtdcContingentConditionType ContingentCondition,
            double StopPrice,
            TThostFtdcVolumeConditionType VolumeCondition)
        {
            return m_Api.SendOrder(
                OrderRef,
                szInstrument,
                Direction,
                szCombOffsetFlag,
                szCombHedgeFlag,
                VolumeTotalOriginal,
                LimitPrice,
                OrderPriceType,
                TimeCondition,
                ContingentCondition,
                StopPrice,
                VolumeCondition);
        }

        public void CancelOrder(ref CThostFtdcOrderField pOrder)
        {
            m_Api.CancelOrder(ref pOrder);
        }

        public void ReqQryTradingAccount()
        {
            m_Api.ReqQryTradingAccount();
        }

        public void ReqQryInvestorPosition(string szInstrument)
        {
            m_Api.ReqQryInvestorPosition(szInstrument);
        }

        public void ReqQryInvestorPositionDetail(string szInstrument)
        {
            m_Api.ReqQryInvestorPositionDetail(szInstrument);
        }

        public void ReqQryInstrumentCommissionRate(string szInstrument)
        {
            m_Api.ReqQryInstrumentCommissionRate(szInstrument);
        }

        public void ReqQryInstrumentMarginRate(string szInstrument, TThostFtdcHedgeFlagType HedgeFlag)
        {
            m_Api.ReqQryInstrumentMarginRate(szInstrument, HedgeFlag);
        }
        
        private void OnConnect_callback(IntPtr pApi, ref CThostFtdcRspUserLoginField pRspUserLogin, ConnectionStatus result)
        {
            _bTdConnected = (ConnectionStatus.E_confirmed == result);
            if (_bTdConnected)
            {
                isConnected = true;
            }

            if (null != OnConnect)
            {
                OnConnect(this, new OnConnectArgs(pApi, ref pRspUserLogin, result));
            }
        }

        private void OnDisconnect_callback(IntPtr pApi, ref CThostFtdcRspInfoField pRspInfo, ConnectionStatus step)
        {
            if(isConnected)
            {
                if (7 == pRspInfo.ErrorID//综合交易平台：还没有初始化
                    || 8 == pRspInfo.ErrorID)//综合交易平台：前置不活跃
                {
                    Disconnect_TD();
                    Connect_TD();
                }
            }

            if (null != OnDisconnect)
            {
                OnDisconnect(this, new OnDisconnectArgs(pApi, ref pRspInfo, step));
            }
        }

        private void OnErrRtnOrderAction_callback(IntPtr pTraderApi, ref CThostFtdcOrderActionField pOrderAction, ref CThostFtdcRspInfoField pRspInfo)
        {
            if (null != OnErrRtnOrderAction)
            {
                OnErrRtnOrderAction(this, new OnErrRtnOrderActionArgs(pTraderApi, ref pOrderAction, ref pRspInfo));
            }
        }

        private void OnErrRtnOrderInsert_callback(IntPtr pTraderApi, ref CThostFtdcInputOrderField pInputOrder, ref CThostFtdcRspInfoField pRspInfo)
        {
            if (null != OnErrRtnOrderInsert)
            {
                OnErrRtnOrderInsert(this, new OnErrRtnOrderInsertArgs(pTraderApi, ref pInputOrder, ref pRspInfo));
            }
        }

        private void OnRspError_callback(IntPtr pApi, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (null != OnRspError)
            {
                OnRspError(this,new OnRspErrorArgs(pApi,ref pRspInfo,nRequestID,bIsLast));
            }
        }

        private void OnRspOrderAction_callback(IntPtr pTraderApi, ref CThostFtdcInputOrderActionField pInputOrderAction, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (null != OnRspOrderAction)
            {
                OnRspOrderAction(this, new OnRspOrderActionArgs(pTraderApi, ref pInputOrderAction, ref pRspInfo, nRequestID, bIsLast));
            }
        }

        private void OnRspOrderInsert_callback(IntPtr pTraderApi, ref CThostFtdcInputOrderField pInputOrder, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (null != OnRspOrderInsert)
            {
                OnRspOrderInsert(this, new OnRspOrderInsertArgs(pTraderApi, ref pInputOrder, ref pRspInfo, nRequestID, bIsLast));
            }
        }

        private void OnRspQryDepthMarketData_callback(IntPtr pTraderApi, ref CThostFtdcDepthMarketDataField pDepthMarketData, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (null != OnRspQryDepthMarketData)
            {
                OnRspQryDepthMarketData(this, new OnRspQryDepthMarketDataArgs(pTraderApi, ref pDepthMarketData, ref pRspInfo, nRequestID, bIsLast));
            }
        }

        private void OnRspQryInstrument_callback(IntPtr pTraderApi, ref CThostFtdcInstrumentField pInstrument, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (null != OnRspQryInstrument)
            {
                OnRspQryInstrument(this, new OnRspQryInstrumentArgs(pTraderApi, ref pInstrument, ref pRspInfo, nRequestID, bIsLast));
            }
        }

        private void OnRspQryInstrumentCommissionRate_callback(IntPtr pTraderApi, ref CThostFtdcInstrumentCommissionRateField pInstrumentCommissionRate, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (null != OnRspQryInstrumentCommissionRate)
            {
                OnRspQryInstrumentCommissionRate(this, new OnRspQryInstrumentCommissionRateArgs(pTraderApi, ref pInstrumentCommissionRate, ref pRspInfo, nRequestID, bIsLast));
            }
        }

        private void OnRspQryInstrumentMarginRate_callback(IntPtr pTraderApi, ref CThostFtdcInstrumentMarginRateField pInstrumentMarginRate, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (null != OnRspQryInstrumentMarginRate)
            {
                OnRspQryInstrumentMarginRate(this, new OnRspQryInstrumentMarginRateArgs(pTraderApi, ref pInstrumentMarginRate, ref pRspInfo, nRequestID, bIsLast));
            }
        }

        private void OnRspQryInvestorPosition_callback(IntPtr pTraderApi, ref CThostFtdcInvestorPositionField pInvestorPosition, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (null != OnRspQryInvestorPosition)
            {
                OnRspQryInvestorPosition(this, new OnRspQryInvestorPositionArgs(pTraderApi, ref pInvestorPosition, ref pRspInfo, nRequestID, bIsLast));
            }
        }

        private void OnRspQryInvestorPositionDetail_callback(IntPtr pTraderApi, ref CThostFtdcInvestorPositionDetailField pInvestorPositionDetail, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (null != OnRspQryInvestorPositionDetail)
            {
                OnRspQryInvestorPositionDetail(this, new OnRspQryInvestorPositionDetailArgs(pTraderApi, ref pInvestorPositionDetail, ref pRspInfo, nRequestID, bIsLast));
            }
        }

        private void OnRspQryOrder_callback(IntPtr pTraderApi, ref CThostFtdcOrderField pOrder, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (null != OnRspQryOrder)
            {
                OnRspQryOrder(this, new OnRspQryOrderArgs(pTraderApi, ref pOrder, ref pRspInfo, nRequestID, bIsLast));
            }
        }

        private void OnRspQryTrade_callback(IntPtr pTraderApi, ref CThostFtdcTradeField pTrade, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (null != OnRspQryTrade)
            {
                OnRspQryTrade(this, new OnRspQryTradeArgs(pTraderApi, ref pTrade, ref pRspInfo, nRequestID, bIsLast));
            }
        }

        private void OnRspQryTradingAccount_callback(IntPtr pTraderApi, ref CThostFtdcTradingAccountField pTradingAccount, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            if (null != OnRspQryTradingAccount)
            {
                OnRspQryTradingAccount(this, new OnRspQryTradingAccountArgs(pTraderApi, ref pTradingAccount, ref pRspInfo, nRequestID, bIsLast));
            }
        }

        private void OnRtnInstrumentStatus_callback(IntPtr pTraderApi, ref CThostFtdcInstrumentStatusField pInstrumentStatus)
        {
            if (null != OnRtnInstrumentStatus)
            {
                OnRtnInstrumentStatus(this, new OnRtnInstrumentStatusArgs(pTraderApi, ref pInstrumentStatus));
            }
        }

        private void OnRtnOrder_callback(IntPtr pTraderApi, ref CThostFtdcOrderField pOrder)
        {
            if (null != OnRtnOrder)
            {
                OnRtnOrder(this, new OnRtnOrderArgs(pTraderApi, ref pOrder));
            }
        }

        private void OnRtnTrade_callback(IntPtr pTraderApi, ref CThostFtdcTradeField pTrade)
        {
            if (null != OnRtnTrade)
            {
                OnRtnTrade(this, new OnRtnTradeArgs(pTraderApi, ref pTrade));
            }
        }
    }
}
