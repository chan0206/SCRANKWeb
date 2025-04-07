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

        public string strConnectionString = "Server=DESKTOP-6RCKDG6\\SurfaceDB;Database=SCRANK;Integrated Security=True;Trust Server Certificate=True";

        public string strSeason { get; set; } = "";
        public string strClass { get; set; } = "";
        public string strRankingView { get; set; } = "";
        public string strState { get; set; } = "";
        public string strStateLong { get; set; } = "";
        public string strStateLogo { get; set; } = "";


        public void GetRankings(string pstrClass, string pstrSeason, string pstrState, string pstrOrderBy)
        {
            string strQuery = "select m.fstrSchoolName, " +
                      "'../img/'+ i.fstrBoundURLCode + '.png' as fimgLogo," +
                      "m.flngPowerRanking, " +
                      "d.fintWins, " +
                      "d.fintLosses, " +
                      "d.fintDistrictWins, " +
                      "d.fintDistrictLosses, " +
                      "d.flngRegRPI, " +
                      "m.flngAdjustedStrengthOfVictory " +

                      "from tblHighSchoolFootballMetrics M, " +
                      "tblHighSchoolFootballTeamData D, " + 
                      "tblHighSchoolInfo I " +

                      "where m.fstrSchoolName = D.fstrSchoolName " +
                      "and m.fstrSchoolName = I.fstrSchoolName " +
                      "and m.fintSeason = d.fintSeason " +
                      "and m.fintSeason = " + pstrSeason + " "+
                      "and m.fstrClass = '" + pstrClass +"' " +
                      "and i.fstrState = '" + pstrState +"' " +
                      "order by m."+pstrOrderBy+" desc";


            dtRankings = new DataTable();
            CallQuery(strQuery, dtRankings, strConnectionString);                     
        }

        public void GetClasses(string pstrSeason, string pstrState)
        {
            string strQuery = "select fstrClass " +

                      "from tblHighSchoolFootballStateClass " +

                      "where fintSeason = " + pstrSeason + " "+
                      "and fstrState = '" + pstrState +"' " +
                      "order by fintClassOrder";


            dtClasses = new DataTable();
            CallQuery(strQuery, dtClasses, strConnectionString);
        }

        public void GetSeasons(string pstrState)
        {
            string strQuery = "select fintSeason " +

                      "from tblHighSchoolFootballStateClass " +

                      "where fstrState = '" + pstrState +"' " +
                      "group by fintSeason";

            dtSeasons = new DataTable();
            CallQuery(strQuery, dtSeasons, strConnectionString);

        }

        public void setSeason(string pstrSeason)
        {
            strSeason = pstrSeason;

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
