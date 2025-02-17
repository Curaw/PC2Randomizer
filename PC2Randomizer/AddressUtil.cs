using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC2Randomizer
{
    internal class AddressUtil
    {
        public static string NAME_OF_EXE = "fastfood2.exe";    //Maybe pc2.exe for old CD-Version

        public static string POPULATION = NAME_OF_EXE + "+0x17BB28";
        public static string TIMELIMIT = NAME_OF_EXE + "+0x17BB2C";
        public static string CITY_NUMBER = NAME_OF_EXE + "+0x17BB40";
        public static string START_HQ_YN = NAME_OF_EXE + "+0x17BB34";
        public static string MISSION_BUILDING_YN = NAME_OF_EXE + "+0x17BB3C";
        public static string START_HQ = NAME_OF_EXE + "+0x17BB30";
        public static string MAX_HQ = NAME_OF_EXE + "+0x17BB38";
        public static string GOAL_1 = NAME_OF_EXE + "+0x17BB48";
        public static string GOAL_2 = NAME_OF_EXE + "+0x17BB4C";
        public static string GOAL_3 = NAME_OF_EXE + "+0x17BB50";
        public static string DIFFICULTY = NAME_OF_EXE + "+0x17BB24";
        public static string CITY_STATS = NAME_OF_EXE + "+0x17C670";
        public static readonly string[] RENT_OFFSETS = { "8", "0", "8C" };
        public static readonly string[] PATIENCE_OFFSETS = { "8", "0", "90" };
        public static readonly string[] BUYPRICE_OFFSETS = { "8", "0", "94" };
        public static readonly string[] INCOME_OFFSETS = { "8", "0", "98" };
        public static readonly string[] QUALITY_OFFSETS = { "8", "0", "9C" };
        public static readonly string[] COMFORT_OFFSETS = { "8", "0", "A0" };
        public static readonly string[] VOLK_OFFSETS = { "8", "0", "54" }; // TODO: Rename me

        //Player
        public static string PLAYER_MONEY = NAME_OF_EXE + "+0x17BCD8";
        public static string PLAYER_BUYER = NAME_OF_EXE + "+0x17BCE0";
        public static string PLAYER_GANGSTER = NAME_OF_EXE + "+0x17BCE4";
        public static string PLAYER_SPECIALIST = NAME_OF_EXE + "+0x17BCE8";
        public static string PLAYER_POLITICIAN = NAME_OF_EXE + "+0x17BCEC";
        public static string PLAYER_SELLER = NAME_OF_EXE + "+0x17BCF0";
        public static string PLAYER_GUARD = NAME_OF_EXE + "+0x17BCF4";

        //Enemy 1
        public static string ENEMY_1 = NAME_OF_EXE + "+0x17BDD4";
        public static string ENEMY_1_NAME = NAME_OF_EXE + "+0x17BD18";
        public static string ENEMY_1_COMPANY_NAME = NAME_OF_EXE + "+0x17BD58";
        public static string ENEMY_1_LOGO = NAME_OF_EXE + "+0x17BDCC";
        public static string ENEMY_1_COLOR = NAME_OF_EXE + "+0x17BDD0";
        public static string ENEMY_1_GENDER = NAME_OF_EXE + "+0x17BDB8";
        public static string ENEMY_1_HAIR = NAME_OF_EXE + "+0x17BDBC";
        public static string ENEMY_1_EYE = NAME_OF_EXE + "+0x17BDC0";
        public static string ENEMY_1_MOUTH = NAME_OF_EXE + "+0x17BDC4";
        public static string ENEMY_1_CHIN = NAME_OF_EXE + "+0x17BDC8";
        public static string ENEMY_1_MONEY = NAME_OF_EXE + "+0x17BD98";
        public static string ENEMY_1_BUYER = NAME_OF_EXE + "+0x17BDA0";
        public static string ENEMY_1_GANGSTER = NAME_OF_EXE + "+0x17BDA4";
        public static string ENEMY_1_SPECIALIST = NAME_OF_EXE + "+0x17BDA8";
        public static string ENEMY_1_POLITICIAN = NAME_OF_EXE + "+0x17BDAC";
        public static string ENEMY_1_SELLER = NAME_OF_EXE + "+0x17BDB0";
        public static string ENEMY_1_GUARD = NAME_OF_EXE + "+0x17BDB4";
        public static string ENEMY_1_STATS = NAME_OF_EXE + "+0x17BD9C";
        //Enemy 2
        public static string ENEMY_2 = NAME_OF_EXE + "+0x17BE94";
        public static string ENEMY_2_NAME = NAME_OF_EXE + "+0x17BDD8";
        public static string ENEMY_2_COMPANY_NAME = NAME_OF_EXE + "+0x17BE18";
        public static string ENEMY_2_LOGO = NAME_OF_EXE + "+0x17BE8C";
        public static string ENEMY_2_COLOR = NAME_OF_EXE + "+0x17BE90";
        public static string ENEMY_2_GENDER = NAME_OF_EXE + "+0x17BE78";
        public static string ENEMY_2_HAIR = NAME_OF_EXE + "+0x17BE7C";
        public static string ENEMY_2_EYE = NAME_OF_EXE + "+0x17BE80";
        public static string ENEMY_2_MOUTH = NAME_OF_EXE + "+0x17BE84";
        public static string ENEMY_2_CHIN = NAME_OF_EXE + "+0x17BE88";
        public static string ENEMY_2_MONEY = NAME_OF_EXE + "+0x17BE58";
        public static string ENEMY_2_BUYER = NAME_OF_EXE + "+0x17BE60";
        public static string ENEMY_2_GANGSTER = NAME_OF_EXE + "+0x17BE64";
        public static string ENEMY_2_SPECIALIST = NAME_OF_EXE + "+0x17BE68";
        public static string ENEMY_2_POLITICIAN = NAME_OF_EXE + "+0x17BE6C";
        public static string ENEMY_2_SELLER = NAME_OF_EXE + "+0x17BE70";
        public static string ENEMY_2_GUARD = NAME_OF_EXE + "+0x17BE74";
        public static string ENEMY_2_STATS = NAME_OF_EXE + "+0x17BE5C";
        //Enemy 3
        public static string ENEMY_3 = NAME_OF_EXE + "+0x17BF54";
        public static string ENEMY_3_NAME = NAME_OF_EXE + "+0x17BE98";
        public static string ENEMY_3_COMPANY_NAME = NAME_OF_EXE + "+0x17BED8";
        public static string ENEMY_3_LOGO = NAME_OF_EXE + "+0x17BF4C";
        public static string ENEMY_3_COLOR = NAME_OF_EXE + "+0x17BF50";
        public static string ENEMY_3_GENDER = NAME_OF_EXE + "+0x17BF38";
        public static string ENEMY_3_HAIR = NAME_OF_EXE + "+0x17BF3C";
        public static string ENEMY_3_EYE = NAME_OF_EXE + "+0x17BF40";
        public static string ENEMY_3_MOUTH = NAME_OF_EXE + "+0x17BF44";
        public static string ENEMY_3_CHIN = NAME_OF_EXE + "+0x17BF48";
        public static string ENEMY_3_MONEY = NAME_OF_EXE + "+0x17BF18";
        public static string ENEMY_3_BUYER = NAME_OF_EXE + "+0x17BF20";
        public static string ENEMY_3_GANGSTER = NAME_OF_EXE + "+0x17BF24";
        public static string ENEMY_3_SPECIALIST = NAME_OF_EXE + "+0x17BF28";
        public static string ENEMY_3_POLITICIAN = NAME_OF_EXE + "+0x17BF2C";
        public static string ENEMY_3_SELLER = NAME_OF_EXE + "+0x17BF30";
        public static string ENEMY_3_GUARD = NAME_OF_EXE + "+0x17BF34";
        public static string ENEMY_3_STATS = NAME_OF_EXE + "+0x17BF1C";
        //Enemy 4
        public static string ENEMY_4 = NAME_OF_EXE + "+0x17C014";
        public static string ENEMY_4_NAME = NAME_OF_EXE + "+0x17BF58";
        public static string ENEMY_4_COMPANY_NAME = NAME_OF_EXE + "+0x17BF98";
        public static string ENEMY_4_LOGO = NAME_OF_EXE + "+0x17C00C";
        public static string ENEMY_4_COLOR = NAME_OF_EXE + "+0x17C010";
        public static string ENEMY_4_GENDER = NAME_OF_EXE + "+0x17BF38";
        public static string ENEMY_4_HAIR = NAME_OF_EXE + "+0x17BF3C";
        public static string ENEMY_4_EYE = NAME_OF_EXE + "+0x17BF40";
        public static string ENEMY_4_MOUTH = NAME_OF_EXE + "+0x17BF44";
        public static string ENEMY_4_CHIN = NAME_OF_EXE + "+0x17BF48";
        public static string ENEMY_4_MONEY = NAME_OF_EXE + "+0x17BFD8";
        public static string ENEMY_4_BUYER = NAME_OF_EXE + "+0x17BFE0";
        public static string ENEMY_4_GANGSTER = NAME_OF_EXE + "+0x17BFE4";
        public static string ENEMY_4_SPECIALIST = NAME_OF_EXE + "+0x17BFE8";
        public static string ENEMY_4_POLITICIAN = NAME_OF_EXE + "+0x17BFEC";
        public static string ENEMY_4_SELLER = NAME_OF_EXE + "+0x17BFF0";
        public static string ENEMY_4_GUARD = NAME_OF_EXE + "+0x17BFF4";
        public static string ENEMY_4_STATS = NAME_OF_EXE + "+0x17BFDC";
        //Enemy 5
        public static string ENEMY_5 = NAME_OF_EXE + "+0x17C0D4";
        public static string ENEMY_5_NAME = NAME_OF_EXE + "+0x17C018";
        public static string ENEMY_5_COMPANY_NAME = NAME_OF_EXE + "+0x17C058";
        public static string ENEMY_5_LOGO = NAME_OF_EXE + "+0x17C0CC";
        public static string ENEMY_5_COLOR = NAME_OF_EXE + "+0x17C0D0";
        public static string ENEMY_5_GENDER = NAME_OF_EXE + "+0x17C0B8";
        public static string ENEMY_5_HAIR = NAME_OF_EXE + "+0x17C0BC";
        public static string ENEMY_5_EYE = NAME_OF_EXE + "+0x17C0C0";
        public static string ENEMY_5_MOUTH = NAME_OF_EXE + "+0x17C0C4";
        public static string ENEMY_5_CHIN = NAME_OF_EXE + "+0x17C0C8";
        public static string ENEMY_5_MONEY = NAME_OF_EXE + "+0x17C098";
        public static string ENEMY_5_BUYER = NAME_OF_EXE + "+0x17C0A0";
        public static string ENEMY_5_GANGSTER = NAME_OF_EXE + "+0x17C0A4";
        public static string ENEMY_5_SPECIALIST = NAME_OF_EXE + "+0x17C0A8";
        public static string ENEMY_5_POLITICIAN = NAME_OF_EXE + "+0x17C0AC";
        public static string ENEMY_5_SELLER = NAME_OF_EXE + "+0x17C0B0";
        public static string ENEMY_5_GUARD = NAME_OF_EXE + "+0x17C0B4";
        public static string ENEMY_5_STATS = NAME_OF_EXE + "+0x17C09C";
    }
}
