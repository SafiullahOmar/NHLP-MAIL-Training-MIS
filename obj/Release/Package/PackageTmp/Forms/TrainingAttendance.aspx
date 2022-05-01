<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="TrainingAttendance.aspx.cs" Inherits="TMIS.Forms.TrainingAttendance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTopic" runat="server">
    TRAINING PARTICIPANT ATTENDANCE
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageTopicDetail" runat="server">
    The below form is going to save the Training participant attendance and give you a facility to manage the already saved data.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageBody" runat="server">
    <script src="../Script/TrainingAttendance.js"></script>
    <section id="horizontal-form-layouts">

        <div class="row ">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <h4 class="card-title" id="horz-layout-card-center">Training Filter</h4>
                        <a class="heading-elements-toggle"><i class="icon-ellipsis font-medium-3"></i></a>
                        <div class="heading-elements">
                            <ul class="list-inline mb-0">
                                <li><a data-action="collapse"><i class="icon-minus4"></i></a></li>
                                <li><a data-action="reload"><i class="icon-reload"></i></a></li>
                                <li></li>
                                <li><a data-action="close"><i class="icon-cross2"></i></a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="card-body collapse in">
                        <div class="card-block">
                            <div class="form form-horizontal">
                                <div class="form-body">
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <p><code>Select the required detials for the training.</code></p>
                                            <label class="col-md-2 label-control" for="ddlYear">Training Year</label>
                                            <div class="col-md-10">
                                                <select class="form-control " id="ddlYear"></select>
                                                <input id="hfAttMstID" type="hidden" />
                                            </div>
                                        </div>
                                        <div class="form-group col-md-12">
                                            <label class="col-md-2 label-control" for="ddlComponent">Component</label>
                                            <div class="col-md-10">
                                                <select class="form-control Component" id="ddlComponent" runat="server"></select>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-12">
                                            <label class="col-md-2 label-control" for="ddlTraining">Select Training</label>
                                            <div class="col-md-10">
                                                <select id="ddlTraining" class="form-control Training" runat="server"></select>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-12">
                                            <label class="col-md-2 label-control" for="ddlDay">Select Day</label>
                                            <div class="col-md-10">
                                                <select id="ddlDay" class="form-control Day" runat="server"></select>
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
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <h4 class="icon-man-woman" id="horz-layout-basic"> Participant List</h4>
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
                            <div class="card-text">
                                <p>Below are the training <code>participant List</code>  for the present check the check box otherwise leave it uncheck.  </p>
                            </div>
                            <div id="dtTableParticipant">
                                <table id='tblData' width='100%' class='table table-xs nowrap table-striped scroll-horizontal table-hover'>
                                    <thead>
                                        <tr ><th>SN</th><th style='text-align:center;'>Name</th><th style='text-align:center;'>Father Name</th><th style='text-align:center;'>Gender</th><th style='text-align:center;'>Duty Station</th><th style='text-align:center;'>Organization</th><th style='text-align:center;'>All <input id='chkCheckUncheckAll' type='checkbox' /></th></tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                 <button type="button" id="btnSave" class="btn btn-primary">
                     <i class="icon-check2"></i> Save
                 </button>
                 <button type="button" id="btnUpdate" class="btn btn-info">
                     <i class="icon-check-circle"></i> Update
                 </button>
            </div>
            
        </div>
        <hr />
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <h4 class="icon-data" id="h1"> Attendance Data </h4>
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
                            <div class="card-text">
                                <p>Find the required <code>attandance day</code>  from the list , According to your right you are able to edit the attendance information into the system.  </p>
                            </div>
                            <div id="dtTableDetail">
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
