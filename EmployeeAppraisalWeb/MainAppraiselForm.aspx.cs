using EmployeeAppraisalServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainAppraiselForm : System.Web.UI.Page
{
    ServiceClient objFeedBack = new ServiceClient();
    public static string GetMacAddress()
    {
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            // Only consider Ethernet network interfaces
            if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                return nic.GetPhysicalAddress().ToString();
            }
        }
        return null;
    }
    public void AddErrorLog(ref Exception strException, string PageName, string UserType, int UserID, int AdminID, string MACAddress = null)
    {
        var DC = new DataClassesDataContext();
        //Insert record in ErrorLog
        tblError objError = new tblError();
        objError.PageName = PageName;
        objError.Description = strException.Message.ToString();
        objError.CreatedOn = Convert.ToDateTime(DateTime.Now);
        objError.UserType = UserType;
        if (UserID != 0)
        {
            objError.UserID = UserID;
        }
        else
        {
            objError.UserID = null;
        }
        if (AdminID != 0)
        {
            objError.AdminID = AdminID;
        }
        else
        {
            objError.AdminID = null;
        }
        if (MACAddress != null)
        {
            objError.MacAddress = MACAddress;
        }
        else
        {
            objError.MacAddress = null;
        }
        DC.tblErrors.InsertOnSubmit(objError);
        DC.SubmitChanges();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string Point = txtRate.Value;
           


            string prjID =Request.QueryString["ProjectID"].ToString();
            int prID = Convert.ToInt32(prjID);

            if (!IsPostBack)
            {
                BindItem(prID);
               

            }
        }
        catch (Exception ex)
        {
            int session = Convert.ToInt32(Session["EmpID"].ToString());
            string PageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            string MACAddress = GetMacAddress();
            AddErrorLog(ref ex, PageName, "Employee", session, 0, MACAddress);
            ClientScript.RegisterStartupScript(GetType(), "abc", "alert('Something went wrong! Try again');", true);
        }
    }
    public void BindItem( int prID)
    {
        var DC = new DataClassesDataContext();

      


        int emp = (from ob in DC.tblEmployees
                   where ob.EmpID == Convert.ToInt32(Session["EmpID"])
                   select ob).Count();



        var project = (from ob in DC.tblProjects
                       where ob.ProjectID == prID
                       select ob).SingleOrDefault();


        if (emp > 0)
        {
            tblEmployee Data = (from ob in DC.tblEmployees
                                where ob.EmpID == Convert.ToInt32(Session["EmpID"])
                                select ob).Single();
            tblClient org = (from ob in DC.tblClients
                             where ob.ClientID == project.ClientID
                             select ob).Single();


          
            ddEmployee.DataSource = BindEMPMail(project.ProjectID);
            ddEmployee.DataValueField = "EmpID";
            ddEmployee.DataTextField = "EmailID";
            ddEmployee.DataBind();
            txtName.Text = GetEmpName(Convert.ToInt32(ddEmployee.SelectedValue));




            ddProduct.DataSource = BindEMPProject(project.ProjectID);
            //ddProduct.DataSource = ProjectDetail;
            ddProduct.DataValueField = "ProjectID";
            ddProduct.DataTextField = "Title";
            ddProduct.DataBind();


        }
    }

    public string GetEmpName(int empId)
    {
        var DC = new DataClassesDataContext();
        var Data = from obj in DC.tblEmployees
                   where obj.EmpID == empId
                   select obj.FirstName + obj.LastName;
        string name = Convert.ToString(Data.Single());
        return name;
    }

    public IQueryable<tblProject> BindEMPProject(int projectID)
    {
        var DC = new DataClassesDataContext();
        var Data = from obj in DC.tblProjects
                   where obj.ProjectID == projectID
                   select obj;
        return Data;
    }
    public IQueryable<tblEmployee> BindEMPMail(int projectId)
    {
        var DC = new DataClassesDataContext();
        var count = DC.tblTeams.Count(ob => ob.ProjectID == projectId);
        if (count <= 0)
            return null;
        List<tblEmployee> employees = new List<tblEmployee>();



        List<tblTeam> teams = DC.tblTeams.Where(ob => ob.ProjectID == projectId).Select(ob => ob).ToList();
        foreach (tblTeam item in teams)
        {
            var Datas = (from obj in DC.tblTeamMembers
                         where obj.TeamID == item.TeamID
                         join obj2 in DC.tblEmployees
                         on obj.EmpID equals obj2.EmpID
                         select obj2).Distinct();
            employees.AddRange(Datas);
        }
        var projectManager = DC.tblEmployees.First(x => x.EmpID == teams.First().CreatedBy);
        employees.Add(projectManager);

        var empData = employees.AsQueryable();
        return empData;
    }
    public static bool CheckForInternetConnection()
    {
        try
        {
            using (var client = new WebClient())
            {
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
        }
        catch
        {
            return false;
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (CheckForInternetConnection() == true)
        {
            try
            {
                var DC = new DataClassesDataContext();
                int ProjectID;
                if (ddProduct.SelectedValue != null)
                {
                    ProjectID = Convert.ToInt32(ddProduct.SelectedValue);
                }
                else
                {
                    ProjectID = 0;
                }

                int Point = Convert.ToInt32(txtRate.Value);

                int count = DC.tblFeedbacks.Count(ob => ob.CreatedBy == Convert.ToInt32(Session["EmpID"]) && ob.ProjectID == Convert.ToInt32(ddProduct.SelectedValue));
                if (count > 0)
                {

                    tblFeedback FeedbackPoint = (from ob in DC.tblFeedbacks
                                                 where ob.EmpID == Convert.ToInt32(Session["EmpID"]) && ob.ProjectID == Convert.ToInt32(ddProduct.SelectedValue)
                                                 select ob).Single();

                    if(Convert.ToInt32(FeedbackPoint.CreatedOn.Month) +3 == Convert.ToInt32(DateTime.Now.Month) && FeedbackPoint.CreatedOn.Year <= DateTime.Now.Year && FeedbackPoint.CreatedBy != Convert.ToInt32(Session["EmpID"]))
                    {
                        FeedbackPoint.FeedBackPoint = Point;
                        FeedbackPoint.Year = DateTime.Now.Year;
                        DC.SubmitChanges();
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('FeedBack Successfully Submitted');window.location ='Default.aspx'</script>");
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The Upraisel cannot be done now.')", true);
                    }

                }
                else
                {
                    string appraisel_type = Feedback_Type.SelectedItem.Value;
                    int empId = Convert.ToInt32(Session["EmpID"]);
                    bool obj = GetFeedBack(ddEmployee.SelectedItem.Text, ProjectID, Point, txtEnquiry.Text,appraisel_type);
                    if (obj == true)
                    {

                     
                        tblFeedback Client = (from ob in DC.tblFeedbacks
                                              where ob.EmailID == ddEmployee.SelectedItem.Text
                                              select ob).Single();
                        DateTime now = DateTime.Now;
                        MailMessage Msg = new MailMessage("emptutorialmail@gmail.com", ddEmployee.SelectedItem.Text);
                        Msg.Subject = "feedback";
                        //Msg.Body = "<html><head></head><body><table><tr><td>Your E-Mail :</td><td>" + InquiryID.EmailID + "</td><br></tr><tr><p><td>" + "Your inquiry Successfull recieved and inquiry under process" + "</td></p></tr></table></body></html>";
                        Msg.Body = "<!DOCTYPE html><html><head><title>Track My Work</title> <meta charset=\"utf - 8\"><meta name=\"viewport\" content=\"width = device - width, initial - scale = 1\"><meta http-equiv=\"X - UA - Compatible\" content=\"IE = edge\" /><style type=\"text / css\"> /* CLIENT-SPECIFIC STYLES */ body, table, td, a{-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%;} /* Prevent WebKit and Windows mobile changing default text sizes */ table, td{mso-table-lspace: 0pt; mso-table-rspace: 0pt;} /* Remove spacing between tables in Outlook 2007 and up */ img{-ms-interpolation-mode: bicubic;} /* Allow smoother rendering of resized image in Internet Explorer */ /* RESET STYLES */ img{border: 0; height: auto; line-height: 100%; outline: none; text-decoration: none;} table{border-collapse: collapse !important;} body{height: 100% !important; margin: 0 !important; padding: 0 !important; width: 100% !important;} /* iOS BLUE LINKS */ a[x-apple-data-detectors] { color: inherit !important; text-decoration: none !important; font-size: inherit !important; font-family: inherit !important; font-weight: inherit !important; line-height: inherit !important; } /* MOBILE STYLES */ @media screen and (max-width: 525px) { /* ALLOWS FOR FLUID TABLES */ .wrapper { width: 100% !important; max-width: 100% !important; } /* ADJUSTS LAYOUT OF LOGO IMAGE */ .logo img { margin: 0 auto !important; } /* USE THESE CLASSES TO HIDE CONTENT ON MOBILE */ .mobile-hide { display: none !important; } .img-max { max-width: 100% !important; width: 100% !important; height: auto !important; } /* FULL-WIDTH TABLES */ .responsive-table { width: 100% !important; } /* UTILITY CLASSES FOR ADJUSTING PADDING ON MOBILE */ .padding { padding: 10px 5% 15px 5% !important; } .padding-meta { padding: 30px 5% 0px 5% !important; text-align: center; } .no-padding { padding: 0 !important; } .section-padding { padding: 50px 15px 50px 15px !important; } /* ADJUST BUTTONS ON MOBILE */ .mobile-button-container { margin: 0 auto; width: 100% !important; } .mobile-button { padding: 15px !important; border: 0 !important; font-size: 16px !important; display: block !important; } } /* ANDROID CENTER FIX */ div[style*=\"margin: 16px 0; \"] { margin: 0 !important; }</style></head><body style=\"margin: 0 !important; padding: 0 !important; \"><!-- HIDDEN PREHEADER TEXT --><div style=\"display: none; font - size: 1px; color: #fefefe; line-height: 1px; font-family: Helvetica, Arial, sans-serif; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;\"> Entice the open with some amazing preheader text. Use a little mystery and get those subscribers to read through...</div><!-- HEADER --><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td align=\"center\" style=\" background:#008bff;\"> <!--[if (gte mso 9)|(IE)]> <table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"500\"> <tr> <td align=\"center\" valign=\"top\" width=\"500\"> <![endif]--> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 500px;\" class=\"wrapper\"> <tr style=\" background:#008bff;\"> <td align=\"center\" valign=\"top\" style=\"padding: 15px 0;\" class=\"logo\"> <h1 style=\"color:white; background:#008bff; font-family:calibri; font-size:40px;\">Track My Work</h1> </td> </tr> </table> <!--[if (gte mso 9)|(IE)]> </td> </tr> </table> <![endif]--> </td> </tr> <tr> <td bgcolor=\"#ffffff\" align=\"center\" style=\"padding: 70px 15px 70px 15px;\" class=\"section-padding\"> <!--[if (gte mso 9)|(IE)]> <table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"500\"> <tr> <td align=\"center\" valign=\"top\" width=\"500\"> <![endif]--> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 500px;\" class=\"responsive-table\"> <tr> <td> <!-- HERO IMAGE --> <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> <tr> <td> <!-- COPY --> <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> <tr> <td align=\"center\" style=\"font-size: 25px; font-family: Helvetica, Arial, sans-serif; color: #333333; padding-top: 30px;\" class=\"padding\">Your Email id is</td> </tr> <tr> <td align=\"center\" style=\"padding: 20px 0 0 0; font-size: 16px; line-height: 25px; font-family: Helvetica, Arial, sans-serif; color: #666666;\" class=\"padding\">" + ddEmployee.SelectedItem.Text + "</td> </tr> <tr> <td align=\"center\" style=\"font-size: 25px; font-family: Helvetica, Arial, sans-serif; color: #333333; padding-top: 30px;\" class=\"padding\">Your Feedback Successfully Submitted.. Thank You For Your Valuable Feedback..</td> </tr> <tr> <td align=\"center\" style=\"padding: 20px 0 0 0; font-size: 16px; line-height: 25px; font-family: Helvetica, Arial, sans-serif; color: #666666;\" class=\"padding\"></td> </tr> </table> </td> </tr> </table> </td> </tr> </table> <!--[if (gte mso 9)|(IE)]> </td> </tr> </table> <![endif]--> </td> </tr> <tr> <td align=\"center\" style=\"padding: 20px 0px; background:#424242; color:white;\"> <!--[if (gte mso 9)|(IE)]> <table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"500\"> <tr> <td align=\"center\" valign=\"top\" width=\"500\"> <![endif]--> <!-- UNSUBSCRIBE COPY --> <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\" style=\"max-width: 500px;\" class=\"responsive-table\"> <tr> <td align=\"center\" style=\"font-size: 12px; line-height: 18px; font-family: Helvetica, Arial, sans-serif; color:white;\"> <b>Track My Work</b> by:- Renown Infosys <span style=\"font-family: Arial, sans-serif; font-size: 12px; color:white;\">&nbsp;&nbsp;</span> </td> </tr> </table> <!--[if (gte mso 9)|(IE)]> </td> </tr> </table> <![endif]--> </td> </tr></table></body></html>";
                        Msg.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                        NetworkCredential MyCredentials = new NetworkCredential("emptutorialmail@gmail.com", "Vikas@123");
                        smtp.Credentials = MyCredentials;
                        smtp.Send(Msg);
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('FeedBack Successfully Submitted');window.location ='Default.aspx'</script>");
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Oops! Something goes wrong...');window.location ='FeedBack.aspx'</script>");
                    }
                }

              

                //TeamLeader
                IQueryable<tblEmployee> TeamMember = (from ob in DC.tblEmployees
                                                      join ob2 in DC.tblTeamModules
                                                      on ob.EmpID equals ob2.EmpID
                                                      join ob3 in DC.tblModules
                                                      on ob2.ModuleID equals ob3.ModuleID
                                                      where ob3.ProjectID == Convert.ToInt32(ddProduct.SelectedValue)
                                                      select ob);

               
                    int cntEmp = (from ob in DC.tblEmpAppraisals
                                  where ob.EmpID == Convert.ToInt32(ddEmployee.SelectedValue)
                                  select ob).Count();
                    if (cntEmp > 0)
                    {
                        tblEmpAppraisal Appraisal = (from ob in DC.tblEmpAppraisals
                                                     where ob.EmpID == Convert.ToInt32(ddEmployee.SelectedValue)
                                                     select ob).Single();
                             
                             if(Convert.ToInt32(Appraisal.CreatedOn.Month) + 3 == Convert.ToInt32(DateTime.Now.Month) && Appraisal.CreatedOn.Year <= DateTime.Now.Year && Appraisal.CreatedBy != Convert.ToInt32(Session["EmpID"]))
                             {
                                  Appraisal.FeedbackRating += Convert.ToInt32(Point);
                                  DC.SubmitChanges();
                             }
                             else
                             {
                                 ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The Upraisel cannot be done now.')", true);
                             }
                    }
                    else
                    {
                        tblEmpAppraisal EmpAppraisalData = new tblEmpAppraisal();
                         EmpAppraisalData.EmpID = Convert.ToInt32(ddEmployee.SelectedValue);
                         EmpAppraisalData.Quality = Convert.ToDecimal(rngQuality.Text);
                         EmpAppraisalData.Avialibility = Convert.ToDecimal(rngAvialibility.Text);
                         EmpAppraisalData.Communication = Convert.ToDecimal(rngCommunication.Text);
                         EmpAppraisalData.Cooperation = Convert.ToDecimal(rngCooperation.Text);
                         EmpAppraisalData.Skills = Convert.ToDecimal(0.0);
                         EmpAppraisalData.Deadlines = Convert.ToDecimal(0.0);
                         EmpAppraisalData.FeedbackRating= Convert.ToInt32(Point);
                         EmpAppraisalData.CreatedOn = DateTime.Now;
                         EmpAppraisalData.CreatedBy = Convert.ToInt32(Session["EmpID"]);
                         DC.tblEmpAppraisals.InsertOnSubmit(EmpAppraisalData);
                         DC.SubmitChanges();
                     }
                

                //Employee
                IQueryable<tblEmployee> Employee = (from ob in DC.tblEmployees
                                                    join ob2 in DC.tblTeamMembers
                                                    on ob.EmpID equals ob2.EmpID
                                                    join ob3 in DC.tblTasks
                                                    on ob2.TaskID equals ob3.TaskID
                                                    join ob4 in DC.tblModules
                                                    on ob3.ModuleID equals ob4.ModuleID
                                                    where ob4.ProjectID == Convert.ToInt32(ddProduct.SelectedValue)
                                                    select ob);
                SetAppraisel();

            }
            catch (Exception ex)
            {
                int session = Convert.ToInt32(Session["EmpID"].ToString());
                string PageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                string MACAddress = GetMacAddress();
                AddErrorLog(ref ex, PageName, "Employee", session, 0, MACAddress);
                ClientScript.RegisterStartupScript(GetType(), "abc", "alert('Something went wrong! Try again');", true);
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "abc", "alert('Please Check your Internet Connection');", true);
        }
        Response.Redirect("Dashboard.aspx");
    }

    public void SetAppraisel()
    {


        var DC = new DataClassesDataContext();
        string prjID = Request.QueryString["ProjectID"].ToString();
        tblModule prID = (from obj in DC.tblModules
                          where obj.ProjectID == Convert.ToInt32(prjID)
                          select obj).Single();


        var data = DC.tblModules.Where(ob => ob.ProjectID == prID.ProjectID);

        tblTeamModule MemberData = DC.tblTeamModules.Single(ob => ob.ModuleID == Convert.ToInt32(prID.ModuleID));
        int cntEmpAppraisal = DC.tblEmpAppraisals.Count(ob => ob.EmpID == Convert.ToInt32(ddEmployee.SelectedValue));
        

        if (MemberData.EmpID != null)
        {
            if (cntEmpAppraisal > 0)
            {
                tblEmpAppraisal EmpAppraisalData = DC.tblEmpAppraisals.Single(ob => ob.EmpID == Convert.ToInt32(ddEmployee.SelectedValue));

                if (Convert.ToInt32(EmpAppraisalData.CreatedOn.Month) + 3 == Convert.ToInt32(DateTime.Now.Month) && EmpAppraisalData.CreatedOn.Year <= DateTime.Now.Year && EmpAppraisalData.CreatedBy != Convert.ToInt32(Session["EmpID"]))
                {
                    EmpAppraisalData.Quality = EmpAppraisalData.Quality + Convert.ToDecimal(rngQuality.Text);
                    EmpAppraisalData.Avialibility = EmpAppraisalData.Avialibility + Convert.ToDecimal(rngAvialibility.Text);
                    EmpAppraisalData.Communication = EmpAppraisalData.Communication + Convert.ToDecimal(rngCommunication.Text);
                    EmpAppraisalData.Cooperation = EmpAppraisalData.Cooperation + Convert.ToDecimal(rngCooperation.Text);
                    DC.SubmitChanges();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The Upraisel cannot be done now.')", true);
                }
                
            }
        }

    }
    public int Rating(string Id)
    {
        int Rating = 0;
        switch (Id)
        {
            case "star-1": Rating = 1; break;
            case "star-2": Rating = 2; break;
            case "star-3": Rating = 3; break;
            case "star-4": Rating = 4; break;
            case "star-5": Rating = 5; break;

        }

        return Rating;

    }
       protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            ddEmployee.SelectedValue = " ";
            txtEnquiry.Text = " ";
            txtName.Text = " ";
            ddProduct.SelectedValue = " ";
            Appraisel_type.SelectedValue = null;

        }
        catch (Exception ex)
        {
            int session = Convert.ToInt32(Session["EmpID"].ToString());
            string PageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            string MACAddress = GetMacAddress();
            AddErrorLog(ref ex, PageName, "Employee", session, 0, MACAddress);
            ClientScript.RegisterStartupScript(GetType(), "abc", "alert('Something went wrong! Try again');", true);
        }
    }

    protected void ddEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtName.Text = GetEmpName(Convert.ToInt32(ddEmployee.SelectedValue));
    }

    public bool GetFeedBack(string Email,int ProjectID,int Point,string Desc,string appraisel_type)
    {
        var DC = new DataClassesDataContext();
        tblFeedback Feedback = new tblFeedback();
        Feedback.EmailID = Email;
        if (ProjectID != 0)
        {
            Feedback.ProjectID = ProjectID;
        }
        Feedback.FeedBackPoint = Convert.ToDecimal(Point);
        Feedback.Description = Desc;
        
        Feedback.CreatedOn = DateTime.Now;
        int cnt = (from obj in DC.tblEmployees
                   where obj.EmailID == Email
                   select obj.EmpID).Count();
        if (cnt > 0)
        {
            int Data = (from obj in DC.tblEmployees
                        where obj.EmailID == Email
                        select obj.EmpID).Single();
            Feedback.EmpID = Data;
        }
        if (appraisel_type == "PeerAppraisel")
        {
            Feedback.FeedbackType = "Peer";
            Feedback.EmpID = Convert.ToInt32(Session["EmpID"]);
            Feedback.peerEmpID = Convert.ToInt32(ddEmployee.SelectedValue);
            Feedback.ManagerID = null;

        }
        else if (appraisel_type == "SelfAppraisel")
        {
            Feedback.FeedbackType = "Employee";
            Feedback.EmpID = Convert.ToInt32(Session["EmpID"]);
            Feedback.peerEmpID = null;
            Feedback.ManagerID = null;
        }
        else
        {
            Feedback.FeedbackType = "Manager";
            Feedback.EmpID = Convert.ToInt32(ddEmployee.SelectedValue);
            Feedback.ManagerID = Convert.ToInt32(Session["EmpID"]);
            Feedback.peerEmpID = null;
        }
        Setappraisel_type(Feedback);
        Feedback.IsRead = false;
        Feedback.Year = DateTime.Now.Year;
        Feedback.CreatedBy = Convert.ToInt32(Session["EmpID"]);
        DC.tblFeedbacks.InsertOnSubmit(Feedback);
        DC.SubmitChanges();
        return true;
    }


    public void Setappraisel_type(tblFeedback feedback)
    {
        if(Appraisel_type.SelectedItem.Text== "Yearly")
        {
            feedback.AppraiselType = "Yearly";
        }
        else if(Appraisel_type.SelectedItem.Text == "Quarterly")
        {
            feedback.AppraiselType = "Quarterly";
        }
        else
        {
            feedback.AppraiselType = "HalfYearly";
        }
    }

}