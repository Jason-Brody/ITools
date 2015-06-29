using SAPAutomation;
using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPAutomation.Extension;
using SAPAutomation.Framework.Attributes;
using SAPAutomation.Interfaces;
using SAPAutomation.Data;
using System.Data;
using System.Diagnostics;

namespace SAPScriptTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            

            //SAPLogon logon = new SAPAutomation.SAPLogon();
            //logon.OpenConnection("serverAddress");
            //logon.Login("UserName", "Password", "Client", "Language");
            //SAPTestHelper.Current.SetSession(logon);

            SAPTestHelper.Current.SetSession();
            DemoScript script = new DemoScript();
            script.CurFrom = "EUR";
            script.CurTo = "USD";
            script.RateType = "M";
            DemoScript.RunAction(script);
        }
    }


   
}
