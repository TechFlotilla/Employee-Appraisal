﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/LoginMaster.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Admin_Adlogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        //function Store_Data()
        //{
        //    window.localStorage.setItem("UserID", document.getElementById("txtmail").value);
        //    window.localStorage.setItem("Password", document.getElementById("txtpass").value);
        //}
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Panel ID="palLogin" runat="server">
        <!-- Login block -->
        <asp:Panel ID="mailer" runat="server" class="alert alert-error alert-dismissable fade in" Visible="false">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <strong>Opps!</strong> User Email or Password Invalied.
        </asp:Panel>
        <div class="login">
            <div class="navbar">
                <div class="navbar-inner">
                    <h6><i class="icon-user"></i>Login page</h6>
                </div>
            </div>
            <div class="well" style="padding: 15px;">
                <div class="control-group">
                    <asp:Panel ID="AlertPanal" runat="server" Visible="false" text="plz enter write value"></asp:Panel>
                    <label class="control-label">Username</label>
                    <div class="controls">
                        <asp:TextBox ID="txtmail" type="email" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">Password:</label>
                    <div class="controls">
                        <asp:TextBox ID="txtpass" type="password" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="login-btn">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" class="btn-danger btn-block" Style="font-size: 18px; margin-top: 30px; font-weight: bold; padding: 5px;" />
                </div>
                <div class="contact-group">
                    <asp:LinkButton ID="lnkFogetPwd" runat="server" OnClick="lnkFogetPwd_Click" Style="margin-left: 124px; font-size: 15px;">Forget Password</asp:LinkButton>
                </div>
            </div>
        </div>
        <!-- /login block -->
    </asp:Panel>

    <asp:Panel ID="Panel2" runat="server">
        <!-- Forgot block -->
        <asp:Panel ID="Panel3" runat="server" class="alert alert-error alert-dismissable fade in" Visible="false">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <strong>Opps!</strong> User Email or Password Invalied.
        </asp:Panel>
        <div class="login">
            <div class="navbar">
                <div class="navbar-inner">
                    <h6><i class="icon-user"></i>Forget Password</h6>
                </div>
                <div class="well" style="padding: 15px;">
                    <asp:Panel ID="Panel4" runat="server" Visible="true">
                        <div class="control-group">
                            <asp:Panel ID="Panel5" runat="server" Visible="false" text="plz enter write value"></asp:Panel>
                            <label class="control-label">Email</label>
                            <div class="controls">
                                <asp:TextBox ID="txtCodeEmail" type="email" runat="server" Width="100%"></asp:TextBox>
                                <asp:Label ID="errorEmailCode" runat="server" Text="Opps! Your mailID is Wrong." style="color: #ad0000; font-weight: bolder;" Visible="false"></asp:Label>
                            </div>
                            <div class="login-btn">
                                <asp:Button ID="btnGetCode" runat="server" Text="Get Code" OnClick="btnGetCode_Click" class="btn-danger btn-block" Style="font-size: 18px; margin-top: 30px; font-weight: bold; padding: 5px;" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel6" runat="server" Visible="false">
                        <div class="control-group">
                            <label class="control-label">Code</label>
                            <div class="controls">
                                <asp:TextBox ID="txtCode" runat="server" Width="100%"></asp:TextBox>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                <div class="login-btn">
                                    <asp:Button ID="btnCheking" runat="server" Text="Verify Code" class="btn-danger btn-block" OnClick="btnCheking_Click" Style="font-size: 18px; margin-top: 30px; font-weight: bold; padding: 5px;" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <!-- /Forgot block -->
    </asp:Panel>

    <asp:Panel ID="Panel8" runat="server" Visible="false">
        <!-- Forgot block -->
        <asp:Panel ID="Panel9" runat="server" class="alert alert-error alert-dismissable fade in" Visible="false">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <strong>Opps!</strong> User Email or Password Invalied.
        </asp:Panel>
        <div class="login">
            <div class="navbar">
                <div class="navbar-inner">
                    <h6><i class="icon-user"></i>Change Password</h6>
                </div>
                <div class="well" style="padding: 15px;">
                    <div class="control-group">
                        <asp:Panel ID="Panel11" runat="server" Visible="false" text="plz enter write value"></asp:Panel>
                        <label class="control-label">Password</label>
                        <div class="controls">
                            <asp:TextBox ID="txtrpass" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">Re-Password</label>
                        <div class="controls">
                            <asp:TextBox ID="txtrepass" runat="server" Width="100%"></asp:TextBox>
                            <asp:Label ID="errorMissMatch" runat="server" Text="Both Password are miss match." style="color: #ad0000; font-weight: bolder;"></asp:Label>
                            <div class="login-btn">
                                <asp:Button ID="btnsub" runat="server" Text="Submit" class="btn-danger btn-block" OnClick="btnsub_Click" Style="font-size: 18px; margin-top: 30px; font-weight: bold; padding: 5px;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /Forgot block -->
    </asp:Panel>
</asp:Content>

