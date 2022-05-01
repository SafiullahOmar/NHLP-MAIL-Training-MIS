<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="FieldVisit.aspx.cs" Inherits="TMIS.Forms.FieldVisit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTopic" runat="server">
    FIELD VISITS BY MAIL/DAIL
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageTopicDetail" runat="server">
    NHLP facilitats the visits conducted by MAIL and DAIL staff in order to strengthen the coordination and to provide the advisory services to the farmers.
     The below form is going to save the field visit information and give you a facility to manupulate the already saved data.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageBody" runat="server">
    <script src="../Script/FieldVisit.js"></script>
    <section id="horizontal-form-layouts">

        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <h4 class="card-title" id="horz-layout-card-center">Field Visit Detail</h4>
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
                                <p>Please fill the below <code>form</code> according to the documents and valid sources. * is required (<code>Value must be provided for the field</code>)</p>
                            </div>
                            <hr />
                            <div class="form form-horizontal">
                                <div class="form-body">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label for="ddlYear">* Year</label>
                                                <select class="form-control " id="ddlYear"></select>
                                                <input id="hfID" type="hidden" />
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label for="txtVisitDate">* Visit Date</label>
                                                <input type="text" class="form-control" placeholder="Visit Date" id="txtVisitDate" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="ddlComponent">* Facilitated By Component</label>
                                                <select class="form-control Component" id="ddlComponent" runat="server"></select>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="ddlSubComponent">Sub Component</label>
                                                <select class="form-control " id="ddlSubComponent"></select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="txtPurpose">* Purpose of Field Visit</label>
                                                <input type="text" class="form-control" placeholder="Purpose of Field Visit" id="txtPurpose" />
                                            </div>
                                        </div>
                                        
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="ddlProvince">* Province</label>
                                                <select class="form-control Province" id="ddlProvince" runat="server"></select>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <label for="ddlDistrict">District</label>
                                                <select class="form-control " id="ddlDistrict"></select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="txtVisitConductedBy">* Field Visit Conducted By</label>
                                                <input type="text" class="form-control" placeholder="Visit Conducted By" id="txtVisitConductedBy" />
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
                        <h4 class="icon-data" id="horz-layout-basic"> Field Visit Saved Information </h4>
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
                                <p>Find the required <code>field visit information</code>  from the list , According to your right you are able to edit the field visit information into the system.  </p>
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
