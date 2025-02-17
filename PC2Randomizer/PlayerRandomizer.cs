using Memory;
using System;
using System.Windows.Forms;

namespace PC2Randomizer
{
    internal class PlayerRandomizer
    {
        private const Int32 MIN_RANDOM_STAT_RANGE = -6;
        private const Int32 MAX_RANDOM_STAT_RANGE = 7; // Exclusive
        private const Int32 MIN_RANDOM_MONEY_RANGE = 70;
        private const Int32 MAX_RANDOM_MONEY_RANGE = 991; // Exclusive

        private Mem mrmy;
        private readonly Random rng;

        // Checkboxes
        private CheckBox moneyCheckBox;
        private CheckBox buyerCheckBox;
        private CheckBox politicianCheckBox;
        private CheckBox gangsterCheckBox;
        private CheckBox sellerCheckBox;
        private CheckBox specialistCheckBox;
        private CheckBox guardCheckBox;

        // NumericInputs
        private NumericUpDown moneyNumericUpDown;
        private NumericUpDown buyerNumericUpDown;
        private NumericUpDown politicanNumericUpDown;
        private NumericUpDown gangsterNumericUpDown;
        private NumericUpDown sellerNumericUpDown;
        private NumericUpDown specialistNumericUpDown;
        private NumericUpDown guardNumericUpDown;

        // Addresses
        private String moneyAddress;
        private String buyerAddress;
        private String politicianAddress;
        private String gangsterAddress;
        private String sellerAddress;
        private String specialistAddress;
        private String guardAddress;

        public PlayerRandomizer(Mem mrmy, Random rng)
        {
            this.mrmy = mrmy;
            this.rng = rng;
        }

        public void setCheckboxes(CheckBox money, CheckBox buyer, CheckBox politician,
            CheckBox gangster, CheckBox seller, CheckBox specialist, CheckBox guard)
        {
            this.moneyCheckBox = money;
            this.buyerCheckBox = buyer;
            this.politicianCheckBox = politician;
            this.gangsterCheckBox = gangster;
            this.sellerCheckBox = seller;
            this.specialistCheckBox = specialist;
            this.guardCheckBox = guard;
        }

        public void setNumerics(NumericUpDown money, NumericUpDown buyer, NumericUpDown politician, 
            NumericUpDown gangster, NumericUpDown seller, NumericUpDown specialist, NumericUpDown guard)
        {
            this.moneyNumericUpDown = money;
            this.buyerNumericUpDown = buyer;
            this.politicanNumericUpDown = politician;
            this.gangsterNumericUpDown = gangster;
            this.sellerNumericUpDown = seller;
            this.specialistNumericUpDown = specialist;
            this.guardNumericUpDown = guard;
        }

        public void setAddresses(String money, String buyer, String politician, 
            String gangster, String seller, String specialist, String guard)
        {
            this.moneyAddress = money;
            this.buyerAddress = buyer;
            this.politicianAddress = politician;
            this.gangsterAddress = gangster;
            this.sellerAddress = seller;
            this.specialistAddress = specialist;
            this.guardAddress = guard;
        }

        public void randomizeMoney()
        {
            if (moneyCheckBox.Checked)
            {
                mrmy.WriteMemory(moneyAddress, "2bytes", moneyNumericUpDown.Value.ToString()); // TODO: Money is broken. Fixme. Player handicap ingame is from 70 (46 00) to 990 (DE 03), so we add a zero the values our UI generated
            }
            else
            {
                int n = rng.Next(MIN_RANDOM_MONEY_RANGE, MAX_RANDOM_MONEY_RANGE);
                mrmy.WriteMemory(moneyAddress, "int", n.ToString());
            }
        }

        public void randomizeBuyer()
        {
            randomizeStat(buyerCheckBox, buyerNumericUpDown, buyerAddress);
        }

        public void randomizePolitician()
        {
            randomizeStat(politicianCheckBox, politicanNumericUpDown, politicianAddress);
        }

        public void randomizeGangster()
        {
            randomizeStat(gangsterCheckBox, gangsterNumericUpDown, gangsterAddress);
        }

        public void randomizeSeller()
        {
            randomizeStat(sellerCheckBox, sellerNumericUpDown, sellerAddress);
        }

        public void randomizeSpecialist()
        {
            randomizeStat(specialistCheckBox, specialistNumericUpDown, specialistAddress);
        }

        public void randomizeGuard()
        {
            randomizeStat(guardCheckBox, guardNumericUpDown, guardAddress);
        }

        private void randomizeStat(CheckBox isFix, NumericUpDown guiElement, String address)
        {
            if (isFix.Checked)
            {
                mrmy.WriteMemory(address, "int", guiElement.Value.ToString());
            }
            else
            {
                int n = rng.Next(MIN_RANDOM_STAT_RANGE, MAX_RANDOM_STAT_RANGE);
                mrmy.WriteMemory(address, "int", n.ToString());
            }
        }
    }
}
