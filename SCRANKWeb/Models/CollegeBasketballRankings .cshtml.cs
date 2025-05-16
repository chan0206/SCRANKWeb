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
    public class CollegeBasketballRankingsModel
    {

        public DataTable dtRankings { get; set; } = new();
        public DataTable dtSeasons { get; set; } = new();

        public string strPathStart = "https://scranksports-e0ahaxcahddyeaam.centralus-01.azurewebsites.net/xmldata/";

        //public string strPathStart = "..\\SCRANKWeb\\wwwroot\\xmldata\\";
        public string intSeason { get; set; } = "";
        public string strConference { get; set; } = "";
        public string strRankingView { get; set; } = "";


        public void GetRankings(string pintSeason, string pstrOrderBy)
        {
            string strPath = strPathStart + "MensCBB/Rankings/" + pintSeason.ToString() + pstrOrderBy + ".xml";
            dtRankings = new DataTable("dtRankings");
            dtRankings.ReadXml(strPath);          
            
        }

        public void GetSeasons()
        {
            string strPath = strPathStart + "MensCBB/General/Seasons.xml";
            dtSeasons = new DataTable("dtSeasons");
            dtSeasons.ReadXml(strPath);
        }

        public void setSeason(string pintSeason)
        {
            intSeason = pintSeason;

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
