using SAPAutomation;
using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITools
{
    class ScriptTemplate
    {
       
        public void Run(List<Screen_1001_Data> datas)
        {
            datas = new List<Screen_1001_Data>();
            var data = datas.First();
            data.Type = "m";
            data.From = "cny";
            data.To = "usd";
            data.Date = "01.08.2015";
            foreach(var d in datas)
            {
                recordAction(d);
            }
        }

        public void recordAction(Screen_1001_Data data)
        {
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").ResizeWorkingPane(130, 31, false);
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiOkCodeField>("wnd[0]/tbar[0]/okcd").Text = "se16";
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").SendVKey(0);
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtDATABROWSE-TABLENAME").Text = "tcurr";
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtDATABROWSE-TABLENAME").CaretPosition = 5;
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").SendVKey(0);
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI1-LOW").Text = data.Type;
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI2-LOW").Text = data.From;
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI3-LOW").Text = data.To;
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiTextField>("wnd[0]/usr/txtI4-LOW").SetFocus();
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiTextField>("wnd[0]/usr/txtI4-LOW").CaretPosition = 0;
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").SendVKey(2);
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiModalWindow>("wnd[1]").Close();
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiTextField>("wnd[0]/usr/txtI4-LOW").Text = data.Date;
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiTextField>("wnd[0]/usr/txtI4-LOW").CaretPosition = 10;
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiButton>("wnd[0]/tbar[1]/btn[8]").Press();
        }

    }

    public class Screen_1001_Data
    {
        public string Type { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Date { get; set; }
    }
}
