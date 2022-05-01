<%@ Page Title="" Language="C#" MasterPageFile="~/OuterSiteMaster2.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="TMIS.DashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="pageTopic" runat="server">
    DASHBOARD
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTopicDetail" runat="server">
    Overview and Statistics of Training MIS.
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageHeader" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageBody" runat="server">
    <div class="card">
        <div class="row">
            <div class="col-md-12">
                <div class="card-block">
                    <div class="form-group">
                        <label for="ddlYear">Select YEAR to display the relevant data and statistics</label>
                        <select class="form-control" id="ddlYear"></select>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div class="container-fluid container-fullw bg-white">
        <div class="row">
        <div class="col-xl-12 col-lg-12">
            <div class="row">
                <div class="col-md-12">
                    <div id="tabs" class="mt-3">
                        <ul id="chartnav" class="nav nav-tabs nav-top-border no-hover-bg">
                            <li id="TrainingByProvince" class="nav-item">
                                <a id="tab1" class="nav-link active" data-toggle="tab" href="#!">Training By Province</a>
                            </li>
                            <li id="TrainingByRegion" class="nav-item">
                                <a id="tab2" class="nav-link" data-toggle="tab" href="#!">Training By Region</a>
                            </li>
                            <li id="ParticipantByProvince" class="nav-item">
                                <a id="tab3" class="nav-link" data-toggle="tab" href="#!">Participant By Province</a>
                            </li>
                            <li id="ParticipantByComponent" class="nav-item">
                                <a id="tab4" class="nav-link" data-toggle="tab" href="#!">Participant By Component</a>
                            </li>
                            <li id="ParticipantByRegion" class="nav-item">
                                <a id="tab5" class="nav-link" data-toggle="tab" href="#!">Participant By Region</a>
                            </li>

                        </ul>
                    </div>
                    <div id="containerChart" class="tab-content px-1 pt-1" style="padding-top: 20px">
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
    <script src="Script/DashBoard.js"></script>
</asp:Content>
