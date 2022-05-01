using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using TMIS.GeneralClasses;
using System.Data;
using System.Text;

namespace TMIS.Reports
{
    public partial class frmReport : System.Web.UI.Page
    {
        private DbGeneral db = new DbGeneral();
        DatabaseCommunicator dc = new DatabaseCommunicator();
        private DataTable dt = new DataTable();
        private string style = "<style>\r\n.tableHeader{width:100px; background-color:#67D476;font-size:14px;font-weight: bold; text-transform: capitalize; border:1px solid black;word-wrap: break-word;}\r\n.tableCell{background-color: #FFFFFF; text-align:center;Vertical-align:middle;;word-wrap: break-word;font-size:0.7em}.title{ font-size: 1.2em;  font-weight: bold;text-align:center;  color: #000000;  font-style: normal;  font-variant: normal; border:0px solid white;   text-transform: capitalize;  background-color: #FFFFff;}\r\n.tableCellGeneral{background-color: #A0F1AB; text-align:left;word-wrap: break-word;font-size:13px;font-weight: bold; text-transform: capitalize; border:1px solid black;}.tableCellProcurementCol{background-color: #d8e4bc; text-align:center;word-wrap: break-word;font-size:13px;font-weight: bold; text-transform: capitalize; border:1px solid black;}\r\n.tableCellProgram{background-color: #b7dee8; text-align:center;word-wrap: break-word;font-weight: bold; font-size:13px;}\r\n.BudgettableHeader{width:100px; background-color:wheat;font-size:13px;font-weight: bold; text-transform: capitalize; border:1px thin red;word-wrap: break-word;}\r\n.BudgettableCellGeneral{background-color: #da9694;height:30px; text-align:center;word-wrap: break-word;font-size:13px;font-weight: bold; text-transform: capitalize; border:1px thin gray;}\r\n.BudgettableCellProgram{background-color: #92d050;height:26px; text-align:center;word-wrap: break-word;font-size:13px;font-weight: bold; text-transform: capitalize; border:1px thin gray;}\r\n.tableTitle{height:25px; text-align:center;word-wrap: break-word;font-size:19px;font-weight: bold; text-transform: capitalize;}\r\n\r\n.tableSubTitle{height:25px; text-align:center;word-wrap: break-word;font-size:14px;font-weight: bold; text-transform: capitalize;}\r\n\r\n.tableSubTitleCell{height:25px; text-align:center;word-wrap: break-word;font-size:14px;}\r\n\r\n .ProjectListtableHeader{width:100px; background-color: #fffff;font-size:16px;font-weight: bold; text-transform: capitalize; border:1px solid black;word-wrap: break-word;}\r\n.ProjectListtableCellGeneral{background-color: #ffc000; text-align:left;word-wrap: break-word;font-size:13px;font-weight: bold; text-transform: capitalize; border:1px solid black;}\r\n.ProjectListWorkCell{background-color: #ffdbb1; text-align:center;word-wrap: break-word;font-weight: bold; font-size:13px;}\r\n.ProjectListGoodsCell{background-color: #cde4ef; text-align:center;word-wrap: break-word;font-weight: bold; font-size:13px;}\r\n.ProjectListServiceCell{background-color: #f0e68e; text-align:center;word-wrap: break-word;font-weight: bold; font-size:13px;}\r\n.ProjectListCell{background-color: #ffffff; text-align:center;word-wrap: break-word;font-weight: bold; font-size:13px;}\r\n\r\n.ProjectPaymentCostListtableHeader{width:100px; background-color: #688a8b;font-size:16px; color:white;font-weight: bold; text-transform: capitalize; border:1px solid black;word-wrap: break-word;}\r\n.Report6tableCellGeneral{background-color: #f5bcb7; text-align:left;word-wrap: break-word;font-size:13px;color:black;font-weight: bold; text-transform: capitalize; border:1px solid black;}\r\n\r\n.DonorTableHeader{width:100px; background-color: #cde4ef;font-size:16px;font-weight: bold; text-transform: capitalize;word-wrap: break-word;}\r\n</style> <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                return;
            }
            this.fill();
        }
        private void fill()
        {
            this.chklstRegion.DataSource = (object)this.db.SelectRecords("select id,name from Region");
            this.chklstRegion.DataTextField = "name";
            this.chklstRegion.DataValueField = "id";
            this.chklstRegion.DataBind();
            this.chklstDistrict.Enabled = false;
            this.chklstProvince.Enabled = false;
            this.chklstRegion.Enabled = false;

            ddlComponent.DataSource = (object)UserInfo.GetUserComponents();
            ddlComponent.DataTextField = "ComponentName";
            ddlComponent.DataValueField = "ComponentID";
            ddlComponent.DataBind();

        }
        protected void chklstRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fromCheckBoxList = this.getSelectedItemsFromCheckBoxList(" where", "Province", "Region", this.chklstRegion);
            if (!string.IsNullOrEmpty(fromCheckBoxList) && this.ddlReportLevel.SelectedValue != "1")
            {
                this.chklstProvince.DataSource = (object)this.db.SelectRecords("select ProvinceID,ProvinceEngName from Province" + fromCheckBoxList);
                this.chklstProvince.DataTextField = "ProvinceEngName";
                this.chklstProvince.DataValueField = "ProvinceID";
                this.chklstProvince.DataBind();
                
            }
            else
            {
                this.chklstProvince.DataSource = (object)"";
                this.chklstProvince.DataBind();
            }
            if (ddlReportType.SelectedValue == "2")
            {
                GetTraining();
            }
        }
        private string getSelectedItemsFromCheckBoxList(string prefix, string tbl, string column, CheckBoxList lst)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(prefix + " " + tbl + "." + column + " in(");
            int num = 0;
            foreach (ListItem listItem in lst.Items)
            {
                if (listItem.Selected)
                {
                    stringBuilder.Append(listItem.Value + ",");
                    ++num;
                }
            }
            if (num == 0)
            {
                stringBuilder.Clear();
            }
            else
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append(")");
            }
            return stringBuilder.ToString();
        }
        private string getSelectedItemsFromDropDownList(string prefix, string tbl, string column, HtmlSelect lst)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(prefix + " " + tbl + "." + column + " in(");
            int num = 0;
            foreach (ListItem listItem in lst.Items)
            {
                if (listItem.Selected)
                {
                    stringBuilder.Append(listItem.Value + ",");
                    ++num;
                }
            }
            if (num == 0)
            {
                stringBuilder.Clear();
            }
            else
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append(")");
            }
            return stringBuilder.ToString();
        }
        protected void chklstProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fromCheckBoxList = this.getSelectedItemsFromCheckBoxList(" where", "District", "ProvinceID", this.chklstProvince);
            string query = "select DistrictID,DistrictEngName from District" + fromCheckBoxList;
            if (!string.IsNullOrEmpty(fromCheckBoxList))
            {
                this.chklstDistrict.DataSource = (object)this.db.SelectRecords(query);
                this.chklstDistrict.DataTextField = "DistrictEngName";
                this.chklstDistrict.DataValueField = "DistrictID";
                this.chklstDistrict.DataBind();
            }
            else
            {
                this.chklstDistrict.DataSource = (object)"";
                this.chklstDistrict.DataBind();
            }
            if (ddlReportType.SelectedValue == "2")
            {
                GetTraining();
            }
        }
        protected void ddlReportLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlReportLevel.SelectedValue != "-1")
            {
                if (this.ddlReportLevel.SelectedValue == "1")
                {
                    this.chklstRegion.Enabled = true;
                    this.chklstDistrict.Enabled = false;
                    this.chklstProvince.Enabled = false;
                }
                else if (this.ddlReportLevel.SelectedValue == "2")
                {
                    this.chklstDistrict.Enabled = false;
                    this.chklstProvince.Enabled = true;
                    this.chklstRegion.Enabled = true;
                }
                else if (this.ddlReportLevel.SelectedValue == "3")
                {
                    this.chklstDistrict.Enabled = true;
                    this.chklstProvince.Enabled = true;
                    this.chklstRegion.Enabled = true;
                }
                this.ChckBxItemsValue(this.chklstProvince, false);
                this.ChckBxItemsValue(this.chklstDistrict, false);
                this.ChckBxItemsValue(this.chklstRegion, false);
                this.chklstProvince.DataSource = (object)"";
                this.chklstProvince.DataBind();
                this.chklstDistrict.DataSource = (object)"";
                this.chklstDistrict.DataBind();
                if (ddlReportType.SelectedValue == "2")
                {
                    GetTraining();
                    rowTraining.Visible = true;
                }
                else
                {
                    rowTraining.Visible = false;
                }
            }
            else
            {
                this.chklstDistrict.Enabled = false;
                this.chklstProvince.Enabled = false;
                this.chklstRegion.Enabled = false;
                this.ChckBxItemsValue(this.chklstRegion, false);
                this.ChckBxItemsValue(this.chklstProvince, false);
                this.ChckBxItemsValue(this.chklstDistrict, false);
                rowTraining.Visible = false;
            }
            
        }
        private void ChckBxItemsValue(CheckBoxList chklst, bool value)
        {
            foreach (ListItem listItem in chklst.Items)
                listItem.Selected = value;
        }
        private HtmlTable HTMLTable(string percentWidth, int borderWidth, string borderColor)
        {
            return new HtmlTable()
            {
                Width = percentWidth,
                Border = borderWidth,
                BorderColor = borderColor
            };
        }
        private HtmlTableCell Cell(string innerHtml, string cssClass)
        {
            HtmlTableCell htmlTableCell = new HtmlTableCell();
            htmlTableCell.InnerHtml = innerHtml;
            htmlTableCell.Attributes.Add("class", cssClass);
            return htmlTableCell;
        }
        private HtmlTableCell Cell(string innerHtml, string cssClass, string textAlign)
        {
            HtmlTableCell htmlTableCell = new HtmlTableCell();
            htmlTableCell.InnerHtml = innerHtml;
            htmlTableCell.Style.Add(HtmlTextWriterStyle.TextAlign, textAlign);
            if (textAlign.ToLower() == "center")
                htmlTableCell.Style.Add(HtmlTextWriterStyle.VerticalAlign, "middle");
            htmlTableCell.Attributes.Add("class", cssClass);
            return htmlTableCell;
        }
        private HtmlTableCell Cell(string innerHtml, string cssClass, int colSpan)
        {
            HtmlTableCell htmlTableCell = new HtmlTableCell();
            htmlTableCell.InnerHtml = innerHtml;
            htmlTableCell.Attributes.Add("class", cssClass);
            htmlTableCell.ColSpan = colSpan;
            return htmlTableCell;
        }
        private HtmlTableCell Cell(string innerHtml, string cssClass, int colSpan, string textAlign)
        {
            HtmlTableCell htmlTableCell = new HtmlTableCell();
            htmlTableCell.InnerHtml = innerHtml;
            htmlTableCell.Attributes.Add("class", cssClass);
            htmlTableCell.ColSpan = colSpan;
            htmlTableCell.Style.Add(HtmlTextWriterStyle.TextAlign, textAlign);
            if (textAlign.ToLower() == "center")
                htmlTableCell.Style.Add(HtmlTextWriterStyle.VerticalAlign, "middle");
            return htmlTableCell;
        }
        private HtmlTableCell Cell(string innerHtml, string cssClass, int colSpan, int rowSpan)
        {
            HtmlTableCell htmlTableCell = new HtmlTableCell();
            htmlTableCell.InnerHtml = innerHtml;
            htmlTableCell.Attributes.Add("class", cssClass);
            htmlTableCell.ColSpan = colSpan;
            htmlTableCell.RowSpan = rowSpan;
            return htmlTableCell;
        }
        private HtmlTableCell CellRowSpan(string innerHtml, string cssClass, int rowSpan)
        {
            HtmlTableCell htmlTableCell = new HtmlTableCell();
            htmlTableCell.InnerHtml = innerHtml;
            htmlTableCell.Attributes.Add("class", cssClass);
            htmlTableCell.RowSpan = rowSpan;
            return htmlTableCell;
        }
        private HtmlTableCell CellRowSpan(string innerHtml, string cssClass, int rowSpan, string textAlign)
        {
            HtmlTableCell htmlTableCell = new HtmlTableCell();
            htmlTableCell.InnerHtml = innerHtml;
            htmlTableCell.Attributes.Add("class", cssClass);
            htmlTableCell.RowSpan = rowSpan;
            htmlTableCell.Style.Add(HtmlTextWriterStyle.TextAlign, textAlign);
            if (textAlign.ToLower() == "center")
                htmlTableCell.Style.Add(HtmlTextWriterStyle.VerticalAlign, "middle");
            return htmlTableCell;
        }
        public string GetUrl(string imagepath)
        {
            string[] strArray = this.Request.Url.AbsoluteUri.Split('/');
            if (strArray.Length < 2)
                return imagepath;
            string str = strArray[0] + "//";
            for (int index = 2; index < strArray.Length - 1; ++index)
                str = str + strArray[index] + "/";
            return str + imagepath;
        }
        private HtmlTable TitleTable(int colspan, string headerText)
        {
            HtmlTable htmlTable = this.HTMLTable("100", 0, "white");
            HtmlTableRow row = new HtmlTableRow();
            string url1 = this.GetUrl("APPContent/images/Gov.png");
            string url2 = this.GetUrl("APPContent/images/MAIL.jpg");
            row.Cells.Add(this.Cell("<img style='float:right' src='" + url1 + "'", ""));
            row.Cells.Add(this.Cell("<img style='float:left' src='" + url2 + "'", ""));
            htmlTable.Rows.Add(row);
            return htmlTable;
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (ddlReportType.SelectedValue == "1")
                TrainingSummary();
            else if (ddlReportType.SelectedValue == "2")
                TrainingDetail();
            else if (ddlReportType.SelectedValue == "3")
                FieldVisit();
            
        }
        private void TrainingSummary()
        {
            DataTable dtOrganization = db.SelectRecords("select * from Organization order by OrgName");

            HtmlGenericControl htmlGenericControl = new HtmlGenericControl();
            htmlGenericControl.Style.Add(HtmlTextWriterStyle.Direction, "ltr");
            HtmlTable htmlTable1 = this.HTMLTable("100", 0, "");
            htmlTable1.Style.Add(HtmlTextWriterStyle.Direction, "ltr");
            htmlTable1.Rows.Add(new HtmlTableRow()
            {
                Cells = {
                                this.Cell("Ministry of Agriculture, Irrigation and Livestock", "tableTitle", 15+(dtOrganization.Rows.Count*2), "center")
                            }
            });
            htmlTable1.Rows.Add(new HtmlTableRow()
            {
                Cells = {
                                this.Cell("National Horticulture & Livestock Project", "tableTitle", 15+(dtOrganization.Rows.Count*2), "center")
                            }
            });
            htmlTable1.Rows.Add(new HtmlTableRow()
            {
                Cells = {
                                this.Cell("NHLP Training for the year ( "+ddlYear.SelectedItem.Text+" )", "tableTitle", 15+(dtOrganization.Rows.Count*2), "center")
                            }
            });
            
            HtmlTable htmlTable2 = this.HTMLTable("100", 1, "black");
            htmlTable2.Style.Add(HtmlTextWriterStyle.Direction, "ltr");
            HtmlTableRow row1 = new HtmlTableRow();
            htmlTable2.Rows.Add(row1);
            HtmlTableRow row2 = new HtmlTableRow();
            htmlTable2.Rows.Add(row2);
            HtmlTableRow row21 = new HtmlTableRow();
            htmlTable2.Rows.Add(row21);
            row1.Cells.Add(this.CellRowSpan("S.NO", "tableHeader", 3, "center"));
            row1.Cells.Add(this.CellRowSpan("Name of Training", "tableHeader", 3, "center"));
            row1.Cells.Add(this.CellRowSpan("Component", "tableHeader", 3, "center"));
            row1.Cells.Add(this.CellRowSpan("Start Date", "tableHeader", 3, "center"));
            row1.Cells.Add(this.CellRowSpan("End Date", "tableHeader", 3, "center"));
            row1.Cells.Add(this.CellRowSpan("Duration", "tableHeader", 3, "center"));
            row1.Cells.Add(this.Cell("Training Location", "tableHeader", 4, "center"));
            row1.Cells.Add(this.Cell("Type and No of Participants", "tableHeader", (dtOrganization.Rows.Count)*2, "center"));

            row2.Cells.Add(this.CellRowSpan("Region", "tableHeader", 2, "center"));
            row2.Cells.Add(this.CellRowSpan("Province", "tableHeader", 2, "center"));
            row2.Cells.Add(this.CellRowSpan("District", "tableHeader", 2, "center"));
            row2.Cells.Add(this.CellRowSpan("Center", "tableHeader", 2, "center"));
            foreach (DataRow rowOrg in dtOrganization.Rows)
            {
                row2.Cells.Add(this.Cell(rowOrg[1].ToString(), "tableHeader", 2, "center"));
                //Male and Female
                row21.Cells.Add(this.CellRowSpan("Male", "tableHeader", 1, "center"));
                row21.Cells.Add(this.CellRowSpan("Female", "tableHeader", 1, "center"));
            }
            

            row1.Cells.Add(this.Cell("Total Participant", "tableHeader", 3, "center"));
            row2.Cells.Add(this.CellRowSpan("Male", "tableHeader", 2, "center"));
            row2.Cells.Add(this.CellRowSpan("Female", "tableHeader", 2, "center"));
            row2.Cells.Add(this.CellRowSpan("Total", "tableHeader", 2, "center"));

            row1.Cells.Add(this.CellRowSpan("Training Conducted By", "tableHeader", 3, "center"));
            row1.Cells.Add(this.CellRowSpan("Remarks", "tableHeader", 3, "center"));
            
            string[] parameters = { "@condition" };
            string[] values={""};
            if (ddlReportLevel.SelectedValue == "1")
                values[0] = this.getSelectedItemsFromCheckBoxList(" and Year=" + ddlYear.SelectedValue + " and", "view_training_participants", "Region", this.chklstRegion) + this.getSelectedItemsFromDropDownList(" and", "view_training_participants", "Component", ddlComponent);
            else if (ddlReportLevel.SelectedValue == "2")
                values[0] = this.getSelectedItemsFromCheckBoxList(" and Year=" + ddlYear.SelectedValue + " and", "view_training_participants", "TrainingProvince", this.chklstProvince) + this.getSelectedItemsFromDropDownList(" and", "view_training_participants", "Component", ddlComponent);
            else if (ddlReportLevel.SelectedValue == "3")
                values[0] = this.getSelectedItemsFromCheckBoxList(" and Year=" + ddlYear.SelectedValue + " and", "view_training_participants", "TrainingDistrict", this.chklstDistrict) + this.getSelectedItemsFromDropDownList(" and", "view_training_participants", "Component", ddlComponent);
            
            
            dt = dc.selectProcedureExecuter(parameters, values, "spPageReportTrainingSummary");
            if (this.dt.Rows.Count > 0)
            {
                int num1 = 1;
                int[] ArrTotal = new int[dtOrganization.Rows.Count*2];
                int TotalParticipantM = 0;
                int TotalParticipantF = 0;
                foreach (DataRow row3 in (InternalDataCollectionBase)this.dt.Rows)
                {
                    
                    HtmlTableRow row4 = new HtmlTableRow();
                    row4.Cells.Add(this.Cell(num1.ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["TrainingTitle"].ToString(), "tableCell", "left"));
                    row4.Cells.Add(this.Cell(row3["Component"].ToString(), "tableCell", "left"));
                    row4.Cells.Add(this.Cell(Convert.ToDateTime(row3["StartDate"].ToString()).ToString("dd-MMM-yyyy"), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(Convert.ToDateTime(row3["EndDate"].ToString()).ToString("dd-MMM-yyyy"), "tableCell", "center"));
                    if(int.Parse(row3["Duration"].ToString())<=1)
                        row4.Cells.Add(this.Cell(row3["Duration"].ToString()+" Day", "tableCell", "center"));
                    else
                        row4.Cells.Add(this.Cell(row3["Duration"].ToString() + " Days", "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["RegionName"].ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["TrainingProvinceName"].ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["TrainingDistrictName"].ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["TrainingLocation"].ToString(), "tableCell", "center"));
                    int TotalMale = 0;
                    int TotalFemale = 0;
                    int ArrIndex = 0;
                    foreach (DataRow rowOrg in dtOrganization.Rows)
                    {
                       
                        row4.Cells.Add(this.Cell(row3[rowOrg[1].ToString()+"-M"].ToString(), "tableCell", "center"));
                        row4.Cells.Add(this.Cell(row3[rowOrg[1].ToString() + "-F"].ToString(), "tableCell", "center"));

                        if (row3[rowOrg[1].ToString() + "-M"].ToString() != "")
                        {
                            TotalMale += int.Parse(row3[rowOrg[1].ToString() + "-M"].ToString());
                            ArrTotal[ArrIndex] += int.Parse(row3[rowOrg[1].ToString() + "-M"].ToString());
                        }
                        ArrIndex++;
                        if (row3[rowOrg[1].ToString() + "-F"].ToString() != "")
                        {
                            TotalFemale += int.Parse(row3[rowOrg[1].ToString() + "-F"].ToString());
                            ArrTotal[ArrIndex] += int.Parse(row3[rowOrg[1].ToString() + "-F"].ToString());
                        }
                        ArrIndex++;
                    }
                    row4.Cells.Add(this.Cell((TotalMale).ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell((TotalFemale).ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell((TotalMale+TotalFemale).ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["Trainer"].ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["TrainingRemarks"].ToString(), "tableCell", "center"));
                    
                    htmlTable2.Rows.Add(row4);
                    ++num1;
                    TotalParticipantM += TotalMale;
                    TotalParticipantF += TotalFemale;
                    
                }
                //Total Row
                HtmlTableRow rowTotal = new HtmlTableRow();
                rowTotal.Cells.Add(this.Cell("Total", "tableCellGeneral",10, "center"));
                for (int i = 0; i < ArrTotal.Length; i++)
                {
                    rowTotal.Cells.Add(this.Cell(ArrTotal[i].ToString(), "tableCellGeneral", 1, "center"));
                }
                rowTotal.Cells.Add(this.Cell(TotalParticipantM.ToString(), "tableCellGeneral", 1, "center"));
                rowTotal.Cells.Add(this.Cell(TotalParticipantF.ToString(), "tableCellGeneral", 1, "center"));
                rowTotal.Cells.Add(this.Cell((TotalParticipantM+TotalParticipantF).ToString(), "tableCellGeneral", 1, "center"));
                rowTotal.Cells.Add(this.Cell("", "tableCellGeneral", 2, "center"));
                htmlTable2.Rows.Add(rowTotal);
            }
            htmlGenericControl.Controls.Add((Control)htmlTable1);
            htmlGenericControl.Controls.Add((Control)htmlTable2);
            this.Response.Clear();
            this.Response.Buffer = true;
            this.Response.ContentType = "application/vnd.ms-excel";
            this.Response.AddHeader("content-disposition", "attachment;filename=TrainingSummaryReport.xls");
            this.Response.Charset = "utf-8";
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter writer = new HtmlTextWriter((TextWriter)stringWriter);
            this.Response.Write(this.style);
            htmlGenericControl.RenderControl(writer);
            this.Response.Write(stringWriter.ToString());
            this.Response.End();

        }
        private void TrainingDetail()
        {
            string[] TraDayP = { "@TrainingID" };
            string[] TraDayV = { ddlTraining.Value};
            DataTable dtTraDays = dc.selectProcedureExecuter(TraDayP, TraDayV, "SharedGetTrainingDays");
            int TrainingDays = 0;
            if (dtTraDays.Rows.Count > 0)
                TrainingDays = int.Parse(dtTraDays.Rows[0][0].ToString());
            
            string[] parameters = { "@TrainingID" };
            string[] values = { ddlTraining.Value };
            
            dt = dc.selectProcedureExecuter(parameters, values, "spPageReportTrainingDetail");
            
            HtmlGenericControl htmlGenericControl = new HtmlGenericControl();
            htmlGenericControl.Style.Add(HtmlTextWriterStyle.Direction, "ltr");
            HtmlTable htmlTable1 = this.HTMLTable("100", 0, "");
            htmlTable1.Style.Add(HtmlTextWriterStyle.Direction, "ltr");
            htmlTable1.Rows.Add(new HtmlTableRow()
            {
                Cells = {
                                this.Cell("Ministry of Agriculture, Irrigation and Livestock", "tableTitle", 12+TrainingDays*2, "center")
                            }
            });
            htmlTable1.Rows.Add(new HtmlTableRow()
            {
                Cells = {
                                this.Cell("National Horticulture & Livestock Project", "tableTitle", 12+TrainingDays*2, "center")
                            }
            });
            htmlTable1.Rows.Add(new HtmlTableRow()
            {
                Cells = {
                                this.Cell("Training Participants List / Attendance Sheet", "tableTitle", 12+TrainingDays*2, "center")
                            }
            });
            htmlTable1.Rows.Add(new HtmlTableRow() { });
            if (dt.Rows.Count > 0)
            {
                htmlTable1.Rows.Add(new HtmlTableRow()
                {
                    Cells = {
                                this.Cell("Component:", "tableSubTitle", 2, "left"),
                                this.Cell(dt.Rows[0]["ComponentName"].ToString(), "tableSubTitleCell", 6, "left"),
                                this.Cell("Presenter:", "tableSubTitle", 2, "left"),
                                this.Cell(dt.Rows[0]["Trainer"].ToString(), "tableSubTitleCell", 10, "left")
                            }
                });
                DateTime StartDate = new DateTime();
                StartDate = Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString());
                DateTime EndDate = new DateTime();
                EndDate = Convert.ToDateTime(dt.Rows[0]["EndDate"].ToString());
                htmlTable1.Rows.Add(new HtmlTableRow()
                {
                    Cells = {
                                this.Cell("Location:", "tableSubTitle", 2, "left"),
                                this.Cell(dt.Rows[0]["RegionName"].ToString()+", "+dt.Rows[0]["TrainingProvinceName"].ToString()+", "+dt.Rows[0]["TrainingDistrictName"].ToString()+", "+dt.Rows[0]["TrainingLocation"].ToString(), "tableSubTitleCell", 6, "left"),
                                this.Cell("Month:", "tableSubTitle", 2, "left"),
                                this.Cell(StartDate.ToString("MMMM"), "tableSubTitleCell", 6, "left")
                                                          }
                });
                htmlTable1.Rows.Add(new HtmlTableRow()
                {
                    Cells = {
                                this.Cell("Start Date:", "tableSubTitle", 2, "left"),
                                this.Cell(StartDate.ToString("dd-MMM-yyyy"), "tableSubTitleCell", 6, "left"),
                                this.Cell("End Date:", "tableSubTitle", 2, "left"),
                                this.Cell(EndDate.ToString("dd-MMM-yyyy"), "tableSubTitleCell", 6, "left")
                                                          }
                });
                htmlTable1.Rows.Add(new HtmlTableRow()
                {
                    Cells = {
                                this.Cell("Training Title:", "tableSubTitle", 2, "left"),
                                this.Cell(dt.Rows[0]["TrainingTitle"].ToString(), "tableSubTitleCell", 14, "left")
                            }
                });
            }

            HtmlTable htmlTable2 = this.HTMLTable("100", 1, "black");
            htmlTable2.Style.Add(HtmlTextWriterStyle.Direction, "ltr");
            HtmlTableRow row1 = new HtmlTableRow();
            htmlTable2.Rows.Add(row1);
            HtmlTableRow row2 = new HtmlTableRow();
            htmlTable2.Rows.Add(row2);
            
            row1.Cells.Add(this.CellRowSpan("S.NO", "tableHeader", 2, "center"));
            row1.Cells.Add(this.CellRowSpan("Name", "tableHeader", 2, "center"));
            row1.Cells.Add(this.CellRowSpan("Father Name", "tableHeader", 2, "center"));
            row1.Cells.Add(this.CellRowSpan("Gender", "tableHeader", 2, "center"));
            row1.Cells.Add(this.CellRowSpan("Position", "tableHeader", 2, "center"));
            row1.Cells.Add(this.CellRowSpan("Organization", "tableHeader", 2, "center"));
            row1.Cells.Add(this.Cell("Duty Station", "tableHeader", 2, "center"));
            row1.Cells.Add(this.CellRowSpan("Mobile", "tableHeader", 2, "center"));
            row1.Cells.Add(this.CellRowSpan("Email", "tableHeader", 2, "center"));
            row1.Cells.Add(this.Cell("Attendance", "tableHeader", TrainingDays*2, "center"));

            row2.Cells.Add(this.CellRowSpan("Province", "tableHeader", 1, "center"));
            row2.Cells.Add(this.CellRowSpan("District", "tableHeader", 1, "center"));
            for(int j=1;j<=TrainingDays;j++)
            {
                row2.Cells.Add(this.Cell("Day "+j, "tableHeader", 1, "center"));
                row2.Cells.Add(this.Cell("Signature ", "tableHeader", 1, "center"));
            }


            row1.Cells.Add(this.Cell("Total", "tableHeader", 2, "center"));
            row2.Cells.Add(this.Cell("Present", "tableHeader", 1, "center"));
            row2.Cells.Add(this.Cell("Absent", "tableHeader", 1, "center"));

            if (this.dt.Rows.Count > 0)
            {
                int num1 = 1;
                int[] ArrTotal = new int[TrainingDays];
                int GrTotalPresent = 0;
                int GrTotalAbsent = 0;
                foreach (DataRow row3 in (InternalDataCollectionBase)this.dt.Rows)
                {

                    HtmlTableRow row4 = new HtmlTableRow();
                    row4.Cells.Add(this.Cell(num1.ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["Name"].ToString(), "tableCell", "left"));
                    row4.Cells.Add(this.Cell(row3["FatherName"].ToString(), "tableCell", "left"));
                    row4.Cells.Add(this.Cell(row3["Gender"].ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["Position"].ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["OrgName"].ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["ProvinceEngName"].ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["DistrictEngName"].ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["MobileNo"].ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["Email"].ToString(), "tableCell", "center"));
                    int TotalPresent = 0;
                    int TotalAbsent = 0;
                    int ArrIndex = 0;
                    for (int j = 1; j <= TrainingDays;j++ )
                    {
                        if (row3[j.ToString()].ToString() == "1")
                        {
                            row4.Cells.Add(this.Cell("P", "tableCell", "center"));
                            TotalPresent++;
                            ArrTotal[ArrIndex]++;
                        }
                        else if (row3[j.ToString()].ToString() == "0")
                        {
                            row4.Cells.Add(this.Cell("A", "tableCell", "center"));
                            TotalAbsent++;
                        }
                        else
                            row4.Cells.Add(this.Cell("", "tableCell", "center"));
                        
                        row4.Cells.Add(this.Cell("", "tableCell", "center"));

                        //if (row3[rowOrg[1].ToString() + "-M"].ToString() != "")
                        //{
                        //    TotalMale += int.Parse(row3[rowOrg[1].ToString() + "-M"].ToString());
                        //    ArrTotal[ArrIndex] += int.Parse(row3[rowOrg[1].ToString() + "-M"].ToString());
                        //}
                        ArrIndex++;
                        //if (row3[rowOrg[1].ToString() + "-F"].ToString() != "")
                        //{
                        //    TotalFemale += int.Parse(row3[rowOrg[1].ToString() + "-F"].ToString());
                        //    ArrTotal[ArrIndex] += int.Parse(row3[rowOrg[1].ToString() + "-F"].ToString());
                        //}
                        //ArrIndex++;
                    }

                    row4.Cells.Add(this.Cell(TotalPresent.ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(TotalAbsent.ToString(), "tableCell", "center"));
                    GrTotalPresent += TotalPresent;
                    GrTotalAbsent += TotalAbsent;

                    htmlTable2.Rows.Add(row4);
                    ++num1;
                    
                }
                //Total Row
                HtmlTableRow rowTotal = new HtmlTableRow();
                rowTotal.Cells.Add(this.Cell("Total", "tableCellGeneral", 10, "center"));
                for (int i = 0; i < ArrTotal.Length; i++)
                {

                    rowTotal.Cells.Add(this.Cell(ArrTotal[i].ToString() + " (P) - " + ((num1-1) - int.Parse(ArrTotal[i].ToString())).ToString()+" (A)", "tableCellGeneral", 2, "center"));
                }
                rowTotal.Cells.Add(this.Cell(GrTotalPresent.ToString(), "tableCellGeneral", 1, "center"));
                rowTotal.Cells.Add(this.Cell(GrTotalAbsent.ToString(), "tableCellGeneral", 1, "center"));
                htmlTable2.Rows.Add(rowTotal);
            }
            htmlGenericControl.Controls.Add((Control)htmlTable1);
            htmlGenericControl.Controls.Add((Control)htmlTable2);
            this.Response.Clear();
            this.Response.Buffer = true;
            this.Response.ContentType = "application/vnd.ms-excel";
            this.Response.AddHeader("content-disposition", "attachment;filename=TrainingDetail.xls");
            this.Response.Charset = "utf-8";
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter writer = new HtmlTextWriter((TextWriter)stringWriter);
            this.Response.Write(this.style);
            htmlGenericControl.RenderControl(writer);
            this.Response.Write(stringWriter.ToString());
            this.Response.End();

        }
        private void FieldVisit()
        {
            
            HtmlGenericControl htmlGenericControl = new HtmlGenericControl();
            htmlGenericControl.Style.Add(HtmlTextWriterStyle.Direction, "ltr");
            HtmlTable htmlTable1 = this.HTMLTable("100", 0, "");
            htmlTable1.Style.Add(HtmlTextWriterStyle.Direction, "ltr");
            htmlTable1.Rows.Add(new HtmlTableRow()
            {
                Cells = {
                                this.Cell("Ministry of Agriculture, Irrigation and Livestock", "tableTitle", 10, "center")
                            }
            });
            htmlTable1.Rows.Add(new HtmlTableRow()
            {
                Cells = {
                                this.Cell("National Horticulture & Livestock Project", "tableTitle", 10, "center")
                            }
            });
            htmlTable1.Rows.Add(new HtmlTableRow()
            {
                Cells = {
                                this.Cell("Monitoring & Evaluation", "tableTitle", 10, "center")
                            }
            });
            htmlTable1.Rows.Add(new HtmlTableRow()
            {
                Cells = {
                                this.Cell("Field Visits By MAIL/DAIL for the year ( "+ddlYear.SelectedItem.Text+" )", "tableTitle", 10, "center")
                            }
            });

            HtmlTable htmlTable2 = this.HTMLTable("100", 1, "black");
            htmlTable2.Style.Add(HtmlTextWriterStyle.Direction, "ltr");
            HtmlTableRow row1 = new HtmlTableRow();
            htmlTable2.Rows.Add(row1);
            HtmlTableRow row2 = new HtmlTableRow();
            htmlTable2.Rows.Add(row2);

            row1.Cells.Add(this.CellRowSpan("S.No", "tableHeader", 2, "center"));
            row1.Cells.Add(this.CellRowSpan("Date", "tableHeader", 2, "center"));
            row1.Cells.Add(this.CellRowSpan("Purpose of Field Visit", "tableHeader", 2, "center"));
            row1.Cells.Add(this.Cell("Visit Location", "tableHeader", 3, "center"));
            row1.Cells.Add(this.Cell("Facilitated By", "tableHeader", 2, "center"));
            row1.Cells.Add(this.CellRowSpan("Visit Conducted By", "tableHeader", 2, "center"));
            row1.Cells.Add(this.CellRowSpan("Remarks", "tableHeader", 2, "center"));

            row2.Cells.Add(this.CellRowSpan("Region", "tableHeader", 1, "center"));
            row2.Cells.Add(this.CellRowSpan("Province", "tableHeader", 1, "center"));
            row2.Cells.Add(this.CellRowSpan("District", "tableHeader", 1, "center"));
            
            row2.Cells.Add(this.CellRowSpan("Component", "tableHeader", 1, "center"));
            row2.Cells.Add(this.CellRowSpan("Sub Component", "tableHeader", 1, "center"));


            string[] parameters = { "@condition" };
            string[] values = { "" };
            if (ddlReportLevel.SelectedValue == "1")
                values[0] = this.getSelectedItemsFromCheckBoxList(" and Year=" + ddlYear.SelectedValue + " and", "view_field_visit", "Region", this.chklstRegion) + this.getSelectedItemsFromDropDownList(" and", "view_field_visit", "Component", ddlComponent);
            else if (ddlReportLevel.SelectedValue == "2")
                values[0] = this.getSelectedItemsFromCheckBoxList(" and Year=" + ddlYear.SelectedValue + " and", "view_field_visit", "Province", this.chklstProvince) + this.getSelectedItemsFromDropDownList(" and", "view_field_visit", "Component", ddlComponent);
            else if (ddlReportLevel.SelectedValue == "3")
                values[0] = this.getSelectedItemsFromCheckBoxList(" and Year=" + ddlYear.SelectedValue + " and", "view_field_visit", "District", this.chklstDistrict) + this.getSelectedItemsFromDropDownList(" and", "view_field_visit", "Component", ddlComponent);


            dt = dc.selectProcedureExecuter(parameters, values, "spPageReportFieldVisitDtl");
            if (this.dt.Rows.Count > 0)
            {
                int num1 = 1;
                
                foreach (DataRow row3 in (InternalDataCollectionBase)this.dt.Rows)
                {

                    HtmlTableRow row4 = new HtmlTableRow();
                    row4.Cells.Add(this.Cell(num1.ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(Convert.ToDateTime(row3["VisitDate"].ToString()).ToString("dd-MMM-yyyy"), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["VisitPurpose"].ToString(), "tableCell", "left"));
                    row4.Cells.Add(this.Cell(row3["RegionName"].ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["ProvinceEngName"].ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["DistrictEngName"].ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["ComponentName"].ToString(), "tableCell", "center"));
                    row4.Cells.Add(this.Cell(row3["SubComponentName"].ToString(), "tableCell", "center"));

                    row4.Cells.Add(this.Cell(row3["VisitConductedBy"].ToString(), "tableCell", "left")); 
                    row4.Cells.Add(this.Cell(row3["Remarks"].ToString(), "tableCell", "center"));

                    htmlTable2.Rows.Add(row4);
                    ++num1;
                    
                }
                //Total Row
                HtmlTableRow rowTotal = new HtmlTableRow();
                rowTotal.Cells.Add(this.Cell("Total", "tableCellGeneral", 8, "center"));
                rowTotal.Cells.Add(this.Cell((--num1).ToString(), "tableCellGeneral", 2, "center"));
                //rowTotal.Cells.Add(this.Cell(TotalParticipantF.ToString(), "tableCellGeneral", 1, "center"));
                //rowTotal.Cells.Add(this.Cell((TotalParticipantM + TotalParticipantF).ToString(), "tableCellGeneral", 1, "center"));
                //rowTotal.Cells.Add(this.Cell("", "tableCellGeneral", 2, "center"));
                htmlTable2.Rows.Add(rowTotal);
            }
            htmlGenericControl.Controls.Add((Control)htmlTable1);
            htmlGenericControl.Controls.Add((Control)htmlTable2);
            this.Response.Clear();
            this.Response.Buffer = true;
            this.Response.ContentType = "application/vnd.ms-excel";
            this.Response.AddHeader("content-disposition", "attachment;filename=FiedVisitReport.xls");
            this.Response.Charset = "utf-8";
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter writer = new HtmlTextWriter((TextWriter)stringWriter);
            this.Response.Write(this.style);
            htmlGenericControl.RenderControl(writer);
            this.Response.Write(stringWriter.ToString());
            this.Response.End();

        }
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            string id = ((Control)sender).ID;
            if (id.Equals("chkAllRegion"))
            {
                if (this.chkAllRegion.Checked && this.chklstRegion.Enabled)
                {
                    foreach (ListItem listItem in this.chklstRegion.Items)
                        listItem.Selected = true;
                    this.chklstRegion_SelectedIndexChanged((object)null, (EventArgs)null);
                }
                else
                {
                    foreach (ListItem listItem in this.chklstRegion.Items)
                        listItem.Selected = false;
                    this.chklstRegion_SelectedIndexChanged((object)null, (EventArgs)null);
                }
            }
            if (id.Equals("chkAllProvince"))
            {
                if (this.chkAllProvince.Checked && this.chklstProvince.Enabled)
                {
                    foreach (ListItem listItem in this.chklstProvince.Items)
                        listItem.Selected = true;
                    this.chklstProvince_SelectedIndexChanged((object)null, (EventArgs)null);
                }
                else
                {
                    foreach (ListItem listItem in this.chklstProvince.Items)
                        listItem.Selected = false;
                    this.chklstProvince_SelectedIndexChanged((object)null, (EventArgs)null);
                }
            }
            if (id.Equals("chkAllDistrict"))
            {
                if (this.chkAllDistrict.Checked && this.chkAllDistrict.Enabled)
                {
                    foreach (ListItem listItem in this.chklstDistrict.Items)
                        listItem.Selected = true;
                    this.chklstDistrict_SelectedIndexChanged((object)null, (EventArgs)null);
                }
                else
                {
                    foreach (ListItem listItem in this.chklstDistrict.Items)
                        listItem.Selected = false;
                    this.chklstDistrict_SelectedIndexChanged((object)null, (EventArgs)null);
                }
            }
            
        }
        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReportType.SelectedValue == "2")
            {
                rowTraining.Visible = true;
            }
            else
            {
                rowTraining.Visible = false;
            }
            if (ddlReportLevel.SelectedValue == "1")
                this.chklstRegion_SelectedIndexChanged((object)null, (EventArgs)null);
            else if (ddlReportLevel.SelectedValue == "2")
                this.chklstProvince_SelectedIndexChanged((object)null, (EventArgs)null);
            else
                this.chklstDistrict_SelectedIndexChanged((object)null, (EventArgs)null);
        }
        protected void ddlComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTraining();
        }
        
        protected void chklstDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Product load...
            if (ddlReportType.SelectedValue == "2")
            {
                GetTraining();
            }
        }
        public void GetTraining()
        {
            string[] parameters = { "@condition" };
            string[] values = { "" };
            if (ddlReportLevel.SelectedValue == "1")
                values[0] = this.getSelectedItemsFromCheckBoxList(" and Year=" + ddlYear.SelectedValue + " and", "view_training_information", "Region", this.chklstRegion) + this.getSelectedItemsFromDropDownList(" and", "view_training_information", "Component", ddlComponent);
            else if (ddlReportLevel.SelectedValue == "2")
                values[0] = this.getSelectedItemsFromCheckBoxList(" and Year=" + ddlYear.SelectedValue + " and", "view_training_information", "Province", this.chklstProvince) + this.getSelectedItemsFromDropDownList(" and", "view_training_information", "Component", ddlComponent);
            else if (ddlReportLevel.SelectedValue == "3")
                values[0] = this.getSelectedItemsFromCheckBoxList(" and Year=" + ddlYear.SelectedValue + " and", "view_training_information", "District", this.chklstDistrict) + this.getSelectedItemsFromDropDownList(" and", "view_training_information", "Component", ddlComponent);

            ddlTraining.DataSource = (object)dc.selectProcedureExecuter(parameters, values, "spPageReportGetTraining");
            ddlTraining.DataTextField = "Name";
            ddlTraining.DataValueField = "TrainingID";
            ddlTraining.DataBind();            
        }
    }
}