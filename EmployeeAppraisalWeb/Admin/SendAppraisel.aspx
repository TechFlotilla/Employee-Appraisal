<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendAppraisel.aspx.cs" Inherits="Admin_SendAppraisel" MasterPageFile="~/Admin/MasterPage.master" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Earnings stats widgets -->
    <div class="row-fluid">

        <div class="span4" >
            <div class="widget">
                        <h6><asp:Label ID="Label1" runat="server" Text="Select The Project"></asp:Label></h6>
                     
                            <asp:DropDownList ID="SelectProject" runat="server" Style="font-size: 14px; border: 1px solid #000000; color: #000000; height: 35px; width: 250px; border-radius: 5px;" AutoPostBack="true" OnSelectedIndexChanged="SelectProject_SelectedIndexChanged">
                                <asp:ListItem>----Projects----</asp:ListItem>
                            </asp:DropDownList>
                      </div>
               </div>
        <div class="span4" >
            <div class="widget">
               <h6><asp:Label ID="Label2" runat="server" Text="Employees related to Project"></asp:Label></h6>
                     
                            <asp:DropDownList ID="Employees" runat="server" Style="font-size: 14px; border: 1px solid #000000; color: #000000; height: 35px; width: 250px; border-radius: 5px;">
                                <asp:ListItem>----Employees----</asp:ListItem>
                            </asp:DropDownList>
                      </div>
            </div>
        </div>
       <div display="inline-block" class="span4">
           <div class="widget">
           
               <%--<asp:button class="btn-default" id="SendNotification" runat="server" onclick="SendNotification_Click" CausesValidation="False" text=" Send-Notification(ALL)"/>--%>
               <asp:Button runat="server" id="SendNotification" class="btn-default" Text="Send-Notification(ALL)" OnClick="SendNotification_Click1" />
          </div>
       </div>

     <div>
        <asp:Chart ID="ChartProject" runat="server" Height="400" Width="500">
            <Titles>
                <asp:Title Name="ProjectModule"></asp:Title>
            </Titles>
            <Series>
                <asp:Series Name="Module" ></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea" BorderWidth="0"></asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
  
</asp:Content>


