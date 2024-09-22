using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Input;
using System.Threading;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;

namespace DebugInjector
{
    public partial class DebugInjectorMain : Form
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(IntPtr dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern int CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint dwFreeType);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("psapi.dll", SetLastError = true)]
        public static extern bool EnumProcessModulesEx(IntPtr hProcess, [Out] IntPtr[] lphModule, uint cb, out uint lpcbNeeded, uint dwFilterFlag);

        [DllImport("psapi.dll", CharSet = CharSet.Auto)]
        public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] char[] lpBaseName, uint nSize);


        public IntPtr PROCESS_ALL_ACCESS = (IntPtr)0x1F0FFF;
        public IntPtr LIST_MODULES_ALL = (IntPtr)0x03;

        private ConfigData configData;
        private string config = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config.json");
        private ContextMenuStrip contextMenu;
        public Panel dllPanel = new Panel();
        public string currentDllpath = "";

        public DebugInjectorMain()
        {
            InitializeComponent();

            contextMenu = new ContextMenuStrip();
            ToolStripMenuItem deleteMenuItem = new ToolStripMenuItem("Delete");

            deleteMenuItem.Click += (s, e) => { DeleteItem(s); };

            ToolStripMenuItem showInfo = new ToolStripMenuItem("Information");

            showInfo.Click += (s, e) => { ShowInfo(s); };
            
            ToolStripMenuItem openPath = new ToolStripMenuItem("Open in explorer");

            openPath.Click += (s, e) => { jumpPath(s); };

            contextMenu.Items.Add(deleteMenuItem);
            contextMenu.Items.Add(showInfo);
            contextMenu.Items.Add(openPath);
            configData = new ConfigData();

            check.Checked = true;

            timer1.Start();

            if (File.Exists(config))
            {
                try
                {
                    LoadConfigData();
                    current.Text = "Current Dll: " + Path.GetFileName(configData.Dll);
                    currentDllpath = configData.Dll;
                    if (configData.url != null)
                    {
                        urlbox.Text = configData.url;
                    }

                    InitializePanels();
                }
                catch
                {

                }
            }
            else
            {
                configData = new ConfigData
                {
                    Panels = new List<PanelData>()
                };

                File.AppendAllText(config, JsonConvert.SerializeObject(configData, Formatting.Indented));
            }

            dllPanel = (Panel)ps;
        }

        public static bool UninjectDll(string processName, string dllPath)
        {
            Process[] processes = Process.GetProcessesByName(processName);

            if (processes.Length == 0)
            {
                MessageBox.Show($"{processName} is not opened", "Debug Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            Process process = processes.First(p => p.Responding);
            IntPtr hProcess = OpenProcess((IntPtr)0x1F0FFF, false, (uint)process.Id);

            if (hProcess == IntPtr.Zero)
            {
                MessageBox.Show("Failed to open process", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            IntPtr[] hModules = new IntPtr[1024];
            uint cbNeeded;
            if (EnumProcessModulesEx(hProcess, hModules, (uint)(IntPtr.Size * hModules.Length), out cbNeeded, (uint)0x03))
            {
                int moduleCount = (int)(cbNeeded / (uint)IntPtr.Size);
                for (int i = 0; i < moduleCount; i++)
                {
                    char[] moduleFileName = new char[1024];
                    GetModuleFileNameEx(hProcess, hModules[i], moduleFileName, (uint)moduleFileName.Length);
                    string moduleFilePath = new string(moduleFileName).TrimEnd('\0');

                    if (moduleFilePath == dllPath)
                    {
                        MessageBox.Show(moduleFilePath);
                        IntPtr hThread = CreateRemoteThread(
                            hProcess, IntPtr.Zero, 0,
                            GetProcAddress(GetModuleHandle("kernel32.dll"), "FreeLibraryAndExitThread"),
                            hModules[i], 0, IntPtr.Zero);

                        if (hThread == IntPtr.Zero)
                        {
                            MessageBox.Show("Failed to create remote thread", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }

                        CloseHandle(hThread);
                        return true;
                    }
                }
            }
            return false;
        }

        private void LoadConfigData()
        {
            if (File.Exists(config))
            {
                string json = File.ReadAllText(config);
                configData = JsonConvert.DeserializeObject<ConfigData>(json);
            }
            else
            {
                configData = new ConfigData
                {
                    Panels = new List<PanelData>(),
                    Dll = ""
                };
            }
        }

        private void DebugInjectorMain_Load(object sender, EventArgs e)
        {

        }

        private void inject_Click(object sender, EventArgs e)
        {
        }

        private void inject_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "DLL File(*.dll)|*.dll";
                ofd.Title = "Select your client's dll";
                ofd.RestoreDirectory = true;
                ofd.CheckFileExists = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Process[] processes = Process.GetProcessesByName("Minecraft.Windows");

                    if (processes.Length == 0)
                    {
                        MessageBox.Show("Open Minecraft First", "Debug Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        status.Text = "Not Injected";
                        return;
                    }

                    var process = processes.First(p => p.Responding);

                    for (int i = 0; i < process.Modules.Count; i++)
                    {
                        if (process.Modules[i].FileName == ofd.FileName)
                        {
                            status.Text = "Injected";
                        }
                        else
                        {
                            status.Text = "Not Injected";
                        }
                    }

                    currentDllpath = ofd.FileName;
                    current.Text = "Current Dll: " + Path.GetFileName(ofd.FileName);

                    SaveConfigData();
                }
                else
                {
                }

            }
            else if (e.Button == MouseButtons.Left)
            {
                if (File.Exists(currentDllpath))
                {
                    Inject(currentDllpath);
                }
                else
                {
                    MessageBox.Show("DLL Not found", "Debug Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                SaveConfigData();
            }
        }

        private void Inject(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("DLL Not found", "Debug Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Process[] processes = Process.GetProcessesByName("Minecraft.Windows");

            if (processes.Length == 0)
            {
                MessageBox.Show("Open Minecraft First", "Debug Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status.Text = "Not Injected";
                return;
            }

            try
            {
                var fileInfo = new FileInfo(filePath);
                var accessControl = fileInfo.GetAccessControl();
                accessControl.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier("S-1-15-2-1"), FileSystemRights.FullControl, InheritanceFlags.None, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                fileInfo.SetAccessControl(accessControl);
            }
            catch (Exception)
            {
                MessageBox.Show("Could not set permissions, try running the injector as admin.");
            }

            var process = processes.First(p => p.Responding);

            foreach (ProcessModule module in process.Modules)
            {
                if (module.FileName == filePath)
                {
                    if (check.Checked)
                    {
                        MessageBox.Show("Already Injected!", "Debug Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        status.Text = "Injected";
                        return;
                    }
                }
            }

            string dllPath = filePath;

            currentDllpath = filePath;
            current.Text = "Current Dll: " + Path.GetFileName(filePath);
            configData.Dll = currentDllpath;

            int processId = processes[0].Id;
            IntPtr processHandle = OpenProcess(PROCESS_ALL_ACCESS, false, (uint)processId);

            byte[] dllPathBytes = Encoding.Unicode.GetBytes(dllPath + "\0");
            IntPtr remoteMemory = VirtualAllocEx(processHandle, IntPtr.Zero, (uint)dllPathBytes.Length, 0x1000, 0x40);
            if (remoteMemory == IntPtr.Zero)
            {
                MessageBox.Show("Could not allocate memory in the target process.", "Debug Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CloseHandle(processHandle);
                return;
            }

            if (!WriteProcessMemory(processHandle, remoteMemory, dllPathBytes, (uint)dllPathBytes.Length, out _))
            {
                MessageBox.Show("Could not write to process memory.", "Debug Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                VirtualFreeEx(processHandle, remoteMemory, 0, 0x8000); // MEM_RELEASE
                CloseHandle(processHandle);
                return;
            }

            IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryW");
            if (loadLibraryAddr == IntPtr.Zero)
            {
                MessageBox.Show("Could not get LoadLibraryW address.", "Debug Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                VirtualFreeEx(processHandle, remoteMemory, 0, 0x8000); // MEM_RELEASE
                CloseHandle(processHandle);
                return;
            }

            IntPtr remoteThread = CreateRemoteThread(processHandle, IntPtr.Zero, 0, loadLibraryAddr, remoteMemory, 0, IntPtr.Zero);
            if (remoteThread == IntPtr.Zero)
            {
                MessageBox.Show("Could not create remote thread.", "Debug Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                VirtualFreeEx(processHandle, remoteMemory, 0, 0x8000); // MEM_RELEASE
                CloseHandle(processHandle);
                return;
            }

            WaitForSingleObject(remoteThread, 5000);

            VirtualFreeEx(processHandle, remoteMemory, 0, 0x8000); // MEM_RELEASE
            CloseHandle(remoteThread);
            CloseHandle(processHandle);

            status.Text = "Injected";
        }

        private void DeleteItem(object sender)
        {
            if (sender is ToolStripMenuItem menuItem)
            {
                if (menuItem.Owner is ContextMenuStrip contextMenu && contextMenu.SourceControl is Panel entryPanel)
                {
                    ps.Controls.Remove(entryPanel);
                    SaveConfigData();
                }
            }
        }
        private void ShowInfo(object sender)
        {
            if (sender is ToolStripMenuItem menuItem)
            {
                if (menuItem.Owner is ContextMenuStrip contextMenu && contextMenu.SourceControl is Panel entryPanel)
                {
                    if (entryPanel.Controls.Count > 0 && entryPanel.Controls[0] is LinkLabel accessButton)
                    {
                        if (!File.Exists(accessButton.Tag.ToString())) { MessageBox.Show($"File not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                        long size = new FileInfo(accessButton.Tag.ToString()).Length;
                        MessageBox.Show($"File path: {accessButton.Tag}\nFile name: {Path.GetFileName(accessButton.Tag.ToString())}\nFile size: {size / 1024 / 1024}.{size / 1024 / 100}MB", "Here's the selected DLL's information");
                    }
                }
            }
        }

        private void jumpPath(object sender)
        {
            if (sender is ToolStripMenuItem menuItem)
            {
                if (menuItem.Owner is ContextMenuStrip contextMenu && contextMenu.SourceControl is Panel entryPanel)
                {
                    if (entryPanel.Controls.Count > 0 && entryPanel.Controls[0] is LinkLabel accessButton)
                    {
                        if (!File.Exists(accessButton.Tag.ToString())) { MessageBox.Show($"File not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                        Process.Start("explorer.exe", Path.GetDirectoryName(new FileInfo(accessButton.Tag.ToString()).FullName));
                    }
                }
            }
        }

        private void InitializePanels()
        {
            foreach (var panelData in configData.Panels)
            {
                Panel entryPanel = new Panel();
                entryPanel.Size = new System.Drawing.Size(ps.Width, 33);
                entryPanel.BorderStyle = BorderStyle.FixedSingle;

                LinkLabel accessButton = new LinkLabel();
                accessButton.Text = $"{panelData.Title}";
                accessButton.Tag = $"{panelData.Path}";
                accessButton.LinkClicked += (sender2, e2) => clicked(sender2, e2, $"{accessButton.Tag}");
                accessButton.Location = new System.Drawing.Point(10, 10);
                accessButton.Dock = DockStyle.Fill;
                accessButton.LinkBehavior = LinkBehavior.NeverUnderline;
                accessButton.LinkColor = System.Drawing.Color.White;
                accessButton.Font = new System.Drawing.Font("Open Sans", 11F);
                accessButton.Location = new System.Drawing.Point(150, 35);

                entryPanel.Controls.Add(accessButton);

                entryPanel.ContextMenuStrip = contextMenu;

                entryPanel.Dock = DockStyle.Top;

                entryPanel.BorderStyle = BorderStyle.FixedSingle;

                ps.Controls.Add(entryPanel);
            }
        }

        private void clicked(object sender, LinkLabelLinkClickedEventArgs e, string path)
        {
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    currentDllpath = path;
                    current.Text = "Current Dll: " + Path.GetFileName(path);
                    Process[] processes = Process.GetProcessesByName("Minecraft.Windows");

                    if (processes.Length == 0)
                    {
                        status.Text = "Not Injected";
                        return;
                    }

                    var process = processes.First(p => p.Responding);

                    for (int i = 0; i < process.Modules.Count; i++)
                    {
                        if (process.Modules[i].FileName == path)
                        {
                            status.Text = "Injected";
                        }
                        else
                        {
                            status.Text = "Not Injected";
                        }
                    }
                }
                catch
                {

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveConfigData();
        }

        private void SaveConfigData()
        {
            if (configData != null && configData.Panels != null)
            {
                if (urlbox.Text != null)
                {
                    configData.url = urlbox.Text;
                }
                configData.Panels?.Clear();
                foreach (Control control in ps.Controls)
                {
                    if (control is Panel entryPanel)
                    {
                        if (entryPanel.Controls.Count > 0 && entryPanel.Controls[0] is LinkLabel accessButton)
                        {
                            PanelData panelData = new PanelData
                            {
                                Title = accessButton.Text,
                                Path = accessButton.Tag.ToString()
                            };

                            configData.Panels.Add(panelData);
                        }
                    }
                }

                string json = JsonConvert.SerializeObject(configData, Formatting.Indented);
                if (json != null)
                {
                    File.WriteAllText(config, json);
                }
            }
        }

        private void addlist_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "DLL File(*.dll)|*.dll";
                ofd.Title = "Select your client's dll";
                ofd.RestoreDirectory = true;
                ofd.CheckFileExists = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (Control control in ps.Controls)
                    {
                        if (control is Panel enp)
                        {
                            if (enp.Controls.Count > 0 && enp.Controls[0] is LinkLabel linkLabel)
                            {
                                if (linkLabel.Tag.ToString() == ofd.FileName)
                                {
                                    MessageBox.Show("This DLL is already registered!", "Debug Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                    }

                    Panel entryPanel = new Panel();
                    entryPanel.Size = new System.Drawing.Size(ps.Width, 33);
                    entryPanel.BorderStyle = BorderStyle.FixedSingle;
                    LinkLabel accessButton = new LinkLabel();
                    accessButton.Text = $"{Path.GetFileName(ofd.FileName)}";
                    accessButton.Tag = $"{ofd.FileName}";
                    accessButton.LinkClicked += (sender2, e2) => clicked(sender2, e2, $"{accessButton.Tag}");
                    accessButton.Location = new System.Drawing.Point(10, 10);
                    accessButton.Dock = DockStyle.Fill;
                    accessButton.LinkBehavior = LinkBehavior.NeverUnderline;
                    accessButton.LinkColor = System.Drawing.Color.White;
                    accessButton.Font = new System.Drawing.Font("Open Sans", 11F);
                    accessButton.Location = new System.Drawing.Point(150, 35);

                    entryPanel.Controls.Add(accessButton);

                    entryPanel.ContextMenuStrip = contextMenu;

                    entryPanel.Dock = DockStyle.Top;

                    entryPanel.BorderStyle = BorderStyle.FixedSingle;

                    ps.Controls.Add(entryPanel);
                    SaveConfigData();
                }
                else
                {    
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (!File.Exists(currentDllpath))
                {
                    return;
                }
                foreach (Control control in ps.Controls)
                {
                    if (control is Panel enp)
                    {
                        if (enp.Controls.Count > 0 && enp.Controls[0] is LinkLabel linkLabel)
                        {
                            if (linkLabel.Tag.ToString() == currentDllpath)
                            {
                                MessageBox.Show("This DLL is already registered!", "Debug Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }

                Panel entryPanel = new Panel();
                entryPanel.Size = new System.Drawing.Size(ps.Width, 33);
                entryPanel.BorderStyle = BorderStyle.FixedSingle;
                LinkLabel accessButton = new LinkLabel();
                accessButton.Text = $"{Path.GetFileName(currentDllpath)}";
                accessButton.Tag = $"{currentDllpath}";
                accessButton.LinkClicked += (sender2, e2) => clicked(sender2, e2, $"{accessButton.Tag}");
                accessButton.Location = new System.Drawing.Point(10, 10);
                accessButton.Dock = DockStyle.Fill;
                accessButton.LinkBehavior = LinkBehavior.NeverUnderline;
                accessButton.LinkColor = System.Drawing.Color.White;
                accessButton.Font = new System.Drawing.Font("Open Sans", 11F);
                accessButton.Location = new System.Drawing.Point(150, 35);

                entryPanel.Controls.Add(accessButton);

                entryPanel.ContextMenuStrip = contextMenu;

                entryPanel.Dock = DockStyle.Top;

                entryPanel.BorderStyle = BorderStyle.FixedSingle;

                ps.Controls.Add(entryPanel);
                SaveConfigData();
            }
        }

        private void addlist_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("Minecraft.Windows");

            if (processes.Length == 0)
            {
                mcstat.Text = "Minecraft: Not Opened";
            } else
            {
                mcstat.Text = "Minecraft: Opened";
            }
        }

        public async Task falseTop()
        {
            await Task.Delay(2000);
            this.TopMost = false;
        }

        private async void openmc_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("Minecraft.Windows");

            this.TopMost = true;
            if (Interaction.Shell("explorer.exe shell:appsFolder\\Microsoft.MinecraftUWP_8wekyb3d8bbwe!App", Wait: false) == 0)
            {
                MessageBox.Show("Minecraft is already open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Process.Start(new ProcessStartInfo
            {
                FileName = "taskkill",
                Arguments = "/f /im runtimebroker.exe",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });
            await falseTop();
        }

        private void closemc_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("Minecraft.Windows");

            DialogResult result = MessageBox.Show("Are u sure?", "Debug Injector", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "taskkill",
                    Arguments = "/f /im Minecraft.Windows.exe",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                MessageBox.Show("Cleaning Done me go home");
                status.Text = "Not Injected";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("Minecraft.Windows");

            if (processes.Length == 0)
            {
                MessageBox.Show("Minecraft is not opened", "Debug Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status.Text = "Not Injected";
                return;
            }

            var process = processes.First(p => p.Responding);
            for (int i = 0; i < process.Modules.Count; i++)
            {
                if (process.Modules[i].FileName == currentDllpath)
                {
                    UninjectDll(process.ProcessName, currentDllpath);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Packages\\Microsoft.MinecraftUWP_8wekyb3d8bbwe\\RoamingState"));
        }

        private async void upload_Click(object sender, EventArgs e)
        {
            if (!File.Exists(currentDllpath)) {

                return; 
            }
            using (var httpClient = new HttpClient())
            {
                using (var fileStream = File.OpenRead(currentDllpath))
                {
                    var fileContent = new StreamContent(fileStream);

                    using (var formData = new MultipartFormDataContent())
                    {
                        var response2 = await httpClient.GetAsync("https://api.gofile.io/getServer");
                        var store = JsonConvert.DeserializeObject<ServerData>(await response2.Content.ReadAsStringAsync());

                        formData.Add(fileContent, "file", Path.GetFileName(currentDllpath));

                        var response = await httpClient.PostAsync("https://" + store.data.server + ".gofile.io/uploadFile", formData);
                        var responseData2 = await response.Content.ReadAsStringAsync();

                        try
                        {

                            var responseData = JsonConvert.DeserializeObject<ResponseData>(responseData2);

                            string downloadPage = responseData.data.downloadPage;

                            Clipboard.SetText(downloadPage);

                            MessageBox.Show($"Successfully uploaded to GoFile!\nLink: {downloadPage}\n(copied in clipboard)", "Debug Injector", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            MessageBox.Show("Failed to upload in GoFile!", "Debug Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void ps_Paint(object sender, PaintEventArgs e)
        {

        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void loaddllfromurl_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string temp = "c:\\windows\\temp";

            DialogResult result = MessageBox.Show("Are u sure?", "Debug Injector", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                foreach (var file in Directory.GetFiles(temp))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {

                    }
                }
                MessageBox.Show("Complete");
            }
        }

        private void loaddllfromurl_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void DebugInjectorMain_DragDrop(object sender, DragEventArgs e)
        {
        }

        private void DebugInjectorMain_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void ps_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void ps_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (string fileName in (string[])e.Data.GetData(DataFormats.FileDrop))
                {

                    string FileName = Path.GetFileName(fileName);

                    Panel entryPanel = new Panel();
                    entryPanel.Size = new System.Drawing.Size(ps.Width, 33);
                    entryPanel.BorderStyle = BorderStyle.FixedSingle;
                    LinkLabel accessButton = new LinkLabel();
                    accessButton.Text = $"{FileName}";
                    accessButton.Tag = $"{fileName}";
                    accessButton.LinkClicked += (sender2, e2) => clicked(sender2, e2, $"{accessButton.Tag}");
                    accessButton.Location = new System.Drawing.Point(10, 10);
                    accessButton.Dock = DockStyle.Fill;
                    accessButton.LinkBehavior = LinkBehavior.NeverUnderline;
                    accessButton.LinkColor = System.Drawing.Color.White;
                    accessButton.Font = new System.Drawing.Font("Open Sans", 11F);
                    accessButton.Location = new System.Drawing.Point(150, 35);

                    entryPanel.Controls.Add(accessButton);

                    entryPanel.ContextMenuStrip = contextMenu;

                    entryPanel.Dock = DockStyle.Top;

                    entryPanel.BorderStyle = BorderStyle.FixedSingle;

                    ps.Controls.Add(entryPanel);
                    SaveConfigData();
                }
            }
        }
    }

    public class PanelData
    {
        public string Title;
        public string Path;
    }

    public class ConfigData
    {
        public List<PanelData> Panels = new List<PanelData>();
        public string Dll { get; set; }
        public string url { get; set; }
    }

    public class ResponseData
    {
        public string status { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string downloadPage { get; set; }
        public string code { get; set; }
        public string parentFolder { get; set; }
        public string fileId { get; set; }
        public string fileName { get; set; }
        public string md5 { get; set; }
    }

    public class ServerData
    {
        public string status { get; set; }
        public storeData data { get; set; }
    }

    public class storeData
    {
        public string server { get; set; }
    }
}