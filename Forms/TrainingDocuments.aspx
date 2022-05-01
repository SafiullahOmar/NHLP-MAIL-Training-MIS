<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="TrainingDocuments.aspx.cs" Inherits="TMIS.Forms.TrainingDocuments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTopic" runat="server">
    TRAINING DOCUMENTS UPLOAD
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageTopicDetail" runat="server">
    The below form is going to upload the Training document and give you a facility to download the already uploaded data.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageBody" runat="server">
    <script src="../Script/TrainingDocuments.js"></script>
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
                        <h4 class="icon-data" id="horz-layout-basic">Uploaded File(s) of the Training</h4>
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
                                <p>Below are the training <code>uploaded document(s)</code>  you can download or delete files according to your rights.  </p>
                            </div>
                            <div id="dtTableDetail">

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class=" col-md-12">
                <div class="card">
                    <div class="card-block">
                        <div class=" card-body  col-md-7 label-control">
                            <hr />
                            <p><b>*** :: Please Upload valid document(s) <code>(Required)</code></b></p>
                        </div>
                        <div class="col-md-5">
                            <hr />
                            <input type="file" id="FileUpload1"  multiple />
                        </div>
                        <div class=" card-body  col-md-7 label-control">
                            <p><b>Remark / Comment</b></p>
                        </div>
                        <div class="col-md-5">
                            <input type="text" class="form-control" placeholder="Remarks" id="txtRemarks" />
                        </div>
                        
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                 <button type="button" id="btnSave" class="btn btn-primary">
                                        <i class="icon-check2"></i>Save
                                    </button>
            </div>
            
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
