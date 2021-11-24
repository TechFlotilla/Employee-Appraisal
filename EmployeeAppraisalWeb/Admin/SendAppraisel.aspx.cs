using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using EmployeeAppraisalServiceReference;
using System.Net.NetworkInformation;

public partial class Admin_SendAppraisel : System.Web.UI.Page
{
    static int ProjectID, EmpID;
    ServiceClient SendAppraisel = new ServiceClient();
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
             projectchart();
              if (!IsPostBack)
              {

                BindProject();
                BindAllEmployee();
               

              }
                //-----Chart
            }
            catch (Exception ex)
            {
                int session = Convert.ToInt32(Session["AdminID"].ToString());
                string PageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                string MACAddress = GetMacAddress();
                AddErrorLog(ref ex, PageName, "Admin", 0, session, MACAddress);
                ClientScript.RegisterStartupScript(GetType(), "abc", "alert('Something went wrong! Try again');", true);
            }
    }

    public void BindProject()
    {

        SelectProject.DataSource = BindActiveProjects();
        SelectProject.DataValueField = "ProjectID";
        SelectProject.DataTextField = "Title";
        SelectProject.DataBind();
    }
    public void BindAllEmployee()
    {
        Employees.DataSource = Fillempddl();
        Employees.DataValueField = "EmpID";
        Employees.DataTextField = "FirstName";
        Employees.DataBind();
    }

    public IQueryable<tblProject> BindActiveProjects()
    {
        var DC = new DataClassesDataContext();
        var Data = (from obj in DC.tblProjects
                    where obj.IsActive == true
                    select obj).Distinct();

        return Data;
    }

    public IQueryable<tblEmployee> Fillempddl()
    {
        var DC = new DataClassesDataContext();
        
        var Data = (from obj in DC.tblEmployees
                    where obj.IsActive == true
                    select obj).Distinct();

        return Data;
    }

    public void projectchart()
    {
        ((Panel)Master.FindControl("Panel1")).Visible = false;

        var dc = new DataClassesDataContext();
        //Chart------

        var Pro = (from ob in dc.tblProjects
                   select ob).ToList();
        int[] y = new int[Pro.Count];
        string[] x = new string[Pro.Count];
        int i = 0;
        foreach (tblProject Project in Pro)
        {
            x[i] = Project.Title;
            y[i] = dc.tblModules.Count(ob => ob.ProjectID == Project.ProjectID);
            i++;
        }
        ChartProject.Series[0].Points.DataBindXY(x, y);
        ChartProject.Series[0].ChartType = SeriesChartType.Line;
        ChartProject.ChartAreas[0].Area3DStyle.Enable3D = false;

        ChartProject.Series["Module"].BorderWidth = 3;


    }


    protected void SelectProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        Employees.DataSource = BindEmployee(Convert.ToInt32(SelectProject.SelectedValue));
        Employees.DataValueField = "EmpID";
        Employees.DataTextField = "FirstName";
        Employees.DataBind();
    }

    private IQueryable<tblEmployee>BindEmployee(int ProjectID)
    {
        var DC = new DataClassesDataContext();
        var count = DC.tblTeams.Count(ob => ob.ProjectID == ProjectID);
        if (count <=0)
            return null;
        List<tblEmployee> employees = new List<tblEmployee>();

        if(count>1)
        {
            List<tblTeam>tbl = DC.tblTeams.Where(ob => ob.ProjectID == ProjectID).Select(ob => ob).ToList();
            foreach (tblTeam teams in tbl)
            {
                var Datas = (from obj in DC.tblTeamMembers
                             where obj.TeamID == teams.TeamID
                             join obj2 in DC.tblEmployees
                             on obj.EmpID equals obj2.EmpID
                             select obj2).Distinct();
                employees.AddRange(Datas);
            }

            var empData = employees.AsQueryable();
            return empData;
        }
        
         var team = DC.tblTeams.SingleOrDefault(ob => ob.ProjectID == ProjectID);
            var Data = (from obj in DC.tblTeamMembers
                        where obj.TeamID == team.TeamID
                        join obj2 in DC.tblEmployees
                        on obj.EmpID equals obj2.EmpID
                        select obj2).Distinct();

            return Data;
    }


    protected void SendNotification_Click1(object sender, EventArgs e)
    {
        try
        {
            var DC = new DataClassesDataContext();
            List<tblTeam> employees = new List<tblTeam>();
            List<tblEmployee> list = BindEmployee(Convert.ToInt32(SelectProject.SelectedValue)).ToList();
            var count = DC.tblTeams.Count(ob => ob.ProjectID == Convert.ToInt32(SelectProject.SelectedValue));
            foreach (var item in list)
            {
                EmpID = item.EmpID;
            }

            //foreach (tblEmployee item in emp )
            //{
            //    RadioButton rd = (RadioButton)item.FindControl("rdEmployee");
            //    if (rd.Checked)
            //    {
            //        EmpID = Convert.ToInt32(rd.Text);
            //    }
            //}




            //Assign Project Manager
            // bool acknow = ProjectObject.AssignProject(ProjectID, EmpID, Convert.ToInt32(Session["AdminID"]));
            //ProjectName
            tblProject ProjectName = (from ob in DC.tblProjects
                                      where ob.ProjectID == Convert.ToInt32(SelectProject.SelectedValue)
                                      select ob).Single();
            //Notification

            //for (int emp = 0; count < 0; count++)
            //{

            //}

            tblNotification Notification = new tblNotification();

            Notification.Title = "Appraisel Notification";
            string description = " <a href=MainAppraiselForm.aspx runat=server>Click Here</a>";
           // string description = "<link href=MainAppraiselForm.aspx text=Click - Here./>";
            Notification.Description = "Enter your Appraisel for" + " " + ProjectName.Title + " " + description;
            Notification.CreatedOn = DateTime.Now;
            Notification.CreatedBy = Convert.ToInt32(Session["AdminID"]);
            DC.tblNotifications.InsertOnSubmit(Notification);
            DC.SubmitChanges();
            tblNotification NID = (from obID in DC.tblNotifications
                                   orderby obID.NotificationID descending
                                   select obID).First();

            tblNotificationDetail Detail = new tblNotificationDetail();
            Detail.NotificationID = NID.NotificationID;
            Detail.PersonID = EmpID;
            Detail.IsAdmin = false;
            Detail.IsRead = false;
            Detail.IsNotify = false;
            DC.tblNotificationDetails.InsertOnSubmit(Detail);
            DC.SubmitChanges();

            Response.Redirect(Request.RawUrl);

            //Response.Write(acknow);
        }
        catch (Exception ex)
        {
            int session = Convert.ToInt32(Session["AdminID"].ToString());
            string PageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            string MACAddress = GetMacAddress();
            AddErrorLog(ref ex, PageName, "Admin", 0, session, MACAddress);
            ClientScript.RegisterStartupScript(GetType(), "abc", "alert('Something went wrong! Try again');", true);
        }
    }
}