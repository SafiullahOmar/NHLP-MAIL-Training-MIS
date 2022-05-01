<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="ComponentSetup.aspx.cs" Inherits="TMIS.Admin.ComponentSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTopic" runat="server">
    COMPONENT SETUP FORM
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageTopicDetail" runat="server">
     The below form is going to save Component and the second data list gives the facility to manipulate the already saved data according to user right.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageBody" runat="server">
    <script src="../Script/ComponentSetup.js"></script>
    <section id="horizontal-form-layouts">

        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <h4 class="card-title" id="horz-layout-card-center">Component Detail</h4>
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
                                 Please fill the below <code>form</code> according to requirements.
                            </div>
                            <hr />
                            <div class="form form-horizontal">
                                <div class="form-body">
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label class="col-md-5 label-control" for="txtName">Component Name</label>
                                            <div class="col-md-7">
                                                <input id="txtName" class="form-control" placeholder="Component Name" type="text">
                                                <input id="hfComponentID" type="hidden" />
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label class="col-md-5 label-control" for="txtNameLocal">Name in Local </label>
                                            <div class="col-md-7">
                                                <input id="txtNameLocal" class="form-control" placeholder="Component Local Name" type="text">
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
                        <h4 class="icon-data" id="horz-layout-basic"> Component Detail Information</h4>
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
                                 Find the required <code>component</code> from the list , According to your right you are able to edit the component into the system.
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
