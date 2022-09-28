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

        private void memberOldVals()    //Member?
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
            PID = mrmy.GetProcIdFromName("fastfood2");  //TODO: fastfood2 auf pc2 vermutlich für die alte CD-Version
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
            int timeLimit = mrmy.ReadInt(AddressUtil.TIMELIMIT);
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
                mrmy.WriteMemory(AddressUtil.ENEMY_1_MONEY, "int", nmrcMoneyE1.Value.ToString());
            }
            else
            {
                int n = rng.Next(70, 991);
                Console.WriteLine("E1 money: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_1_MONEY, "int", n.ToString());
            }
            //EinkaeuferE1
            if (chkEinkaeuferE1.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_1_EINKAEUFER, "int", nmrcEinkaeuferE1.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E1 Einkaeufer: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_1_EINKAEUFER, "int", n.ToString());
            }
            //PolitikerE1
            if (chkPolitikerE1.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_1_POLITIKER, "int", nmrcPolitikerE1.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E1 Politiker: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_1_POLITIKER, "int", n.ToString());
            }
            //GangsterE1
            if (chkGangsterE1.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_1_GANGSTER, "int", nmrcGangsterE1.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E1 Gangster: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_1_GANGSTER, "int", n.ToString());
            }
            //VerkaeuferE1
            if (chkVerkaeuferE1.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_1_VERKAEUFER, "int", nmrcVerkaeuferE1.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E1 Verkaeufer: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_1_VERKAEUFER, "int", n.ToString());
            }
            //FachkraftE1
            if (chkFachkraftE1.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_1_FACHKRAFT, "int", nmrcFachkraftE1.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E1 Fachkraft: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_1_FACHKRAFT, "int", n.ToString());
            }
            //WacheE1
            if (chkWacheE1.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_1_WACHE, "int", nmrcWacheE1.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E1 Wache: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_1_WACHE, "int", n.ToString());
            }

            //MoneyE2
            if (chkMoneyE2.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_2_MONEY, "int", nmrcMoneyE2.Value.ToString());
            }
            else
            {
                int n = rng.Next(70, 991);
                Console.WriteLine("E2 money: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_2_MONEY, "int", n.ToString());
            }
            //EinkaeuferE2
            if (chkEinkaeuferE2.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_2_EINKAEUFER, "int", nmrcEinkaeuferE2.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E2 Einkaeufer: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_2_EINKAEUFER, "int", n.ToString());
            }
            //PolitikerE2
            if (chkPolitikerE2.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_2_POLITIKER, "int", nmrcPolitikerE2.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E2 Politiker: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_2_POLITIKER, "int", n.ToString());
            }
            //GangsterE2
            if (chkGangsterE2.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_2_GANGSTER, "int", nmrcGangsterE2.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E2 Gangster: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_2_GANGSTER, "int", n.ToString());
            }
            //VerkaeuferE2
            if (chkVerkaeuferE2.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_2_VERKAEUFER, "int", nmrcVerkaeuferE2.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E2 Verkaeufer: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_2_VERKAEUFER, "int", n.ToString());
            }
            //FachkraftE2
            if (chkFachkraftE2.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_2_FACHKRAFT, "int", nmrcFachkraftE2.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E2 Fachkraft: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_2_FACHKRAFT, "int", n.ToString());
            }
            //WacheE2
            if (chkWacheE2.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_2_WACHE, "int", nmrcWacheE2.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E2 Wache: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_2_WACHE, "int", n.ToString());
            }

            //MoneyE3
            if (chkMoneyE3.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_3_MONEY, "int", nmrcMoneyE3.Value.ToString());
            }
            else
            {
                int n = rng.Next(70, 991);
                Console.WriteLine("E3 money: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_3_MONEY, "int", n.ToString());
            }
            //EinkaeuferE3
            if (chkEinkaeuferE3.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_3_EINKAEUFER, "int", nmrcEinkaeuferE3.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E3 Einkaeufer: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_3_EINKAEUFER, "int", n.ToString());
            }
            //PolitikerE3
            if (chkPolitikerE3.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_3_POLITIKER, "int", nmrcPolitikerE3.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E3 Politiker: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_3_POLITIKER, "int", n.ToString());
            }
            //GangsterE3
            if (chkGangsterE3.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_3_GANGSTER, "int", nmrcGangsterE3.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E3 Gangster: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_3_GANGSTER, "int", n.ToString());
            }
            //VerkaeuferE3
            if (chkVerkaeuferE3.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_3_VERKAEUFER, "int", nmrcVerkaeuferE3.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E3 Verkaeufer: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_3_VERKAEUFER, "int", n.ToString());
            }
            //FachkraftE3
            if (chkFachkraftE3.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_3_FACHKRAFT, "int", nmrcFachkraftE3.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E3 Fachkraft: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_3_FACHKRAFT, "int", n.ToString());
            }
            //WacheE3
            if (chkWacheE3.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_3_WACHE, "int", nmrcWacheE3.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E3 Wache: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_3_WACHE, "int", n.ToString());
            }

            //MoneyE4
            if (chkMoneyE4.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_4_MONEY, "int", nmrcMoneyE4.Value.ToString());
            }
            else
            {
                int n = rng.Next(70, 991);
                Console.WriteLine("E4 money: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_4_MONEY, "int", n.ToString());
            }
            //EinkaeuferE4
            if (chkEinkaeuferE4.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_4_EINKAEUFER, "int", nmrcEinkaeuferE4.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E4 Einkaeufer: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_4_EINKAEUFER, "int", n.ToString());
            }
            //PolitikerE4
            if (chkPolitikerE4.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_4_POLITIKER, "int", nmrcPolitikerE4.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E4 Politiker: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_4_POLITIKER, "int", n.ToString());
            }
            //GangsterE4
            if (chkGangsterE4.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_4_GANGSTER, "int", nmrcGangsterE4.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E4 Gangster: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_4_GANGSTER, "int", n.ToString());
            }
            //VerkaeuferE4
            if (chkVerkaeuferE4.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_4_VERKAEUFER, "int", nmrcVerkaeuferE4.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E4 Verkaeufer: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_4_VERKAEUFER, "int", n.ToString());
            }
            //FachkraftE4
            if (chkFachkraftE4.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_4_FACHKRAFT, "int", nmrcFachkraftE4.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E4 Fachkraft: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_4_FACHKRAFT, "int", n.ToString());
            }
            //WacheE4
            if (chkWacheE4.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_4_WACHE, "int", nmrcWacheE4.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E4 Wache: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_4_WACHE, "int", n.ToString());
            }

            //MoneyE5
            if (chkMoneyE5.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_5_MONEY, "int", nmrcMoneyE5.Value.ToString());
            }
            else
            {
                int n = rng.Next(70, 991);
                Console.WriteLine("E5 money: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_5_MONEY, "int", n.ToString());
            }
            //EinkaeuferE5
            if (chkEinkaeuferE5.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_5_EINKAEUFER, "int", nmrcEinkaeuferE5.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E5 Einkaeufer: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_5_EINKAEUFER, "int", n.ToString());
            }
            //PolitikerE5
            if (chkPolitikerE5.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_5_POLITIKER, "int", nmrcPolitikerE5.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E5 Politiker: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_5_POLITIKER, "int", n.ToString());
            }
            //GangsterE5
            if (chkGangsterE5.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_5_GANGSTER, "int", nmrcGangsterE5.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E5 Gangster: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_5_GANGSTER, "int", n.ToString());
            }
            //VerkaeuferE5
            if (chkVerkaeuferE5.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_5_VERKAEUFER, "int", nmrcVerkaeuferE5.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E5 Verkaeufer: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_5_VERKAEUFER, "int", n.ToString());
            }
            //FachkraftE5
            if (chkFachkraftE5.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_5_FACHKRAFT, "int", nmrcFachkraftE5.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E5 Fachkraft: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_5_FACHKRAFT, "int", n.ToString());
            }
            //WacheE5
            if (chkWacheE5.Checked)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_5_WACHE, "int", nmrcWacheE5.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("E5 Wache: " + n);
                mrmy.WriteMemory(AddressUtil.ENEMY_5_WACHE, "int", n.ToString());
            }
        }

        private void randomizePlayerStats()
        {
            //Money
            if(chkMoneySelf.Checked)
            {
                mrmy.WriteMemory(AddressUtil.PLAYER_MONEY, "int", nmrcMoneySelf.Value.ToString());
            } else
            {
                int n = rng.Next(70, 991);
                Console.WriteLine("Playermoney: " + n);
                mrmy.WriteMemory(AddressUtil.PLAYER_MONEY, "int", n.ToString());
            }
            //Einkaeufer
            if (chkEinkaeuferSelf.Checked)
            {
                mrmy.WriteMemory(AddressUtil.PLAYER_EINKAEUFER, "int", nmrcEinkaeuferSelf.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("Player Einkaeufer: " + n);
                mrmy.WriteMemory(AddressUtil.PLAYER_EINKAEUFER, "int", n.ToString());
            }
            //Politiker
            if (chkPolitikerSelf.Checked)
            {
                mrmy.WriteMemory(AddressUtil.PLAYER_POLITIKER, "int", nmrcPolitikerSelf.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("Player Politiker: " + n);
                mrmy.WriteMemory(AddressUtil.PLAYER_POLITIKER, "int", n.ToString());
            }
            //Gangster
            if (chkGangsterSelf.Checked)
            {
                mrmy.WriteMemory(AddressUtil.PLAYER_GANGSTER, "int", nmrcGangsterSelf.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("Player Gangster: " + n);
                mrmy.WriteMemory(AddressUtil.PLAYER_GANGSTER, "int", n.ToString());
            }
            //Verkaeufer
            if (chkVerkaeuferSelf.Checked)
            {
                mrmy.WriteMemory(AddressUtil.PLAYER_VERKAEUFER, "int", nmrcVerkaeuferSelf.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("Player Verkaeufer: " + n);
                mrmy.WriteMemory(AddressUtil.PLAYER_VERKAEUFER, "int", n.ToString());
            }
            //Fachkraft
            if (chkFachkraftSelf.Checked)
            {
                mrmy.WriteMemory(AddressUtil.PLAYER_FACHKRAFT, "int", nmrcFachkraftSelf.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("Player Fachkraft: " + n);
                mrmy.WriteMemory(AddressUtil.PLAYER_FACHKRAFT, "int", n.ToString());
            }
            //Wache
            if (chkWacheSelf.Checked)
            {
                mrmy.WriteMemory(AddressUtil.PLAYER_WACHE, "int", nmrcWacheSelf.Value.ToString());
            }
            else
            {
                int n = rng.Next(0, 13) - 6;
                Console.WriteLine("Player Wache: " + n);
                mrmy.WriteMemory(AddressUtil.PLAYER_WACHE, "int", n.ToString());
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
                mrmy.WriteMemory(AddressUtil.ENEMY_1_LOGO, "int", index.ToString());
            }
            if (cmbColorE1.SelectedIndex != 0)
            {
                index = cmbColorE1.SelectedIndex - 1;
                mrmy.WriteMemory(AddressUtil.ENEMY_1_COLOR, "int", index.ToString());
            }
            //Gegner 2
            if (cmbIconE2.SelectedIndex != 0)
            {
                index = cmbIconE2.SelectedIndex - 1;
                mrmy.WriteMemory(AddressUtil.ENEMY_2_LOGO, "int", index.ToString());
            }
            if (cmbColorE2.SelectedIndex != 0)
            {
                index = cmbColorE2.SelectedIndex - 1;
                mrmy.WriteMemory(AddressUtil.ENEMY_2_COLOR, "int", index.ToString());
            }
            //Gegner 3
            if (cmbIconE3.SelectedIndex != 0)
            {
                index = cmbIconE3.SelectedIndex - 1;
                mrmy.WriteMemory(AddressUtil.ENEMY_3_LOGO, "int", index.ToString());
            }
            if (cmbColorE3.SelectedIndex != 0)
            {
                index = cmbColorE3.SelectedIndex - 1;
                mrmy.WriteMemory(AddressUtil.ENEMY_3_COLOR, "int", index.ToString());
            }
            //Gegner 4
            if (cmbIconE4.SelectedIndex != 0)
            {
                index = cmbIconE4.SelectedIndex - 1;
                mrmy.WriteMemory(AddressUtil.ENEMY_4_LOGO, "int", index.ToString());
            }
            if (cmbColorE4.SelectedIndex != 0)
            {
                index = cmbColorE4.SelectedIndex - 1;
                mrmy.WriteMemory(AddressUtil.ENEMY_4_COLOR, "int", index.ToString());
            }
            //Gegner 5
            if (cmbIconE5.SelectedIndex != 0)
            {
                index = cmbIconE5.SelectedIndex - 1;
                mrmy.WriteMemory(AddressUtil.ENEMY_5_LOGO, "int", index.ToString());
            }
            if (cmbColorE5.SelectedIndex != 0)
            {
                index = cmbColorE5.SelectedIndex - 1;
                mrmy.WriteMemory(AddressUtil.ENEMY_5_COLOR, "int", index.ToString());
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
                    mrmy.WriteMemory(AddressUtil.GOAL_1, "int", "-1");
                }
                else if (cmbGoal1.SelectedIndex == 2)
                {
                    mrmy.WriteMemory(AddressUtil.GOAL_1, "int", "0");
                }
                else
                {
                    valueToWrite = cmbGoal1.SelectedIndex - 1;
                    mrmy.WriteMemory(AddressUtil.GOAL_1, "int", valueToWrite.ToString());
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
                mrmy.WriteMemory(AddressUtil.GOAL_1, "int", n.ToString());
                fixGoals(n);
            }
            //Goal 2
            if (cmbGoal2.SelectedIndex != 0)
            {
                if (cmbGoal2.SelectedIndex == 1)
                {
                    mrmy.WriteMemory(AddressUtil.GOAL_2, "int", "-1");
                }
                else if (cmbGoal2.SelectedIndex == 2)
                {
                    mrmy.WriteMemory(AddressUtil.GOAL_2, "int", "0");
                }
                else
                {
                    valueToWrite = cmbGoal2.SelectedIndex - 1;
                    mrmy.WriteMemory(AddressUtil.GOAL_2, "int", valueToWrite.ToString());
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
                mrmy.WriteMemory(AddressUtil.GOAL_2, "int", n.ToString());
                fixGoals(n);
            }
            //Goal 3
            if (cmbGoal3.SelectedIndex != 0)
            {
                if (cmbGoal3.SelectedIndex == 1)
                {
                    mrmy.WriteMemory(AddressUtil.GOAL_3, "int", "-1");
                }
                else if (cmbGoal3.SelectedIndex == 2)
                {
                    mrmy.WriteMemory(AddressUtil.GOAL_3, "int", "0");
                }
                else
                {
                    valueToWrite = cmbGoal3.SelectedIndex - 1;
                    mrmy.WriteMemory(AddressUtil.GOAL_3, "int", valueToWrite.ToString());
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
                mrmy.WriteMemory(AddressUtil.GOAL_3, "int", n.ToString());
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
                mrmy.WriteMemory(AddressUtil.DIFFICULTY, "int", nmrcDifficulty.Value.ToString());
            } else
            {
                int n = rng.Next(0, 11);
                Console.WriteLine("Difficulty: " + n);
                mrmy.WriteMemory(AddressUtil.DIFFICULTY, "int", n.ToString());
            }
        }

        private void randomizeTimeLimit()
        {
            if(chkTimelimit.Checked)
            {
                mrmy.WriteMemory(AddressUtil.TIMELIMIT, "int", nmrcTimelimit.Value.ToString());
            } else
            {
                int n = rng.Next(1, 12);
                Console.WriteLine("Timelimit (11=unlimited): " + n);
                mrmy.WriteMemory(AddressUtil.TIMELIMIT, "int", n.ToString());
            }
        }

        private void randomizeCity()
        {
            if (cmbCity.SelectedIndex == 0)
            {
                int n = rng.Next(0, 10);
                mrmy.WriteMemory(AddressUtil.CITY_NUMBER, "int", n.ToString());
                Console.WriteLine("City: " + n);
            } else
            {
                mrmy.WriteMemory(AddressUtil.CITY_NUMBER, "int", (cmbCity.SelectedIndex-1).ToString());
            }
        }

        private void randomizePopulation()
        {
            if(!chkPopulation.Checked)
            {
                int n = rng.Next(100, 501);
                mrmy.WriteMemory(AddressUtil.POPULATION, "int", n.ToString());
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
                string addressToWrite = getAddressFromPointer(AddressUtil.CITY_STATS, AddressUtil.RENT_OFFSETS);
                writeFloatToPointer(addressToWrite, (float)nmrcRent.Value);
            } else
            {
                int n = rng.Next(1, 21);
                float rent = n / 10f;
                Console.WriteLine("Miete: " + rent);
                string addressToWrite = getAddressFromPointer(AddressUtil.CITY_STATS, AddressUtil.RENT_OFFSETS);
                writeFloatToPointer(addressToWrite, rent);
            }
            //Geduld
            if (chkPatience.Checked)
            {
                string addressToWrite = getAddressFromPointer(AddressUtil.CITY_STATS, AddressUtil.PATIENCE_OFFSETS);
                writeFloatToPointer(addressToWrite, (float)nmrcPatience.Value);
            }
            else
            {
                int n = rng.Next(1, 21);
                float patience = n / 10f;
                Console.WriteLine("Geduld: " + patience);
                string addressToWrite = getAddressFromPointer(AddressUtil.CITY_STATS, AddressUtil.PATIENCE_OFFSETS);
                writeFloatToPointer(addressToWrite, patience);
            }
            //Kaufpreis
            if (chkBuyprice.Checked)
            {
                string addressToWrite = getAddressFromPointer(AddressUtil.CITY_STATS, AddressUtil.BUYPRICE_OFFSETS);
                writeFloatToPointer(addressToWrite, (float)nmrcBuyprice.Value);
            }
            else
            {
                int n = rng.Next(1, 21);
                float buyprice = n / 10f;
                Console.WriteLine("Kaufpreis: " + buyprice);
                string addressToWrite = getAddressFromPointer(AddressUtil.CITY_STATS, AddressUtil.BUYPRICE_OFFSETS);
                writeFloatToPointer(addressToWrite, buyprice);
            }
            //Einkommen
            if (chkIncome.Checked)
            {
                string addressToWrite = getAddressFromPointer(AddressUtil.CITY_STATS, AddressUtil.INCOME_OFFSETS);
                writeFloatToPointer(addressToWrite, (float)nmrcIncome.Value);
            }
            else
            {
                int n = rng.Next(1, 21);
                float income = n / 10f;
                Console.WriteLine("Einkommen: " + income);
                string addressToWrite = getAddressFromPointer(AddressUtil.CITY_STATS, AddressUtil.INCOME_OFFSETS);
                writeFloatToPointer(addressToWrite, income);
            }
            //Qualitaet
            if (chkQuality.Checked)
            {
                string addressToWrite = getAddressFromPointer(AddressUtil.CITY_STATS, AddressUtil.QUALITY_OFFSETS);
                writeFloatToPointer(addressToWrite, (float)nmrcQuality.Value);
            }
            else
            {
                int n = rng.Next(1, 21);
                float quality = n / 10f;
                Console.WriteLine("Qualitaet: " + quality);
                string addressToWrite = getAddressFromPointer(AddressUtil.CITY_STATS, AddressUtil.QUALITY_OFFSETS);
                writeFloatToPointer(addressToWrite, quality);
            }
            //Komfort
            if (chkComfort.Checked)
            {
                string addressToWrite = getAddressFromPointer(AddressUtil.CITY_STATS, AddressUtil.COMFORT_OFFSETS);
                writeFloatToPointer(addressToWrite, (float)nmrcComfort.Value);
            }
            else
            {
                int n = rng.Next(1, 21);
                float comfort = n / 10f;
                Console.WriteLine("Komfort: " + comfort);
                string addressToWrite = getAddressFromPointer(AddressUtil.CITY_STATS, AddressUtil.COMFORT_OFFSETS);
                writeFloatToPointer(addressToWrite, comfort);
            }
        }

        private void setEnemyAmount(int n)
        {
            if (n == 5)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_1, "int", "1");
                mrmy.WriteMemory(AddressUtil.ENEMY_2, "int", "1");
                mrmy.WriteMemory(AddressUtil.ENEMY_3, "int", "1");
                mrmy.WriteMemory(AddressUtil.ENEMY_4, "int", "1");
                mrmy.WriteMemory(AddressUtil.ENEMY_5, "int", "1");
            }
            else if (n == 4)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_1, "int", "1");
                mrmy.WriteMemory(AddressUtil.ENEMY_2, "int", "1");
                mrmy.WriteMemory(AddressUtil.ENEMY_3, "int", "1");
                mrmy.WriteMemory(AddressUtil.ENEMY_4, "int", "1");
                mrmy.WriteMemory(AddressUtil.ENEMY_5, "int", "0");
            }
            else if (n == 3)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_1, "int", "1");
                mrmy.WriteMemory(AddressUtil.ENEMY_2, "int", "1");
                mrmy.WriteMemory(AddressUtil.ENEMY_3, "int", "1");
                mrmy.WriteMemory(AddressUtil.ENEMY_4, "int", "0");
                mrmy.WriteMemory(AddressUtil.ENEMY_5, "int", "0");
            }
            else if (n == 2)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_1, "int", "1");
                mrmy.WriteMemory(AddressUtil.ENEMY_2, "int", "1");
                mrmy.WriteMemory(AddressUtil.ENEMY_3, "int", "0");
                mrmy.WriteMemory(AddressUtil.ENEMY_4, "int", "0");
                mrmy.WriteMemory(AddressUtil.ENEMY_5, "int", "0");
            }
            else if (n == 1)
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_1, "int", "1");
                mrmy.WriteMemory(AddressUtil.ENEMY_2, "int", "0");
                mrmy.WriteMemory(AddressUtil.ENEMY_3, "int", "0");
                mrmy.WriteMemory(AddressUtil.ENEMY_4, "int", "0");
                mrmy.WriteMemory(AddressUtil.ENEMY_5, "int", "0");
            }
            else
            {
                mrmy.WriteMemory(AddressUtil.ENEMY_1, "int", "0");
                mrmy.WriteMemory(AddressUtil.ENEMY_2, "int", "0");
                mrmy.WriteMemory(AddressUtil.ENEMY_3, "int", "0");
                mrmy.WriteMemory(AddressUtil.ENEMY_4, "int", "0");
                mrmy.WriteMemory(AddressUtil.ENEMY_5, "int", "0");
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
            mrmy.WriteMemory(AddressUtil.MISSION_BUILDING_YN, "int", buildingsAndMissions.ToString());
            //0, Missionen + 65536, Startfiliale + 256, maxHQ + 1
        }

        private void setMaxHQ(string newVal)
        {
            Console.WriteLine("Max HQ: " + newVal);
            maxHQ = int.Parse(newVal);
            mrmy.WriteMemory(AddressUtil.MAX_HQ, "int", newVal);
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
                    mrmy.WriteMemory(AddressUtil.START_HQ_YN, "int", "1");
                } else
                {
                    Console.WriteLine("StartHQ?: " + 0);
                    mrmy.WriteMemory(AddressUtil.START_HQ_YN, "int", "0");
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
                    mrmy.WriteMemory(AddressUtil.START_HQ_YN, "int", "1");
                }
                else
                {
                    mrmy.WriteMemory(AddressUtil.START_HQ_YN, "int", "0");
                }
                mrmy.WriteMemory(AddressUtil.START_HQ, "int", nmrcStartHQ.Value.ToString());
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

            byte[] bytes = mrmy.ReadBytes(AddressUtil.CITY_STATS, 4);
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
            mrmy.WriteMemory(AddressUtil.ENEMY_1_STATS, "int", "0");
            mrmy.WriteMemory(AddressUtil.ENEMY_2_STATS, "int", "0");
            mrmy.WriteMemory(AddressUtil.ENEMY_3_STATS, "int", "0");
            mrmy.WriteMemory(AddressUtil.ENEMY_4_STATS, "int", "0");
            mrmy.WriteMemory(AddressUtil.ENEMY_5_STATS, "int", "0");
        }

        private bool isGameLoaded()
        {
            //Die Funktion liest den Pointer bis zu den Citystats und wirft bei Fehlern false
            string addressToWrite = getAddressFromPointer(AddressUtil.CITY_STATS, AddressUtil.RENT_OFFSETS);
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
