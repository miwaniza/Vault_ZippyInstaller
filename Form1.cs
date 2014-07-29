using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Globalization;
using System.Management;
using Microsoft.Web.Administration;



namespace WindowsFormsApplication1
    {
    public partial class Form1 : Form
        {
        
        public Form1 ()
            {
            InitializeComponent();
            InitForm();
            _syncContext = SynchronizationContext.Current;
            CheckUacState();
            CheckRebootNeeded();
            CheckIISState();
            CheckIISTimeoutState();
            
            }

 

        private void InitForm ()
            {
            System.Resources.ResourceManager resources = new
            System.Resources.ResourceManager( typeof( Form1 ) );
                       }

        private void CheckUacState ()
            {
            if (!GetRegValueBoolState(@"Software\Microsoft\Windows\CurrentVersion\policies\system", "EnableLUA"))
            {
                buttonUAC.Text = "UAC is OFF - OK";
                buttonUAC.BackColor = Color.LightGreen;
                buttonUAC.Refresh();
            }
            else
            {
                buttonUAC.Text = "UAC is ON - Click to disable";
                buttonUAC.BackColor = Color.OrangeRed;
                buttonUAC.Refresh();
            };
            }

        private void GetIISTimeoutState()
        {
            listBox1.Items.Clear();
            double timeout;
            using (ServerManager serverManager = new ServerManager())
            
            {
                Configuration config = serverManager.GetApplicationHostConfiguration();
                ConfigurationSection sitesSection = config.GetSection("system.applicationHost/sites");
                ConfigurationElementCollection allSites = sitesSection.GetCollection();
                
                foreach(ConfigurationElement site in allSites)
                {
                    
                    ConfigurationElement limitsElement = site.GetChildElement("limits");
                    TimeSpan t = (TimeSpan) limitsElement["connectionTimeout"];
                    timeout = t.TotalSeconds;
                    if (timeout < 900)
                    {
                        listBox1.Items.Add(site["name"] );

                    }
                    
                }
               
            }
            
            
        }

        private void CheckIISTimeoutState()
        {
            GetIISTimeoutState();

            if (listBox1.Items.Count == 0)
            {
                buttonTimeout.Text = "All sites' timeouts are greater then 900s - OK";
                buttonTimeout.BackColor = Color.LightGreen;
                buttonTimeout.Refresh();
            }
            else
            {
                buttonTimeout.Text = "Select site and from list\nand click here to set timeout to 900s";
                buttonTimeout.BackColor = Color.OrangeRed;
                buttonTimeout.Refresh();
            };
           
        }



        private bool GetRegRebootNeeded()
            {
                
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Control\Session Manager", false);

                // get value
                object value = key.GetValue("PendingFileRenameOperations", null, RegistryValueOptions.None);
            
                if (value == null)
                {
                    return false;
                }
                else
                {
                    return true;
                };
        }

        private void CheckRebootNeeded()
        {
            if (!GetRegRebootNeeded())
            {
                buttonReboot.Text = "Reboot not needed - OK";
                buttonReboot.BackColor = Color.LightGreen;
                buttonReboot.Refresh();
            }
            else
            {
                buttonReboot.Text = "Reboot needed - Click to disable";
                buttonReboot.BackColor = Color.OrangeRed;
                buttonReboot.Refresh();
            };
        }

        private void CheckIISState()
        {
            if (GetRegIISState().Count==0)
            {
                buttonIIS.Text = "IIS installation not needed - OK";
                buttonIIS.BackColor = Color.LightGreen;
                buttonIIS.Refresh();
            }
            else
            {
                buttonIIS.Text = "IIS installation needed - Click to start";
                buttonIIS.BackColor = Color.OrangeRed;
                buttonIIS.Refresh();
            };
        }

        private bool GetRegValueBoolState(string subKey, string keyValue)
        {

            RegistryKey key = Registry.LocalMachine.OpenSubKey(subKey, false);
            object value = key.GetValue(keyValue, 0, RegistryValueOptions.None);
            return Convert.ToBoolean(value);

        }

        private void buttonASP_Click ( object sender, EventArgs e )
            {
            ReRegisterASPFilters();
            }

        private void ReRegisterASPFilters ()
            {
            
            try
                {
                string windir = Environment.GetEnvironmentVariable("windir");
                Process first = Process.Start( windir+@"\microsoft.net\framework\v4.0.30319\aspnet_regiis.exe", "-i -enable" );
                first.WaitForExit();
                Process second = Process.Start( windir + @"\microsoft.net\framework64\v4.0.30319\aspnet_regiis.exe", "-i -enable");
                second.WaitForExit();
                }
            catch
                {
                MessageBox.Show( "Smth. wrong" );
                }
            finally
                {
                MessageBox.Show( "Done!" );
                }
            }


        public SynchronizationContext _syncContext { get; set; }

        private void buttonIIS_Click ( object sender, EventArgs e )
            {
            InstallIIS();
            }

        private void InstallIIS ()
            {

            List<string> FeaturesToInstall = GetRegIISState();

            string str = "";
            if (FeaturesToInstall.Count == 0)
            {
                MessageBox.Show("All IIS features are already installed");
                return;
            };
            foreach (string feature in FeaturesToInstall)
            {
                str += " - " + feature + "\n";
            };

            FeaturesToInstall.Add("IIS-ManagementConsole"); 
            FeaturesToInstall.Add("IIS-WebServerManagementTools");
            FeaturesToInstall.Add("IIS-Performance");
            FeaturesToInstall.Add("IIS-HealthAndDiagnostics");
            FeaturesToInstall.Add("IIS-IIS6ManagementCompatibility");
            FeaturesToInstall.Add("WAS-WindowsActivationService");
            
            DialogResult dr = MessageBox.Show("This operation will install next features:\n" + str, "IIS installation", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                string cmdargument = "";
                foreach (string feature in FeaturesToInstall)
                {
                    cmdargument += @" /FeatureName:" + feature;
                };

                try
                {
                    Process proc = new Process();
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.FileName=Environment.GetEnvironmentVariable("windir") + "\\SysNative\\dism.exe";
                    proc.StartInfo.Arguments=@"/Online /Enable-Feature" + cmdargument;
                    
                    proc.Start();
                    
                    proc.WaitForExit();
                    
                    proc.Close();
                }
                catch
                {
                    MessageBox.Show("Smth. wrong");
                }
                finally
                {
                    MessageBox.Show("Done!");
                };
            };
            CheckIISState();
            }

        private List<string> GetRegIISState()
        {
            string[,] input = { { "IIS-WebServerRole", "W3SVC" },
                                    {"IIS-ASP","ASPNET"},
                                    {"IIS-Metabase","Metabase"},
                                    //{"IIS-IIS6ManagementCompatibility"," "},
                                    //{"IIS-ManagementConsole","LegacySnapin"},
                                    {"IIS-NetFxExtensibility","NetFxExtensibility"},
                                    {"IIS-ISAPIExtensions","ISAPIExtensions"},
                                    {"IIS-ISAPIFilter","ISAPIFilter"},
                                    {"IIS-DefaultDocument","DefaultDocument"},
                                    {"IIS-DirectoryBrowsing","DirectoryBrowse"},
                                    {"IIS-HttpErrors","HttpErrors"},
                                    {"IIS-StaticContent","HttpCompressionStatic"},
                                    {"IIS-HttpLogging","HttpLogging"},
                                    {"IIS-RequestMonitor","RequestMonitor"},
                                    {"IIS-HttpCompressionStatic","HttpCompressionStatic"},
                                    {"IIS-RequestFiltering","RequestFiltering"},
                                    //{"WAS-WindowsActivationService"," "},
                                    {"WAS-ProcessModel","ProcessModel"},
                                    {"WAS-NetFxEnvironment","NetFxEnvironment"},
                                    {"WAS-ConfigurationAPI", "WASConfigurationAPI" }};


            List<string> FeaturesToInstall = new List<string>();
            
            FeaturesToInstall.AddRange(FilterInstalledFeatures(input));
            return FeaturesToInstall;
        }


        

        private IEnumerable<string> FilterInstalledFeatures(string[,] input)
        {
            List<string> FeaturesToInstall = new List<string>();

            for (int i = 0; i < input.GetLength(0); i++)
            {
                if (!GetRegValueBoolState(@"Software\Microsoft\InetStp\Components\", input[i, 1]))
                {
                    FeaturesToInstall.Add(input[i, 0]);
                }
            }
            

            
            return FeaturesToInstall;
        }

    

        private void buttonUAC_Click ( object sender, EventArgs e )
            {
            bool UACstate = GetRegValueBoolState(@"Software\Microsoft\Windows\CurrentVersion\policies\system", "EnableLUA");
            SetRegUacState( !UACstate );
            CheckUacState();
            }

        private void Form1_Load ( object sender, EventArgs e )
            {
            
            }

        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            DisplayHelp();
           
        }

        private void DisplayHelp()
        {
            string filename = "Vault Zippy Installer Help.txt";
            if (!System.IO.File.Exists(filename))
                System.IO.File.WriteAllText(filename, Properties.Resources.Help);
            System.Diagnostics.Process.Start(filename);
        }

        private static void Export(string exportPath, string registryPath)
        {
            string path = "\"" + exportPath + "\"";
            string key = "\"" + registryPath + "\"";
            Process proc = new Process();

            try
            {
                proc.StartInfo.FileName = "regedit.exe";
                proc.StartInfo.UseShellExecute = false;

                proc = Process.Start("regedit.exe", "/e " + path + " " + key + "");
                proc.WaitForExit();
            }
            catch (Exception)
            {
                proc.Dispose();
            }
        }

        private void buttonReboot_Click(object sender, EventArgs e)
        {
            if (GetRegRebootNeeded())
            {
                HackRebootNeeded();
            }
            else
            {
                MessageBox.Show("Everything is OK - just relax!");
            }
            CheckRebootNeeded();
        }

        private void SetRegUacState(bool state)
        {
            if (state == false)
            {
                DialogResult dr = MessageBox.Show("This will disable UAC", "Attention", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (dr == DialogResult.OK)
                {
                    Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\policies\system", "EnableLUA", Convert.ToInt32(state));
                }
            }
            else
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\policies\system", "EnableLUA", Convert.ToInt32(state));
            }
        }

        private void SetIISTimeout(string IISsiteName, double timeout)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                Configuration config = serverManager.GetApplicationHostConfiguration();
                ConfigurationSection sitesSection = config.GetSection("system.applicationHost/sites");
                ConfigurationElementCollection sitesCollection = sitesSection.GetCollection();

                ConfigurationElement siteElement = FindElement(sitesCollection, "site", "name", @"Default Web Site");
                if (siteElement == null) throw new InvalidOperationException("Element not found!");

                ConfigurationElement limitsElement = siteElement.GetChildElement("limits");
                limitsElement["connectionTimeout"] = TimeSpan.FromSeconds(timeout);

                serverManager.CommitChanges();
            }
        }

        private static ConfigurationElement FindElement(ConfigurationElementCollection collection, string elementTagName, params string[] keyValues)
        {
            foreach (ConfigurationElement element in collection)
            {
                if (String.Equals(element.ElementTagName, elementTagName, StringComparison.OrdinalIgnoreCase))
                {
                    bool matches = true;
                    for (int i = 0; i < keyValues.Length; i += 2)
                    {
                        object o = element.GetAttributeValue(keyValues[i]);
                        string value = null;
                        if (o != null)
                        {
                            value = o.ToString();
                        }
                        if (!String.Equals(value, keyValues[i + 1], StringComparison.OrdinalIgnoreCase))
                        {
                            matches = false;
                            break;
                        }
                    }
                    if (matches)
                    {
                        return element;
                    }
                }
            }
            return null;
        }

        private void HackRebootNeeded()
        {
            DialogResult dr = MessageBox.Show("This operation will delete registry key value. Create a backup?\n - Yes - to delete with backup.\n - No - just delete.\n - Cancel - to abort.", "Attention", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

            switch (dr)
            {
                case DialogResult.Yes:
                    {
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                        saveFileDialog1.Filter = "All files (*.*)|*.*|Registry files (*.reg)|*.reg";
                        saveFileDialog1.FilterIndex = 2;
                        saveFileDialog1.RestoreDirectory = true;
                        saveFileDialog1.OverwritePrompt = true;
                        saveFileDialog1.FileName = "PendingFileRenameOperations_bak";
                        
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            Export(saveFileDialog1.FileName, @"HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Session Manager");  
                        }

                        RegistryKey key = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Control\Session Manager", true);
                        key.DeleteValue("PendingFileRenameOperations", false);
                        
                    }
                    break;
                case DialogResult.No:
                    {
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Control\Session Manager", true);
                        key.DeleteValue("PendingFileRenameOperations", false);
                    }
                    break;
                case DialogResult.Cancel:
                    break;
                default:
                    break;
            };

            

        }

        private void buttonTimeout_Click(object sender, EventArgs e)
        {
            if ((listBox1.SelectedItem != null)&&(listBox1.Items.Count>0))
            {
                SetIISTimeout(listBox1.SelectedItem.ToString(), 900);
                MessageBox.Show("Timeout increased to 900s");
            }
            if ((listBox1.SelectedItem == null) && (listBox1.Items.Count > 0))
            {
                MessageBox.Show("Select site, please");
            }

            if ((listBox1.SelectedItem == null) && (listBox1.Items.Count == 0))
            {
                MessageBox.Show("Keep calm - all's O.K.");
            }

            CheckIISTimeoutState();
        }



        }
    }
