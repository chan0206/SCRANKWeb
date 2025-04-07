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

        public string strConnectionString = "Server=DESKTOP-6RCKDG6\\SurfaceDB;Database=SCRANK;Integrated Security=True;Trust Server Certificate=True";

        public string strSeason { get; set; } = "";
        public string strConference { get; set; } = "";
        public string strRankingView { get; set; } = "";


        public void GetRankings(string pstrSeason, string pstrOrderBy)
        {
            string strQuery = "select m.fstrSchoolName, " +
                      "m.flngPowerRanking, " +
                      "d.fintWins, " +
                      "d.fintLosses, " +
                      "d.fintConferenceWins, " +
                      "d.fintConferenceLosses, " +
                      "m.flngDominanceRating, " +
                      "m.flngAdjustedStrengthOfVictory " +

                      "from tblCollegeBasketballTeamMetrics M, " +
                      "tblCollegeBasketballTeamData D, " +
                      "tblCollegeInfo I " +

                      "where m.fstrSchoolName = D.fstrSchoolName " +
                      "and m.fstrSchoolName = I.fstrSchoolName " +
                      "and m.fintSeason = d.fintSeason " +
                      "and m.fintSeason = " + pstrSeason + " "+
                      "order by m."+pstrOrderBy+" desc";


            dtRankings = new DataTable();
            CallQuery(strQuery, dtRankings, strConnectionString);
            
            
        }

        public void GetSeasons()
        {
            dtSeasons = new DataTable();
            dtSeasons.Columns.Add("fstrSeason");

            dtSeasons.Rows.Add("2025");

        }

        public void setSeason(string pstrSeason)
        {
            strSeason = pstrSeason;

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
