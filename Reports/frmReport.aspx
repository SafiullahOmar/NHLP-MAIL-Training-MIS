<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmReport.aspx.cs" Inherits="TMIS.Reports.frmReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTopic" runat="server">
    SYSTEM DATA REPORTS
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageTopicDetail" runat="server">
Select Your Required Field (Report Level,Report Type,Report Year repectively) and then click on Generate Button.Your Report will be generated in Excel Format.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageBody" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            
            
            maintainScrollPosition();
            //  DrpDownGroup();
        });

        function pageLoad() {
            $("#<%=ddlComponent.ClientID%>").selectpicker({
                liveSearch: true,
                liveSearchPlaceholder: 'Search Component',
                actionsBox: true
            });
            $("#<%=ddlReportType.ClientID%>").select2({
               placeholder: 'Select Report'
            });
            $("#<%=ddlTraining.ClientID%>").select2();
            maintainScrollPosition();
        }
        function maintainScrollPosition() {
            $("#<%=chklstRegion.ClientID%>").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
            $("#<%=chklstProvince.ClientID%>").scrollTop($('#<%=hfScrollPositionProvince.ClientID%>').val());
            $("#<%=chklstDistrict.ClientID%>").scrollTop($('#<%=hfScrollPositionDistrict.ClientID%>').val());
        }
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
          }
          function setScrollPositionProvince(scrollValue) {
              $('#<%=hfScrollPositionProvince.ClientID%>').val(scrollValue);
              console.log($('#hfScrollPositionProvince').val());
          }
          function setScrollPositionDistrict(scrollValue) {
              $('#<%=hfScrollPositionDistrict.ClientID%>').val(scrollValue);
        }
        function DrpDownGroup() {
            $("#<%=ddlReportType.ClientID%> option").eq(1).before("<optgroup style='background-color:red;' label='--TRAINING REPORT--' />");
            //$("#<%=ddlReportType.ClientID%>").select2().css('background-color', 'red');;
            // $('.select2-selection').css('background-color', 'blue');
        }
    </script>
    <style>
        .ov {
            overflow-y: scroll;
        }
        .test123 {
            font-size:14px;
            font-weight:bold;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <section class="with-multi-selection">
        <div class="row">
            <div class="col-xs-12">
                <div class="card">
                    <div class="card-header">
                        <h4 class="card-title">Data Reports</h4>
                        <a class="heading-elements-toggle"><i class="icon-ellipsis font-medium-3"></i></a>
                        <div class="heading-elements">
                            <ul class="list-inline mb-0">
                                <li><a data-action="collapse"><i class="icon-minus4"></i></a></li>
                                <li><a data-action="reload"><i class="icon-reload"></i></a></li>
                                <li><a data-action="expand"><i class="icon-expand2"></i></a></li>
                                <li><a data-action="close"><i class="icon-cross2"></i></a></li>
                            </ul>
                        </div>
                    </div>



                    <div class="card-body collapse in">
                        <div class="card-block">
                            <div class="form form-horizontal">
                                <div class="form-body">
                                    
                                    <div class="row">
            
                                        <div class="col-md-12">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <script type="text/javascript">
                                                        Sys.Application.add_load(DrpDownGroup);
                                                    </script>
                                                    <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                                    <asp:HiddenField ID="hfScrollPositionProvince" Value="0" runat="server" />
                                                    <asp:HiddenField ID="hfScrollPositionDistrict" Value="0" runat="server" />
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <div class="form-group">
                                                                <label for="ddlReportLevel">Report Level</label>
                                                                <asp:DropDownList ID="ddlReportLevel" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlReportLevel_SelectedIndexChanged">
                                                                    <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                                                    <asp:ListItem Text="Region" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Province" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="District" Value="3"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label for="ddlReportType">Report Type</label>
                                                                <asp:DropDownList ID="ddlReportType" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                                                                    <%--Training--%>
                                                                    <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                                                    <asp:ListItem Text="Summary Report" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Detail Training Report" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Field Visit Report" Value="3"></asp:ListItem>
                                                                    
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <div class="form-group">
                                                                <label for="ddlYear">Year</label>
                                                                <asp:DropDownList ID="ddlYear" CssClass="form-control" runat="server" DataSourceID="SqlDataSource1" DataTextField="Year" DataValueField="YearID">
                                                                </asp:DropDownList>
                                                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TrainingDBConnectionString %>" SelectCommand="SharedGetYear" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label for="ddlReportType">Component</label>
                                                                <select id="ddlComponent" class="form-control" multiple runat="server">
                                                                </select>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <hr />

                                                    <div class="row">
                                                        <div class="col-md-4 ">
                                                            <div class="card border-warning">
                                                                    <div class="card-header" style="background-color:orange; color:white; font-weight:bolder;">
                                                                        Region<div style="float:right;">
                                                                            <label><strong>Check All </strong></label>
                                                                            <asp:CheckBox ID="chkAllRegion" runat="server" OnCheckedChanged="chkAll_CheckedChanged" AutoPostBack="true" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="card-body" style="height: 250px;">
                                                                        <div class="form-group">
                                                                            <div class="form-check">
                                                                                <asp:CheckBoxList ID="chklstRegion" AutoPostBack="true" onscroll="setScrollPosition(this.scrollTop);" Height="250px" CssClass="form-control ov" runat="server" OnSelectedIndexChanged="chklstRegion_SelectedIndexChanged">
                                                                                </asp:CheckBoxList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div id="Div1" class="card border-info">
                                                                <div class="card-header" style="background-color:#6DC0F3; color:white; font-weight:bolder;">
                                                                    Province<div style="float:right;">
                                                                        <label><strong>Check All </strong></label>
                                                                        <asp:CheckBox ID="chkAllProvince" runat="server" OnCheckedChanged="chkAll_CheckedChanged" AutoPostBack="true" />
                                                                    </div>
                                                                </div>
                                                                <div class="card-body" style="height: 250px;">
                                                                    <div class="form-group">
                                                                        <div class="checkbox clip-check check-success checkbox-inline">
                                                                            <asp:CheckBoxList ID="chklstProvince" runat="server" onscroll="setScrollPositionProvince(this.scrollTop);" Height="250px" CssClass="form-control ov" AutoPostBack="true" OnSelectedIndexChanged="chklstProvince_SelectedIndexChanged"></asp:CheckBoxList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="col-md-4">
                                                            <div id="Div2" class="card border-success">
                                                                <div class="card-header" style="background-color:#67D476; color:white; font-weight:bolder;">
                                                                    District<div style="float:right;">
                                                                        <label><strong>Check All </strong></label>
                                                                        <asp:CheckBox ID="chkAllDistrict" runat="server" OnCheckedChanged="chkAll_CheckedChanged" AutoPostBack="true" />
                                                                    </div>
                                                                </div>
                                                                <div class="card-body" style="height: 250px;">
                                                                    <div class="form-group">
                                                                        <div class="checkbox clip-check check-purple checkbox-inline">
                                                                            <asp:CheckBoxList ID="chklstDistrict" Height="250px" onscroll="setScrollPositionDistrict(this.scrollTop);" CssClass="form-control ov" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chklstDistrict_SelectedIndexChanged"></asp:CheckBoxList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>


                                                    </div>
                                                    <div class="row" id="rowTraining" runat="server" visible="false">
                                                        <div class="col-md-12 ">
                                                            <div class="form-group">
                                                                <label for="ddlReportType">Select Training</label>
                                                                <select id="ddlTraining" class="form-control" runat="server">
                                                                </select>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <hr />
                                            <div class="row">
                                                <div class="col-md-5">
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Button ID="btnGenerate" CssClass="btn btn-success btn-block" runat="server" Text="Generate Report" OnClick="btnGenerate_Click" />
                                                </div>
                                                <div class="col-md-5">
                                                </div>
                                            </div>
                                        </div>
            
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
