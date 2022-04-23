using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SALES
{
    class TBL_BRANCH
    {
        SQLServer Sqlc;

        public TBL_BRANCH()
        {

        }
        public Int32 ID_BRANCH
        {
            get;
            set;
        }
        public string BRANCH_NAME
        {
            get;
            set;
        }
        public string BRANCH_ADRESS
        {
            get;
            set;
        }
        public string BRANCH_MOBILE_1
        {
            get;
            set;
        }
        public string BRANCH_MOBILE_2
        {
            get;
            set;
        }
        public string BRANCH_MANGER
        {
            get;
            set;
        }
        public string BRANCH_DETAILS
        {
            get;
            set;
        }
        public DataSet Select_braches(string branch_name)
        {
            string str1 = String.Format("Select * from TBL_BRANCH WHERE BRANCH_NAME LIKE N'%" + branch_name + "%'");

            return Sqlc.runSQLDataSet(str1);
            //return Sqlc.runSQLDataSet("Select * from TBL_BRANCH WHERE BRANCH_NAME LIKE N'%" + branch_name + "%'");
        }
        public List<TBL_BRANCH> List_Branch()
        {
            List<TBL_BRANCH> List_Class = new List<TBL_BRANCH>();
            String SQL = String.Format("Select * From TBL_BRANCH ");
            DataSet Dts = Sqlc.runSQLDataSet(SQL);

            foreach (DataRow Row in Dts.Tables[0].Rows)
            {
                TBL_BRANCH TBLBRANCH = new TBL_BRANCH();
                TBLBRANCH.ID_BRANCH = Convert.ToInt32(Row["ID_BRANCH"].ToString());
                TBLBRANCH.BRANCH_NAME = Row["BRANCH_NAME"].ToString();
                TBLBRANCH.BRANCH_ADRESS = Row["BRANCH_ADRESS"].ToString();
                TBLBRANCH.BRANCH_MOBILE_1 = Row["BRANCH_MOBILE_1"].ToString();
                TBLBRANCH.BRANCH_MOBILE_2 = Row["BRANCH_MOBILE_2"].ToString();
                TBLBRANCH.BRANCH_MANGER = Row["BRANCH_MANGER"].ToString();
                TBLBRANCH.BRANCH_DETAILS = Row["BRANCH_DETAILS"].ToString();
                ////tblClient.fld_Client_Picture = Row["fld_Client_Picture"].ToString();
                //TBL_BRANCH.fld_Client_RecordBy = Row["fld_Client_RecordBy"].ToString();

                List_Class.Add(TBLBRANCH);
            }
            return List_Class;
        }
        public Boolean FUN_Save()
        {
            String SQL = String.Format("Insert Into TBL_BRANCH Values ({0}, '{1}', '{2}', '{3}', '{4}', '{5}','{6}')",
                                        ID_BRANCH, BRANCH_NAME, BRANCH_ADRESS, BRANCH_MOBILE_1, BRANCH_MOBILE_2, BRANCH_MANGER, BRANCH_DETAILS);
            Sqlc.runSQLString(SQL);
            return true;
        }
        public Boolean FUN_Update(Int32 SE)
        {
            String SQL = String.Format("Update  TBL_BRANCH set " + "BRANCH_NAME='{0}'," +
                                                                "BRANCH_ADRESS ='{1}'," +
                                                                "BRANCH_MOBILE_1 ='{2}'," +
                                                                "BRANCH_MOBILE_2 ='{3}'," +
                                                                "BRANCH_MANGER ='{4}'," +
                                                                "BRANCH_DETAILS ='{5}' " +
                                                                                            "Where ID_BRANCH = " + SE,
                                                                                                BRANCH_NAME,
                                                                                                BRANCH_ADRESS,
                                                                                                BRANCH_MOBILE_1,
                                                                                                BRANCH_MOBILE_2,
                                                                                                BRANCH_MANGER,
                                                                                                BRANCH_DETAILS);
            Sqlc.runSQLString(SQL);
            return true;
        }
        public DataSet Fun_DeleteSE(Int32 SE)
        {
            String SQL = String.Format("Delete from TBL_BRANCH where id_branch = " + SE);
            return Sqlc.runSQLDataSet(SQL);
        }
        public override string ToString()
        {
            return BRANCH_NAME;
        }
    }
}
