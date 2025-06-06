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
    public class CollegeBasketballModel
    {

        public DataTable dtRankings { get; set; } = new();
        public DataTable dtSeasons { get; set; } = new();
        public DataTable dtConferences { get; set; } = new();
        public DataTable dtConferenceStandings { get; set; } = new();
        public DataTable dtTeamViewInfo { get; set; } = new();

        public string strPathStart = "";

        public string strPathStartWeb = "https://scranksports-e0ahaxcahddyeaam.centralus-01.azurewebsites.net/xmldata/";

        public string strPathStartLocal = "..\\SCRANKWeb\\wwwroot\\xmldata\\";
        public Int64 intSeason { get; set; } = 0;
        public string strConference { get; set; } = "";
        public string strRankingView { get; set; } = "";
        public string strTeam { get; set; } = "";

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


        public void GetRankings(Int64 pintSeason, string pstrOrderBy)
        {
            SetPath(ref strPathStart);
            string strPath = strPathStart + "MensCBB/Rankings/" + pintSeason.ToString() + pstrOrderBy + ".xml";
            dtRankings = new DataTable("dtRankings");
            dtRankings.ReadXml(strPath);          
            
        }

        public void GetConferenceStandings(Int64 pintSeason)
        {
            SetPath(ref strPathStart);
            string strPath = strPathStart + "MensCBB/Rankings/" + pintSeason.ToString() + "ConferenceStandings.xml";
            dtConferenceStandings = new DataTable("dtConferenceStandings");
            dtConferenceStandings.ReadXml(strPath);

        }

        public void GetSeasons()
        {
            SetPath(ref strPathStart);
            string strPath = strPathStart + "MensCBB/General/Seasons.xml";
            dtSeasons = new DataTable("dtSeasons");
            dtSeasons.ReadXml(strPath);
        }

        public void GetConferences(Int64 pintSeason)
        {
            SetPath(ref strPathStart);
            string strPath = strPathStart + "MensCBB/General/" + pintSeason.ToString() + "Conferences.xml";
            dtConferences = new DataTable("dtConferences");            
            dtConferences.ReadXml(strPath);
        }

        public void GetTeamViewInfo(int pintSeason, string pstrState)
        {
            SetPath(ref strPathStart);
            string strPath = strPathStart + "MensCBB/Rankings/" + pintSeason.ToString() + "TeamView.xml";
            dtTeamViewInfo = new DataTable("dtTeamViewInfo");
            dtTeamViewInfo.ReadXml(strPath);

        }

        public void setSeason(Int64 pintSeason)
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
