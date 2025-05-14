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

namespace SCRANKWeb.Models
{
    public class HighSchoolFootballRankingsModel
    {

        public DataTable dtRankings { get; set; } = new();
        public DataTable dtSeasons { get; set; } = new();
        public DataTable dtClasses { get; set; } = new();
        public DataTable dtDistricts { get; set; } = new();
        public DataTable dtDistrictStandings { get; set; } = new();

        public string strPathStart = "..\\SCRANKWeb\\wwwroot\\xmldata\\";
        public int intSeason { get; set; } = 0;
        public string strClass { get; set; } = "";
        public string strRankingView { get; set; } = "";
        public string strState { get; set; } = "";
        public string strStateLong { get; set; } = "";
        public string strStateLogo { get; set; } = "";


        public void GetRankings(string pstrClass, int pintSeason, string pstrState, string pstrOrderBy)
        {   
            string strPath = strPathStart + pstrState + "Football\\Rankings\\" + pintSeason.ToString() + pstrClass + pstrOrderBy +".xml";
            dtRankings = new DataTable("dtRanking");
            dtRankings.ReadXml(strPath);                   
        }

        public void GetClasses(int pintSeason, string pstrState)
        {
            string strPath = strPathStart + pstrState + "Football\\General\\" + pintSeason.ToString() + "Classes.xml";
            dtClasses = new DataTable("dtClasses");
            dtClasses.ReadXml(strPath);
        }

        public void GetDistricts(int pintSeason, string pstrState, string pstrClass)
        {

            string strPath = strPathStart + pstrState + "Football\\General\\" + pintSeason.ToString() + pstrClass + "Districts.xml";           
            dtDistricts = new DataTable("dtDistricts");
            dtDistricts.ReadXml(strPath);
        }

        public void GetDistrictStandings(int pintSeason, string pstrState)
        {
            string strPath = strPathStart + pstrState + "Football\\Rankings\\" + pintSeason.ToString() + "DistrictStandings.xml";
            dtDistrictStandings = new DataTable("dtDistrictStandings");
            dtDistrictStandings.ReadXml(strPath);
        }

        public void GetSeasons(string pstrState)
        {
            string strPath = strPathStart + pstrState + "Football\\General\\Seasons.xml";
            dtSeasons = new DataTable("dtSeasons");
            dtSeasons.ReadXml(strPath);
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
