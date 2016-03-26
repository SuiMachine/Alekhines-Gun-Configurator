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
        #region keys
        Dictionary<ushort, string> keyCollection = new Dictionary<ushort, string>()
        {
            {0x00, "None" },
            {0x01, "ESC" },
            {0x02, "1" },
            {0x03, "2" },
            {0x04, "3" },
            {0x05, "4" },
            {0x06, "5" },
            {0x07, "6" },
            {0x08, "7" },
            {0x09, "8" },
            {0x0A, "9" },
            {0x0B, "0" },
            {0x0C, "-" },
            {0x0D, "=" },
            {0x0E, "Backspace" },
            {0x0F, "Tab" },
            {0x10, "Q" },
            {0x11, "W" },
            {0x12, "E" },
            {0x13, "R" },
            {0x14, "T" },
            {0x15, "Y" },
            {0x16, "U" },
            {0x17, "I" },
            {0x18, "O" },
            {0x19, "P" },
            {0x1A, "[" },
            {0x1B, "]" },
            {0x1C, "Enter" },
            {0x1D, "Left Ctrl" },
            {0x1E, "A" },
            {0x1F, "S" },
            {0x20, "D" },
            {0x21, "F" },
            {0x22, "G" },
            {0x23, "H" },
            {0x24, "J" },
            {0x25, "K" },
            {0x26, "L" },
            {0x27, ";" },
            {0x28, "\'" },
            {0x29, "~" },
            {0x2A, "Left Shift" },
            {0x2B, "\\" },
            {0x2C, "Z" },
            {0x2D, "X" },
            {0x2E, "C" },
            {0x2F, "V" },
            {0x30, "B" },
            {0x31, "N" },
            {0x32, "M" },
            {0x33, "," },
            {0x34, "." },
            {0x35, "/" },
            {0x36, "Right Shift" },
            {0x37, "Num *" },
            {0x38, "Left Alt" },
            {0x39, "Spacebar" },
            {0x3A, "Capslock" },
            {0x3B, "F1" },
            {0x3C, "F2" },
            {0x3D, "F3" },
            {0x3E, "F4" },
            {0x3F, "F5" },
            {0x40, "F6" },
            {0x41, "F7" },
            {0x42, "F8" },
            {0x43, "F9" },
            {0x44, "F10" },
            {0x46, "Scroll Lock" },
            {0x47, "Num 7" },
            {0x48, "Num 8" },
            {0x49, "Num 9" },
            {0x4A, "Num -" },
            {0x4B, "Num 4" },
            {0x4C, "Num 5" },
            {0x4D, "Num 6" },
            {0x4E, "Num +" },
            {0x4F, "Num 1" },
            {0x50, "Num 2" },
            {0x51, "Num 3" },
            {0x52, "Num 0" },
            {0x53, "Num ." },
            {0x57, "F11" },
            {0x58, "F12" },
            {0x9C, "Num Enter" },
            {0xC5, "Numlock" },
            {0xC8, "Up" },
            {0xCB, "Right" },
            {0xCD, "Left" },
            {0xD0, "Down" },
            {0x0101, "Mouse Left Button" },
            {0x0102, "Mouse Right Button" },
            {0x0103, "Mouse Middle Button" }
        };

        public ushort buttonForward { get; set; }
        public ushort buttonBackward { get; set; }
        public ushort buttonLeft { get; set; }
        public ushort buttonRight { get; set; }
        public ushort buttonAction { get; set; }
        public ushort buttonPickup { get; set; }
        public ushort buttonUse { get; set; }
        public ushort buttonCrouch { get; set; }
        public ushort buttonInstinct { get; set; }
        public ushort buttonFire { get; set; }
        public ushort buttonSecondaryFire { get; set; }
        public ushort buttonRun { get; set; }
        public ushort buttonRaiseKnife { get; set; }
        public ushort buttonChloroform { get; set; }
        public ushort buttonGarrote { get; set; }
        public ushort buttonPistol { get; set; }
        public ushort buttonMachinegun { get; set; }
        public ushort buttonUseAttractDevice { get; set; }
        public ushort buttonHolster { get; set; }
        public ushort buttonReload { get; set; }
        public ushort buttonWhistle { get; set; }
        public ushort buttonShowMap { get; set; }
        public ushort buttonShowTask { get; set; }
        public ushort buttonShowObjective { get; set; }
        public ushort buttonInventory { get; set; }
        public ushort buttonDropItem { get; set; }
        #endregion

        static string _Config = "smersh.shadvs";
        static string _ConfigBackup = "smersh.shadvs.bak";
        static string _Config2 = "settings.scg";
        static string _ConfigBackup2 = "settings.scg.bak";
        static string _Location = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "my games", "AlekhinesGun");
        static string PCGW_URL = "http://pcgamingwiki.com/";
        static string donationURL = "https://www.twitchalerts.com/donate/suicidemachine";
        byte_operations helper, settingsHelper;

        #region GlobalVariables
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
        // Stuff below is used for settings.scg
        // Audio variables
        enum adressesStatic : int
        {
            //Prefix here
            //Unkown here
            MouseSensitivity = 0x08,
            MouseInverted = 0x0C,
            //unkown here
            buttonForward = 0x11,
            buttonBackward = 0x13,
            buttonLeft = 0x15,
            buttonRight = 0x17,
            buttonAction = 0x19,
            buttonPickup = 0x1B,
            buttonUse = 0x1D,
            buttonCrouch = 0x1F,
            buttonInsinct = 0x21,
            buttonFire = 0x23,
            buttonSecondaryFire = 0x25,
            buttonRun = 0x27,
            buttonRaiseKnife = 0x29,
            buttonChloroform = 0x2B,
            buttonGarrote = 0x2D,
            buttonPistol = 0x2F,
            buttonMachinegun = 0x31,
            buttonUseAttractDevice = 0x33,
            buttonHolsterGun = 0x35,
            buttonReloadGun = 0x37,
            buttonWhistle = 0x39,
            //buttonSelectPlayer - leftover
            buttonShowMap = 0x3D,
            buttonShowTask = 0x3F,
            buttonShowObjectives = 0x41,
            buttonInventory = 0x43,
            buttonDropItem = 0x45,
            MusicVolume = 0x4B,
            AmbientVolume = 0x4F,
            EffectsVolume = 0x53,
            VoiceVolume = 0x57,
            DynamicShadows = 0x5B,
            PostFilters = 0x5C,
            AmbientOcclusion = 0x5D,
            HDRRendering = 0x5E,
            Gamma = 0x5F,
            AspectRatio = 0x63,
            ControllerEnabled = 0x67,
            Language = 0x68
        }

        float mouseSensitivity = 0;
        byte mouseInverted = 0;

        float MusicVolume = 1f;
        float EffectsVolume = 1f;
        float AmbientVolume = 1f;
        float VoiceVolume = 1f;
        byte DynamicShadows = 0;
        byte PostFilters = 0;
        byte AmbientOcclusion = 0;
        byte HDRRendering = 0;
        int AspectRatio = 0;
        float Gamma = 0;
        byte ControllerEnabled = 0;
        int Language = 0;
        #endregion

        public Form1()
        {
            InitializeComponent();
            #region keepHiddenAtAllTimes
            CB_Key_Forward.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Forward.DisplayMember = "Value";
            CB_Key_Forward.ValueMember = "Key";
            CB_Key_Backward.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Backward.DisplayMember = "Value";
            CB_Key_Backward.ValueMember = "Key";
            CB_Key_MoveLeft.DataSource = new BindingSource(keyCollection, null);
            CB_Key_MoveLeft.DisplayMember = "Value";
            CB_Key_MoveLeft.ValueMember = "Key";
            CB_Key_MoveRight.DataSource = new BindingSource(keyCollection, null);
            CB_Key_MoveRight.DisplayMember = "Value";
            CB_Key_MoveRight.ValueMember = "Key";
            CB_Key_Action.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Action.DisplayMember = "Value";
            CB_Key_Action.ValueMember = "Key";
            CB_Key_Pickup.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Pickup.DisplayMember = "Value";
            CB_Key_Pickup.ValueMember = "Key";
            CB_Key_Use.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Use.DisplayMember = "Value";
            CB_Key_Use.ValueMember = "Key";
            CB_Key_Crouch.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Crouch.DisplayMember = "Value";
            CB_Key_Crouch.ValueMember = "Key";
            CB_Key_Instinct.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Instinct.DisplayMember = "Value";
            CB_Key_Instinct.ValueMember = "Key";
            CB_Key_Fire.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Fire.DisplayMember = "Value";
            CB_Key_Fire.ValueMember = "Key";
            CB_Key_FireSecondary.DataSource = new BindingSource(keyCollection, null);
            CB_Key_FireSecondary.DisplayMember = "Value";
            CB_Key_FireSecondary.ValueMember = "Key";
            CB_Key_Run.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Run.DisplayMember = "Value";
            CB_Key_Run.ValueMember = "Key";
            CB_Key_Knife.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Knife.DisplayMember = "Value";
            CB_Key_Knife.ValueMember = "Key";
            CB_Key_Cholorform.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Cholorform.DisplayMember = "Value";
            CB_Key_Cholorform.ValueMember = "Key";
            CB_Key_Garrote.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Garrote.DisplayMember = "Value";
            CB_Key_Garrote.ValueMember = "Key";
            CB_Key_Pistol.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Pistol.DisplayMember = "Value";
            CB_Key_Pistol.ValueMember = "Key";
            CB_Key_Machinegun.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Machinegun.DisplayMember = "Value";
            CB_Key_Machinegun.ValueMember = "Key";
            CB_Key_UseAttract.DataSource = new BindingSource(keyCollection, null);
            CB_Key_UseAttract.DisplayMember = "Value";
            CB_Key_UseAttract.ValueMember = "Key";
            CB_Key_Holster.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Holster.DisplayMember = "Value";
            CB_Key_Holster.ValueMember = "Key";
            CB_Key_Reload.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Reload.DisplayMember = "Value";
            CB_Key_Reload.ValueMember = "Key";
            CB_Key_Whistle.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Whistle.DisplayMember = "Value";
            CB_Key_Whistle.ValueMember = "Key";
            CB_Key_Showmap.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Showmap.DisplayMember = "Value";
            CB_Key_Showmap.ValueMember = "Key";
            CB_Key_ShowObjectives.DataSource = new BindingSource(keyCollection, null);
            CB_Key_ShowObjectives.DisplayMember = "Value";
            CB_Key_ShowObjectives.ValueMember = "Key";
            CB_Key_ShowTask.DataSource = new BindingSource(keyCollection, null);
            CB_Key_ShowTask.DisplayMember = "Value";
            CB_Key_ShowTask.ValueMember = "Key";
            CB_Key_Inventory.DataSource = new BindingSource(keyCollection, null);
            CB_Key_Inventory.DisplayMember = "Value";
            CB_Key_Inventory.ValueMember = "Key";
            CB_Key_DropItem.DataSource = new BindingSource(keyCollection, null);
            CB_Key_DropItem.DisplayMember = "Value";
            CB_Key_DropItem.ValueMember = "Key";
            #endregion


            if (!File.Exists(Path.Combine(_Location, _Config)))
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

                byte[] data2 = GetBytesFromAFile(Path.Combine(_Location, _Config2));
                settingsHelper = new byte_operations(data2);

                mouseSensitivity = BitConverter.ToSingle(data2, (int)adressesStatic.MouseSensitivity);
                mouseInverted = data2[(int)adressesStatic.MouseInverted];

                //Buttons here
                buttonForward = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonForward);
                buttonBackward = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonBackward);
                buttonLeft = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonLeft);
                buttonRight = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonRight);
                buttonAction = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonAction);
                buttonPickup = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonPickup);
                buttonUse = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonUse);
                buttonCrouch = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonCrouch);
                buttonInstinct = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonInsinct);
                buttonFire = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonFire);
                buttonSecondaryFire = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonSecondaryFire);
                buttonRun = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonRun);
                buttonRaiseKnife = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonRaiseKnife);
                buttonChloroform = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonChloroform);
                buttonGarrote = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonGarrote);
                buttonPistol = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonPistol);
                buttonMachinegun = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonMachinegun);
                buttonUseAttractDevice = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonUseAttractDevice);
                buttonHolster = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonHolsterGun);
                buttonReload = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonReloadGun);
                buttonWhistle = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonWhistle);
                buttonShowMap = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonShowMap);
                buttonShowTask = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonShowTask);
                buttonShowObjective = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonShowObjectives);
                buttonInventory = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonInventory);
                buttonDropItem = BitConverter.ToUInt16(data2, (int)adressesStatic.buttonDropItem);

                MusicVolume = BitConverter.ToSingle(data2, (int)adressesStatic.MusicVolume);
                AmbientVolume = BitConverter.ToSingle(data2, (int)adressesStatic.AmbientVolume);
                EffectsVolume = BitConverter.ToSingle(data2, (int)adressesStatic.EffectsVolume);
                VoiceVolume = BitConverter.ToSingle(data2, (int)adressesStatic.VoiceVolume);
                DynamicShadows = data2[(int)adressesStatic.DynamicShadows];
                PostFilters = data2[(int)adressesStatic.PostFilters];
                AmbientOcclusion = data2[(int)adressesStatic.AmbientOcclusion];
                HDRRendering = data2[(int)adressesStatic.HDRRendering];
                AspectRatio = BitConverter.ToInt32(data2, (int)adressesStatic.AspectRatio);
                ControllerEnabled = data2[(int)adressesStatic.ControllerEnabled];
                Language = BitConverter.ToInt32(data2, (int)adressesStatic.Language);
                Gamma = BitConverter.ToSingle(data2, (int)adressesStatic.Gamma);

                //Now display stuff
                TBar_MouseSensitivity.Value = (int)(mouseSensitivity * 10);
                L_MouseSensitivity.Text = "Mouse Sensitivity: " + (mouseSensitivity * 10).ToString();
                CB_InvertMouseYAxis.Checked = Convert.ToBoolean(mouseInverted);

                //Button bindings, because imagine creating that many events and stuff
                CB_Key_Forward.DataBindings.Add("SelectedValue", this, "buttonForward", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Backward.DataBindings.Add("SelectedValue", this, "buttonBackward", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_MoveLeft.DataBindings.Add("SelectedValue", this, "buttonLeft", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_MoveRight.DataBindings.Add("SelectedValue", this, "buttonRight", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Action.DataBindings.Add("SelectedValue", this, "buttonAction", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Pickup.DataBindings.Add("SelectedValue", this, "buttonPickup", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Use.DataBindings.Add("SelectedValue", this, "buttonUse", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Crouch.DataBindings.Add("SelectedValue", this, "buttonCrouch", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Instinct.DataBindings.Add("SelectedValue", this, "buttonInstinct", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Fire.DataBindings.Add("SelectedValue", this, "buttonFire", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_FireSecondary.DataBindings.Add("SelectedValue", this, "buttonSecondaryFire", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Run.DataBindings.Add("SelectedValue", this, "buttonRun", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Knife.DataBindings.Add("SelectedValue", this, "buttonRaiseKnife", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Cholorform.DataBindings.Add("SelectedValue", this, "buttonChloroform", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Garrote.DataBindings.Add("SelectedValue", this, "buttonGarrote", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Pistol.DataBindings.Add("SelectedValue", this, "buttonPistol", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Machinegun.DataBindings.Add("SelectedValue", this, "buttonMachinegun", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_UseAttract.DataBindings.Add("SelectedValue", this, "buttonUseAttractDevice", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Holster.DataBindings.Add("SelectedValue", this, "buttonHolster", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Reload.DataBindings.Add("SelectedValue", this, "buttonReload", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Whistle.DataBindings.Add("SelectedValue", this, "buttonWhistle", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Showmap.DataBindings.Add("SelectedValue", this, "buttonShowMap", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_ShowObjectives.DataBindings.Add("SelectedValue", this, "buttonShowObjective", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_ShowTask.DataBindings.Add("SelectedValue", this, "buttonShowTask", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_Inventory.DataBindings.Add("SelectedValue", this, "buttonInventory", false, DataSourceUpdateMode.OnPropertyChanged);
                CB_Key_DropItem.DataBindings.Add("SelectedValue", this, "buttonDropItem", false, DataSourceUpdateMode.OnPropertyChanged);

                //Volume
                TBar_EffectsVolume.Value = (int)(EffectsVolume * 100);
                L_EffectsVolume.Text = "Effects Volume: " + (EffectsVolume * 100).ToString() + "%";
                TBar_MusicVolume.Value = (int)(MusicVolume * 100);
                L_MusicVolume.Text = "Music Volume: " + (MusicVolume * 100).ToString() + "%";
                TBar_VoiceVolume.Value = (int)(VoiceVolume * 100);
                L_VoiceVolume.Text = "Voice Volume: " + (VoiceVolume * 100).ToString() + "%";
                TBar_AmbientVolume.Value = (int)(AmbientVolume * 100);
                L_AmbientVolume.Text = "Ambient Volume: " + (AmbientVolume * 100).ToString() + "%";
                CBox_DynamicShadows.SelectedIndex = DynamicShadows;
                CBox_PostFilters.SelectedIndex = PostFilters;
                CBox_AmbientOcclusion.SelectedIndex = AmbientOcclusion;
                CBox_HDRRendering.SelectedIndex = HDRRendering;
                CBox_AspectRatio.SelectedIndex = AspectRatio;
                CB_ControllerEnabled.Checked = Convert.ToBoolean(ControllerEnabled);
                CBox_Lanuage.SelectedIndex = Language;
                TBar_Gamma.Value = (int)(Gamma * 50);
                L_Gamma.Text = "Gamma: " + (Gamma*50).ToString();

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
            if (!File.Exists(Path.Combine(_Location, _ConfigBackup2)))
            {
                File.Copy(Path.Combine(_Location, _Config2), Path.Combine(_Location, _ConfigBackup2));
            }

            //Replacing bytes in byte array of smersh.shadvs
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

            //Replacing bytes in byte array of settings.scg
            settingsHelper.replaceBytes(BitConverter.GetBytes(mouseSensitivity), (int)adressesStatic.MouseSensitivity);
            settingsHelper.replaceBytes(mouseInverted, (int)adressesStatic.MouseInverted);
            //buttons
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonForward), (int)adressesStatic.buttonForward);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonBackward), (int)adressesStatic.buttonBackward);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonLeft), (int)adressesStatic.buttonLeft);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonRight), (int)adressesStatic.buttonRight);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonAction), (int)adressesStatic.buttonAction);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonPickup), (int)adressesStatic.buttonPickup);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonUse), (int)adressesStatic.buttonUse);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonCrouch), (int)adressesStatic.buttonCrouch);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonInstinct), (int)adressesStatic.buttonInsinct);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonFire), (int)adressesStatic.buttonFire);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonSecondaryFire), (int)adressesStatic.buttonSecondaryFire);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonRun), (int)adressesStatic.buttonRun);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonRaiseKnife), (int)adressesStatic.buttonRaiseKnife);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonChloroform), (int)adressesStatic.buttonChloroform);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonGarrote), (int)adressesStatic.buttonGarrote);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonPistol), (int)adressesStatic.buttonPistol);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonMachinegun), (int)adressesStatic.buttonMachinegun);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonUseAttractDevice), (int)adressesStatic.buttonUseAttractDevice);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonReload), (int)adressesStatic.buttonReloadGun);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonWhistle), (int)adressesStatic.buttonWhistle);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonShowMap), (int)adressesStatic.buttonShowMap);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonShowTask), (int)adressesStatic.buttonShowTask);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonShowObjective), (int)adressesStatic.buttonShowObjectives);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonInventory), (int)adressesStatic.buttonInventory);
            settingsHelper.replaceBytes(BitConverter.GetBytes(buttonDropItem), (int)adressesStatic.buttonDropItem);
            //buttonsDone
            settingsHelper.replaceBytes(BitConverter.GetBytes(MusicVolume), (int)adressesStatic.MusicVolume);
            settingsHelper.replaceBytes(BitConverter.GetBytes(AmbientVolume), (int)adressesStatic.AmbientVolume);
            settingsHelper.replaceBytes(BitConverter.GetBytes(EffectsVolume), (int)adressesStatic.EffectsVolume);
            settingsHelper.replaceBytes(BitConverter.GetBytes(VoiceVolume), (int)adressesStatic.VoiceVolume);
            settingsHelper.replaceBytes(DynamicShadows, (int)adressesStatic.DynamicShadows);
            settingsHelper.replaceBytes(PostFilters, (int)adressesStatic.PostFilters);
            settingsHelper.replaceBytes(AmbientOcclusion, (int)adressesStatic.AmbientOcclusion);
            settingsHelper.replaceBytes(HDRRendering, (int)adressesStatic.HDRRendering);
            settingsHelper.replaceBytes(BitConverter.GetBytes(Gamma), (int)adressesStatic.Gamma);
            settingsHelper.replaceBytes(BitConverter.GetBytes(AspectRatio), (int)adressesStatic.AspectRatio);
            settingsHelper.replaceBytes(ControllerEnabled, (int)adressesStatic.ControllerEnabled);
            settingsHelper.replaceBytes(BitConverter.GetBytes(Convert.ToUInt16(Language)), (int)adressesStatic.Language);

            //Writting and getting results
            bool success = true;
            bool success2 = true;
            success = WriteBytesToAFile(Path.Combine(_Location, _Config), helper.returnArray());
            success2 = WriteBytesToAFile(Path.Combine(_Location, _Config2), settingsHelper.returnArray());

            if (!success && !success2)
                MessageBox.Show("There was an error writting to config files!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if(!success)
            {
                MessageBox.Show("There was an error writting to \"smersh.shadvs\"!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(!success2)
            {
                MessageBox.Show("There was an error writting to \"settings.scg\"!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Successfully made the changes!", "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region ButtonEvents
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

        private void TBar_EffectsVolume_Scroll(object sender, EventArgs e)
        {
            EffectsVolume = TBar_EffectsVolume.Value / 100.0f;
            L_EffectsVolume.Text = "Effects Volume: " + (EffectsVolume*100).ToString() + "%";
        }

        private void TBar_MusicVolume_Scroll(object sender, EventArgs e)
        {
            MusicVolume = TBar_MusicVolume.Value / 100.0f;
            L_MusicVolume.Text = "Music Volume: " + (MusicVolume * 100).ToString() + "%";
        }

        private void TBar_VoiceVolume_Scroll(object sender, EventArgs e)
        {
            VoiceVolume = TBar_VoiceVolume.Value / 100.0f;
            L_VoiceVolume.Text = "Voice Volume: " + (VoiceVolume * 100).ToString() + "%";
        }

        private void TBar_MouseSensitivity_Scroll(object sender, EventArgs e)
        {
            mouseSensitivity = TBar_MouseSensitivity.Value/10f;
            L_MouseSensitivity.Text = "Mouse Sensitivity: " + (mouseSensitivity * 10).ToString();
        }

        private void TBar_Gamma_Scroll(object sender, EventArgs e)
        {
            Gamma = TBar_Gamma.Value / 50f;
            L_Gamma.Text = "Gamma: " + (Gamma * 50).ToString();
        }

        private void CB_InvertMouseYAxis_CheckedChanged(object sender, EventArgs e)
        {
            mouseInverted = Convert.ToByte(CB_InvertMouseYAxis.Checked);
        }

        private void CB_ControllerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_ControllerEnabled.Checked)
                ControllerEnabled = 1;
            else
                ControllerEnabled = 0;
        }

        private void CBox_HDRRendering_SelectedIndexChanged(object sender, EventArgs e)
        {
            HDRRendering = (byte)CBox_HDRRendering.SelectedIndex;
        }

        private void CBox_AspectRatio_SelectedIndexChanged(object sender, EventArgs e)
        {
            AspectRatio = (byte)CBox_AspectRatio.SelectedIndex;
        }

        private void CBox_DynamicShadows_SelectedIndexChanged(object sender, EventArgs e)
        {
            DynamicShadows = (byte)CBox_DynamicShadows.SelectedIndex;
        }

        private void CBox_PostFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            PostFilters = (byte)CBox_PostFilters.SelectedIndex;
        }

        private void CBox_AmbientOcclusion_SelectedIndexChanged(object sender, EventArgs e)
        {
            AmbientOcclusion = (byte)CBox_AmbientOcclusion.SelectedIndex;
        }

        private void CBox_Lanuage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Language = CBox_Lanuage.SelectedIndex;
        }

        private void TBar_AmbientVolume_Scroll(object sender, EventArgs e)
        {
            AmbientVolume = TBar_AmbientVolume.Value / 100.0f;
            L_AmbientVolume.Text = "Ambient Volume: " + (AmbientVolume * 100).ToString() + "%";
        }
        #endregion
    }
}
