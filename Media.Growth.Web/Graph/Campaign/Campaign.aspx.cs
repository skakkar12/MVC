using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;


namespace WebApplication3.Report.Campaign
{
    public partial class Campaign : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Render_Chart();
        }

               
        protected void Render_Chart()
        {
            //initialising sql variables
            string constring;
            SqlConnection myConn;
            string commandText;
            SqlCommand myComm;
            SqlDataReader reader;


            double Budget = 80000;
            StringBuilder Periodbuildstring = new StringBuilder();
            StringBuilder databuildstring = new StringBuilder();
            StringBuilder fieldbuildstring = new StringBuilder();



            //get the data for the period and other parameters in arrray                        done
            //call optimiser and it will give optspends                                         done
            //initalise the highchart and generate the graph                                    done
            //save the data to the campaign table for the campaign created
            //save the data to the other media table (campaign id, spendouts )etc fields



            //getting the data from the database
            constring = WebConfigurationManager.ConnectionStrings["phasingSQL"].ToString();
            myConn = new SqlConnection(constring);
            commandText = "select period, Salesbase, carryover, Upliftmax, spendhalf from MediaChannelPlan";
            myComm = new SqlCommand(commandText, myConn);
            myConn.Open();
            reader = myComm.ExecuteReader();
            

            Periodbuildstring.Append(",\\,");
            databuildstring.Append(",,,,\\,");


            //Creating the Fields Input
            string Fields = ",Period,SalesBase,Carryover,UpliftMax,SpendHalf";
            char delimiter = ',';
            string[] FieldsInput = Fields.Split(delimiter);
            

            Object[] chartValues = new Object[6]; // declare an object for the chart rendering  
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Periodbuildstring.Append(reader["Period"]);
                    Periodbuildstring.Append("\\,");
                    databuildstring.Append(reader["SalesBase"]);
                    databuildstring.Append(",");
                    databuildstring.Append(reader["Carryover"]);
                    databuildstring.Append(",");
                    databuildstring.Append(reader["UpliftMax"]);
                    databuildstring.Append(",");
                    databuildstring.Append(reader["SpendHalf"]);
                    databuildstring.Append("\\,");
                }
             
            }

            
            //Creating the Text data
            Periodbuildstring.Remove(Periodbuildstring.Length - 2, 2);
            string TextData = Periodbuildstring.ToString();
            char txtdelimiter = '\\';
            string[] TextRows = TextData.Split(txtdelimiter);
                   
            char delimit = ',';
            string[,] TextInput = new string[TextRows.Count(), TextRows[0].Split(delimit).Count()];
             for (int i = 0; i < TextRows.Count(); i++)
            {
                for (int j = 1; j < TextRows[i].Split(delimit).Count(); j++)
                {
                    TextInput[i, j] = TextRows[i].Split(delimit)[j];
                }
            }
                        

             
            //Creating the Numeric data
            databuildstring.Remove(databuildstring.Length - 2, 2);
            string NumData = databuildstring.ToString();
             char numdelimiter = '\\';
            string[] NumRows = NumData.Split(numdelimiter);

             
            double[,] NumInput = new double[NumRows.Count(), TextRows[0].Split(delimit).Count() + 3];
            for (int i = 0; i < NumRows.Count(); i++)
            {
                for (int j = 1; j < NumRows[i].Split(delimit).Count(); j++)
                {
                    if (NumRows[i].Split(delimit)[j] == "")
                    {
                        NumInput[i, j] = double.NaN;

                    }
                    else
                    {
                        NumInput[i, j] = Convert.ToDouble(NumRows[i].Split(delimit)[j]);
                    }
                }
            }
            
            
            //Calling the Optimiser and setting Inputs and creating Optimising the budget
            Optimiser.Opt testOpt = new Optimiser.Opt();
            string optSuccess;
            
            optSuccess = testOpt.SetInputs(FieldsInput, TextInput, NumInput).ToString();
            optSuccess = testOpt.Optimise(Budget);

            double[] result = testOpt.GetOptSpends();

            for (int i = 0; i < result.Length-1; i++)
            {
                chartValues[i] = result[i];
            }
                                 
                        
            //Declaring Highchart chart and creating the Highcharts
            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
                .InitChart(new Chart
                {
                    DefaultSeriesType = ChartTypes.Column
                })
                .SetTitle(new Title
                {
                    //Text = "Monthly Number of Pizza",
                    Text = "Monthly Budget",

                    X = -20
                })
                .SetSubtitle(new Subtitle
                {
                    Text = "Source: Mindshare database",
                    X = -20
                })
                .SetXAxis(new XAxis
                {
                    Categories = new[] { "W1", "W2", "W3", "W4", "W5", "W6" }
                })
                 .SetPlotOptions(new PlotOptions { Column = new PlotOptionsColumn { Stacking = Stackings.Normal } })
                .SetSeries(new[]
                        {
                      
                            //Name = "# Pizza",
                            //Data = new Data(chartValues)   // Here we put the dbase data into the chart       
                            new Series { Name = "Ads ON TV", Data = new Data(chartValues) },
                            new Series { Name = "Newspaper ADS", Data = new Data(chartValues) },
                            new Series { Name = "Magazine Ads", Data = new Data(chartValues) },
                            new Series { Name = "Social Networking Ads", Data = new Data(chartValues) }

                    });

            chrtMyChart.Text = chart.ToHtmlString(); // Let's visualize the chart into the webform.
        }

    }
}