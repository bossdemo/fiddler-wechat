using System;
using System.Windows.Forms;
using Fiddler;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class Violin : IAutoTamper    // Ensure class is public, or Fiddler won't see it!
{
    private UserInterface myCtrl; //MyControl自定义控件

    public void OnLoad()
    {
        /* Load your UI here */
        myCtrl = new UserInterface();
    }
    public void OnBeforeUnload() { }

    public void AutoTamperRequestBefore(Session oSession)
    {
        
    }
    public void AutoTamperRequestAfter(Session oSession) {
        if (oSession.url.Contains("mp.weixin.qq.com/mp/getmasssendmsg?__biz")) {
            FileStream fs = new FileStream("D:/cookie.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            string cookie = oSession.RequestHeaders.AllValues("Cookie");
            string url = oSession.url;
            string pattern = @"frommsgid=\d{5,}";
            MatchCollection matchs = Regex.Matches(url, pattern);
            string frommsgid = "";
            foreach (Match match in matchs) {
                frommsgid = match.Value;
                myCtrl.AddResult(frommsgid);
            }
            sw.WriteLine(cookie+"\t"+ url+ "\t"+ frommsgid);
            sw.Close();
        }
    }
    public void AutoTamperResponseBefore(Session oSession) { }
    public void AutoTamperResponseAfter(Session oSession) { }
    public void OnBeforeReturningError(Session oSession) { }
}