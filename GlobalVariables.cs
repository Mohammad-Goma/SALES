using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SALES
{
    class GlobalVariables
    {
        public Int64 OfficeSE = 1;
        public int Test_Edit = 0;
        public Int64 MAXID = 0;
        public String Path_file = "";
        public int DataG_I = 0;
        public String EventName = "";

        public DateTime DT_Now = DateTime.Now;
        public Int64 DateNowN = 0;
        public string DateNowGS = "";
        public string DateNowHS = "";
        //public String Time_NowS;
        //public Int64 Time_NowN;

        public int Select_Index = 0;
        public int DataGRow_Select = 0;

        public Int64 Fiscalyear = 0;

        public GlobalVariables(SQLServer SQLCP)                         // هنا لابد من اضافة كل الجداول 
        {
            //tBLBRANCH = new TBL_BRANCH();
        }

        //public TBL_BRANCH tBLBRANCH;

        //public DataSet dsbranch;

        //public DataRow dtrbranch;

    }
}
