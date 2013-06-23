using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using Skyline.Core.Helper;

namespace Skyline.Core
{
    public class BuilderObjectBiz
    {
        private List<BuilderObject> list;
        private List<File3dattribute> Oraclelist;
        private DataSet ds;
        /// <summary>
        /// 根据信息树ID 查询物体信息
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public List<File3dattribute> GetModelInfo(string ObjectID)
        {
            ds = new DataSet();
            ADODBHelper m_OracleHelper = new ADODBHelper(ADODBHelper.ConfigConnectionString, true);
            ds = m_OracleHelper.OpenDS("select t.* from FILE3DATTRIBUTE t where t.OBJECTID = " + ObjectID + "");
            this.Oraclelist = new List<File3dattribute>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                File3dattribute boTemp = new File3dattribute();
                boTemp.Objectid = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                boTemp.Ysdm = ds.Tables[0].Rows[i][1].ToString();
                boTemp.Yslx = ds.Tables[0].Rows[i][2].ToString();
                boTemp.Mc = ds.Tables[0].Rows[i][3].ToString();
                boTemp.Lxr = ds.Tables[0].Rows[i][4].ToString();
                boTemp.Gxsj = ds.Tables[0].Rows[i][5].ToString();
                boTemp.Lxdh = ds.Tables[0].Rows[i][6].ToString();
                boTemp.Xxdz = ds.Tables[0].Rows[i][7].ToString();
                boTemp.Jlxh = ds.Tables[0].Rows[i][8].ToString();

                this.Oraclelist.Add(boTemp);
            }
            m_OracleHelper.Dispose();
            return this.Oraclelist;
        }
        /// <summary>
        /// 根据信息树ID 查询物体信息
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public List<BuilderObject> GetBuilderInfo(string ObjectID)  
        {
            SqlHelper sh = new SqlHelper();
            ds = new DataSet();


            ds = sh.selectAll("builderObject", "buildObjectID='" + ObjectID + "'");
            list = new List<BuilderObject>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                BuilderObject boTemp = new BuilderObject();
                boTemp.BuilId           = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                boTemp.BuilName         = ds.Tables[0].Rows[i][1].ToString();
                //输出的是第i行的第1列的数据
               // boTemp.BuilItemID       = ds.Tables[0].Rows[i][2].ToString();
                boTemp.BuilHeight       = ds.Tables[0].Rows[i][2].ToString();
                boTemp.BuilArea         = ds.Tables[0].Rows[i][3].ToString();
                boTemp.BuilInfo         = ds.Tables[0].Rows[i][4].ToString();
                boTemp.BuilLongitude    = ds.Tables[0].Rows[i][5].ToString();
                boTemp.BuilLatitude     = ds.Tables[0].Rows[i][6].ToString();
                boTemp.BuidObjectID = ds.Tables[0].Rows[i][7].ToString();
                //Tables、Rows都是集合，所以它们可以通过索引来访问其中的成员
                list.Add(boTemp);
            }

            return list;    
        }

        /// <summary>
        /// 根据条件查询建筑列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<BuilderObject> GetBuilderByName(string where)
        {
            SqlHelper sh = new SqlHelper();
            ds = new DataSet();

            ds = sh.selectAll("builderObject", "buildName like '%" + where + "%'");

            list = new List<BuilderObject>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                BuilderObject boTemp = new BuilderObject();
                boTemp.BuilId = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                boTemp.BuilName = ds.Tables[0].Rows[i][1].ToString();
               // boTemp.BuilItemID = ds.Tables[0].Rows[i][2].ToString();
                boTemp.BuilHeight = ds.Tables[0].Rows[i][2].ToString();
                boTemp.BuilArea = ds.Tables[0].Rows[i][3].ToString();
                boTemp.BuilInfo = ds.Tables[0].Rows[i][4].ToString();
                boTemp.BuilLongitude = ds.Tables[0].Rows[i][5].ToString();
                boTemp.BuilLatitude = ds.Tables[0].Rows[i][6].ToString();
                boTemp.BuidObjectID = ds.Tables[0].Rows[i][7].ToString();
                list.Add(boTemp);
            }

            return list; 
        }

        public List<File3dattribute> GetModelByName(string Name)
        {
            ds = new DataSet();
            ADODBHelper m_OracleHelper = new ADODBHelper(ADODBHelper.ConfigConnectionString, true);
            ds = m_OracleHelper.OpenDS(String.Format("select t.* from FILE3DATTRIBUTE t where t.mc like '%{0}%'", Name));

            this.Oraclelist = new List<File3dattribute>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                File3dattribute boTemp = new File3dattribute();
                boTemp.Objectid = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                boTemp.Ysdm = ds.Tables[0].Rows[i][1].ToString();
                boTemp.Yslx = ds.Tables[0].Rows[i][2].ToString();
                boTemp.Mc = ds.Tables[0].Rows[i][3].ToString();
                boTemp.Lxr = ds.Tables[0].Rows[i][4].ToString();
                boTemp.Gxsj = ds.Tables[0].Rows[i][5].ToString();
                boTemp.Lxdh = ds.Tables[0].Rows[i][6].ToString();
                boTemp.Xxdz = ds.Tables[0].Rows[i][7].ToString();
                boTemp.Jlxh = ds.Tables[0].Rows[i][8].ToString();
                try
                {
                    boTemp.Cx = Convert.ToDouble(ds.Tables[0].Rows[i][44].ToString());
                    boTemp.Cy = Convert.ToDouble(ds.Tables[0].Rows[i][45].ToString());
                }
                catch (Exception)
                {
                    boTemp.Cx = 0;
                    boTemp.Cy = 0;
                  //  throw;
                }
                
                this.Oraclelist.Add(boTemp);
            }
            m_OracleHelper.Dispose();
            return this.Oraclelist;
        }
    }
}
