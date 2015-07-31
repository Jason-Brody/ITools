using SAPAutomation;
using SAPAutomation.Framework.Attributes;
using SAPAutomation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPAutomation.Extension;
using SAPFEWSELib;

namespace SAPScriptTemplate
{
    public class DemoScript 
    {
        /// Exchange Rate Type
       
        public System.String RateType { get; set; }
        /// From currency
       
        public System.String CurFrom { get; set; }
        /// To-currency
       
        public System.String CurTo { get; set; }

        public static void RunAction(DemoScript Data)
        {
            
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiOkCodeField>("wnd[0]/tbar[0]/okcd").Text = "se16";
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").SendVKey(0);
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtDATABROWSE-TABLENAME").Text = "TCURR";
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiButton>("wnd[0]/tbar[0]/btn[0]").Press();
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI1-LOW").Text = Data.RateType;
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI2-LOW").Text = Data.CurFrom;
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI3-LOW").Text = Data.CurTo;
            SAPTestHelper.Current.TakeScreenShot(@"\\yanzhou17.asiapacific.hpqcorp.net\Shared Folder\8.jpg");
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI3-LOW").SetFocus();
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI3-LOW").CaretPosition = 3;
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiButton>("wnd[0]/tbar[1]/btn[8]").Press();
        }


    }
}
