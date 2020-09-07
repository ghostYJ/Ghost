using System;

using Ghost.DDD;
using Ghost.Login.IDao;
using Ghost.Login.Domain;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Reflection;

public partial class test : System.Web.UI.Page
{
    public bool isTrue { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        string s = "2";
        IList<string> list = new List<string>() { s };
        gv.DataSource = list;
        gv.DataBind();

    }

    protected void btnClick_Click(object sender, EventArgs e)
    {
        //Object target = "Jerry";
        //Object arg = "ff";
        //Type[] argTypes = new Type[] { arg.GetType() };
        //Type[] targetTypes = new Type[] { target.GetType() };

        //MethodInfo method = target.GetType().GetMethod("Contains", targetTypes);

        //Object[] arguments = new Object[] { arg };
        //Boolean result = Convert.ToBoolean(method.Invoke(target, arguments));
        //dynamic target = "Jeryy";
        //dynamic arg = "ff";
        //Boolean result = target.Contains(arg);

        //lblTest.Text = result ? "yes" : "no";

        //string name = "Jeryy";
        //string lastName = name.Substring(1);
        //lblTest.Text = lastName;
        //GhostUser user = new GhostUser();
        //user.NickName = "我家有个呆子";
        //user.RealName = "小明";
        //user.Sex = Sex.保密;
        //user.Mobile = "18502502070";
        //user.PassWord = "123";
        //user.Email = "728607814@qq.com";
        //user.Province = "江苏省";
        //user.City = "南京";
        //user.BirthDay = new DateTime(1994, 09, 09);
        //user.IsVip = true;
        //user.VipLevel = VipLevel.VipVI;
        //user.AccountBalance = new decimal(100000);
        //IocContainer.Get<IGhostUserDao>().Insert(user);
        //GhostUser user = IocContainer.Get<IGhostUserDao>().SelectById(2792325440969113600);

        B a = new B();
        
        lblTest.Text = a.Show() + "   " + a.Show1();
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string s = (string)e.Row.DataItem;
            Label lblTest2 = e.Row.FindControl("lblTest2") as Label;

            lblTest2.Text = s;
        }
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        byte b = 100;
        b = (byte)(b + 200);
        int row = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
        TextBox tbTest1 = gv.Rows[row].FindControl("tbTest1") as TextBox;
        Label lblTest2 = gv.Rows[row].FindControl("lblTest2") as Label;

        tbTest1.Text = "已变化";
        lblTest2.Text = "2";
    }
    public void SetOut(ref TextBox s1)
    {

    }

    public class A
    {
        public string Show()
        {
            return "a";
        }

        public virtual string Show1()
        {
            return "a";
        }
    }
    public class B : A
    {
        public string Show()
        {
            return "b";
        }
        public override string Show1()
        {
            return "b";
        }
    }

}