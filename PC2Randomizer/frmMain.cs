using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Memory;

namespace PC2Randomizer
{
    public partial class frmMain : Form
    {
        const string emptySettingsString = "0;0;0;0;False;0;10;False;False;0;0;10;False;False;0;False;0,10;False;False;False;False;0,10;False;0,10;0,10;0,10;0,10;100;False;0;0;False;False;False;0;0;False;False;False;0;0;0;100;False;False;0;0;False;0;0;False;False;0;0;False;False;0;0;100;False;0;0;False;0;False;0;False;0;0;False;False;False;100;False;0;0;0;0;False;0;False;0;False;False;False;0;0;False;100;False;0;0;0;0;False;0;False;False;0;False;False;False;0;0;100;False;0;0;0;0;False;False;0;False;False;False;False;0;0;0;100;False;0;0;";

        const string POPULATIONADDR = "fastfood2.exe+0x17BB28";
        const string TIMELIMITADDR = "fastfood2.exe+0x17BB2C";
        const string CITYNUMBERADDR = "fastfood2.exe+0x17BB40";
        const string STARTHQYNADDR = "fastfood2.exe+0x17BB34";
        const string MISSIONBUILDINGYNADDR = "fastfood2.exe+0x17BB3C";
        const string STARTHQADDR = "fastfood2.exe+0x17BB30";
        const string MAXHQADDR = "fastfood2.exe+0x17BB38";
        const string GOAL1ADDR = "fastfood2.exe+0x17BB48";
        const string GOAL2ADDR = "fastfood2.exe+0x17BB4C";
        const string GOAL3ADDR = "fastfood2.exe+0x17BB50";
        const string DIFFICULTYADDR = "fastfood2.exe+0x17BB24";
        const string CITYSTATSADDR = "fastfood2.exe+0x17C670";
        readonly string[] RENTOFFSETS = { "8", "0", "8C" };
        readonly string[] PATIENCEOFFSETS = { "8", "0", "90" };
        readonly string[] BUYPRICEOFFSETS = { "8", "0", "94" };
        readonly string[] INCOMEOFFSETS = { "8", "0", "98" };
        readonly string[] QUALITYOFFSETS = { "8", "0", "9C" };
        readonly string[] COMFORTOFFSETS = { "8", "0", "A0" };

        //Player
        const string PLAYERMONEYADDR = "fastfood2.exe+0x17BCD8";
        const string PLAYEREINKAEUFERADDR = "fastfood2.exe+0x17BCE0";
        const string PLAYERGANGSTERADDR = "fastfood2.exe+0x17BCE4";
        const string PLAYERFACHKRAFTADDR = "fastfood2.exe+0x17BCE8";
        const string PLAYERPOLITIKERADDR = "fastfood2.exe+0x17BCEC";
        const string PLAYERVERKAEUFERADDR = "fastfood2.exe+0x17BCF0";
        const string PLAYERWACHEADDR = "fastfood2.exe+0x17BCF4";

        //Enemy 1
        const string ENEMY1ADDR = "fastfood2.exe+0x17BDD4";
        const string E1NAMEADDR = "fastfood2.exe+0x17BD18";
        const string E1COMPANYNAMEADDR = "fastfood2.exe+0x17BD58";
        const string E1LOGOADDR = "fastfood2.exe+0x17BDCC";
        const string E1COLORADDR = "fastfood2.exe+0x17BDD0";
        const string E1GENDERADDR = "fastfood2.exe+0x17BDB8";
        const string E1HAIRADDR = "fastfood2.exe+0x17BDBC";
        const string E1EYEADDR = "fastfood2.exe+0x17BDC0";
        const string E1MOUTHADDR = "fastfood2.exe+0x17BDC4";
        const string E1CHINADDR = "fastfood2.exe+0x17BDC8";
        const string E1MONEYADDR = "fastfood2.exe+0x17BD98";
        const string E1EINKAEUFERADDR = "fastfood2.exe+0x17BDA0";
        const string E1GANGSTERADDR = "fastfood2.exe+0x17BDA4";
        const string E1FACHKRAFTADDR = "fastfood2.exe+0x17BDA8";
        const string E1POLITIKERADDR = "fastfood2.exe+0x17BDAC";
        const string E1VERKAEUFERADDR = "fastfood2.exe+0x17BDB0";
        const string E1WACHEADDR = "fastfood2.exe+0x17BDB4";
        const string E1STATSADDR = "fastfood2.exe+0x17BD9C";
        //Enemy 2
        const string ENEMY2ADDR = "fastfood2.exe+0x17BE94";
        const string E2NAMEADDR = "fastfood2.exe+0x17BDD8";
        const string E2COMPANYNAMEADDR = "fastfood2.exe+0x17BE18";
        const string E2LOGOADDR = "fastfood2.exe+0x17BE8C";
        const string E2COLORADDR = "fastfood2.exe+0x17BE90";
        const string E2GENDERADDR = "fastfood2.exe+0x17BE78";
        const string E2HAIRADDR = "fastfood2.exe+0x17BE7C";
        const string E2EYEADDR = "fastfood2.exe+0x17BE80";
        const string E2MOUTHADDR = "fastfood2.exe+0x17BE84";
        const string E2CHINADDR = "fastfood2.exe+0x17BE88";
        const string E2MONEYADDR = "fastfood2.exe+0x17BE58";
        const string E2EINKAEUFERADDR = "fastfood2.exe+0x17BE60";
        const string E2GANGSTERADDR = "fastfood2.exe+0x17BE64";
        const string E2FACHKRAFTADDR = "fastfood2.exe+0x17BE68";
        const string E2POLITIKERADDR = "fastfood2.exe+0x17BE6C";
        const string E2VERKAEUFERADDR = "fastfood2.exe+0x17BE70";
        const string E2WACHEADDR = "fastfood2.exe+0x17BE74";
        const string E2STATSADDR = "fastfood2.exe+0x17BE5C";
        //Enemy 3
        const string ENEMY3ADDR = "fastfood2.exe+0x17BF54";
        const string E3NAMEADDR = "fastfood2.exe+0x17BE98";
        const string E3COMPANYNAMEADDR = "fastfood2.exe+0x17BED8";
        const string E3LOGOADDR = "fastfood2.exe+0x17BF4C";
        const string E3COLORADDR = "fastfood2.exe+0x17BF50";
        const string E3GENDERADDR = "fastfood2.exe+0x17BF38";
        const string E3HAIRADDR = "fastfood2.exe+0x17BF3C";
        const string E3EYEADDR = "fastfood2.exe+0x17BF40";
        const string E3MOUTHADDR = "fastfood2.exe+0x17BF44";
        const string E3CHINADDR = "fastfood2.exe+0x17BF48";
        const string E3MONEYADDR = "fastfood2.exe+0x17BF18";
        const string E3EINKAEUFERADDR = "fastfood2.exe+0x17BF20";
        const string E3GANGSTERADDR = "fastfood2.exe+0x17BF24";
        const string E3FACHKRAFTADDR = "fastfood2.exe+0x17BF28";
        const string E3POLITIKERADDR = "fastfood2.exe+0x17BF2C";
        const string E3VERKAEUFERADDR = "fastfood2.exe+0x17BF30";
        const string E3WACHEADDR = "fastfood2.exe+0x17BF34";
        const string E3STATSADDR = "fastfood2.exe+0x17BF1C";
        //Enemy 4
        const string ENEMY4ADDR = "fastfood2.exe+0x17C014";
        const string E4NAMEADDR = "fastfood2.exe+0x17BF58";
        const string E4COMPANYNAMEADDR = "fastfood2.exe+0x17BF98";
        const string E4LOGOADDR = "fastfood2.exe+0x17C00C";
        const string E4COLORADDR = "fastfood2.exe+0x17C010";
        const string E4GENDERADDR = "fastfood2.exe+0x17BF38";
        const string E4HAIRADDR = "fastfood2.exe+0x17BF3C";
        const string E4EYEADDR = "fastfood2.exe+0x17BF40";
        const string E4MOUTHADDR = "fastfood2.exe+0x17BF44";
        const string E4CHINADDR = "fastfood2.exe+0x17BF48";
        const string E4MONEYADDR = "fastfood2.exe+0x17BFD8";
        const string E4EINKAEUFERADDR = "fastfood2.exe+0x17BFE0";
        const string E4GANGSTERADDR = "fastfood2.exe+0x17BFE4";
        const string E4FACHKRAFTADDR = "fastfood2.exe+0x17BFE8";
        const string E4POLITIKERADDR = "fastfood2.exe+0x17BFEC";
        const string E4VERKAEUFERADDR = "fastfood2.exe+0x17BFF0";
        const string E4WACHEADDR = "fastfood2.exe+0x17BFF4";
        const string E4STATSADDR = "fastfood2.exe+0x17BFDC";
        //Enemy 5
        const string ENEMY5ADDR = "fastfood2.exe+0x17C0D4";
        const string E5NAMEADDR = "fastfood2.exe+0x17C018";
        const string E5COMPANYNAMEADDR = "fastfood2.exe+0x17C058";
        const string E5LOGOADDR = "fastfood2.exe+0x17C0CC";
        const string E5COLORADDR = "fastfood2.exe+0x17C0D0";
        const string E5GENDERADDR = "fastfood2.exe+0x17C0B8";
        const string E5HAIRADDR = "fastfood2.exe+0x17C0BC";
        const string E5EYEADDR = "fastfood2.exe+0x17C0C0";
        const string E5MOUTHADDR = "fastfood2.exe+0x17C0C4";
        const string E5CHINADDR = "fastfood2.exe+0x17C0C8";
        const string E5MONEYADDR = "fastfood2.exe+0x17C098";
        const string E5EINKAEUFERADDR = "fastfood2.exe+0x17C0A0";
        const string E5GANGSTERADDR = "fastfood2.exe+0x17C0A4";
        const string E5FACHKRAFTADDR = "fastfood2.exe+0x17C0A8";
        const string E5POLITIKERADDR = "fastfood2.exe+0x17C0AC";
        const string E5VERKAEUFERADDR = "fastfood2.exe+0x17C0B0";
        const string E5WACHEADDR = "fastfood2.exe+0x17C0B4";
        const string E5STATSADDR = "fastfood2.exe+0x17C09C";

