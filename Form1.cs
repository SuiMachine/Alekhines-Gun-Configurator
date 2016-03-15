using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace AlekhinesGunConfigurator
{
    public partial class Form1 : Form
    {
        static string _Config = "smersh.shadvs";
        static string _ConfigBackup = "smersh.shadvs.bak";
        static string _Location = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "my games", "AlekhinesGun");
        static string PCGW_URL = "http://pcgamingwiki.com/";
        static string donationURL = "https://www.twitchalerts.com/donate/suicidemachine";
        byte_operations helper;

        int adressResolutionX = 0;
        int ResolutionX = 0;
        int adressResolutionY = 0;
        int ResolutionY = 0;
        int adressScreen = 0;
        int screen = 0;
        int adressRefreshRate = 0;
        int refreshRate = 0;
        int adressColorBPP = 0;
        int colorBPP = 0;
        int adressVsync = 0;
        int Vsync = 0;
        int adressTrippleBuffering = 0;
        int TrippleBuffering = 0;
        int adressWindowed = 0;
        int Windowed = 0;

        int adressShadowQuality = 0;
        int ShadowQuality = 0;
        int adressGrassDensitiy = 0;
        int GrassDensity = 0;
        int adressGrassDistance = 0;
        int GrassDistance = 0;
        int adressAntiAliasing = 0;
        int AntiAliasing = 0;
        int adressTextureFilter = 0;
        int TextureFilter = 0;
        int adressTextureQuality = 0;
        int TextureQuality = 0;
  

        public Form1()
        {
            InitializeComponent();
            if(!File.Exists(Path.Combine(_Location, _Config)))
            {
                MessageBox.Show("No config file found. Please start the game at least ones, before running the program.");
                Close();
            }
            else
            {
                if (!File.Exists(Path.Combine(_Location, "Cache", "configurator_note_shown")))
                {
                    MessageBox.Show("This program is provided as is. I (developer) do not take any responsibility for unintentional behaviour (game refusing to start after using it / graphical bugs / corrupted settings etc). Use it on your own responsibility.", "Important", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    File.Create((Path.Combine(_Location, "Cache", "configurator_note_shown")));
                }
                byte[] data = GetBytesFromAFile(Path.Combine(_Location, _Config));
                helper = new byte_operations(data);
                adressResolutionX = helper.findAdress(StringToByteArray("ModeWidth"), 16);
                adressResolutionY = helper.findAdress(StringToByteArray("ModeHeight"), 16);
                adressScreen = helper.findAdress(StringToByteArray("Adapter"), 15);
                adressRefreshRate = helper.findAdress(StringToByteArray("ModeRefreshRate"), 15+8);
                adressColorBPP = helper.findAdress(StringToByteArray("ModeBpp"), 12);
                adressVsync = helper.findAdress(StringToByteArray("VSync"), 12);
                adressTrippleBuffering = helper.findAdress(StringToByteArray("TrippleBuffering"), 16+8);
                adressWindowed = helper.findAdress(StringToByteArray("ModeWindowed"), 20);


                adressShadowQuality = helper.findAdress(StringToByteArray("ShadowQuality"), 13+7);
                adressGrassDensitiy = helper.findAdress(StringToByteArray("GrassDensity"), 12 + 8);
                adressGrassDistance = helper.findAdress(StringToByteArray("GrassViewDist"), 13 + 7);
                adressAntiAliasing = helper.findAdress(StringToByteArray("Antialiasing"), 20);
                adressTextureFilter = helper.findAdress(StringToByteArray("TextureFilter"), 13+7);
                adressTextureQuality = helper.findAdress(StringToByteArray("TextureQuality"), 14 + 8);

                Trace.WriteLine("Found address: 0x" + adressResolutionX.ToString("X4"));
                Trace.WriteLine("Found address: 0x" + adressResolutionY.ToString("X4"));
                Trace.WriteLine("Found address: 0x" + adressScreen.ToString("X4"));
                Trace.WriteLine("Found address: 0x" + adressRefreshRate.ToString("X4"));
                Trace.WriteLine("Found address: 0x" + adressColorBPP.ToString("X4"));
                Trace.WriteLine("Found address: 0x" + adressVsync.ToString("X4"));
                Trace.WriteLine("Found address: 0x" + adressTrippleBuffering.ToString("X4"));
                Trace.WriteLine("Found address: 0x" + adressWindowed.ToString("X4"));
                Trace.WriteLine("Found address: 0x" + adressShadowQuality.ToString("X4"));
                Trace.WriteLine("Found address: 0x" + adressGrassDensitiy.ToString("X4"));
                Trace.WriteLine("Found address: 0x" + adressGrassDistance.ToString("X4"));
                Trace.WriteLine("Found address: 0x" + adressAntiAliasing.ToString("X4"));
                Trace.WriteLine("Found address: 0x" + adressTextureFilter.ToString("X4"));
                Trace.WriteLine("Found address: 0x" + adressTextureQuality.ToString("X4"));


                if (adressResolutionX == 0 ||
                    adressResolutionY == 0 ||
                    adressScreen == 0 ||
                    adressRefreshRate == 0 ||
                    adressColorBPP == 0 ||
                    adressVsync == 0 ||                
                    adressTrippleBuffering == 0 ||
                    adressWindowed == 0 ||
                    adressShadowQuality == 0 ||
                    adressGrassDensitiy == 0 ||
                    adressGrassDistance == 0 ||
                    adressAntiAliasing == 0 ||
                    adressTextureFilter == 0 ||
                    adressTextureQuality == 0)
                {
                    MessageBox.Show("Not all variables were found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                else
                {
                    ResolutionX = BitConverter.ToInt32(data, adressResolutionX);            //ResolutionX
                    TB_ResX.Text = ResolutionX.ToString();
                    ResolutionY = BitConverter.ToInt32(data, adressResolutionY);            //Resolution Y
                    TB_ResY.Text = ResolutionY.ToString();
                    screen = BitConverter.ToInt32(data, adressScreen);                      //Screen/Adapter
                    TB_Screen.Text = screen.ToString();
                    refreshRate = BitConverter.ToInt32(data, adressRefreshRate);            //RefreshRate
                    TB_RefreshRate.Text = refreshRate.ToString();
                    colorBPP = BitConverter.ToInt32(data, adressColorBPP);                  //ColorBPP
                    if (colorBPP == 32) CB_32BitColor.Checked = true; else CB_32BitColor.Checked = false;
                    Vsync = BitConverter.ToInt32(data, adressVsync);                        //Vsync
                    CB_Vsync.Checked = Convert.ToBoolean(Vsync);
                    TrippleBuffering = BitConverter.ToInt32(data, adressTrippleBuffering);  //TrippleBuffering
                    CB_TrippleBuffering.Checked = Convert.ToBoolean(TrippleBuffering);
                    Windowed = BitConverter.ToInt32(data, adressWindowed);                  //WindowedMode
                    CB_Windowed.Checked = Convert.ToBoolean(Windowed);


                    ShadowQuality = BitConverter.ToInt32(data, adressShadowQuality);        //Shadow Quality
                    if (ShadowQuality >= 0 && ShadowQuality <= 2) CBox_ShadowQuality.SelectedIndex = ShadowQuality; else CBox_ShadowQuality.SelectedIndex = 2;
                    GrassDensity = BitConverter.ToInt32(data, adressGrassDensitiy);         //Grass Density
                    if (GrassDensity >= 0 && GrassDensity <= 2) CBox_GrassDensity.SelectedIndex = GrassDensity; else CBox_GrassDensity.SelectedIndex = 2;
                    GrassDistance = BitConverter.ToInt32(data, adressGrassDistance);         //Grass View Distance
                    if (GrassDistance >= 0 && GrassDistance <= 8) CBox_GrassViewDist.SelectedIndex = GrassDistance; else CBox_GrassViewDist.SelectedIndex = 4;
                    AntiAliasing = BitConverter.ToInt32(data, adressAntiAliasing);          //Anti-aliasing
                    if (AntiAliasing == 2) CBox_AA.SelectedItem = CBox_AA.Items[1];
                    else if (AntiAliasing == 4) CBox_AA.SelectedItem = CBox_AA.Items[2];
                    else CBox_AA.SelectedItem = CBox_AA.Items[0];
                    TextureFilter = BitConverter.ToInt32(data, adressTextureFilter);        //Texture filter
                    if (TextureFilter >= 0 && TextureFilter <= 8) CB_TextureFilter.SelectedIndex = TextureFilter; else CB_TextureFilter.SelectedIndex = 2;
                    TextureQuality = BitConverter.ToInt32(data, adressTextureQuality);      //Texture quality
                    if (TextureQuality >= 0 && TextureQuality <= 4) CB_TextureQuality.SelectedIndex = TextureQuality; else CB_TextureQuality.SelectedIndex = 0;
                }
            }
        }


        #region Load_Save
        private byte[] GetBytesFromAFile(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(filename);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        private bool WriteBytesToAFile(string filename, byte[] usedData)
        {
            try
            {
                File.WriteAllBytes(@filename, usedData);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return false;
            }
        }

        private void B_WriteToAFile_Click(object sender, EventArgs e)
        {
            //Backup
            if (!File.Exists(Path.Combine(_Location, _ConfigBackup)))
            {
                File.Copy(Path.Combine(_Location, _Config), Path.Combine(_Location, _ConfigBackup));
            }

            helper.replaceBytes(BitConverter.GetBytes(ResolutionX), adressResolutionX);
            helper.replaceBytes(BitConverter.GetBytes(ResolutionY), adressResolutionY);
            helper.replaceBytes(BitConverter.GetBytes(screen), adressScreen);
            helper.replaceBytes(BitConverter.GetBytes(refreshRate), adressRefreshRate);
            helper.replaceBytes(BitConverter.GetBytes(colorBPP), adressColorBPP);
            helper.replaceBytes(BitConverter.GetBytes(Vsync), adressVsync);
            helper.replaceBytes(BitConverter.GetBytes(TrippleBuffering), adressTrippleBuffering);
            helper.replaceBytes(BitConverter.GetBytes(Windowed), adressWindowed);

            helper.replaceBytes(BitConverter.GetBytes(ShadowQuality), adressShadowQuality);
            helper.replaceBytes(BitConverter.GetBytes(GrassDensity), adressGrassDensitiy);
            helper.replaceBytes(BitConverter.GetBytes((byte)GrassDistance), adressGrassDistance);       //because apparently not enough bytes, int to byte
            helper.replaceBytes(BitConverter.GetBytes(AntiAliasing), adressAntiAliasing);
            helper.replaceBytes(BitConverter.GetBytes(TextureFilter), adressTextureFilter);
            helper.replaceBytes(BitConverter.GetBytes(TextureQuality), adressTextureQuality);



            bool success = true;
            success = WriteBytesToAFile(Path.Combine(_Location, _Config), helper.returnArray());

            if (!success)
                MessageBox.Show("There was an error writting to a file!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                MessageBox.Show("Successfully made the changes!", "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        static byte[] StringToByteArray(string str)
        {
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(str);
            return bytes;
        }

        private void LL_PCGW_link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(PCGW_URL);
        }

        private void P_Donate_Click(object sender, EventArgs e)
        {
            Process.Start(donationURL);
        }

        private void P_developer_Click(object sender, EventArgs e)
        {
            Process.Start(donationURL);
        }

        #region UIToValueParse
        private void TB_ResX_TextChanged(object sender, EventArgs e)
        {
            int var;
            if(int.TryParse(TB_ResX.Text, out var))
            {
                ResolutionX = var;
            }
        }

        private void TB_ResY_TextChanged(object sender, EventArgs e)
        {
            int var;
            if (int.TryParse(TB_ResY.Text, out var))
            {
                ResolutionY = var;
            }
        }

        private void CBox_AA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBox_AA.SelectedIndex == 0)
                AntiAliasing = 0;
            else if (CBox_AA.SelectedIndex == 1)
                AntiAliasing = 2;
            else if (CBox_AA.SelectedIndex == 2)
                AntiAliasing = 4;
        }

        private void CBox_ShadowQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShadowQuality = CBox_ShadowQuality.SelectedIndex;
        }

        private void CBox_GrassDensity_SelectedIndexChanged(object sender, EventArgs e)
        {
            GrassDensity = CBox_GrassDensity.SelectedIndex;
        }

        private void CBox_GrassViewDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            GrassDistance = CBox_GrassViewDist.SelectedIndex;
        }

        private void CB_32BitColor_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_32BitColor.Checked)
                colorBPP = 32;
            else
                colorBPP = 16;
        }

        private void CB_Vsync_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_Vsync.Checked)
                Vsync = 1;
            else
                Vsync = 0;
        }

        private void CB_TrippleBuffering_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_TrippleBuffering.Checked)
                TrippleBuffering = 1;
            else
                TrippleBuffering = 0;
        }

        private void CB_Windowed_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_Windowed.Checked)
                Windowed = 1;
            else
                Windowed = 0;
        }

        private void TB_RefreshRate_TextChanged(object sender, EventArgs e)
        {
            int output;
            if(int.TryParse(TB_RefreshRate.Text, out output))
            {
                refreshRate = output;
            }
        }

        private void CB_TextureFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextureFilter = CB_TextureFilter.SelectedIndex;
        }

        private void CB_TextureQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextureQuality = CB_TextureQuality.SelectedIndex;
        }
        #endregion
    }
}
