<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="TrainingParticipants.aspx.cs" Inherits="TMIS.Forms.TrainingParticipants" %>
<asp:Content ID="Content1" ContentPlaceHolderID="css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTopic" runat="server">
    MANAGING TRAINING PARTICIPANT
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageTopicDetail" runat="server">
    The below form is going to save the Training participant and give you a facility to manupulate the already saved data.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageBody" runat="server">
    <script src="../Script/TrainingParticipants.js"></script>
    <section id="horizontal-form-layouts">

        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <h4 class="card-title" id="horz-layout-card-center">Participant Detail</h4>
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
                                <p>Please fill the below <code>form</code> according to the documents and valid sources.</p>
                            </div>
                            <hr />
                            <div class="form form-horizontal">
                                <div class="form-body">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label for="ddlYear">Year</label>
                                                <select class="form-control " id="ddlYear"></select>
                                                <input id="hfParticipantID" type="hidden" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="ddlComponent">Component</label>
                                                <select class="form-control Component" id="ddlComponent" runat="server"></select>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="ddlTraining">Training</label>
                                                <select class="form-control Training" id="ddlTraining" runat="server"></select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="txtName">Name of Participant</label>
                                                <input type="text" class="form-control" placeholder="Name of Participant" id="txtName" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="txtTrainingTitle">Father Name</label>
                                                <input type="text" class="form-control" placeholder="Father Name" id="txtFatherName" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="ddlGender">Gender</label>
                                                <select class="form-control " id="ddlGender">
                                                    <option value="M">Male</option>
                                                    <option value="F">Female</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="ddlProvince">Duty Station (Province)</label>
                                                <select class="form-control Province" id="ddlProvince" runat="server"></select>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="ddlDistrict">Duty Station (District)</label>
                                                <select class="form-control " id="ddlDistrict"></select>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="ddlOrganization">Organization</label>
                                                <select class="form-control " id="ddlOrganization"></select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="txtPosition">Position</label>
                                                <input type="text" class="form-control" placeholder="Position" id="txtPosition" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="txtMobile">Mobile No</label>
                                                <input type="text" class="form-control" placeholder="Mobile No" id="txtMobile" onkeydown="AllowNumbers(event);" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="txtEmail">Email</label>
                                                <input type="text" class="form-control" placeholder="Email" id="txtEmail" />
                                            </div>
                                        </div>
                                        
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="txtRemarks">Remarks</label>
                                                <input type="text" class="form-control" placeholder="Remarks" id="txtRemarks" />
                                            </div>
                                        </div>
                                        
                                        
                                    </div>
                                    
                                </div>
                                <div class="form-actions center">
                                    <button type="button" class="btn btn-warning mr-1" onclick="ClearForm();">
                                        <i class="icon-cross2"></i>Cancel
	                           
                                    </button>
                                    <button type="button" id="btnSave" class="btn btn-primary">
                                        <i class="icon-check2"></i>Save
	                           
                                    </button>
                                    <button type="button" id="btnUpdate" class="btn btn-info">
                                        <i class="icon-check-circle"></i>Update
	                           
                                    </button>
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
                        <h4 class="icon-data" id="horz-layout-basic">Training Participant Information </h4>
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
                                <p>Find the required <code>Participant</code>  from the list , According to your right you are able to edit the Training information into the system.  </p>
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
