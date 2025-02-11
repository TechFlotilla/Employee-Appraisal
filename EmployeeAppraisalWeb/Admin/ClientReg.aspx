﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="ClientReg.aspx.cs" Inherits="Admin_ClientReg" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        span {
            font-size: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divPage" runat="server">
        <asp:ScriptManager ID="ScriptClient" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="updateClienet" runat="server">
            <ContentTemplate>
                <div style="display: inline-block; margin-bottom: 30px; width: 100%;">

                    <%-- client id true kervani 6e servic ma--%>

                    <!-- Client form elements -->
                    <asp:Panel ID="errorEmail" runat="server" class="alert alert-error alert-dismissable fade in" Visible="false">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong>Opps!</strong> Email already exist!!!!!
                    </asp:Panel>
                    
                    <div class="col-md-12">
                        <div style="margin-bottom: 1px;">
                            <div class="navbar-inner">
                                <h6>Add New Client</h6>
                            </div>
                        </div>
                        <div style="border: 1px solid #CAC8C8; display: inline-block; width: 100%; padding: 20px;">

                            <div class="col-md-6 control-group">
                                <span>Client Name:</span>
                                <div class="controls">
                                    <asp:TextBox ID="txtname" runat="server" class="span12 form-control" required="" MaxLength="30" pattern="[A-Za-z\s_]+" title="Accept Alphabet Only"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 control-group">
                                <span>Contact No:</span>
                                <div class="controls">
                                    <asp:TextBox ID="txtCNO" runat="server" class="span12 form-control" type="number" required="" min="999999999" max="9999999999999" oninvalid="this.setCustomValidity('Please Enter Valid Contact Number')" onchange="try{setCustomValidity('')}catch(e){}"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 control-group">
                                <span>Client EmailID:</span>
                                <div class="controls">
                                    <asp:TextBox ID="txtEmail" runat="server" class="span12 form-control" required="" type="email" OnTextChanged="txtEmail_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 control-group">
                                <span>Company Name:</span>
                                <div class="controls">
                                    <asp:TextBox ID="txtComName" runat="server" class="span12 form-control" required="" MaxLength="20" pattern="[A-Za-z\s_]+" oninvalid="this.setCustomValidity('Accept Alphabet Only')" onchange="try{setCustomValidity('')}catch(e){}"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 control-group">
                                <span>Company URL[Optional]:</span>
                                <div class="controls">
                                    <asp:TextBox ID="txtComUrl" runat="server" class="span12 form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 control-group">
                                <span>Address[Optional]:</span>
                                <div class="controls">
                                    <asp:TextBox ID="txtAddress" runat="server" class="span12 form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 control-group">
                                <span>Land Mark[Optional]:</span>
                                <div class="controls">
                                    <asp:TextBox ID="txtLandMark" runat="server" class="span12 form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 control-group">
                                <span>Country Name:</span>
                                <div class="controls">
                                    <asp:DropDownList ID="ddCountryName" runat="server" class="span12 form-control" AutoPostBack="true" required="" OnSelectedIndexChanged="ddCountryName_SelectedIndexChanged">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-6 control-group">
                                <span>State Name:</span>
                                <div class="controls">
                                    <asp:DropDownList ID="ddStateName" runat="server" class="span12 form-control" AutoPostBack="true" OnSelectedIndexChanged="ddStateName_SelectedIndexChanged" required="">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-6 control-group">
                                <span>City Name:</span>
                                <div class="controls">
                                    <asp:DropDownList ID="ddCityName" runat="server" class="span12 form-control" required="">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-6 control-group">
                                <span>Zip Code:</span>
                                <div class="controls">
                                    <asp:TextBox ID="txtZipCode" runat="server" class="span12 form-control" required="" type="number" min="100000" max="999999" oninvalid="this.setCustomValidity('Enter Proper ZipCode')" onchange="try{setCustomValidity('')}catch(e){}"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 control-group">
                                <span>Client Password:</span>
                                <div class="controls">
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="password" class="span12 form-control" required="" type="Password" pattern="(?=^.{8,}$).*$" title="Password must be 8 letter"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-12 group-control">
                                <div class="controls">
                                    <asp:Button ID="btnInsert" runat="server" Text="Insert" class="btn  btn-primary form-control" BackColor="#4d4d4d" Style="width: 12%; margin-right: 10px;" OnClick="btnInsert_Click" />
                                    <asp:Button ID="Button2" runat="server" Text="Reset" class="btn  btn-primary form-control" BackColor="#4d4d4d" Style="width: 12%;" OnClick="Button2_Click" formnovalidate/>
                                </div>
                            </div>

                        </div>
                    </div>
                    <!-- /Client form elements -->
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnInsert" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="divError" class="text-center col-md-12" runat="server" visible="false">
        <h1>You Can't Access this Page. You don't have  Permission.</h1>
        <h3>Please contact to Admin.</h3>
    </div>
</asp:Content>

