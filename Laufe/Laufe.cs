using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Laufe
{
    public partial class Laufe : Form
    {

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        private delegate void SaveSetting(string path);

        private bool isLock = false;
        private int Index;
        private XmlDocument xdSetting = new XmlDocument();
        private XmlNode xnRoot;
        private XmlElement xeCurrent;
        private readonly IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShellClass();
        private readonly Color colorKey = Color.FromArgb(28, 28, 28);
        private readonly Color colorKeyHighLight = Color.FromArgb(128, 28, 28);
        private readonly Color colorFrame = Color.FromArgb(129, 199, 212);
        private readonly Color colorFrameLocked = Color.FromArgb(204, 150, 12);
        private readonly Color colorMain = Color.FromArgb(72, 156, 186);
        private readonly Color colorIcon = Color.FromArgb(185, 232, 237);

        private struct ExeFile
        {
            internal string Path;
            internal string Arguments;
            internal string WorkingDirectory;
            internal Bitmap Icon;
        }

        public Laufe()
        {
            InitializeComponent();

            #region 控件颜色
            this.BackColor = colorFrame;
            this.plnMain.BackColor = colorMain;
            pnl1.BackColor = colorKey;
            lbl1.BackColor = colorKey;
            pnl2.BackColor = colorKey;
            lbl2.BackColor = colorKey;
            pnl3.BackColor = colorKey;
            lbl3.BackColor = colorKey;
            pnl4.BackColor = colorKey;
            lbl4.BackColor = colorKey;
            pnl5.BackColor = colorKey;
            lbl5.BackColor = colorKey;
            pnl6.BackColor = colorKey;
            lbl6.BackColor = colorKey;
            pnl7.BackColor = colorKey;
            lbl7.BackColor = colorKey;
            pnl8.BackColor = colorKey;
            lbl8.BackColor = colorKey;
            pnl9.BackColor = colorKey;
            lbl9.BackColor = colorKey;
            pnl0.BackColor = colorKey;
            lbl0.BackColor = colorKey;
            pnlQ.BackColor = colorKey;
            lblQ.BackColor = colorKey;
            pnlW.BackColor = colorKey;
            lblW.BackColor = colorKey;
            pnlE.BackColor = colorKey;
            lblE.BackColor = colorKey;
            pnlR.BackColor = colorKey;
            lblR.BackColor = colorKey;
            pnlT.BackColor = colorKey;
            lblT.BackColor = colorKey;
            pnlY.BackColor = colorKey;
            lblY.BackColor = colorKey;
            pnlU.BackColor = colorKey;
            lblU.BackColor = colorKey;
            pnlI.BackColor = colorKey;
            lblI.BackColor = colorKey;
            pnlO.BackColor = colorKey;
            lblO.BackColor = colorKey;
            pnlP.BackColor = colorKey;
            lblP.BackColor = colorKey;
            pnlA.BackColor = colorKey;
            lblA.BackColor = colorKey;
            pnlS.BackColor = colorKey;
            lblS.BackColor = colorKey;
            pnlD.BackColor = colorKey;
            lblD.BackColor = colorKey;
            pnlF.BackColor = colorKey;
            lblF.BackColor = colorKey;
            pnlG.BackColor = colorKey;
            lblG.BackColor = colorKey;
            pnlH.BackColor = colorKey;
            lblH.BackColor = colorKey;
            pnlJ.BackColor = colorKey;
            lblJ.BackColor = colorKey;
            pnlK.BackColor = colorKey;
            lblK.BackColor = colorKey;
            pnlL.BackColor = colorKey;
            lblL.BackColor = colorKey;
            pnlSemicolon.BackColor = colorKey;
            lblSemicolon.BackColor = colorKey;
            pnlZ.BackColor = colorKey;
            lblZ.BackColor = colorKey;
            pnlX.BackColor = colorKey;
            lblX.BackColor = colorKey;
            pnlC.BackColor = colorKey;
            lblC.BackColor = colorKey;
            pnlV.BackColor = colorKey;
            lblV.BackColor = colorKey;
            pnlB.BackColor = colorKey;
            lblB.BackColor = colorKey;
            pnlN.BackColor = colorKey;
            lblN.BackColor = colorKey;
            pnlM.BackColor = colorKey;
            lblM.BackColor = colorKey;
            pnlComma.BackColor = colorKey;
            lblComma.BackColor = colorKey;
            pnlPeriod.BackColor = colorKey;
            lblPeriod.BackColor = colorKey;
            pnlQuestion.BackColor = colorKey;
            lblQuestion.BackColor = colorKey;
            this.picQ.BackColor = colorIcon;
            this.picW.BackColor = colorIcon;
            this.picE.BackColor = colorIcon;
            this.picR.BackColor = colorIcon;
            this.picT.BackColor = colorIcon;
            this.picY.BackColor = colorIcon;
            this.picU.BackColor = colorIcon;
            this.picI.BackColor = colorIcon;
            this.picO.BackColor = colorIcon;
            this.picP.BackColor = colorIcon;
            this.picA.BackColor = colorIcon;
            this.picS.BackColor = colorIcon;
            this.picD.BackColor = colorIcon;
            this.picF.BackColor = colorIcon;
            this.picG.BackColor = colorIcon;
            this.picH.BackColor = colorIcon;
            this.picJ.BackColor = colorIcon;
            this.picK.BackColor = colorIcon;
            this.picL.BackColor = colorIcon;
            this.picSemicolon.BackColor = colorIcon;
            this.picZ.BackColor = colorIcon;
            this.picX.BackColor = colorIcon;
            this.picC.BackColor = colorIcon;
            this.picV.BackColor = colorIcon;
            this.picB.BackColor = colorIcon;
            this.picN.BackColor = colorIcon;
            this.picM.BackColor = colorIcon;
            this.picComma.BackColor = colorIcon;
            this.picPeriod.BackColor = colorIcon;
            this.picQuestion.BackColor = colorIcon;
            #endregion

            try
            {
                xdSetting.Load(Application.StartupPath + @"\Laufe.xml");
            }
            catch
            {
                xnRoot = xdSetting.CreateNode(XmlNodeType.Element, "Laufe", "");
                for (int i = 0; i < 10; i++)
                {
                    XmlElement xePanel = xdSetting.CreateElement("Panel");
                    xePanel.SetAttribute("Index", i.ToString());
                    if (i == 0)
                    {
                        xePanel.SetAttribute("Current", "true");
                    }
                    else
                    {
                        xePanel.SetAttribute("Current", "false");
                    }
                    xnRoot.AppendChild(xePanel);
                }

                xdSetting.AppendChild(xnRoot);
                xdSetting.Save(Application.StartupPath + @"\Laufe.xml");
            }

            #region 事件注册

            picQ.AllowDrop = true;
            picW.AllowDrop = true;
            picE.AllowDrop = true;
            picR.AllowDrop = true;
            picT.AllowDrop = true;
            picY.AllowDrop = true;
            picU.AllowDrop = true;
            picI.AllowDrop = true;
            picO.AllowDrop = true;
            picP.AllowDrop = true;
            picA.AllowDrop = true;
            picS.AllowDrop = true;
            picD.AllowDrop = true;
            picF.AllowDrop = true;
            picG.AllowDrop = true;
            picH.AllowDrop = true;
            picJ.AllowDrop = true;
            picK.AllowDrop = true;
            picL.AllowDrop = true;
            picSemicolon.AllowDrop = true;
            picZ.AllowDrop = true;
            picX.AllowDrop = true;
            picC.AllowDrop = true;
            picV.AllowDrop = true;
            picB.AllowDrop = true;
            picN.AllowDrop = true;
            picM.AllowDrop = true;
            picComma.AllowDrop = true;
            picPeriod.AllowDrop = true;
            picQuestion.AllowDrop = true;

            picQ.DragEnter += pictureBox_DragEnter;
            picW.DragEnter += pictureBox_DragEnter;
            picE.DragEnter += pictureBox_DragEnter;
            picR.DragEnter += pictureBox_DragEnter;
            picT.DragEnter += pictureBox_DragEnter;
            picY.DragEnter += pictureBox_DragEnter;
            picU.DragEnter += pictureBox_DragEnter;
            picI.DragEnter += pictureBox_DragEnter;
            picO.DragEnter += pictureBox_DragEnter;
            picP.DragEnter += pictureBox_DragEnter;
            picA.DragEnter += pictureBox_DragEnter;
            picS.DragEnter += pictureBox_DragEnter;
            picD.DragEnter += pictureBox_DragEnter;
            picF.DragEnter += pictureBox_DragEnter;
            picG.DragEnter += pictureBox_DragEnter;
            picH.DragEnter += pictureBox_DragEnter;
            picJ.DragEnter += pictureBox_DragEnter;
            picK.DragEnter += pictureBox_DragEnter;
            picL.DragEnter += pictureBox_DragEnter;
            picSemicolon.DragEnter += pictureBox_DragEnter;
            picZ.DragEnter += pictureBox_DragEnter;
            picX.DragEnter += pictureBox_DragEnter;
            picC.DragEnter += pictureBox_DragEnter;
            picV.DragEnter += pictureBox_DragEnter;
            picB.DragEnter += pictureBox_DragEnter;
            picN.DragEnter += pictureBox_DragEnter;
            picM.DragEnter += pictureBox_DragEnter;
            picComma.DragEnter += pictureBox_DragEnter;
            picPeriod.DragEnter += pictureBox_DragEnter;
            picQuestion.DragEnter += pictureBox_DragEnter;

            picQ.DragLeave += pictureBox_DragLeave;
            picW.DragLeave += pictureBox_DragLeave;
            picE.DragLeave += pictureBox_DragLeave;
            picR.DragLeave += pictureBox_DragLeave;
            picT.DragLeave += pictureBox_DragLeave;
            picY.DragLeave += pictureBox_DragLeave;
            picU.DragLeave += pictureBox_DragLeave;
            picI.DragLeave += pictureBox_DragLeave;
            picO.DragLeave += pictureBox_DragLeave;
            picP.DragLeave += pictureBox_DragLeave;
            picA.DragLeave += pictureBox_DragLeave;
            picS.DragLeave += pictureBox_DragLeave;
            picD.DragLeave += pictureBox_DragLeave;
            picF.DragLeave += pictureBox_DragLeave;
            picG.DragLeave += pictureBox_DragLeave;
            picH.DragLeave += pictureBox_DragLeave;
            picJ.DragLeave += pictureBox_DragLeave;
            picK.DragLeave += pictureBox_DragLeave;
            picL.DragLeave += pictureBox_DragLeave;
            picSemicolon.DragLeave += pictureBox_DragLeave;
            picZ.DragLeave += pictureBox_DragLeave;
            picX.DragLeave += pictureBox_DragLeave;
            picC.DragLeave += pictureBox_DragLeave;
            picV.DragLeave += pictureBox_DragLeave;
            picB.DragLeave += pictureBox_DragLeave;
            picN.DragLeave += pictureBox_DragLeave;
            picM.DragLeave += pictureBox_DragLeave;
            picComma.DragLeave += pictureBox_DragLeave;
            picPeriod.DragLeave += pictureBox_DragLeave;
            picQuestion.DragLeave += pictureBox_DragLeave;

            picQ.DragDrop += pictureBox_DragDrop;
            picW.DragDrop += pictureBox_DragDrop;
            picE.DragDrop += pictureBox_DragDrop;
            picR.DragDrop += pictureBox_DragDrop;
            picT.DragDrop += pictureBox_DragDrop;
            picY.DragDrop += pictureBox_DragDrop;
            picU.DragDrop += pictureBox_DragDrop;
            picI.DragDrop += pictureBox_DragDrop;
            picO.DragDrop += pictureBox_DragDrop;
            picP.DragDrop += pictureBox_DragDrop;
            picA.DragDrop += pictureBox_DragDrop;
            picS.DragDrop += pictureBox_DragDrop;
            picD.DragDrop += pictureBox_DragDrop;
            picF.DragDrop += pictureBox_DragDrop;
            picG.DragDrop += pictureBox_DragDrop;
            picH.DragDrop += pictureBox_DragDrop;
            picJ.DragDrop += pictureBox_DragDrop;
            picK.DragDrop += pictureBox_DragDrop;
            picL.DragDrop += pictureBox_DragDrop;
            picSemicolon.DragDrop += pictureBox_DragDrop;
            picZ.DragDrop += pictureBox_DragDrop;
            picX.DragDrop += pictureBox_DragDrop;
            picC.DragDrop += pictureBox_DragDrop;
            picV.DragDrop += pictureBox_DragDrop;
            picB.DragDrop += pictureBox_DragDrop;
            picN.DragDrop += pictureBox_DragDrop;
            picM.DragDrop += pictureBox_DragDrop;
            picComma.DragDrop += pictureBox_DragDrop;
            picPeriod.DragDrop += pictureBox_DragDrop;
            picQuestion.DragDrop += pictureBox_DragDrop;

            pnl1.MouseClick += Index_Chanage;
            pnl2.MouseClick += Index_Chanage;
            pnl3.MouseClick += Index_Chanage;
            pnl4.MouseClick += Index_Chanage;
            pnl5.MouseClick += Index_Chanage;
            pnl6.MouseClick += Index_Chanage;
            pnl7.MouseClick += Index_Chanage;
            pnl8.MouseClick += Index_Chanage;
            pnl9.MouseClick += Index_Chanage;
            pnl0.MouseClick += Index_Chanage;

            lbl1.MouseClick += Index_Chanage;
            lbl2.MouseClick += Index_Chanage;
            lbl3.MouseClick += Index_Chanage;
            lbl4.MouseClick += Index_Chanage;
            lbl5.MouseClick += Index_Chanage;
            lbl6.MouseClick += Index_Chanage;
            lbl7.MouseClick += Index_Chanage;
            lbl8.MouseClick += Index_Chanage;
            lbl9.MouseClick += Index_Chanage;
            lbl0.MouseClick += Index_Chanage;

            pnl1.DragEnter += Index_Chanage;
            pnl2.DragEnter += Index_Chanage;
            pnl3.DragEnter += Index_Chanage;
            pnl4.DragEnter += Index_Chanage;
            pnl5.DragEnter += Index_Chanage;
            pnl6.DragEnter += Index_Chanage;
            pnl7.DragEnter += Index_Chanage;
            pnl8.DragEnter += Index_Chanage;
            pnl9.DragEnter += Index_Chanage;
            pnl0.DragEnter += Index_Chanage;

            lbl1.DragEnter += Index_Chanage;
            lbl2.DragEnter += Index_Chanage;
            lbl3.DragEnter += Index_Chanage;
            lbl4.DragEnter += Index_Chanage;
            lbl5.DragEnter += Index_Chanage;
            lbl6.DragEnter += Index_Chanage;
            lbl7.DragEnter += Index_Chanage;
            lbl8.DragEnter += Index_Chanage;
            lbl9.DragEnter += Index_Chanage;
            lbl0.DragEnter += Index_Chanage;

            picQ.MouseEnter += Key_MouseEnter;
            picW.MouseEnter += Key_MouseEnter;
            picE.MouseEnter += Key_MouseEnter;
            picR.MouseEnter += Key_MouseEnter;
            picT.MouseEnter += Key_MouseEnter;
            picY.MouseEnter += Key_MouseEnter;
            picU.MouseEnter += Key_MouseEnter;
            picI.MouseEnter += Key_MouseEnter;
            picO.MouseEnter += Key_MouseEnter;
            picP.MouseEnter += Key_MouseEnter;
            picA.MouseEnter += Key_MouseEnter;
            picS.MouseEnter += Key_MouseEnter;
            picD.MouseEnter += Key_MouseEnter;
            picF.MouseEnter += Key_MouseEnter;
            picG.MouseEnter += Key_MouseEnter;
            picH.MouseEnter += Key_MouseEnter;
            picJ.MouseEnter += Key_MouseEnter;
            picK.MouseEnter += Key_MouseEnter;
            picL.MouseEnter += Key_MouseEnter;
            picSemicolon.MouseEnter += Key_MouseEnter;
            picZ.MouseEnter += Key_MouseEnter;
            picX.MouseEnter += Key_MouseEnter;
            picC.MouseEnter += Key_MouseEnter;
            picV.MouseEnter += Key_MouseEnter;
            picB.MouseEnter += Key_MouseEnter;
            picN.MouseEnter += Key_MouseEnter;
            picM.MouseEnter += Key_MouseEnter;
            picComma.MouseEnter += Key_MouseEnter;
            picPeriod.MouseEnter += Key_MouseEnter;
            picQuestion.MouseEnter += Key_MouseEnter;

            picQ.MouseLeave += Key_MouseLeave;
            picW.MouseLeave += Key_MouseLeave;
            picE.MouseLeave += Key_MouseLeave;
            picR.MouseLeave += Key_MouseLeave;
            picT.MouseLeave += Key_MouseLeave;
            picY.MouseLeave += Key_MouseLeave;
            picU.MouseLeave += Key_MouseLeave;
            picI.MouseLeave += Key_MouseLeave;
            picO.MouseLeave += Key_MouseLeave;
            picP.MouseLeave += Key_MouseLeave;
            picA.MouseLeave += Key_MouseLeave;
            picS.MouseLeave += Key_MouseLeave;
            picD.MouseLeave += Key_MouseLeave;
            picF.MouseLeave += Key_MouseLeave;
            picG.MouseLeave += Key_MouseLeave;
            picH.MouseLeave += Key_MouseLeave;
            picJ.MouseLeave += Key_MouseLeave;
            picK.MouseLeave += Key_MouseLeave;
            picL.MouseLeave += Key_MouseLeave;
            picSemicolon.MouseLeave += Key_MouseLeave;
            picZ.MouseLeave += Key_MouseLeave;
            picX.MouseLeave += Key_MouseLeave;
            picC.MouseLeave += Key_MouseLeave;
            picV.MouseLeave += Key_MouseLeave;
            picB.MouseLeave += Key_MouseLeave;
            picN.MouseLeave += Key_MouseLeave;
            picM.MouseLeave += Key_MouseLeave;
            picComma.MouseLeave += Key_MouseLeave;
            picPeriod.MouseLeave += Key_MouseLeave;
            picQuestion.MouseLeave += Key_MouseLeave;

            this.LostFocus += Laufe_LostFocus;
            this.Load += Laufe_Load;
            this.MouseWheel += Laufe_MouseWheel;

            #endregion

        }


        protected override void WndProc(ref Message message)
        {
            try
            {
                if (message.WParam.ToInt64() == 493)
                {
                    if (this.Visible == false)
                    {
                        this.Location = new Point(((Screen.FromPoint(Cursor.Position).Bounds.Width - this.Width) / 2), ((Screen.FromPoint(Cursor.Position).Bounds.Height - this.Height) / 2));
                        AnimateWindow(this.Handle, 8, 0x00000010 + 0x00080000 + 0x00020000);
                        SetForegroundWindow(this.Handle);
                        this.Visible = true;
                    }
                    else
                    {
                        isLock = false;
                        this.BackColor = colorFrame;
                        this.Visible = false;
                    }
                }

                base.WndProc(ref message);
            }
            catch
            {
                base.WndProc(ref message);
                return;
            }
        }

        private static byte[] HexStringToByteArray(string data)
        {
            byte[] buffer = new byte[data.Length / 2];
            for (int i = 0; i < data.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(data.Substring(i, 2), 16);
            return buffer;
        }

        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 2);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();
        }

        private bool AddKey(PictureBox pic, ExeFile exeFile)
        {
            try
            {
                pic.Tag = exeFile;
                pic.BackgroundImage = ((ExeFile)pic.Tag).Icon;
                pic.MouseClick += pictureBox_MouseClick;
                pic.MouseDown += pictureBox_MouseDown;
                return true;
            }
            catch
            {
                RemoveKey(pic);
                return false;
            }
        }

        private void RemoveKey(PictureBox pic)
        {
            pic.Tag = null;
            pic.BackgroundImage = null;
            pic.MouseClick -= pictureBox_MouseClick;
            pic.MouseDown -= pictureBox_MouseDown;
        }

        private void ChanagePanel(int index)
        {
            switch (Index)
            {
                case 0:
                    pnl1.BackColor = colorKey;
                    lbl1.BackColor = colorKey;
                    break;
                case 1:
                    pnl2.BackColor = colorKey;
                    lbl2.BackColor = colorKey;
                    break;
                case 2:
                    pnl3.BackColor = colorKey;
                    lbl3.BackColor = colorKey;
                    break;
                case 3:
                    pnl4.BackColor = colorKey;
                    lbl4.BackColor = colorKey;
                    break;
                case 4:
                    pnl5.BackColor = colorKey;
                    lbl5.BackColor = colorKey;
                    break;
                case 5:
                    pnl6.BackColor = colorKey;
                    lbl6.BackColor = colorKey;
                    break;
                case 6:
                    pnl7.BackColor = colorKey;
                    lbl7.BackColor = colorKey;
                    break;
                case 7:
                    pnl8.BackColor = colorKey;
                    lbl8.BackColor = colorKey;
                    break;
                case 8:
                    pnl9.BackColor = colorKey;
                    lbl9.BackColor = colorKey;
                    break;
                case 9:
                    pnl0.BackColor = colorKey;
                    lbl0.BackColor = colorKey;
                    break;
            }

            switch (index)
            {
                case 0:
                    pnl1.BackColor = colorKeyHighLight;
                    lbl1.BackColor = colorKeyHighLight;
                    break;
                case 1:
                    pnl2.BackColor = colorKeyHighLight;
                    lbl2.BackColor = colorKeyHighLight;
                    break;
                case 2:
                    pnl3.BackColor = colorKeyHighLight;
                    lbl3.BackColor = colorKeyHighLight;
                    break;
                case 3:
                    pnl4.BackColor = colorKeyHighLight;
                    lbl4.BackColor = colorKeyHighLight;
                    break;
                case 4:
                    pnl5.BackColor = colorKeyHighLight;
                    lbl5.BackColor = colorKeyHighLight;
                    break;
                case 5:
                    pnl6.BackColor = colorKeyHighLight;
                    lbl6.BackColor = colorKeyHighLight;
                    break;
                case 6:
                    pnl7.BackColor = colorKeyHighLight;
                    lbl7.BackColor = colorKeyHighLight;
                    break;
                case 7:
                    pnl8.BackColor = colorKeyHighLight;
                    lbl8.BackColor = colorKeyHighLight;
                    break;
                case 8:
                    pnl9.BackColor = colorKeyHighLight;
                    lbl9.BackColor = colorKeyHighLight;
                    break;
                case 9:
                    pnl0.BackColor = colorKeyHighLight;
                    lbl0.BackColor = colorKeyHighLight;
                    break;
            }

            for (int i = 0; i < 10; i++)
            {
                if (i == Index)
                {
                    ((XmlElement)xnRoot.ChildNodes[i]).SetAttribute("Current", "false");
                }
                if (i == index)
                {
                    xeCurrent = (XmlElement)xnRoot.ChildNodes[i];
                    xeCurrent.SetAttribute("Current", "true");

                    RemoveKey(picQ);
                    RemoveKey(picW);
                    RemoveKey(picE);
                    RemoveKey(picR);
                    RemoveKey(picT);
                    RemoveKey(picY);
                    RemoveKey(picU);
                    RemoveKey(picI);
                    RemoveKey(picO);
                    RemoveKey(picP);
                    RemoveKey(picA);
                    RemoveKey(picS);
                    RemoveKey(picD);
                    RemoveKey(picF);
                    RemoveKey(picG);
                    RemoveKey(picH);
                    RemoveKey(picJ);
                    RemoveKey(picK);
                    RemoveKey(picL);
                    RemoveKey(picSemicolon);
                    RemoveKey(picZ);
                    RemoveKey(picX);
                    RemoveKey(picC);
                    RemoveKey(picV);
                    RemoveKey(picB);
                    RemoveKey(picN);
                    RemoveKey(picM);
                    RemoveKey(picComma);
                    RemoveKey(picPeriod);
                    RemoveKey(picQuestion);

                    foreach (XmlElement xe in xeCurrent.ChildNodes)
                    {
                        ExeFile exeFile;

                        try
                        {
                            exeFile = new ExeFile()
                            {

                                Path = xe.GetAttribute("Path"),
                                Arguments = xe.GetAttribute("Arguments"),
                                WorkingDirectory = xe.GetAttribute("WorkingDirectory"),
                                Icon = new Bitmap(new MemoryStream(HexStringToByteArray(xe.GetAttribute("Icon"))))
                            };
                        }
                        catch
                        {
                            continue;
                        }


                        switch (xe.GetAttribute("Name"))
                        {
                            case "Q":
                                AddKey(picQ, exeFile);
                                break;
                            case "W":
                                AddKey(picW, exeFile);
                                break;
                            case "E":
                                AddKey(picE, exeFile);
                                break;
                            case "R":
                                AddKey(picR, exeFile);
                                break;
                            case "T":
                                AddKey(picT, exeFile);
                                break;
                            case "Y":
                                AddKey(picY, exeFile);
                                break;
                            case "U":
                                AddKey(picU, exeFile);
                                break;
                            case "I":
                                AddKey(picI, exeFile);
                                break;
                            case "O":
                                AddKey(picO, exeFile);
                                break;
                            case "P":
                                AddKey(picP, exeFile);
                                break;
                            case "A":
                                AddKey(picA, exeFile);
                                break;
                            case "S":
                                AddKey(picS, exeFile);
                                break;
                            case "D":
                                AddKey(picD, exeFile);
                                break;
                            case "F":
                                AddKey(picF, exeFile);
                                break;
                            case "G":
                                AddKey(picG, exeFile);
                                break;
                            case "H":
                                AddKey(picH, exeFile);
                                break;
                            case "J":
                                AddKey(picJ, exeFile);
                                break;
                            case "K":
                                AddKey(picK, exeFile);
                                break;
                            case "L":
                                AddKey(picL, exeFile);
                                break;
                            case "Semicolon":
                                AddKey(picSemicolon, exeFile);
                                break;
                            case "Z":
                                AddKey(picZ, exeFile);
                                break;
                            case "X":
                                AddKey(picX, exeFile);
                                break;
                            case "C":
                                AddKey(picC, exeFile);
                                break;
                            case "V":
                                AddKey(picV, exeFile);
                                break;
                            case "B":
                                AddKey(picB, exeFile);
                                break;
                            case "N":
                                AddKey(picN, exeFile);
                                break;
                            case "M":
                                AddKey(picM, exeFile);
                                break;
                            case "Comma":
                                AddKey(picComma, exeFile);
                                break;
                            case "Period":
                                AddKey(picPeriod, exeFile);
                                break;
                            case "Question":
                                AddKey(picQuestion, exeFile);
                                break;
                        }
                    }

                }
            }

            Index = index;

            BeginInvoke(new SaveSetting(xdSetting.Save), Application.StartupPath + @"\Laufe.xml");
        }

        private void ChanageKeyColor(string ControlName, bool HighLight)
        {
            if (HighLight)
            {
                switch (ControlName)
                {
                    case "picQ":
                    case "pnlQ":
                    case "lblQ":
                        pnlQ.BackColor = colorKeyHighLight;
                        lblQ.BackColor = colorKeyHighLight;
                        break;

                    case "picW":
                    case "pnlW":
                    case "lblW":
                        pnlW.BackColor = colorKeyHighLight;
                        lblW.BackColor = colorKeyHighLight;
                        break;

                    case "picE":
                    case "pnlE":
                    case "lblE":
                        pnlE.BackColor = colorKeyHighLight;
                        lblE.BackColor = colorKeyHighLight;
                        break;

                    case "picR":
                    case "pnlR":
                    case "lblR":
                        pnlR.BackColor = colorKeyHighLight;
                        lblR.BackColor = colorKeyHighLight;
                        break;

                    case "picT":
                    case "pnlT":
                    case "lblT":
                        pnlT.BackColor = colorKeyHighLight;
                        lblT.BackColor = colorKeyHighLight;
                        break;

                    case "picY":
                    case "pnlY":
                    case "lblY":
                        pnlY.BackColor = colorKeyHighLight;
                        lblY.BackColor = colorKeyHighLight;
                        break;

                    case "picU":
                    case "pnlU":
                    case "lblU":
                        pnlU.BackColor = colorKeyHighLight;
                        lblU.BackColor = colorKeyHighLight;
                        break;

                    case "picI":
                    case "pnlI":
                    case "lblI":
                        pnlI.BackColor = colorKeyHighLight;
                        lblI.BackColor = colorKeyHighLight;
                        break;

                    case "picO":
                    case "pnlO":
                    case "lblO":
                        pnlO.BackColor = colorKeyHighLight;
                        lblO.BackColor = colorKeyHighLight;
                        break;

                    case "picP":
                    case "pnlP":
                    case "lblP":
                        pnlP.BackColor = colorKeyHighLight;
                        lblP.BackColor = colorKeyHighLight;
                        break;

                    case "picA":
                    case "pnlA":
                    case "lblA":
                        pnlA.BackColor = colorKeyHighLight;
                        lblA.BackColor = colorKeyHighLight;
                        break;

                    case "picS":
                    case "pnlS":
                    case "lblS":
                        pnlS.BackColor = colorKeyHighLight;
                        lblS.BackColor = colorKeyHighLight;
                        break;

                    case "picD":
                    case "pnlD":
                    case "lblD":
                        pnlD.BackColor = colorKeyHighLight;
                        lblD.BackColor = colorKeyHighLight;
                        break;

                    case "picF":
                    case "pnlF":
                    case "lblF":
                        pnlF.BackColor = colorKeyHighLight;
                        lblF.BackColor = colorKeyHighLight;
                        break;

                    case "picG":
                    case "pnlG":
                    case "lblG":
                        pnlG.BackColor = colorKeyHighLight;
                        lblG.BackColor = colorKeyHighLight;
                        break;

                    case "picH":
                    case "pnlH":
                    case "lblH":
                        pnlH.BackColor = colorKeyHighLight;
                        lblH.BackColor = colorKeyHighLight;
                        break;

                    case "picJ":
                    case "pnlJ":
                    case "lblJ":
                        pnlJ.BackColor = colorKeyHighLight;
                        lblJ.BackColor = colorKeyHighLight;
                        break;

                    case "picK":
                    case "pnlK":
                    case "lblK":
                        pnlK.BackColor = colorKeyHighLight;
                        lblK.BackColor = colorKeyHighLight;
                        break;

                    case "picL":
                    case "pnlL":
                    case "lblL":
                        pnlL.BackColor = colorKeyHighLight;
                        lblL.BackColor = colorKeyHighLight;
                        break;

                    case "picSemicolon":
                    case "pnlSemicolon":
                    case "lblSemicolon":
                        pnlSemicolon.BackColor = colorKeyHighLight;
                        lblSemicolon.BackColor = colorKeyHighLight;
                        break;

                    case "picZ":
                    case "pnlZ":
                    case "lblZ":
                        pnlZ.BackColor = colorKeyHighLight;
                        lblZ.BackColor = colorKeyHighLight;
                        break;

                    case "picX":
                    case "pnlX":
                    case "lblX":
                        pnlX.BackColor = colorKeyHighLight;
                        lblX.BackColor = colorKeyHighLight;
                        break;

                    case "picC":
                    case "pnlC":
                    case "lblC":
                        pnlC.BackColor = colorKeyHighLight;
                        lblC.BackColor = colorKeyHighLight;
                        break;

                    case "picV":
                    case "pnlV":
                    case "lblV":
                        pnlV.BackColor = colorKeyHighLight;
                        lblV.BackColor = colorKeyHighLight;
                        break;

                    case "picB":
                    case "pnlB":
                    case "lblB":
                        pnlB.BackColor = colorKeyHighLight;
                        lblB.BackColor = colorKeyHighLight;
                        break;

                    case "picN":
                    case "pnlN":
                    case "lblN":
                        pnlN.BackColor = colorKeyHighLight;
                        lblN.BackColor = colorKeyHighLight;
                        break;

                    case "picM":
                    case "pnlM":
                    case "lblM":
                        pnlM.BackColor = colorKeyHighLight;
                        lblM.BackColor = colorKeyHighLight;
                        break;

                    case "picComma":
                    case "pnlComma":
                    case "lblComma":
                        pnlComma.BackColor = colorKeyHighLight;
                        lblComma.BackColor = colorKeyHighLight;
                        break;

                    case "picPeriod":
                    case "pnlPeriod":
                    case "lblPeriod":
                        pnlPeriod.BackColor = colorKeyHighLight;
                        lblPeriod.BackColor = colorKeyHighLight;
                        break;

                    case "picQuestion":
                    case "pnlQuestion":
                    case "lblQuestion":
                        pnlQuestion.BackColor = colorKeyHighLight;
                        lblQuestion.BackColor = colorKeyHighLight;
                        break;

                }
            }
            else
            {
                switch (ControlName)
                {
                    case "picQ":
                    case "pnlQ":
                    case "lblQ":
                        pnlQ.BackColor = colorKey;
                        lblQ.BackColor = colorKey;
                        break;

                    case "picW":
                    case "pnlW":
                    case "lblW":
                        pnlW.BackColor = colorKey;
                        lblW.BackColor = colorKey;
                        break;

                    case "picE":
                    case "pnlE":
                    case "lblE":
                        pnlE.BackColor = colorKey;
                        lblE.BackColor = colorKey;
                        break;

                    case "picR":
                    case "pnlR":
                    case "lblR":
                        pnlR.BackColor = colorKey;
                        lblR.BackColor = colorKey;
                        break;

                    case "picT":
                    case "pnlT":
                    case "lblT":
                        pnlT.BackColor = colorKey;
                        lblT.BackColor = colorKey;
                        break;

                    case "picY":
                    case "pnlY":
                    case "lblY":
                        pnlY.BackColor = colorKey;
                        lblY.BackColor = colorKey;
                        break;

                    case "picU":
                    case "pnlU":
                    case "lblU":
                        pnlU.BackColor = colorKey;
                        lblU.BackColor = colorKey;
                        break;

                    case "picI":
                    case "pnlI":
                    case "lblI":
                        pnlI.BackColor = colorKey;
                        lblI.BackColor = colorKey;
                        break;

                    case "picO":
                    case "pnlO":
                    case "lblO":
                        pnlO.BackColor = colorKey;
                        lblO.BackColor = colorKey;
                        break;

                    case "picP":
                    case "pnlP":
                    case "lblP":
                        pnlP.BackColor = colorKey;
                        lblP.BackColor = colorKey;
                        break;

                    case "picA":
                    case "pnlA":
                    case "lblA":
                        pnlA.BackColor = colorKey;
                        lblA.BackColor = colorKey;
                        break;

                    case "picS":
                    case "pnlS":
                    case "lblS":
                        pnlS.BackColor = colorKey;
                        lblS.BackColor = colorKey;
                        break;

                    case "picD":
                    case "pnlD":
                    case "lblD":
                        pnlD.BackColor = colorKey;
                        lblD.BackColor = colorKey;
                        break;

                    case "picF":
                    case "pnlF":
                    case "lblF":
                        pnlF.BackColor = colorKey;
                        lblF.BackColor = colorKey;
                        break;

                    case "picG":
                    case "pnlG":
                    case "lblG":
                        pnlG.BackColor = colorKey;
                        lblG.BackColor = colorKey;
                        break;

                    case "picH":
                    case "pnlH":
                    case "lblH":
                        pnlH.BackColor = colorKey;
                        lblH.BackColor = colorKey;
                        break;

                    case "picJ":
                    case "pnlJ":
                    case "lblJ":
                        pnlJ.BackColor = colorKey;
                        lblJ.BackColor = colorKey;
                        break;

                    case "picK":
                    case "pnlK":
                    case "lblK":
                        pnlK.BackColor = colorKey;
                        lblK.BackColor = colorKey;
                        break;

                    case "picL":
                    case "pnlL":
                    case "lblL":
                        pnlL.BackColor = colorKey;
                        lblL.BackColor = colorKey;
                        break;

                    case "picSemicolon":
                    case "pnlSemicolon":
                    case "lblSemicolon":
                        pnlSemicolon.BackColor = colorKey;
                        lblSemicolon.BackColor = colorKey;
                        break;

                    case "picZ":
                    case "pnlZ":
                    case "lblZ":
                        pnlZ.BackColor = colorKey;
                        lblZ.BackColor = colorKey;
                        break;

                    case "picX":
                    case "pnlX":
                    case "lblX":
                        pnlX.BackColor = colorKey;
                        lblX.BackColor = colorKey;
                        break;

                    case "picC":
                    case "pnlC":
                    case "lblC":
                        pnlC.BackColor = colorKey;
                        lblC.BackColor = colorKey;
                        break;

                    case "picV":
                    case "pnlV":
                    case "lblV":
                        pnlV.BackColor = colorKey;
                        lblV.BackColor = colorKey;
                        break;

                    case "picB":
                    case "pnlB":
                    case "lblB":
                        pnlB.BackColor = colorKey;
                        lblB.BackColor = colorKey;
                        break;

                    case "picN":
                    case "pnlN":
                    case "lblN":
                        pnlN.BackColor = colorKey;
                        lblN.BackColor = colorKey;
                        break;

                    case "picM":
                    case "pnlM":
                    case "lblM":
                        pnlM.BackColor = colorKey;
                        lblM.BackColor = colorKey;
                        break;

                    case "picComma":
                    case "pnlComma":
                    case "lblComma":
                        pnlComma.BackColor = colorKey;
                        lblComma.BackColor = colorKey;
                        break;

                    case "picPeriod":
                    case "pnlPeriod":
                    case "lblPeriod":
                        pnlPeriod.BackColor = colorKey;
                        lblPeriod.BackColor = colorKey;
                        break;

                    case "picQuestion":
                    case "pnlQuestion":
                    case "lblQuestion":
                        pnlQuestion.BackColor = colorKey;
                        lblQuestion.BackColor = colorKey;
                        break;

                }
            }
        }

        private Timer LockTimer = new Timer();

        private void Launch(PictureBox pic)
        {
            if (pic.Tag != null)
            {
                string path = ((ExeFile)pic.Tag).Path;
                string arguments = ((ExeFile)pic.Tag).Arguments;
                string workingDirectory = ((ExeFile)pic.Tag).WorkingDirectory;

                if (!File.Exists(path))
                {
                    RemoveKey(pic);

                    foreach (XmlElement xe in xeCurrent.ChildNodes)
                    {
                        if (xe.GetAttribute("Name") == pic.Name.Substring(3))
                        {
                            xeCurrent.RemoveChild(xe);
                        }
                    }

                    BeginInvoke(new SaveSetting(xdSetting.Save), Application.StartupPath + @"\Laufe.xml");

                    return;
                }

                try
                {
                    SetForegroundWindow(this.Handle);
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = path;
                    process.StartInfo.Arguments = arguments;

                    if (workingDirectory == "")
                    {
                        process.StartInfo.WorkingDirectory = path.Substring(0, path.LastIndexOf('\\'));
                    }
                    else
                    {
                        process.StartInfo.WorkingDirectory = workingDirectory;
                    }

                    process.Start();

                    if (!isLock)
                    {
                        this.Visible = false;
                    }
                }
                catch
                {
                    return;
                }
            }

        }

        private void Laufe_Load(object sender, EventArgs e)
        {
            xnRoot = xdSetting.SelectSingleNode("Laufe");

            foreach (XmlElement xe in xnRoot.ChildNodes)
            {
                if (xe.GetAttribute("Current") == "true")
                {
                    Index = Convert.ToInt32(xe.GetAttribute("Index"));
                    xeCurrent = xe;
                    ChanagePanel(Index);
                }
            }

            LockTimer.Interval = 100;
            LockTimer.Tick += LockTimer_Tick;
            LockTimer.Start();
        }

        private void Laufe_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Opacity = 1;
        }

        private void LockTimer_Tick(object sender, EventArgs e)
        {
            if (isLock)
            {
                SetForegroundWindow(this.Handle);
                this.Focus();
            }
        }

        private void Laufe_LostFocus(object sender, EventArgs e)
        {
            if (!isLock)
            {
                this.Visible = false;
            }
        }

        private void Laufe_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Oemtilde:
                    {
                        if (isLock)
                        {
                            isLock = false;
                            this.BackColor = colorFrame;
                            this.Visible = false;
                        }
                        else
                        {
                            isLock = true;
                            this.BackColor = colorFrameLocked;
                        }

                        break;
                    }
                case Keys.Escape:
                    Program.UnregisterHotKey(this.Handle, 493);
                    Application.Exit();
                    break;
                case Keys.D1:
                    ChanagePanel(0);
                    break;
                case Keys.D2:
                    ChanagePanel(1);
                    break;
                case Keys.D3:
                    ChanagePanel(2);
                    break;
                case Keys.D4:
                    ChanagePanel(3);
                    break;
                case Keys.D5:
                    ChanagePanel(4);
                    break;
                case Keys.D6:
                    ChanagePanel(5);
                    break;
                case Keys.D7:
                    ChanagePanel(6);
                    break;
                case Keys.D8:
                    ChanagePanel(7);
                    break;
                case Keys.D9:
                    ChanagePanel(8);
                    break;
                case Keys.D0:
                    ChanagePanel(9);
                    break;
                case Keys.Q:
                    Launch(picQ);
                    break;
                case Keys.W:
                    Launch(picW);
                    break;
                case Keys.E:
                    Launch(picE);
                    break;
                case Keys.R:
                    Launch(picR);
                    break;
                case Keys.T:
                    Launch(picT);
                    break;
                case Keys.Y:
                    Launch(picY);
                    break;
                case Keys.U:
                    Launch(picU);
                    break;
                case Keys.I:
                    Launch(picI);
                    break;
                case Keys.O:
                    Launch(picO);
                    break;
                case Keys.P:
                    Launch(picP);
                    break;
                case Keys.A:
                    Launch(picA);
                    break;
                case Keys.S:
                    Launch(picS);
                    break;
                case Keys.D:
                    Launch(picD);
                    break;
                case Keys.F:
                    Launch(picF);
                    break;
                case Keys.G:
                    Launch(picG);
                    break;
                case Keys.H:
                    Launch(picH);
                    break;
                case Keys.J:
                    Launch(picJ);
                    break;
                case Keys.K:
                    Launch(picK);
                    break;
                case Keys.L:
                    Launch(picL);
                    break;
                case Keys.OemSemicolon:
                    Launch(picSemicolon);
                    break;
                case Keys.Z:
                    Launch(picZ);
                    break;
                case Keys.X:
                    Launch(picX);
                    break;
                case Keys.C:
                    Launch(picC);
                    break;
                case Keys.V:
                    Launch(picV);
                    break;
                case Keys.B:
                    Launch(picB);
                    break;
                case Keys.N:
                    Launch(picN);
                    break;
                case Keys.M:
                    Launch(picM);
                    break;
                case Keys.Oemcomma:
                    Launch(picComma);
                    break;
                case Keys.OemPeriod:
                    Launch(picPeriod);
                    break;
                case Keys.OemQuestion:
                    Launch(picQuestion);
                    break;
            }
        }

        private void Laufe_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                int index = Index > 0 ? Index - 1 : 9;
                ChanagePanel(index);
            }
            else
            {
                int index = Index < 9 ? Index + 1 : 0;
                ChanagePanel(index);
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                PictureBox pic = (PictureBox)sender;
                string path = "";
                string arguments = "";
                string workingDirectory = "";
                string icon = "";

                foreach (XmlElement xe in xeCurrent.ChildNodes)
                {
                    if (xe.GetAttribute("Name") == pic.Name.Substring(3))
                    {
                        path = xe.GetAttribute("Path");
                        arguments = xe.GetAttribute("Arguments");
                        workingDirectory = xe.GetAttribute("WorkingDirectory");
                        icon = xe.GetAttribute("Icon");
                        RemoveKey(pic);
                        xeCurrent.RemoveChild(xe);
                    }
                }

                pic.DoDragDrop(new DataObject(DataFormats.FileDrop, new string[] { path, arguments, workingDirectory, icon }), DragDropEffects.Link);
                BeginInvoke(new SaveSetting(xdSetting.Save), Application.StartupPath + @"\Laufe.xml");
            }
        }

        private void pictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                ChanageKeyColor(((Control)sender).Name, true);
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pictureBox_DragLeave(object sender, EventArgs e)
        {
            ChanageKeyColor(((Control)sender).Name, false);
        }

        private void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox pic = (PictureBox)sender;

            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            string arguments = "";
            string workingDirectory = "";
            string icon = "";

            if (path.Substring(path.Length - 4, 4) == ".lnk")
            {
                IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(path);
                path = shortcut.TargetPath;
                arguments = shortcut.Arguments;
                workingDirectory = shortcut.WorkingDirectory;
                icon = shortcut.IconLocation.Split(',')[0];
            }

            ExeFile exeFile = new ExeFile
            {
                Path = path,
                Arguments = arguments,
                WorkingDirectory = workingDirectory
            };

            try
            {
                if (((System.Array)e.Data.GetData(DataFormats.FileDrop)).Length == 1)
                {
                    if (icon != "")
                    {
                        exeFile.Icon = System.Drawing.Icon.ExtractAssociatedIcon(icon).ToBitmap();
                    }
                    else
                    {
                        exeFile.Icon = System.Drawing.Icon.ExtractAssociatedIcon(path).ToBitmap();
                    }
                }
                else
                {
                    exeFile.Arguments = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(1).ToString();
                    exeFile.WorkingDirectory = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(2).ToString();
                    exeFile.Icon = new Bitmap(new MemoryStream(HexStringToByteArray(((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(3).ToString())));
                }
            }
            catch
            {
                return;
            }

            if (pic.Tag != null)
            {
                RemoveKey(pic);

                foreach (XmlElement xe in xeCurrent.ChildNodes)
                {
                    if (xe.GetAttribute("Name") == pic.Name.Substring(3))
                    {
                        xeCurrent.RemoveChild(xe);
                    }
                }
            }

            if (AddKey(pic, exeFile))
            {
                MemoryStream ms = new MemoryStream();
                exeFile.Icon.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteIcon = ms.GetBuffer();

                XmlElement xeKey = xdSetting.CreateElement("Key");
                xeKey.SetAttribute("Name", pic.Name.Substring(3));
                xeKey.SetAttribute("Path", exeFile.Path);
                xeKey.SetAttribute("Arguments", exeFile.Arguments);
                xeKey.SetAttribute("WorkingDirectory", exeFile.WorkingDirectory);
                xeKey.SetAttribute("Icon", ByteArrayToHexString(byteIcon));
                xeCurrent.AppendChild(xeKey);
            }

            BeginInvoke(new SaveSetting(xdSetting.Save), Application.StartupPath + @"\Laufe.xml");
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)sender;

            if (e.Button == MouseButtons.Left)
            {
                Launch(pic);
            }
            else if (e.Button == MouseButtons.Right)
            {
                RemoveKey(pic);

                foreach (XmlElement xe in xeCurrent.ChildNodes)
                {
                    if (xe.GetAttribute("Name") == pic.Name.Substring(3))
                    {
                        xeCurrent.RemoveChild(xe);
                    }
                }

                BeginInvoke(new SaveSetting(xdSetting.Save), Application.StartupPath + @"\Laufe.xml");
            }
        }

        private void Index_Chanage(object sender, EventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "pnl1":
                case "lbl1":
                    ChanagePanel(0);
                    break;
                case "pnl2":
                case "lbl2":
                    ChanagePanel(1);
                    break;
                case "pnl3":
                case "lbl3":
                    ChanagePanel(2);
                    break;
                case "pnl4":
                case "lbl4":
                    ChanagePanel(3);
                    break;
                case "pnl5":
                case "lbl5":
                    ChanagePanel(4);
                    break;
                case "pnl6":
                case "lbl6":
                    ChanagePanel(5);
                    break;
                case "pnl7":
                case "lbl7":
                    ChanagePanel(6);
                    break;
                case "pnl8":
                case "lbl8":
                    ChanagePanel(7);
                    break;
                case "pnl9":
                case "lbl9":
                    ChanagePanel(8);
                    break;
                case "pnl0":
                case "lbl0":
                    ChanagePanel(9);
                    break;
            }
        }

        private void Key_MouseEnter(object sender, EventArgs e)
        {
            ChanageKeyColor(((Control)sender).Name, true);
        }

        private void Key_MouseLeave(object sender, EventArgs e)
        {
            ChanageKeyColor(((Control)sender).Name, false);
        }

    }
}
