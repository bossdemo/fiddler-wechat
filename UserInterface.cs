using Fiddler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


class UserInterface : UserControl
{
    private TabPage tabPage; //添加一个标签页 用来放置控件
    private CheckBox chkb_Enabled;  //用来启用或禁用插件
    private TextBox textBox_Result;  //用来保存最后的结果
    private Button btn_Clear;  //清空按钮
    public bool bEnabled;
    public delegate void Delegate_AddResult(string strUrl);//定义输出结果的委托
    public UserInterface()
    {
        this.bEnabled = false;
        this.InitializeUI();
        FiddlerApplication.UI.tabsViews.TabPages.Add(this.tabPage); //新建一个tabPage
    }

    public void InitializeUI() //初始化UI
    {
        this.tabPage = new TabPage("WechatArticle");
        this.tabPage.AutoScroll = true;

        this.chkb_Enabled = new CheckBox();
        this.chkb_Enabled.Top = 10;
        this.chkb_Enabled.Left = 20;
        this.chkb_Enabled.Text = "Enable";
        this.chkb_Enabled.Checked = false;  //初始化为不选中

        this.btn_Clear = new Button();
        this.btn_Clear.Text = "Clear";
        this.btn_Clear.Left = 200;
        this.btn_Clear.Top = 10;
        this.Enabled = false;

        this.textBox_Result = new TextBox();
        this.textBox_Result.Top = 50;
        this.textBox_Result.Left = 20;
        this.textBox_Result.Width = 1000;
        this.textBox_Result.Height = 600;
        this.textBox_Result.ReadOnly = true;
        this.textBox_Result.Multiline = true;  //多行显示
        this.textBox_Result.ScrollBars = ScrollBars.Vertical;  //垂直滚动条

        this.tabPage.Controls.Add(this.chkb_Enabled);
        this.tabPage.Controls.Add(this.btn_Clear);
        this.tabPage.Controls.Add(this.textBox_Result);

        /*给chkb_Enabled添加CheckedChanged事件处理*/
        this.chkb_Enabled.CheckedChanged += new EventHandler(this.chkb_Enabled_CheckedChanged);
        this.btn_Clear.Click += new EventHandler(this.btn_Clear_Click);
    }

    private void btn_Clear_Click(object obj, EventArgs args)
    {
        this.textBox_Result.Text = "";
    }
    private void chkb_Enabled_CheckedChanged(object obj, EventArgs args)
    {
        this.SuspendLayout();
        this.bEnabled = this.chkb_Enabled.Checked;
        this.btn_Clear.Enabled = this.bEnabled;
        this.ResumeLayout();
    }

    public void AddResult(string strUrl)
    {
        if (!this.textBox_Result.InvokeRequired)
            this.textBox_Result.AppendText(strUrl + "\r\n");
        else
        {
            Delegate_AddResult delegate_addresult = new Delegate_AddResult(this.AddResult);
            this.textBox_Result.Invoke(delegate_addresult, strUrl);
        }
    }

}
