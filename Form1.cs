using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirusKiller
{
    public partial class Form1 : Form
    {
        private FileSystemWatcher watcher = null;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
           

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            watcher = new FileSystemWatcher("c:\\ProgramData\\");
            watcher.NotifyFilter = NotifyFilters.Attributes |
                                   NotifyFilters.CreationTime |
                                   NotifyFilters.DirectoryName |
                                   NotifyFilters.FileName |
                                   NotifyFilters.LastAccess |
                                   NotifyFilters.LastWrite |
                                   NotifyFilters.Security |
                                   NotifyFilters.Size;

            // Watch all files.
            watcher.Filter = "*.*";
            watcher.Created += Watcher_Created;
            watcher.EnableRaisingEvents = true;

            filesToDelete.Add("kingsoft.exe");
            filesToDelete.Add("expl0rer.exe");
            filesToDelete.Add("ieplare.exe");
            filesToDelete.Add("nssm.exe");
            filesToDelete.Add("MSASCui.exe");
            filesToDelete.Add("SystemSettlngs.exe");
            filesToDelete.Add("SystemSetting.exe");
            filesToDelete.Add("Process.exe");
            filesToDelete.Add("winlnlt.exe");
            filesToDelete.Add("WindowsUpgrade.exe");
            filesToDelete.Add("msdc.exe");
            filesToDelete.Add("Fiddlere.exe");
            filesToDelete.Add("shovst.exe");
            filesToDelete.Add("lqrtqe.exe");
            filesToDelete.Add("apkls.exe");
            filesToDelete.Add("winlog.exe");
            filesToDelete.Add("svchosts.exe");
            filesToDelete.Add("win1ogins.exe");
            filesToDelete.Add("shovsts.exe");
            filesToDelete.Add("fcty.exe");
            filesToDelete.Add("pool.exe");
            filesToDelete.Add("pool2.exe");
            filesToDelete.Add("pool3.exe");
            filesToDelete.Add("secury.exe");
            filesToDelete.Add("m.bat");
            filesToDelete.Add("4.vbs");

            filesToDelete.Add("2.exe");
            filesToDelete.Add("s2.exe");
            this.notifyIcon1 = new NotifyIcon();

            this.notifyIcon1.Icon = new System.Drawing.Icon("Icon1.ico");
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            var fullFileName = e.FullPath;
            var fileName = e.Name;

            if (filesToDelete.Contains(fileName))
            {
                txtLog.InvokeIfRequired(
                    () =>
                    {
                        txtLog.Text += "Created:" + fileName +  $"at datetime: {DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff")} " +Environment.NewLine;

                        while (!IsFileReady(fullFileName))
                        {
                            Thread.Sleep(100);
                        }
                        File.Copy(fullFileName, "c:\\ProgramData\\copyForVirus\\" + fileName, true);
                        File.Delete(fullFileName);
                        txtLog.Text += "Deleted File:" + fileName + $" at {DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff")} " + Environment.NewLine;
                    }
                );
            }
        }

        List<string> filesToDelete = new List<string>();

        public static bool IsFileReady(String sFilename)
        {
            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.
            try
            {
                using (FileStream inputStream = File.Open(sFilename, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    if (inputStream.Length > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipTitle = "Minimize to Tray App";
            notifyIcon1.BalloonTipText = "You have successfully minimized your form.";

            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }
    }
}
