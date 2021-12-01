<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainAppraiselForm.aspx.cs" Inherits="MainAppraiselForm" MasterPageFile ="~/MasterPage.master" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Feedback Form Flat Responsive widget Template :: w3layouts</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="Feedback Form Widget Responsive, Login form web template,Flat Pricing tables,Flat Drop downs  Sign up Web Templates, Flat Web Templates, Login signup Responsive web template, Smartphone Compatible web template, free webdesigns for Nokia, Samsung, LG, SonyEricsson, Motorola web design" />
    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <!-- font files -->
    <link href="//fonts.googleapis.com/css?family=Abel" rel="stylesheet" />
    <link href="//fonts.googleapis.com/css?family=Raleway:100,200,300,400,500,600,700,800,900" rel="stylesheet" />
    <!-- /font files -->
    <!-- css files -->
    <link href="Client_Temp/FeedBack/css/style.css" rel="stylesheet" />
    <!-- /css files -->
    <style>
        /** {
            margin-left: 10px;
            padding-left: 10px;
            font-family: roboto;
        }*/

        div.stars {
            width: 270px;
            display: inline-block;
        }

        input.star {
            display: none;
        }

        label.star {
            float: right;
            padding: 5px;
            font-size: 30px;
            color: #444;
            transition: all .2s;
        }

        input.star:checked ~ label.star:before {
            content: '\f005';
            color: #FD4;
            transition: all .25s;
        }

        input.star-5:checked ~ label.star:before {
            color: #FE7;
            text-shadow: 0 0 20px #952;
        }

        input.star-1:checked ~ label.star:before {
            color: #F62;
        }

        label.star:hover {
            transform: rotate(-15deg) scale(1.3);
        }

        label.star:before {
            content: '\f006';
            font-family: FontAwesome;
        }
    </style>
     <script src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad() {
            jQuery(document).ready(function () {
                var v = $("#<%=txtRate.ClientID %>").val();
                //alert(v);
                $("input[name=starC][value=" + v + "]").attr('checked', 'checked');

                $('input:radio[name="star"]').change(function () {
                    //alert($("input[type=radio]:checked").val());
                    $("#<%=txtRate.ClientID %>").val($("input[type=radio]:checked").val());
                    //alert($("#<%=txtRate.ClientID %>").val().toString());
                });
            });
        }

    </script>
    <style>
              input[type=range] {
            -webkit-appearance: none;
            width: 100%;
            margin: 13.8px 0;
        }

            input[type=range]:focus {
                outline: none;
            }

            input[type=range]::-webkit-slider-runnable-track {
                width: 100%;
                height: 6.4px;
                cursor: pointer;
                background: #008BFF;
                border-radius: 1.3px;
            }

            input[type=range]::-webkit-slider-thumb {
                /*box-shadow: 1px 1px 1px #000000, 0px 0px 1px #0d0d0d;*/
                border: 1px solid #000000;
                height: 20px;
                width: 10px;
                border-radius: 4px;
                background: #ded3d3;
                cursor: pointer;
                -webkit-appearance: none;
                margin-top: -7px;
            }

            input[type=range]:focus::-webkit-slider-runnable-track {
                background: #367ebd;
            }

            input[type=range]::-moz-range-track {
                width: 100%;
                height: 8.4px;
                cursor: pointer;
                box-shadow: 1px 1px 1px #000000, 0px 0px 1px #0d0d0d;
                background: #3071a9;
                border-radius: 1.3px;
                border: 0.2px solid #010101;
            }

            input[type=range]::-moz-range-thumb {
                box-shadow: 1px 1px 1px #000000, 0px 0px 1px #0d0d0d;
                border: 1px solid #000000;
                height: 36px;
                width: 16px;
                border-radius: 3px;
                background: #fff0ff;
                cursor: pointer;
            }

            input[type=range]::-ms-track {
                width: 100%;
                height: 8.4px;
                cursor: pointer;
                background: transparent;
                border-color: transparent;
                color: transparent;
            }

            input[type=range]::-ms-fill-lower {
                background: #2a6495;
                border: 0.2px solid #010101;
                border-radius: 2.6px;
                box-shadow: 1px 1px 1px #000000, 0px 0px 1px #0d0d0d;
            }

            input[type=range]::-ms-fill-upper {
                background: #3071a9;
                border: 0.2px solid #010101;
                border-radius: 2.6px;
                box-shadow: 1px 1px 1px #000000, 0px 0px 1px #0d0d0d;
            }

            input[type=range]::-ms-thumb {
                box-shadow: 1px 1px 1px #000000, 0px 0px 1px #0d0d0d;
                border: 1px solid #000000;
                height: 36px;
                width: 16px;
                border-radius: 3px;
                background: #fff0ff;
                cursor: pointer;
                height: 8.4px;
            }

            input[type=range]:focus::-ms-fill-lower {
                background: #3071a9;
            }

            input[type=range]:focus::-ms-fill-upper {
                background: #367ebd;
            }
    </style>
    
    <style>
        .fileContainer {
            overflow: hidden;
            position: relative;
            cursor: pointer;
            padding: 10px;
          /*  margin-left: 2px;*/
        }

            .fileContainer [type=file] {
                cursor: inherit;
                display: block;
                font-size: 999px;
                filter: alpha(opacity=0);
                min-height: 50%;
                min-width: 50%;
                opacity: 0;
                position: absolute;
                right: 0;
                text-align: right;
                top: 0;
            }
    </style>
   <%--<script src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>--%>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>
    <asp:updatepanel id="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container feedback" style="height: 100%; width: 100%; margin-top: 0px; margin-bottom: 20px;">
                <h1 class="header-w3ls">AppRaisel Form</h1>
                <div class="content-w3ls" style="height: 750px; width: 1000px;">
                    <div class="form-w3ls">
                        <div class="content-wthree1">
                            <div class="grid-agileits1" style="margin-left:-12px;">
                                <div class="col-md-12">
                                    <div class="col-md-6 header" style="margin-left:-8px;">
                                        <asp:Label ID="lblEmail" class="header" runat="server">Email Address <span>:</span></asp:Label>
                                    </div>
                                    <div class="col-md-6" style="margin-top: 20px; margin-left: -18px;">
                                        <asp:DropDownList ID="ddEmployee" runat="server" Style="font-size: 14px; border: 1px solid #fff; color: #fff; height: 35px; width: 250px; border-radius: 5px;" AutoPostBack="true" OnSelectedIndexChanged="ddEmployee_SelectedIndexChanged">
                                            <asp:ListItem Value="EmpMail">Employees-Mail</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                

                                <div class="col-md-12" style="margin-top: 30px;">
                                    <div class="col-md-6 header" style="margin-left:-8px;">
                                        <asp:Label ID="lblName" class="header" runat="server">Employe Name <span>:</span></asp:Label>
                                    </div>
                                    <div class="col-md-6" style="margin-top: 20px; margin-left: -18px;">
                                        <asp:TextBox ID="txtName" class="form-control" runat="server" placeholder="Employe Name" title="Please enter your Full Name" required="" pattern="[a-zA-Z ]+" Style="font-size: 14px; border: 1px solid #fff; color: #fff; height: 35px; width: 250px;"></asp:TextBox>
                                    </div>
                                </div>
                                   <div class="col-md-12" style="margin-top: 30px;">
                                    <div class="col-md-6 header" style="margin-left:-8px;">
                                        <asp:Label ID="lblProduct" class="header" runat="server">Products <span>:</span></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-6" id="select" style="margin-top: 20px; margin-left: -2px;">
                                    <asp:DropDownList ID="ddProduct" runat="server" Style="font-size: 14px; border: 1px solid #fff; color: #fff; height: 35px; width: 250px; border-radius: 5px;">
                                        <asp:ListItem Value="Products">Products</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="grid-agileits2">
                                 <div id="PanelAppraisal" runat="server" style="margin-top:10px;">
                                        <%--<asp:LinkButton ID="LinkButton1" class="header" runat="server" Width="100%" Style="font-weight: bolder; color: #5a5a5a; border: 0px solid; border-bottom: 1px solid; padding-bottom: 2px; border-bottom-color: #808080;">Employee Appraisal              :</asp:LinkButton>--%>
                                       <asp:Label ID="Label1" class="header" runat="server">Employee Appraisal  <span>:</span></asp:Label>
                                        <div class="col-md-12" style="margin-top: 8px; margin-left: -30px;">
                                            <div class="col-md-5" style="margin-top: 7px;">
                                                <asp:Label ID="Label2" runat="server" Text="Quality" Style="font-size: 14px; color: #808080; margin-top: 10px;"></asp:Label>
                                            </div>
                                            <div class="col-md-7">
                                                <%--<input type="range" name="rngQuality" min="1" max="5" value="1" style="cursor: pointer;" />--%>
                                                <asp:TextBox ID="rngQuality" type="range" runat="server" min="1" max="5" Text="1" Style="cursor: pointer;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12" style="margin-top: 8px; margin-left: -30px;">
                                            <div class="col-md-5" style="margin-top: 7px;">
                                                <asp:Label ID="Label9" runat="server" Text="Avialibility" Style="font-size: 14px; color: #808080; margin-top: 10px;"></asp:Label>
                                            </div>
                                            <div class="col-md-7">
                                                <%--<input type="range" name="rngAva" min="1" max="5" value="1" style="cursor: pointer;" />--%>
                                                <asp:TextBox ID="rngAvialibility" type="range" runat="server" min="1" max="5" Text="1" Style="cursor: pointer;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12" style="margin-top: 8px; margin-left: -30px;">
                                            <div class="col-md-5" style="margin-top: 7px;">
                                                <asp:Label ID="Label11" runat="server" Text="Communication" Style="font-size: 14px; color: #808080; margin-top: 10px;"></asp:Label>
                                            </div>
                                            <div class="col-md-7">
                                                <%--<input type="range" name="rngComm" min="1" max="5" value="1" style="cursor: pointer;" />--%>
                                                <asp:TextBox ID="rngCommunication" type="range" runat="server" min="1" max="5" Text="1" Style="cursor: pointer;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12" style="margin-top: 8px; margin-left: -30px;">
                                            <div class="col-md-5" style="margin-top: 7px;">
                                                <asp:Label ID="Label12" runat="server" Text="Cooperation" Style="font-size: 14px; color: #808080; margin-top: 10px;"></asp:Label>
                                            </div>
                                            <div class="col-md-7">
                                                <%--<input type="range" name="rngCoop" min="1" max="5" value="1" style="cursor: pointer;" />--%>
                                                <asp:TextBox ID="rngCooperation" type="range" runat="server" min="1" max="5" Text="1" Style="cursor: pointer;"></asp:TextBox>
                                            </div>
                                        </div>

                                     <div class="col-md-7" style="margin-top: 8px; margin-left: -30px;" >
                                         <asp:Label ID="Label4" class="header" runat="server">Employee Appraisal  <span>:</span></asp:Label>
                                         <div>
                                             <asp:DropDownList ID="Appraisel_type" runat="server" Style="font-size: 14px; border: 1px solid #fff; color: #fff; height: 35px; width: 250px; border-radius: 5px;">
                                                 <asp:ListItem>Yearly</asp:ListItem>
                                                 <asp:ListItem>Quarterly</asp:ListItem>
                                                 <asp:ListItem>HalfYearly</asp:ListItem>
                                             </asp:DropDownList>
                                         </div>
                                     </div>
                                    </div>
                                
                                </div>    
                             </div>
                              
                        </div>
                      
                    <div class="col-md-12 content-wthree2" style="margin-top: 30px; margin-left: 10px;">
                        <div class="grid-w3layouts1">
                            <div class="w3-agile1">
                                <label class="rating">Ratings for Employee<span>:</span></label>
                                <div class="cont" >
                                    <div class="stars" style="margin-left: -80px;">
                                      <input class="star star-5" id="star-5" type="radio" name="star" value="5" />
                                      <label class="star star-5" for="star-5"></label>
                                      <input class="star star-5" id="star-4" type="radio" name="star" value="4" />
                                      <label class="star star-4" for="star-4"></label>
                                      <input class="star star-5" id="star-3" type="radio" name="star" value="3" />
                                      <label class="star star-3" for="star-3"></label>
                                      <input class="star star-5" id="star-2" type="radio" name="star" value="2" />
                                      <label class="star star-2" for="star-2"></label>
                                      <input class="star star-5" id="star-1" type="radio" name="star" value="1" />
                                      <label class="star star-1" for="star-1"></label>
                                      <input type="hidden" id="txtRate" runat ="server"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div>
                            <div  class="col-md-6 header"  style="margin-left:-16px;">
                                        <asp:Label ID="Label3" class="header" runat="server">Appraisel Type <span>:</span></asp:Label>
                                    </div>
                            <div style="margin-left:-16px;">
                                <asp:RadioButtonList runat="server" id="Feedback_Type">
                                    <asp:ListItem>PeerAppraisel</asp:ListItem>
                                    <asp:ListItem>SelfAppraisel</asp:ListItem>
                                    <asp:ListItem>ManagerAppraisel</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                       
                    <div class="col-md-12 content-wthree3">
                        <div class="col-md-12" style="margin-left:5px;">
                            <div class="col-md-12 enquiry" style="margin-left: 15px;">
                                <asp:Label ID="lblEnquiry" class="enquiry" runat="server">Feedback <span>:</span></asp:Label>
                            </div>
                            <div class="col-md-12 message" style="margin-left: -273px; margin-top: 15px;">
                                <asp:TextBox ID="txtEnquiry" class="form-control" runat="server" TextMode="MultiLine" placeholder="Your Queries" title="Please enter Your Queries" Style="background-color: #333; font-size: 14px; border: 1px solid #fff; color: #fff; height: 150px; padding: 15px; width: 147%;" required="" pattern="[a-zA-Z ]+"></asp:TextBox>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>
                       
                    </div>
                    <div class="content-wthree4">
                        <div class="col-md-12" style="margin-left: 10px; margin-top: 80px;">
                            <asp:Button ID="btnSubmit" class="register" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        </div>
                        <div class="col-md-12" style="margin-top: -50px; margin-left: 470px;">
                            <asp:Button ID="btnReset" class="reset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                        </div>
                        <div class="clear"></div>
                    </div>
                </div>
         </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnReset" />
        </Triggers>
    </asp:updatepanel>
</asp:Content>