        int creditProgress = 0;
        Mem mrmy = new Mem();
        int PID = 0;
        private readonly Random rng = new Random();
        bool readyToStartGame = false;
        decimal oldMoneyself;
        decimal oldPopulation;
        int buildingsAndMissions = 0;
        int maxHQ = 0;
        bool forceNoMissions = false;   //TODO: missionen abschalten, passiert aber automatisch bei 0 gegnern also gg
        int enemyAmount = 0;
        string[] settings = new string[125];

        //mrmy.WriteMemory(populationAddr,"int",nmrcPopulation.Value.ToString());

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            memberOldVals();
            fillComboBoxes();
        }

        private void fillComboBoxes()
        {
            foreach (TabPage page in tabControl.TabPages) {
                foreach (Control currControl in page.Controls)
                {
                    if (currControl is ComboBox)
                    {
                        ComboBox foundBox = (ComboBox)currControl;
                        foundBox.SelectedIndex = 0;
                    }
                }
            }
        }

        private void memberOldVals()
        {
            oldMoneyself = nmrcMoneySelf.Value;
            oldPopulation = nmrcPopulation.Value;
        }

        private void nmrcMoneySelf_ValueChanged(object sender, EventArgs e)
        {
            if (nmrcMoneySelf.Value % 10 != 0)
            {
                if (oldMoneyself < nmrcMoneySelf.Value)
                {
                    nmrcMoneySelf.Value += 10 - (nmrcMoneySelf.Value % 10);
                } else
                {
                    nmrcMoneySelf.Value -= nmrcMoneySelf.Value % 10;
                }
                oldMoneyself = nmrcMoneySelf.Value;
            }
        }

        private void nmrcMoneyE1_ValueChanged(object sender, EventArgs e)
        {
            if (nmrcMoneyE1.Value % 10 != 0)
            {
                if (oldMoneyself < nmrcMoneyE1.Value)
                {
                    nmrcMoneyE1.Value += 10 - (nmrcMoneyE1.Value % 10);
                }
                else
                {
                    nmrcMoneyE1.Value -= nmrcMoneyE1.Value % 10;
                }
                oldMoneyself = nmrcMoneyE1.Value;
            }
        }

        private void nmrcMoneyE2_ValueChanged(object sender, EventArgs e)
        {
            if (nmrcMoneyE2.Value % 10 != 0)
            {
                if (oldMoneyself < nmrcMoneyE2.Value)
                {
                    nmrcMoneyE2.Value += 10 - (nmrcMoneyE2.Value % 10);
                }
                else
                {
                    nmrcMoneyE2.Value -= nmrcMoneyE2.Value % 10;
                }
                oldMoneyself = nmrcMoneyE2.Value;
            }
        }

        private void nmrcMoneyE3_ValueChanged(object sender, EventArgs e)
        {
            if (nmrcMoneyE3.Value % 10 != 0)
            {
                if (oldMoneyself < nmrcMoneyE3.Value)
                {
                    nmrcMoneyE3.Value += 10 - (nmrcMoneyE3.Value % 10);
                }
                else
                {
                    nmrcMoneyE3.Value -= nmrcMoneyE3.Value % 10;
                }
                oldMoneyself = nmrcMoneyE3.Value;
            }
        }

        private void nmrcMoneyE4_ValueChanged(object sender, EventArgs e)
        {
            if (nmrcMoneyE4.Value % 10 != 0)
            {
                if (oldMoneyself < nmrcMoneyE4.Value)
                {
                    nmrcMoneyE4.Value += 10 - (nmrcMoneyE4.Value % 10);
                }
                else
                {
                    nmrcMoneyE4.Value -= nmrcMoneyE4.Value % 10;
                }
                oldMoneyself = nmrcMoneyE4.Value;
            }
        }

