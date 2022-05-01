<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="ShowError.aspx.cs" Inherits="TMIS.ShowError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTopic" runat="server">
    Error Page
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageTopicDetail" runat="server">
    Details for the Error here...
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageBody" runat="server">
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        $('#tabs').tab();
    });
</script> 
    <div class="row">
        <div class="col-xl-12 col-lg-12">
            <div class="card">
                <div class="card-body">
                    <div class="card-block text-xs-left">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="content" class="mt-3">
                                        <ul id="Ul1" class="nav nav-tabs nav-top-border no-hover-bg" data-tabs="tabs">
                                        <li class="nav-item"><a href="#red" class="nav-link active" data-toggle="tab">Message</a></li>
                                        <li class="nav-item"><a href="#orange" class="nav-link" data-toggle="tab">Source</a></li>
                                        <li class="nav-item"><a href="#yellow" class="nav-link" data-toggle="tab">Stack Trace</a></li>
                                        <li class="nav-item"><a href="#green" class="nav-link" data-toggle="tab">Inner Exception</a></li>
                                        
                                        </ul>
                                        <div id="my-tab-content" class="tab-content px-1 pt-1">
                                        <div class="tab-pane active" id="red">
                                            <h2>Message</h2>
                                         <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                        
                                        </div>
                                        <div class="tab-pane " id="orange">
                                        <h2>Source</h2>
                                        <asp:Label ID="lblSource" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="tab-pane" id="yellow">
                                        <h2>Stack Trace</h2>
                                         <asp:Label ID="lblStackTrace" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="tab-pane" id="green">
                                        <h2>Inner Exception</h2>
                                             <asp:Label ID="lblInnerException" runat="server" Text=""></asp:Label>
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

 <asp:HiddenField ID="hidLastTab" Value="0" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
