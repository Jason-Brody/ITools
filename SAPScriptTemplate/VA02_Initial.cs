using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPAutomation;
using SAPFEWSELib;

namespace SAPScriptTemplate
{
    public class Sales_Initial
    {
        public string Order 
        { 
            get
            {
                return SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtVBAK-VBELN").Text;
            }
            set 
            {
                SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtVBAK-VBELN").Text = value;
            }
        }

        public void Sales()
        {
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiToolbar>("tbar[1]").FindByName<GuiButton>("btn[5]").Press();
        }
    }

    public class VA02_Sales_Initial:Sales_Initial
    {

    }

    public class Test
    {
        public void testOrder()
        {
            VA02_Sales_Initial screen = new VA02_Sales_Initial();
            screen.Order = "ABC";
            screen.Sales();
        }
    }
}
