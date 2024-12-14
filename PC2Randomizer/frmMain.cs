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


        bool forceNoMissions = false;   //TODO: is this needed?
        int creditProgress = 0;
        Mem mrmy = new Mem();
        int PID = 0;
        private readonly Random rng = new Random();
        bool readyToStartGame = false;
        decimal oldMoneyself;
        decimal oldPopulation;
        int buildingsAndMissions = 0;
        int maxHQ = 0;
        int enemyAmount = 0;
        string[] settings = new string[125];

        PlayerRandomizer playerRNG;
        PlayerRandomizer Enemy1RNG;
        PlayerRandomizer Enemy2RNG;
        PlayerRandomizer Enemy3RNG;
        PlayerRandomizer Enemy4RNG;
        PlayerRandomizer Enemy5RNG;

        CityRandomizer cityRNG;

        public frmMain()
        {
            InitializeComponent();
        }

        //After starting the application
        private void Form1_Load(object sender, EventArgs e)
        {
            memberOldVals();
            fillComboBoxes();
            createPlayerObjects();
            CityRandomizer cityRandomizer = new CityRandomizer(mrmy,rng);
        }

        private void memberOldVals()    //Member?
        {
            oldMoneyself = nmrcMoneySelf.Value;
            oldPopulation = nmrcPopulation.Value;
        }
        //This method defaults selection on every Combobox to index 0
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

        private void createPlayerObjects()
        {
            PlayerRandomizer playerRNG = new PlayerRandomizer(mrmy, rng);
            playerRNG.setCheckboxes(chkMoneySelf, chkEinkaeuferSelf, chkPolitikerSelf, chkGangsterSelf, chkVerkaeuferSelf, chkFachkraftSelf, chkWacheSelf);
            playerRNG.setNumerics(nmrcMoneySelf, nmrcEinkaeuferSelf, nmrcPolitikerSelf, nmrcGangsterSelf, nmrcVerkaeuferSelf, nmrcFachkraftSelf, nmrcWacheSelf);
            playerRNG.setAddresses(AddressUtil.PLAYER_MONEY, AddressUtil.PLAYER_BUYER, AddressUtil.PLAYER_POLITICIAN,
                AddressUtil.PLAYER_GANGSTER, AddressUtil.PLAYER_SELLER, AddressUtil.PLAYER_SPECIALIST, AddressUtil.PLAYER_GUARD);

            Enemy1RNG = new PlayerRandomizer(mrmy, rng);
            Enemy1RNG.setCheckboxes(chkMoneyE1, chkEinkaeuferE1, chkPolitikerE1, chkGangsterE1, chkVerkaeuferE1, chkFachkraftE1, chkWacheE1);
            Enemy1RNG.setNumerics(nmrcMoneyE1, nmrcEinkaeuferE1, nmrcPolitikerE1, nmrcGangsterE1, nmrcVerkaeuferE1, nmrcFachkraftE1, nmrcWacheE1);
            Enemy1RNG.setAddresses(AddressUtil.ENEMY_1_MONEY, AddressUtil.ENEMY_1_BUYER, AddressUtil.ENEMY_1_POLITICIAN,
                AddressUtil.ENEMY_1_GANGSTER, AddressUtil.ENEMY_1_SELLER, AddressUtil.ENEMY_1_SPECIALIST, AddressUtil.ENEMY_1_GUARD);

            Enemy2RNG = new PlayerRandomizer(mrmy, rng);
            Enemy2RNG.setCheckboxes(chkMoneyE2, chkEinkaeuferE2, chkPolitikerE2, chkGangsterE2, chkVerkaeuferE2, chkFachkraftE2, chkWacheE2);
            Enemy2RNG.setNumerics(nmrcMoneyE2, nmrcEinkaeuferE2, nmrcPolitikerE2, nmrcGangsterE2, nmrcVerkaeuferE2, nmrcFachkraftE2, nmrcWacheE2);
            Enemy2RNG.setAddresses(AddressUtil.ENEMY_2_MONEY, AddressUtil.ENEMY_2_BUYER, AddressUtil.ENEMY_2_POLITICIAN,
    AddressUtil.ENEMY_2_GANGSTER, AddressUtil.ENEMY_2_SELLER, AddressUtil.ENEMY_2_SPECIALIST, AddressUtil.ENEMY_2_GUARD);

            Enemy3RNG = new PlayerRandomizer(mrmy, rng);
            Enemy3RNG.setCheckboxes(chkMoneyE3, chkEinkaeuferE3, chkPolitikerE3, chkGangsterE3, chkVerkaeuferE3, chkFachkraftE3, chkWacheE3);
            Enemy3RNG.setNumerics(nmrcMoneyE3, nmrcEinkaeuferE3, nmrcPolitikerE3, nmrcGangsterE3, nmrcVerkaeuferE3, nmrcFachkraftE3, nmrcWacheE3);
            Enemy3RNG.setAddresses(AddressUtil.ENEMY_3_MONEY, AddressUtil.ENEMY_3_BUYER, AddressUtil.ENEMY_3_POLITICIAN,
    AddressUtil.ENEMY_3_GANGSTER, AddressUtil.ENEMY_3_SELLER, AddressUtil.ENEMY_3_SPECIALIST, AddressUtil.ENEMY_3_GUARD);

            Enemy4RNG = new PlayerRandomizer(mrmy, rng);
            Enemy4RNG.setCheckboxes(chkMoneyE4, chkEinkaeuferE4, chkPolitikerE4, chkGangsterE4, chkVerkaeuferE4, chkFachkraftE4, chkWacheE4);
            Enemy4RNG.setNumerics(nmrcMoneyE4, nmrcEinkaeuferE4, nmrcPolitikerE4, nmrcGangsterE4, nmrcVerkaeuferE4, nmrcFachkraftE4, nmrcWacheE4);
            Enemy4RNG.setAddresses(AddressUtil.ENEMY_4_MONEY, AddressUtil.ENEMY_4_BUYER, AddressUtil.ENEMY_4_POLITICIAN,
    AddressUtil.ENEMY_4_GANGSTER, AddressUtil.ENEMY_4_SELLER, AddressUtil.ENEMY_4_SPECIALIST, AddressUtil.ENEMY_4_GUARD);

            Enemy5RNG = new PlayerRandomizer(mrmy, rng);
            Enemy5RNG.setCheckboxes(chkMoneyE5, chkEinkaeuferE5, chkPolitikerE5, chkGangsterE5, chkVerkaeuferE5, chkFachkraftE5, chkWacheE5);
            Enemy5RNG.setNumerics(nmrcMoneyE5, nmrcEinkaeuferE5, nmrcPolitikerE5, nmrcGangsterE5, nmrcVerkaeuferE5, nmrcFachkraftE5, nmrcWacheE5);
            Enemy5RNG.setAddresses(AddressUtil.ENEMY_5_MONEY, AddressUtil.ENEMY_5_BUYER, AddressUtil.ENEMY_5_POLITICIAN,
    AddressUtil.ENEMY_5_GANGSTER, AddressUtil.ENEMY_5_SELLER, AddressUtil.ENEMY_5_SPECIALIST, AddressUtil.ENEMY_5_GUARD);
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

        //This hooks itself onto the process named "fastfood2" and starts our freegame-timer
        //see tmrFreegame_Tick for further information
        private void btnConnect_Click(object sender, EventArgs e)
        {
            PID = mrmy.GetProcIdFromName("fastfood2");  //TODO: fastfood2 would be pc2 for the old CD-Version
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

        //This method tries to read the chosen timelimit from the "freegame" menu on every timer tick.
        //If it succeeds, then the player is inside the "freegame" menu and we can start changing all values
        //If it did not succeed, then the player must still be in the main menu of the game
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
                    //See tmrIngame_tick for further information
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

        //TODO: Why is this here?
        private void resetValues()
        {
            buildingsAndMissions = 0;
        }

        //When the player is in the "freegame" menu, all settings are locked
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

        //As soon as the player comes back to the main menu, we unlock the settings
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
            randomizeAllEnemyStats();
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

        private void randomizeEnemy1Stats()
        {
            Enemy1RNG.randomizeMoney();
            Enemy1RNG.randomizeBuyer();
            Enemy1RNG.randomizePolitician();
            Enemy1RNG.randomizeGangster();
            Enemy1RNG.randomizeSeller();
            Enemy1RNG.randomizeSpecialist();
            Enemy1RNG.randomizeGuard();
        }

        private void randomizeEnemy2Stats()
        {
            Enemy2RNG.randomizeMoney();
            Enemy2RNG.randomizeBuyer();
            Enemy2RNG.randomizePolitician();
            Enemy2RNG.randomizeGangster();
            Enemy2RNG.randomizeSeller();
            Enemy2RNG.randomizeSpecialist();
            Enemy2RNG.randomizeGuard();
        }
        private void randomizeEnemy3Stats()
        {
            Enemy3RNG.randomizeMoney();
            Enemy3RNG.randomizeBuyer();
            Enemy3RNG.randomizePolitician();
            Enemy3RNG.randomizeGangster();
            Enemy3RNG.randomizeSeller();
            Enemy3RNG.randomizeSpecialist();
            Enemy3RNG.randomizeGuard();
        }
        private void randomizeEnemy4Stats()
        {
            Enemy4RNG.randomizeMoney();
            Enemy4RNG.randomizeBuyer();
            Enemy4RNG.randomizePolitician();
            Enemy4RNG.randomizeGangster();
            Enemy4RNG.randomizeSeller();
            Enemy4RNG.randomizeSpecialist();
            Enemy4RNG.randomizeGuard();
        }
        private void randomizeEnemy5Stats()
        {
            Enemy5RNG.randomizeMoney();
            Enemy5RNG.randomizeBuyer();
            Enemy5RNG.randomizePolitician();
            Enemy5RNG.randomizeGangster();
            Enemy5RNG.randomizeSeller();
            Enemy5RNG.randomizeSpecialist();
            Enemy5RNG.randomizeGuard();
        }

        private void randomizeAllEnemyStats()
        {
            randomizeEnemy1Stats();
            randomizeEnemy2Stats();
            randomizeEnemy3Stats();
            randomizeEnemy4Stats();
            randomizeEnemy5Stats();
        }

        private void randomizePlayerStats()
        {
            playerRNG.randomizeMoney();
            playerRNG.randomizeBuyer();
            playerRNG.randomizePolitician();
            playerRNG.randomizeGangster();
            playerRNG.randomizeSeller();
            playerRNG.randomizeSpecialist();
            playerRNG.randomizeGuard();
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

        //TODO: Do not have two goals of the same type
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

        //Certain goals need certain options enabled to work (e.g. have your hq on level 6 needs the maxHQ setting set to 6)
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
            mrmy.WriteMemory(AddressUtil.ENEMY_1, "int", n >= 1 ? "1" : "0");
            mrmy.WriteMemory(AddressUtil.ENEMY_2, "int", n >= 2 ? "1" : "0");
            mrmy.WriteMemory(AddressUtil.ENEMY_3, "int", n >= 3 ? "1" : "0");
            mrmy.WriteMemory(AddressUtil.ENEMY_4, "int", n >= 4 ? "1" : "0");
            mrmy.WriteMemory(AddressUtil.ENEMY_5, "int", n >= 5 ? "1" : "0");
            
            enemyAmount = n;
        }

        private void randomizeBuildingsAndMission()
        {
            //Missions yes/no
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

        //Easter egg
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

        //This method will be invoked pretty quickly after the player started the game
        //CityStats can only be changed after the game already started
        private void tmrIngame_Tick(object sender, EventArgs e)
        {
            setRandomStatsToZero();

            //As soon as we can read the CityStats we know the game started
            //Then we may change them
            byte[] bytes = mrmy.ReadBytes(AddressUtil.CITY_STATS, 4);
            if(!isGameLoaded())
            {
                return;
            }
            randomizeCityStats();
            lblInfo.Text = "Game started. Good Luck have fun!";
            tmrIngame.Stop();
        }

        //Having a stat over a certain amount (7, I think) crashes the game
        //so we have to set random stats to zero
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
            //This function reads the pointer all the way down to the Citystats and return false when an error occurs
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
