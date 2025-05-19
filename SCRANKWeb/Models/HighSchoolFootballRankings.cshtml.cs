using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;
//using HtmlAgilityPack;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Data.Common;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace SCRANKWeb.Models
{
    public class HighSchoolFootballRankingsModel
    {

        public DataTable dtRankings { get; set; } = new();
        public DataTable dtSeasons { get; set; } = new();
        public DataTable dtProjectionSeasons { get; set; } = new();
        public DataTable dtClasses { get; set; } = new();
        public DataTable dtDistricts { get; set; } = new();
        public DataTable dtDistrictStandings { get; set; } = new();
        public DataTable dtProjections { get; set; } = new();
        public DataTable dtDistrictProjections { get; set; } = new();

        public string strPathStart = "";

        public string strPathStartWeb = "https://scranksports-e0ahaxcahddyeaam.centralus-01.azurewebsites.net/xmldata/";

        public string strPathStartLocal = "..\\SCRANKWeb\\wwwroot\\xmldata\\";
        public int intSeason { get; set; } = 0;
        public string strClass { get; set; } = "";
        public string strRankingView { get; set; } = "";
        public string strState { get; set; } = "";
        public string strStateLong { get; set; } = "";
        public string strStateLogo { get; set; } = "";

        public void SetPath(ref string pstrPath)
        {
            if (Debugger.IsAttached)
            {
                pstrPath = strPathStartLocal;
            }
            else
            {
                pstrPath = strPathStartWeb;
            }

        }

        public void GetRankings(string pstrClass, int pintSeason, string pstrState, string pstrOrderBy)
        {
            SetPath(ref strPathStart);
            string strPath = strPathStart + pstrState + "Football/Rankings/" + pintSeason.ToString() + pstrClass + pstrOrderBy +".xml";
            dtRankings = new DataTable("dtRanking");
            dtRankings.ReadXml(strPath);                   
        }

        public void GetProjections(string pstrClass, int pintSeason, string pstrState)
        {
            SetPath(ref strPathStart);
            string strPath = strPathStart + pstrState + "Football/Rankings/" + pintSeason.ToString() + pstrClass + "Projections.xml";
            dtProjections = new DataTable("dtProjections");
            dtProjections.ReadXml(strPath);
        }

        public void GetClasses(int pintSeason, string pstrState)
        {
            SetPath(ref strPathStart);
            string strPath = strPathStart + pstrState + "Football/General/" + pintSeason.ToString() + "Classes.xml";
            dtClasses = new DataTable("dtClasses");
            dtClasses.ReadXml(strPath);
        }

        public void GetDistricts(int pintSeason, string pstrState, string pstrClass)
        {

            SetPath(ref strPathStart);
            string strPath = strPathStart + pstrState + "Football/General/" + pintSeason.ToString() + pstrClass + "Districts.xml";           
            dtDistricts = new DataTable("dtDistricts");
            dtDistricts.ReadXml(strPath);
        }

        public void GetDistrictStandings(int pintSeason, string pstrState)
        {
            SetPath(ref strPathStart);
            string strPath = strPathStart + pstrState + "Football/Rankings/" + pintSeason.ToString() + "DistrictStandings.xml";
            dtDistrictStandings = new DataTable("dtDistrictStandings");
            dtDistrictStandings.ReadXml(strPath);
        }

        public void GetSeasons(string pstrState)
        {
            SetPath(ref strPathStart);
            string strPath = strPathStart + pstrState + "Football/General/Seasons.xml";
            dtSeasons = new DataTable("dtSeasons");
            dtSeasons.ReadXml(strPath);
        }

        public void GetProjectionSeasons(string pstrState)
        {
            //SetPath(ref strPathStart);
            //string strPath = strPathStart + pstrState + "Football/General/Seasons.xml";
            dtProjectionSeasons = new DataTable("dtProjectionSeasons");
            dtProjectionSeasons.Columns.Add("fintSeason", typeof(int));
            dtProjectionSeasons.Rows.Add(2025);
            //    dtSeasons.ReadXml(strPath);
        }

        public void setSeason(int pintSeason)
        {
            intSeason = pintSeason;

        }
        public void setStateInfo(string pstrState)
        {
            switch (pstrState)
            {
                case "ID":
                    strStateLong = "Idaho";
                    break;
                case "IA":
                    strStateLong = "Iowa";
                    break;
                case "MT":
                    strStateLong = "Montana";
                    break;
            }

            strStateLogo = "../img/" + strStateLong + "outline.png";

        }

        public static void CallQuery(string pstrQuery, DataTable pdtTable, string pstrConnectionstring)
        {
            using (SqlConnection _con = new SqlConnection(pstrConnectionstring))
            {
                using (SqlCommand _cmd = new SqlCommand(pstrQuery, _con))
                {
                    SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
                    _con.Open();
                    _dap.Fill(pdtTable);
                    _con.Close();
                }
            }
        }
    }
    
}
