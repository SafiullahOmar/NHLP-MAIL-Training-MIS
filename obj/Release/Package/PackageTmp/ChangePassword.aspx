<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="TMIS.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTopic" runat="server">
    Change Password
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageTopicDetail" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageBody" runat="server">
    <div class="container-fluid container-fullw bg-white">
        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-6">
                <asp:ChangePassword ID="ChangePassword1" runat="server"
                    ContinueDestinationPageUrl="Dashboard.aspx"
                    SuccessPageUrl="Dashboard.aspx">
                    <ChangePasswordTemplate>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <asp:Label ID="CurrentPasswordLabel" runat="server"
                                        AssociatedControlID="CurrentPassword"> Current Password </asp:Label>

                                    <asp:TextBox ID="CurrentPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server"
                                        ControlToValidate="CurrentPassword" ErrorMessage="Password is required."
                                        ToolTip="Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <asp:Label ID="NewPasswordLabel" runat="server"
                                        AssociatedControlID="NewPassword">New Password</asp:Label>

                                    <asp:TextBox ID="NewPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server"
                                        ControlToValidate="NewPassword" ErrorMessage="New Password is required."
                                        ToolTip="New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <asp:Label ID="ConfirmNewPasswordLabel" runat="server"
                                        AssociatedControlID="ConfirmNewPassword">Confirm Password</asp:Label>

                                    <asp:TextBox ID="ConfirmNewPassword" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server"
                                        ControlToValidate="ConfirmNewPassword"
                                        ErrorMessage="Confirm New Password is required."
                                        ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>

                                    <asp:CompareValidator ID="NewPasswordCompare" runat="server"
                                        ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                                        Display="Dynamic"
                                        ErrorMessage="The Confirm New Password must match the New Password entry."
                                        ValidationGroup="ChangePassword1"></asp:CompareValidator>
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12">
                                <asp:Button ID="ChangePasswordPushButton" runat="server" CssClass=" btn btn-success"
                                    CommandName="ChangePassword" Text="Change Password"
                                    ValidationGroup="ChangePassword1" />

                                <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CssClass="btn btn-default" PostBackUrl="~/Dashboard.aspx"
                                    CommandName="Cancel" Text="Cancel" />
                            </div>
                        </div>
                    </ChangePasswordTemplate>
                </asp:ChangePassword>
            </div>
            <div class="col-md-3"></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
