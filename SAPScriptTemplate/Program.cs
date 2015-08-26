using SAPAutomation;
using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPAutomation.Framework.Attributes;
using SAPAutomation.Interfaces;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;

namespace SAPScriptTemplate
{
    class Program
    {
        static void Main(string[] args)
        {

            
            SAPTestHelper.Current.SetSession();

            SAPTestHelper.Current.SAPGuiSession.StartTransaction("VA01");

            SAPTestHelper.Current.SAPGuiSession.StartRequest += (s) => {
                SAPTestHelper.Current.TakeScreenShot(SAPTestHelper.Current.ScreenDatas.Count.ToString() + ".jpg");
                Console.WriteLine(SAPTestHelper.Current.ScreenDatas.Count);
            };
            //Console.ReadLine();
            //SAPTestHelper.Current.TurnScreenLog(true);
            SAPTestHelper.Current.SAPGuiSession.EndTransaction();

            DemoScript script = new DemoScript();
            script.CurFrom = "EUR";
            script.CurTo = "USD";
            script.RateType = "M";
            DemoScript.RunAction(script);

            //SAPTestHelper.Current.TurnScreenLog(false);
            var screen = SAPTestHelper.Current.ScreenDatas;
            XmlSerializer xs = new XmlSerializer(typeof(List<ScreenData>));
            using (FileStream fs = new FileStream("test.xml", FileMode.Create))
            {

                xs.Serialize(fs, new List<ScreenData>(screen));
            }

        }
    }


   
}