        private void nmrcMoneyE5_ValueChanged(object sender, EventArgs e)
        {
            if (nmrcMoneyE5.Value % 10 != 0)
            {
                if (oldMoneyself < nmrcMoneyE5.Value)
                {
                    nmrcMoneyE5.Value += 10 - (nmrcMoneyE5.Value % 10);
                }
                else
                {
                    nmrcMoneyE5.Value -= nmrcMoneyE5.Value % 10;
                }
                oldMoneyself = nmrcMoneyE5.Value;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            PID = mrmy.GetProcIdFromName("fastfood2");  //TODO: fastfood2 auf pc2 vermutlich für die alte Version
            if (PID > 0)
            {
                mrmy.OpenProcess(PID);
                btnConnect.Enabled = false;
                tmrFreegame.Start();
            }
            else
            {
                lblInfo.Text = "No Process found. Is your game running?";
            }
        }

        private void tmrFreegame_Tick(object sender, EventArgs e)
        {
            int timeLimit = mrmy.ReadInt(TIMELIMITADDR);
            if (timeLimit > 0)
            {
                if (readyToStartGame == false)
                {
                    deactivateUI();
                    randomizeAll();
                    System.Media.SystemSounds.Asterisk.Play();
                    readyToStartGame = true;
                    tmrIngame.Start();
                    lblInfo.Text = "Everything set. Start the game now!";
                }
            } else
            {
                activateUI();
                resetValues();
                readyToStartGame = false;
                lblInfo.Text = "Connected to the Game! Start a free Game now.\nThe sound will indicate when the randomizer is ready.";
            }
        }

        private void resetValues()
        {
            buildingsAndMissions = 0;
        }

        private void deactivateUI()
        {
            foreach (Control currControl in this.Controls)
            {
                if(currControl.Name.Equals("lblInfo"))
                {
                    continue;
                }
                currControl.Enabled = false;
            }
        }

        private void activateUI()
        {
            foreach (Control currControl in this.Controls)
            {
                if (currControl.Name.Equals("btnConnect"))
                {
                    continue;
                }
                currControl.Enabled = true;
            }
        }

        private void nmrcStartHQ_ValueChanged(object sender, EventArgs e)
        {
            if (nmrcStartHQ.Value > nmrcMaxHQ.Value)
            {
                nmrcStartHQ.Value = nmrcMaxHQ.Value;
            }
        }

        private void nmrcEnemyAmount_ValueChanged(object sender, EventArgs e)
        {
            if (chkEnemyAmount.Checked && nmrcEnemyAmount.Value == 0)
            {
                cmbMissions.SelectedIndex = 2;
                cmbMissions.Enabled = false;
            } else
            {
                cmbMissions.Enabled = true;
            }
        }

        private void cmbMissions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMissions.SelectedIndex == 1 && nmrcEnemyAmount.Value == 0)
            {
                nmrcEnemyAmount.Value = 1;
            }
        }

        private void randomizeAll()
        {
            Console.WriteLine("Settings: ");
            randomizeEnemyAmount();
            randomizeEnemyDesign();
            randomizeEnemyStats();
            randomizeBuildingsAndMission();
            randomizeStartHQ();
            randomizePopulation();
            randomizeCity();
            randomizeTimeLimit();
            randomizeDifficulty();
            randomizeGoals();
            randomizePlayerStats();

            //randomizeCityStats passiert automatisch, wenn das Spiel gestartet wird im timer.tick
        }

        private void randomizeEnemyStats()
        {
            //MoneyE1
            if (chkMoneyE1.Checked)
            {
                mrmy.WriteMemory(E1MONEYADDR, "int", nmrcMoneyE1.Value.ToString());
            }
            else
            {
                int n = rng.Next(70, 991);
                Console.WriteLine("E1 money: " + n);
                mrmy.WriteMemory(E1MONEYADDR, "int", n.ToString());
            }
            //EinkaeuferE1
            if (chkEinkaeuferE1.Checked)
            {
                mrmy.WriteMemory(E1EINKAEUFERADDR, "int", nmrcEinkaeuferE1.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E1 Einkaeufer: " + n);
                mrmy.WriteMemory(E1EINKAEUFERADDR, "int", n.ToString());
            }
            //PolitikerE1
            if (chkPolitikerE1.Checked)
            {
                mrmy.WriteMemory(E1POLITIKERADDR, "int", nmrcPolitikerE1.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E1 Politiker: " + n);
                mrmy.WriteMemory(E1POLITIKERADDR, "int", n.ToString());
            }
            //GangsterE1
            if (chkGangsterE1.Checked)
            {
                mrmy.WriteMemory(E1GANGSTERADDR, "int", nmrcGangsterE1.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E1 Gangster: " + n);
                mrmy.WriteMemory(E1GANGSTERADDR, "int", n.ToString());
            }
            //VerkaeuferE1
            if (chkVerkaeuferE1.Checked)
            {
                mrmy.WriteMemory(E1VERKAEUFERADDR, "int", nmrcVerkaeuferE1.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E1 Verkaeufer: " + n);
                mrmy.WriteMemory(E1VERKAEUFERADDR, "int", n.ToString());
            }
            //FachkraftE1
            if (chkFachkraftE1.Checked)
            {
                mrmy.WriteMemory(E1FACHKRAFTADDR, "int", nmrcFachkraftE1.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E1 Fachkraft: " + n);
                mrmy.WriteMemory(E1FACHKRAFTADDR, "int", n.ToString());
            }
            //WacheE1
            if (chkWacheE1.Checked)
            {
                mrmy.WriteMemory(E1WACHEADDR, "int", nmrcWacheE1.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E1 Wache: " + n);
                mrmy.WriteMemory(E1WACHEADDR, "int", n.ToString());
            }

            //MoneyE2
            if (chkMoneyE2.Checked)
            {
                mrmy.WriteMemory(E2MONEYADDR, "int", nmrcMoneyE2.Value.ToString());
            }
            else
            {
                int n = rng.Next(70, 991);
                Console.WriteLine("E2 money: " + n);
                mrmy.WriteMemory(E2MONEYADDR, "int", n.ToString());
            }
            //EinkaeuferE2
            if (chkEinkaeuferE2.Checked)
            {
                mrmy.WriteMemory(E2EINKAEUFERADDR, "int", nmrcEinkaeuferE2.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E2 Einkaeufer: " + n);
                mrmy.WriteMemory(E2EINKAEUFERADDR, "int", n.ToString());
            }
            //PolitikerE2
            if (chkPolitikerE2.Checked)
            {
                mrmy.WriteMemory(E2POLITIKERADDR, "int", nmrcPolitikerE2.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E2 Politiker: " + n);
                mrmy.WriteMemory(E2POLITIKERADDR, "int", n.ToString());
            }
            //GangsterE2
            if (chkGangsterE2.Checked)
            {
                mrmy.WriteMemory(E2GANGSTERADDR, "int", nmrcGangsterE2.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E2 Gangster: " + n);
                mrmy.WriteMemory(E2GANGSTERADDR, "int", n.ToString());
            }
            //VerkaeuferE2
            if (chkVerkaeuferE2.Checked)
            {
                mrmy.WriteMemory(E2VERKAEUFERADDR, "int", nmrcVerkaeuferE2.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E2 Verkaeufer: " + n);
                mrmy.WriteMemory(E2VERKAEUFERADDR, "int", n.ToString());
            }
            //FachkraftE2
            if (chkFachkraftE2.Checked)
            {
                mrmy.WriteMemory(E2FACHKRAFTADDR, "int", nmrcFachkraftE2.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E2 Fachkraft: " + n);
                mrmy.WriteMemory(E2FACHKRAFTADDR, "int", n.ToString());
            }
            //WacheE2
            if (chkWacheE2.Checked)
            {
                mrmy.WriteMemory(E2WACHEADDR, "int", nmrcWacheE2.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E2 Wache: " + n);
                mrmy.WriteMemory(E2WACHEADDR, "int", n.ToString());
            }

            //MoneyE3
            if (chkMoneyE3.Checked)
            {
                mrmy.WriteMemory(E3MONEYADDR, "int", nmrcMoneyE3.Value.ToString());
            }
            else
            {
                int n = rng.Next(70, 991);
                Console.WriteLine("E3 money: " + n);
                mrmy.WriteMemory(E3MONEYADDR, "int", n.ToString());
            }
            //EinkaeuferE3
            if (chkEinkaeuferE3.Checked)
            {
                mrmy.WriteMemory(E3EINKAEUFERADDR, "int", nmrcEinkaeuferE3.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E3 Einkaeufer: " + n);
                mrmy.WriteMemory(E3EINKAEUFERADDR, "int", n.ToString());
            }
            //PolitikerE3
            if (chkPolitikerE3.Checked)
            {
                mrmy.WriteMemory(E3POLITIKERADDR, "int", nmrcPolitikerE3.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E3 Politiker: " + n);
                mrmy.WriteMemory(E3POLITIKERADDR, "int", n.ToString());
            }
            //GangsterE3
            if (chkGangsterE3.Checked)
            {
                mrmy.WriteMemory(E3GANGSTERADDR, "int", nmrcGangsterE3.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E3 Gangster: " + n);
                mrmy.WriteMemory(E3GANGSTERADDR, "int", n.ToString());
            }
            //VerkaeuferE3
            if (chkVerkaeuferE3.Checked)
            {
                mrmy.WriteMemory(E3VERKAEUFERADDR, "int", nmrcVerkaeuferE3.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E3 Verkaeufer: " + n);
                mrmy.WriteMemory(E3VERKAEUFERADDR, "int", n.ToString());
            }
            //FachkraftE3
            if (chkFachkraftE3.Checked)
            {
                mrmy.WriteMemory(E3FACHKRAFTADDR, "int", nmrcFachkraftE3.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E3 Fachkraft: " + n);
                mrmy.WriteMemory(E3FACHKRAFTADDR, "int", n.ToString());
            }
            //WacheE3
            if (chkWacheE3.Checked)
            {
                mrmy.WriteMemory(E3WACHEADDR, "int", nmrcWacheE3.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E3 Wache: " + n);
                mrmy.WriteMemory(E3WACHEADDR, "int", n.ToString());
            }

            //MoneyE4
            if (chkMoneyE4.Checked)
            {
                mrmy.WriteMemory(E4MONEYADDR, "int", nmrcMoneyE4.Value.ToString());
            }
            else
            {
                int n = rng.Next(70, 991);
                Console.WriteLine("E4 money: " + n);
                mrmy.WriteMemory(E4MONEYADDR, "int", n.ToString());
            }
            //EinkaeuferE4
            if (chkEinkaeuferE4.Checked)
            {
                mrmy.WriteMemory(E4EINKAEUFERADDR, "int", nmrcEinkaeuferE4.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E4 Einkaeufer: " + n);
                mrmy.WriteMemory(E4EINKAEUFERADDR, "int", n.ToString());
            }
            //PolitikerE4
            if (chkPolitikerE4.Checked)
            {
                mrmy.WriteMemory(E4POLITIKERADDR, "int", nmrcPolitikerE4.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E4 Politiker: " + n);
                mrmy.WriteMemory(E4POLITIKERADDR, "int", n.ToString());
            }
            //GangsterE4
            if (chkGangsterE4.Checked)
            {
                mrmy.WriteMemory(E4GANGSTERADDR, "int", nmrcGangsterE4.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E4 Gangster: " + n);
                mrmy.WriteMemory(E4GANGSTERADDR, "int", n.ToString());
            }
            //VerkaeuferE4
            if (chkVerkaeuferE4.Checked)
            {
                mrmy.WriteMemory(E4VERKAEUFERADDR, "int", nmrcVerkaeuferE4.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E4 Verkaeufer: " + n);
                mrmy.WriteMemory(E4VERKAEUFERADDR, "int", n.ToString());
            }
            //FachkraftE4
            if (chkFachkraftE4.Checked)
            {
                mrmy.WriteMemory(E4FACHKRAFTADDR, "int", nmrcFachkraftE4.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E4 Fachkraft: " + n);
                mrmy.WriteMemory(E4FACHKRAFTADDR, "int", n.ToString());
            }
            //WacheE4
            if (chkWacheE4.Checked)
            {
                mrmy.WriteMemory(E4WACHEADDR, "int", nmrcWacheE4.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E4 Wache: " + n);
                mrmy.WriteMemory(E4WACHEADDR, "int", n.ToString());
            }

            //MoneyE5
            if (chkMoneyE5.Checked)
            {
                mrmy.WriteMemory(E5MONEYADDR, "int", nmrcMoneyE5.Value.ToString());
            }
            else
            {
                int n = rng.Next(70, 991);
                Console.WriteLine("E5 money: " + n);
                mrmy.WriteMemory(E5MONEYADDR, "int", n.ToString());
            }
            //EinkaeuferE5
            if (chkEinkaeuferE5.Checked)
            {
                mrmy.WriteMemory(E5EINKAEUFERADDR, "int", nmrcEinkaeuferE5.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E5 Einkaeufer: " + n);
                mrmy.WriteMemory(E5EINKAEUFERADDR, "int", n.ToString());
            }
            //PolitikerE5
            if (chkPolitikerE5.Checked)
            {
                mrmy.WriteMemory(E5POLITIKERADDR, "int", nmrcPolitikerE5.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E5 Politiker: " + n);
                mrmy.WriteMemory(E5POLITIKERADDR, "int", n.ToString());
            }
            //GangsterE5
            if (chkGangsterE5.Checked)
            {
                mrmy.WriteMemory(E5GANGSTERADDR, "int", nmrcGangsterE5.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E5 Gangster: " + n);
                mrmy.WriteMemory(E5GANGSTERADDR, "int", n.ToString());
            }
            //VerkaeuferE5
            if (chkVerkaeuferE5.Checked)
            {
                mrmy.WriteMemory(E5VERKAEUFERADDR, "int", nmrcVerkaeuferE5.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E5 Verkaeufer: " + n);
                mrmy.WriteMemory(E5VERKAEUFERADDR, "int", n.ToString());
            }
            //FachkraftE5
            if (chkFachkraftE5.Checked)
            {
                mrmy.WriteMemory(E5FACHKRAFTADDR, "int", nmrcFachkraftE5.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E5 Fachkraft: " + n);
                mrmy.WriteMemory(E5FACHKRAFTADDR, "int", n.ToString());
            }
            //WacheE5
            if (chkWacheE5.Checked)
            {
                mrmy.WriteMemory(E5WACHEADDR, "int", nmrcWacheE5.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E5 Wache: " + n);
                mrmy.WriteMemory(E5WACHEADDR, "int", n.ToString());
            }
        }

        private void randomizePlayerStats()
        {
            //Money
            if(chkMoneySelf.Checked)
            {
                mrmy.WriteMemory(PLAYERMONEYADDR, "int", nmrcMoneySelf.Value.ToString());
            } else
            {
                int n = rng.Next(70, 991);
                Console.WriteLine("Playermoney: " + n);
                mrmy.WriteMemory(PLAYERMONEYADDR, "int", n.ToString());
            }
            //Einkaeufer
            if (chkEinkaeuferSelf.Checked)
            {
                mrmy.WriteMemory(PLAYEREINKAEUFERADDR, "int", nmrcEinkaeuferSelf.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("Player Einkaeufer: " + n);
                mrmy.WriteMemory(PLAYEREINKAEUFERADDR, "int", n.ToString());
            }
            //Politiker
            if (chkPolitikerSelf.Checked)
            {
                mrmy.WriteMemory(PLAYERPOLITIKERADDR, "int", nmrcPolitikerSelf.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("Player Politiker: " + n);
                mrmy.WriteMemory(PLAYERPOLITIKERADDR, "int", n.ToString());
            }
            //Gangster
            if (chkGangsterSelf.Checked)
            {
                mrmy.WriteMemory(PLAYERGANGSTERADDR, "int", nmrcGangsterSelf.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("Player Gangster: " + n);
                mrmy.WriteMemory(PLAYERGANGSTERADDR, "int", n.ToString());
            }
            //Verkaeufer
            if (chkVerkaeuferSelf.Checked)
            {
                mrmy.WriteMemory(PLAYERVERKAEUFERADDR, "int", nmrcVerkaeuferSelf.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("Player Verkaeufer: " + n);
                mrmy.WriteMemory(PLAYERVERKAEUFERADDR, "int", n.ToString());
            }
            //Fachkraft
            if (chkFachkraftSelf.Checked)
            {
                mrmy.WriteMemory(PLAYERFACHKRAFTADDR, "int", nmrcFachkraftSelf.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("Player Fachkraft: " + n);
                mrmy.WriteMemory(PLAYERFACHKRAFTADDR, "int", n.ToString());
            }
            //Wache
            if (chkWacheSelf.Checked)
            {
                mrmy.WriteMemory(PLAYERWACHEADDR, "int", nmrcWacheSelf.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("Player Wache: " + n);
                mrmy.WriteMemory(PLAYERWACHEADDR, "int", n.ToString());
            }
        }

        private void randomizeEnemyDesign()
        {
            //Eigentlich nur setzen, da Logo, Farbe, Name immer random sind
            int index = 0;
            //Gegner 1
            if (cmbIconE1.SelectedIndex != 0)
            {
                index = cmbIconE1.SelectedIndex - 1;
                mrmy.WriteMemory(E1LOGOADDR, "int", index.ToString());
            }
            if (cmbColorE1.SelectedIndex != 0)
            {
                index = cmbColorE1.SelectedIndex - 1;
                mrmy.WriteMemory(E1COLORADDR, "int", index.ToString());
            }
            //Gegner 2
            if (cmbIconE2.SelectedIndex != 0)
            {
                index = cmbIconE2.SelectedIndex - 1;
                mrmy.WriteMemory(E2LOGOADDR, "int", index.ToString());
            }
            if (cmbColorE2.SelectedIndex != 0)
            {
                index = cmbColorE2.SelectedIndex - 1;
                mrmy.WriteMemory(E2COLORADDR, "int", index.ToString());
            }
            //Gegner 3
            if (cmbIconE3.SelectedIndex != 0)
            {
                index = cmbIconE3.SelectedIndex - 1;
                mrmy.WriteMemory(E3LOGOADDR, "int", index.ToString());
            }
            if (cmbColorE3.SelectedIndex != 0)
            {
                index = cmbColorE3.SelectedIndex - 1;
                mrmy.WriteMemory(E3COLORADDR, "int", index.ToString());
            }
            //Gegner 4
            if (cmbIconE4.SelectedIndex != 0)
            {
                index = cmbIconE4.SelectedIndex - 1;
                mrmy.WriteMemory(E4LOGOADDR, "int", index.ToString());
            }
            if (cmbColorE4.SelectedIndex != 0)
            {
                index = cmbColorE4.SelectedIndex - 1;
                mrmy.WriteMemory(E4COLORADDR, "int", index.ToString());
            }
            //Gegner 5
            if (cmbIconE5.SelectedIndex != 0)
            {
                index = cmbIconE5.SelectedIndex - 1;
                mrmy.WriteMemory(E5LOGOADDR, "int", index.ToString());
            }
            if (cmbColorE5.SelectedIndex != 0)
            {
                index = cmbColorE5.SelectedIndex - 1;
                mrmy.WriteMemory(E5COLORADDR, "int", index.ToString());
            }
        }

        //TODO: Nicht 2 gleiche goals
        private void randomizeGoals()
        {
            int valueToWrite = 0;
            //Goal 1
            if (cmbGoal1.SelectedIndex != 0)
            {
                if (cmbGoal1.SelectedIndex == 1)
                {
                    mrmy.WriteMemory(GOAL1ADDR, "int", "-1");
                }
                else if (cmbGoal1.SelectedIndex == 2)
                {
                    mrmy.WriteMemory(GOAL1ADDR, "int", "0");
                }
                else
                {
                    valueToWrite = cmbGoal1.SelectedIndex - 1;
                    mrmy.WriteMemory(GOAL1ADDR, "int", valueToWrite.ToString());
                    fixGoals(cmbGoal1.SelectedIndex - 1);
                }
            }
            else
            {
                int n = rng.Next(2, 45);
                if(n == 44)
                {
                    n = 0;
                }
                Console.WriteLine("Goal1: " + n);
                mrmy.WriteMemory(GOAL1ADDR, "int", n.ToString());
                fixGoals(n);
            }
            //Goal 2
            if (cmbGoal2.SelectedIndex != 0)
            {
                if (cmbGoal2.SelectedIndex == 1)
                {
                    mrmy.WriteMemory(GOAL2ADDR, "int", "-1");
                }
                else if (cmbGoal2.SelectedIndex == 2)
                {
                    mrmy.WriteMemory(GOAL2ADDR, "int", "0");
                }
                else
                {
                    valueToWrite = cmbGoal2.SelectedIndex - 1;
                    mrmy.WriteMemory(GOAL2ADDR, "int", valueToWrite.ToString());
                    fixGoals(cmbGoal2.SelectedIndex - 1);
                }
            }
            else
            {
                int n = rng.Next(2, 46);
                if (n == 44)
                {
                    n = 0;
                } else if (n == 45)
                {
                    n = -1;
                }
                Console.WriteLine("Goal2: " + n);
                mrmy.WriteMemory(GOAL2ADDR, "int", n.ToString());
                fixGoals(n);
            }
            //Goal 3
            if (cmbGoal3.SelectedIndex != 0)
            {
                if (cmbGoal3.SelectedIndex == 1)
                {
                    mrmy.WriteMemory(GOAL3ADDR, "int", "-1");
                }
                else if (cmbGoal3.SelectedIndex == 2)
                {
                    mrmy.WriteMemory(GOAL3ADDR, "int", "0");
                }
                else
                {
                    valueToWrite = cmbGoal3.SelectedIndex - 1;
                    mrmy.WriteMemory(GOAL3ADDR, "int", valueToWrite.ToString());
                    fixGoals(cmbGoal3.SelectedIndex - 1);
                }
            }
            else
            {
                int n = rng.Next(2, 46);
                if (n == 44)
                {
                    n = 0;
                }
                else if (n == 45)
                {
                    n = -1;
                }
                Console.WriteLine("Goal3: " + n);
                mrmy.WriteMemory(GOAL3ADDR, "int", n.ToString());
                fixGoals(n);
            }
        }

        private void fixGoals(int chosenGoal)
        {
            fixHQ6(chosenGoal);
            fixHQ3(chosenGoal);
            fixGoal19(chosenGoal);
        }

        private void fixHQ6(int n)
        {
            if(n == 2 || n == 18 || n == 20 || n == 28 || n == 29)
            {
                setMaxHQ("6");
            }
        }

        private void fixHQ3(int n)
        {
            if (n == 14 && maxHQ < 3)
            {
                setMaxHQ("3");
            }
        }

        private void fixGoal19(int n)
        {
            if (n == 19 && enemyAmount == 1)
            {
                setEnemyAmount(2);
            }
        }

        private void randomizeDifficulty()
        {
            if(chkDifficulty.Checked == true)
            {
                mrmy.WriteMemory(DIFFICULTYADDR, "int", nmrcDifficulty.Value.ToString());
            } else
            {
                int n = rng.Next(0, 11);
                Console.WriteLine("Difficulty: " + n);
                mrmy.WriteMemory(DIFFICULTYADDR, "int", n.ToString());
            }
        }

        private void randomizeTimeLimit()
        {
            if(chkTimelimit.Checked)
            {
                mrmy.WriteMemory(TIMELIMITADDR, "int", nmrcTimelimit.Value.ToString());
            } else
            {
                int n = rng.Next(1, 12);
                Console.WriteLine("Timelimit (11=unlimited): " + n);
                mrmy.WriteMemory(TIMELIMITADDR, "int", n.ToString());
            }
        }

        private void randomizeCity()
        {
            if (cmbCity.SelectedIndex == 0)
            {
                int n = rng.Next(0, 10);
                mrmy.WriteMemory(CITYNUMBERADDR, "int", n.ToString());
                Console.WriteLine("City: " + n);
            } else
            {
                mrmy.WriteMemory(CITYNUMBERADDR, "int", (cmbCity.SelectedIndex-1).ToString());
            }
        }

        private void randomizePopulation()
        {
            if(!chkPopulation.Checked)
            {
                int n = rng.Next(100, 501);
                mrmy.WriteMemory(POPULATIONADDR, "int", n.ToString());
                Console.WriteLine("Population: " + n);
            }
        }

        private void randomizeEnemyAmount()
        {
            if(chkEnemyAmount.Checked)
            {
                setEnemyAmount((int)nmrcEnemyAmount.Value);
            } else
            {
                int n = rng.Next(0, 6);
                Console.WriteLine("Enemies: " + n);
                setEnemyAmount(n);
            }
        }

        private void randomizeCityStats()
        {
            //Miete
            if (chkRent.Checked)
            {
                string addressToWrite = getAddressFromPointer(CITYSTATSADDR, RENTOFFSETS);
                writeFloatToPointer(addressToWrite, (float)nmrcRent.Value);
            } else
            {
                int n = rng.Next(1, 21);
                float rent = n / 10f;
                Console.WriteLine("Miete: " + rent);
                string addressToWrite = getAddressFromPointer(CITYSTATSADDR, RENTOFFSETS);
                writeFloatToPointer(addressToWrite, rent);
            }
            //Geduld
            if (chkPatience.Checked)
            {
                string addressToWrite = getAddressFromPointer(CITYSTATSADDR, PATIENCEOFFSETS);
                writeFloatToPointer(addressToWrite, (float)nmrcPatience.Value);
            }
            else
            {
                int n = rng.Next(1, 21);
                float patience = n / 10f;
                Console.WriteLine("Geduld: " + patience);
                string addressToWrite = getAddressFromPointer(CITYSTATSADDR, PATIENCEOFFSETS);
                writeFloatToPointer(addressToWrite, patience);
            }
            //Kaufpreis
            if (chkBuyprice.Checked)
            {
                string addressToWrite = getAddressFromPointer(CITYSTATSADDR, BUYPRICEOFFSETS);
                writeFloatToPointer(addressToWrite, (float)nmrcBuyprice.Value);
            }
            else
            {
                int n = rng.Next(1, 21);
                float buyprice = n / 10f;
                Console.WriteLine("Kaufpreis: " + buyprice);
                string addressToWrite = getAddressFromPointer(CITYSTATSADDR, BUYPRICEOFFSETS);
                writeFloatToPointer(addressToWrite, buyprice);
            }
            //Einkommen
            if (chkIncome.Checked)
            {
                string addressToWrite = getAddressFromPointer(CITYSTATSADDR, INCOMEOFFSETS);
                writeFloatToPointer(addressToWrite, (float)nmrcIncome.Value);
            }
            else
            {
                int n = rng.Next(1, 21);
                float income = n / 10f;
                Console.WriteLine("Einkommen: " + income);
                string addressToWrite = getAddressFromPointer(CITYSTATSADDR, INCOMEOFFSETS);
                writeFloatToPointer(addressToWrite, income);
            }
            //Qualitaet
            if (chkQuality.Checked)
            {
                string addressToWrite = getAddressFromPointer(CITYSTATSADDR, QUALITYOFFSETS);
                writeFloatToPointer(addressToWrite, (float)nmrcQuality.Value);
            }
            else
            {
                int n = rng.Next(1, 21);
                float quality = n / 10f;
                Console.WriteLine("Qualitaet: " + quality);
                string addressToWrite = getAddressFromPointer(CITYSTATSADDR, QUALITYOFFSETS);
                writeFloatToPointer(addressToWrite, quality);
            }
            //Komfort
            if (chkComfort.Checked)
            {
                string addressToWrite = getAddressFromPointer(CITYSTATSADDR, COMFORTOFFSETS);
                writeFloatToPointer(addressToWrite, (float)nmrcComfort.Value);
            }
            else
            {
                int n = rng.Next(1, 21);
                float comfort = n / 10f;
                Console.WriteLine("Komfort: " + comfort);
                string addressToWrite = getAddressFromPointer(CITYSTATSADDR, COMFORTOFFSETS);
                writeFloatToPointer(addressToWrite, comfort);
            }
        }

        private void setEnemyAmount(int n)
        {
            if (n == 5)
            {
                mrmy.WriteMemory(ENEMY1ADDR, "int", "1");
                mrmy.WriteMemory(ENEMY2ADDR, "int", "1");
                mrmy.WriteMemory(ENEMY3ADDR, "int", "1");
                mrmy.WriteMemory(ENEMY4ADDR, "int", "1");
                mrmy.WriteMemory(ENEMY5ADDR, "int", "1");
            }
            else if (n == 4)
            {
                mrmy.WriteMemory(ENEMY1ADDR, "int", "1");
                mrmy.WriteMemory(ENEMY2ADDR, "int", "1");
                mrmy.WriteMemory(ENEMY3ADDR, "int", "1");
                mrmy.WriteMemory(ENEMY4ADDR, "int", "1");
                mrmy.WriteMemory(ENEMY5ADDR, "int", "0");
            }
            else if (n == 3)
            {
                mrmy.WriteMemory(ENEMY1ADDR, "int", "1");
                mrmy.WriteMemory(ENEMY2ADDR, "int", "1");
                mrmy.WriteMemory(ENEMY3ADDR, "int", "1");
                mrmy.WriteMemory(ENEMY4ADDR, "int", "0");
                mrmy.WriteMemory(ENEMY5ADDR, "int", "0");
            }
            else if (n == 2)
            {
                mrmy.WriteMemory(ENEMY1ADDR, "int", "1");
                mrmy.WriteMemory(ENEMY2ADDR, "int", "1");
                mrmy.WriteMemory(ENEMY3ADDR, "int", "0");
                mrmy.WriteMemory(ENEMY4ADDR, "int", "0");
                mrmy.WriteMemory(ENEMY5ADDR, "int", "0");
            }
            else if (n == 1)
            {
                mrmy.WriteMemory(ENEMY1ADDR, "int", "1");
                mrmy.WriteMemory(ENEMY2ADDR, "int", "0");
                mrmy.WriteMemory(ENEMY3ADDR, "int", "0");
                mrmy.WriteMemory(ENEMY4ADDR, "int", "0");
                mrmy.WriteMemory(ENEMY5ADDR, "int", "0");
            }
            else
            {
                mrmy.WriteMemory(ENEMY1ADDR, "int", "0");
                mrmy.WriteMemory(ENEMY2ADDR, "int", "0");
                mrmy.WriteMemory(ENEMY3ADDR, "int", "0");
                mrmy.WriteMemory(ENEMY4ADDR, "int", "0");
                mrmy.WriteMemory(ENEMY5ADDR, "int", "0");
                forceNoMissions = true;
            }
            enemyAmount = n;
        }

        private void randomizeBuildingsAndMission()
        {
            if(!forceNoMissions)
            {
                //Missionen Ja/NEin
                if (cmbMissions.SelectedIndex == 0)
                {
                    int n = rng.Next(0, 2);
                    Console.WriteLine("Missionen: " + n);
                    if (n == 1)
                    {
                        buildingsAndMissions += 65536;
                    }
                }
                else if (cmbMissions.SelectedIndex == 1)
                {
                    buildingsAndMissions += 65536;
                }
            }
            //Startfiliale Ja/NEin
            if(cmbStartFiliale.SelectedIndex == 0)
            {
                int n = rng.Next(0, 2);
                Console.WriteLine("Startfiliale: " + n);
                if (n == 1)
                {
                    buildingsAndMissions += 256;
                }
            }
            else if (cmbStartFiliale.SelectedIndex == 1)
            {
                buildingsAndMissions += 256;
            }
            //MaxHQ Ja/Nein
            if (!chkMaxHQ.Checked) {
                int n = rng.Next(0, 7);
                Console.WriteLine("Max HQ: " + n);
                if (n != 0)
                {
                    buildingsAndMissions += 1;
                    Console.WriteLine("HQ Limit?: " + n);
                }
                setMaxHQ(n.ToString());
            }
            else
            {
                buildingsAndMissions += 1;
                setMaxHQ(nmrcMaxHQ.Value.ToString());
            }
            mrmy.WriteMemory(MISSIONBUILDINGYNADDR, "int", buildingsAndMissions.ToString());
            //0, Missionen + 65536, Startfiliale + 256, maxHQ + 1
        }

        private void setMaxHQ(string newVal)
        {
            Console.WriteLine("Max HQ: " + newVal);
            maxHQ = int.Parse(newVal);
            mrmy.WriteMemory(MAXHQADDR, "int", newVal);
        }

        private void randomizeStartHQ()
        {
            int startHQ = 0;
            if (!chkStartHQ.Checked)
            {
                int n = rng.Next(0, (int)nmrcMaxHQ.Value + 1);
                if (n != 0)
                {
                    Console.WriteLine("StartHQ?: " + 1);
                    mrmy.WriteMemory(STARTHQYNADDR, "int", "1");
                } else
                {
                    Console.WriteLine("StartHQ?: " + 0);
                    mrmy.WriteMemory(STARTHQYNADDR, "int", "0");
                }
                if(startHQ > maxHQ)
                {
                    setMaxHQ(n.ToString());
                }
            }
            else
            {
                if (nmrcStartHQ.Value > 0)
                {
                    mrmy.WriteMemory(STARTHQYNADDR, "int", "1");
                }
                else
                {
                    mrmy.WriteMemory(STARTHQYNADDR, "int", "0");
                }
                mrmy.WriteMemory(STARTHQADDR, "int", nmrcStartHQ.Value.ToString());
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (creditProgress)
            {
                case 0:
                    creditProgress = (e.KeyCode == Keys.B) ? creditProgress + 1 : 0;
                    break;
                case 1:
                    creditProgress = (e.KeyCode == Keys.E) ? creditProgress + 1 : 0;
                    break;
                case 2:
                    creditProgress = (e.KeyCode == Keys.E) ? creditProgress + 1 : 0;
                    break;
                case 3:
                    creditProgress = (e.KeyCode == Keys.R) ? creditProgress + 1 : 0;
                    break;
                case 4:
                    creditProgress = (e.KeyCode == Keys.E) ? creditProgress + 1 : 0;
                    if(creditProgress == 5)
                    {
                        showLogo();
                        this.KeyPreview = false;
                    } else
                    {
                        creditProgress = 0;
                    }
                    break;
                default:
                    break;
            }
        }

        private void showLogo()
        {
            lblCredits.Text = "Created by:";
            picBoxLogo.Image = Properties.Resources.logo_notext;
        }

        private string addressToString(byte[] address)
        {
            if(address == null)
            {
                return "";
            }

            byte[] buffer = new byte[address.Length];

            int j = address.Length - 1;
            for (int i = 0; i < address.Length; i++)
            {
                buffer[i] = address[j];
                j--;
            }
            string retVal = BitConverter.ToString(buffer);

            return retVal.Replace("-", string.Empty);
        }

        private string addOffsetToAddress(string address, int offset)
        {
            if(address == "")
            {
                return "";
            }
            IntPtr pointer = new IntPtr(Convert.ToInt32(address, 16));
            pointer = IntPtr.Add(pointer, offset);

            int val = pointer.ToInt32();
            return address = val.ToString("x");
        }

        private string pointerToPointer(string address, string offset)
        {
            int decOffset = int.Parse(offset, System.Globalization.NumberStyles.HexNumber);

            address = addOffsetToAddress(address, decOffset);
            byte[] bytes = mrmy.ReadBytes(address, 4);
            return address = addressToString(bytes);
        }

        private float getFloatFromAddress(string address)
        {
            uint num = uint.Parse(address, System.Globalization.NumberStyles.AllowHexSpecifier);
            byte[] bytes = BitConverter.GetBytes(num);
            float floatVal = BitConverter.ToSingle(bytes, 0);
            return floatVal;
        }

        private float getFloatFromPointer(string address, string[] offsets)
        {
            byte[] bytes = mrmy.ReadBytes(address, 4);
            string newAddress = addressToString(bytes);

            foreach (string s in offsets)
            {
                newAddress = pointerToPointer(newAddress, s);
            }
            return getFloatFromAddress(newAddress);
        }

        private string getAddressFromPointer(string address, string[] offsets)
        {
            byte[] bytes = mrmy.ReadBytes(address, 4);
            string newAddress = addressToString(bytes);

            if(newAddress == "" || newAddress == "00000000")
            {
                return "";
            }

            for (int i = 0; i < offsets.Length; i++)
            {
                if (i == offsets.Length - 1)
                {
                    int decOffset = int.Parse(offsets[i], System.Globalization.NumberStyles.HexNumber);
                    newAddress = addOffsetToAddress(newAddress, decOffset);
                } else
                {
                    newAddress = pointerToPointer(newAddress, offsets[i]);
                }
            }
            return newAddress;
        }

        private void writeFloatToPointer(string address, float value)
        {
            mrmy.WriteMemory(address, "float", value.ToString());
        }

        private void tmrIngame_Tick(object sender, EventArgs e)
        {
            //Ganz am Anfang sichergehen, dass der Spieler keine randomStats eingestellt hat
            //Sonst kann das Game crashen
            setRandomStatsToZero();

            byte[] bytes = mrmy.ReadBytes(CITYSTATSADDR, 4);
            if(!isGameLoaded())
            {
                return;
            }
            randomizeCityStats();
            lblInfo.Text = "Game started. Good Luck have fun!";
            tmrIngame.Stop();
        }

        private void setRandomStatsToZero()
        {
            mrmy.WriteMemory(E1STATSADDR, "int", "0");
            mrmy.WriteMemory(E2STATSADDR, "int", "0");
            mrmy.WriteMemory(E3STATSADDR, "int", "0");
            mrmy.WriteMemory(E4STATSADDR, "int", "0");
            mrmy.WriteMemory(E5STATSADDR, "int", "0");
        }

        private bool isGameLoaded()
        {
            //Die Funktion liest den Pointer bis zu den Citystats und wirft bei Fehlern false
            string addressToWrite = getAddressFromPointer(CITYSTATSADDR, RENTOFFSETS);
            if(addressToWrite == "")
            {
                return false;
            }
            return true;
        }

        private void safeSettings(StreamWriter sw)
        {
            int i = 0;
            foreach (TabPage page in tabControl.TabPages)
            {
                foreach (Control currControl in page.Controls)
                {
                    if (currControl is GroupBox)
                    {
                        foreach (Control nestedControl in currControl.Controls)
                        {
                            i = safeControl(sw, nestedControl, i);
                        }
                    }
                    else
                    {
                        i = safeControl(sw, currControl, i);
                    }
                }
            }
            sw.Close();
        }

        private int safeControl(StreamWriter sw, Control currControl, int i)
        {
            if (currControl is ComboBox)
            {
                ComboBox foundBox = (ComboBox)currControl;
                sw.Write(foundBox.SelectedIndex + ";");
                //sw.Write(foundBox.SelectedIndex + ";");
                settings[i] = foundBox.SelectedIndex.ToString();
                i++;
            }
            else if (currControl is NumericUpDown)
            {
                NumericUpDown foundNmrc = (NumericUpDown)currControl;
                sw.Write(foundNmrc.Value + ";");
                //sw.Write(foundNmrc.Value + ";");
                settings[i] = foundNmrc.Value.ToString();
                i++;
            }
            else if (currControl is CheckBox)
            {
                CheckBox foundChkBox = (CheckBox)currControl;
                sw.Write(foundChkBox.Checked + ";");
                //sw.Write(foundChkBox.Checked + ";");
                settings[i] = foundChkBox.Checked.ToString();
                i++;
            }
            return i;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DialogResult saveresult = saveFileDialog.ShowDialog();
            if (saveresult == DialogResult.OK)
            {
                clearSettingsArray();
                string path = saveFileDialog.FileName;
                StreamWriter sw = new StreamWriter(path, false);
                try
                {
                    safeSettings(sw);
                    MessageBox.Show("Settings saved to:\n" + path,"Sucess!",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    sw.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error saving Settings: " + error.Message,"Save Settings",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    sw.Close();
                }
            }
        }

        private void loadSettings(StreamReader sr)
        {
            string wholeLine = sr.ReadLine();
            settings = wholeLine.Split(';');
            int i = 0;

            foreach (TabPage page in tabControl.TabPages)
            {
                foreach (Control currControl in page.Controls)
                {
                    if (currControl is GroupBox)
                    {
                        foreach (Control nestedControl in currControl.Controls)
                        {
                            i = fillControl(nestedControl, i);
                        }
                    } else
                    {
                        i = fillControl(currControl, i);
                    }
                }
            }
        }

        private int fillControl(Control currControl, int i)
        {
            if (currControl is ComboBox)
            {
                ComboBox foundBox = (ComboBox)currControl;
                foundBox.SelectedIndex = Convert.ToInt32(settings[i]);
                i = i + 1;
            }
            else if (currControl is NumericUpDown)
            {
                NumericUpDown foundNmrc = (NumericUpDown)currControl;
                foundNmrc.Value = Convert.ToDecimal(settings[i]);
                i = i + 1;
            }
            else if (currControl is CheckBox)
            {
                CheckBox foundChkBox = (CheckBox)currControl;
                foundChkBox.Checked = Convert.ToBoolean(settings[i]);
                i = i + 1;
            }
            return i;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DialogResult openresult = openFileDialog.ShowDialog();
            if (openresult == DialogResult.OK)
            {
                clearSettingsArray();
                string path = openFileDialog.FileName;
                StreamReader sr = new StreamReader(path);
                try
                {
                    loadSettings(sr);
                    sr.Close();
                    MessageBox.Show("Settings loaded!\n", "Sucess!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception error)
                {
                    sr.Close();
                    emptyAllSettings();
                    MessageBox.Show("Error loading Settings: " + error.Message, "Load Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void emptyAllSettings()
        {
            clearSettingsArray();
            int i = 0;

            foreach (TabPage page in tabControl.TabPages)
            {
                foreach (Control currControl in page.Controls)
                {
                    if (currControl is GroupBox)
                    {
                        foreach (Control nestedControl in currControl.Controls)
                        {
                            i = fillControl(nestedControl, i);
                        }
                    }
                    else
                    {
                        i = fillControl(currControl, i);
                    }
                }
            }
        }

        private void clearSettingsArray()
        {
            string[] emptySettings = emptySettingsString.Split(';');
            for (int i = 0; i < emptySettings.Length; i++)
            {
                settings[i] = emptySettings[i];
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
