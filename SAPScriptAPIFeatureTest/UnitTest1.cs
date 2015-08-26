using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAPAutomation;
using SAPFEWSELib;
using System.Threading;

namespace SAPScriptAPIFeatureTest
{
    public delegate void OnRequestErrorHandler();
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            Test t = new Test();
            t.RequestError += t_RequestError;
            
            var session = SAPTestHelper.Current.SAPGuiSession;
            bool act = SAPTestHelper.Current.SAPGuiSession.IsActive;
            session.TestToolMode = 1;
            session.StartTransaction("VA02");
            session.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiCTextField>("VBAK-VBELN").Text = "72171235281";
            session.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiButton>("btn[5]").Press();
            var data = SAPTestHelper.Current.ScreenDatas;
        }

        void t_RequestError()
        {
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiCTextField>("VBAK-VBELN").Text = "7217123528";
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiButton>("btn[5]").Press();
        }

        
    }

    public class Test
    {
        private GuiSession _session;
        public Test()
        {
            SAPTestHelper.Current.SetSession();
            _session = SAPTestHelper.Current.SAPGuiSession;
            _session.EndRequest += _session_EndRequest;
        }

        void _session_EndRequest(GuiSession Session)
        {
            GuiStatusbar status = _session.FindById<GuiStatusbar>("wnd[0]/sbar");
            if (status != null)
            {
                switch (status.MessageType)
                {
                    case "E":
                        if (RequestError != null)
                        {
                            RequestError();
                            
                            Thread.Sleep(1000);
                        }
                            
                        break;
                    case "S":

                        break;
                    default:

                        break;
                }
            }
            
        }

        public event OnRequestErrorHandler RequestError;

        
    }
}
