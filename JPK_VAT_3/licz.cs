using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPK_VAT_3
{
    class licz
    {
        public static string k_10(DataTable dt, int i)  // wartość sprzedaży zwolnionej
        {
            if (decimal.Parse(dt.Rows[i][32].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() != "EXP" && dt.Rows[i][5].ToString().Trim() != "WDT" && dt.Rows[i][5].ToString().Trim() != "OOV" && dt.Rows[i][5].ToString().Trim() != "USLUE" && dt.Rows[i][5].ToString().Trim() != "USL")) //jeśli sprzedaż zwolniona
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][32].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_11(DataTable dt, int i)  // wartość USLUE oraz USL
        {
            if (decimal.Parse(dt.Rows[i][31].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() == "USL" || dt.Rows[i][5].ToString().Trim() == "USLUE")) //Usługi zagraniczne
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][31].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_12(DataTable dt, int i)  // wartość USLUE 
        {
            if (decimal.Parse(dt.Rows[i][31].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() == "USLUE")) //Usługi unijne
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][31].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_13(DataTable dt, int i)  // sp. krajowa - stawka 0
        {
            if (decimal.Parse(dt.Rows[i][31].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() != "EXP" && dt.Rows[i][5].ToString().Trim() != "WDT" && dt.Rows[i][5].ToString().Trim() != "OOV" && dt.Rows[i][5].ToString().Trim() != "USLUE" && dt.Rows[i][5].ToString().Trim() != "USL")) //jeśli sprzedaż zwolniona krajowa
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][31].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_15(DataTable dt, int i)  // wartość netto sprzedaży krajowej ze stawką 5%
        {
            if (decimal.Parse(dt.Rows[i][30].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() != "OOZ" && dt.Rows[i][5].ToString().Trim() != "IMP" && dt.Rows[i][5].ToString().Trim() != "IMPUE" && dt.Rows[i][5].ToString().Trim() != "A33" && dt.Rows[i][5].ToString().Trim() != "WNT")) //jeśli sp. nie tagowana
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][30].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_16(DataTable dt, int i)  // VAT od sprzedaży krajowej ze stawką 5%
        {
            if (decimal.Parse(dt.Rows[i][35].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() != "OOZ" && dt.Rows[i][5].ToString().Trim() != "IMP" && dt.Rows[i][5].ToString().Trim() != "IMPUE" && dt.Rows[i][5].ToString().Trim() != "A33" && dt.Rows[i][5].ToString().Trim() != "WNT")) //jeśli sp. nie tagowana
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][35].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_17(DataTable dt, int i)  // wartość netto sprzedaży krajowej ze stawką 8%
        {
            if (decimal.Parse(dt.Rows[i][29].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() != "OOZ" && dt.Rows[i][5].ToString().Trim() != "IMP" && dt.Rows[i][5].ToString().Trim() != "IMPUE" && dt.Rows[i][5].ToString().Trim() != "A33" && dt.Rows[i][5].ToString().Trim() != "WNT")) //jeśli sp. nie tagowana
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][29].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_18(DataTable dt, int i)  // VAT od sprzedaży krajowej ze stawką 8%
        {
            if (decimal.Parse(dt.Rows[i][34].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() != "OOZ" && dt.Rows[i][5].ToString().Trim() != "IMP" && dt.Rows[i][5].ToString().Trim() != "IMPUE" && dt.Rows[i][5].ToString().Trim() != "A33" && dt.Rows[i][5].ToString().Trim() != "WNT")) //jeśli sp. nie tagowana
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][34].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_19(DataTable dt, int i)  // wartość netto sprzedaży krajowej ze stawką 23%
        {
            if (decimal.Parse(dt.Rows[i][28].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() != "OOZ" && dt.Rows[i][5].ToString().Trim() != "IMP" && dt.Rows[i][5].ToString().Trim() != "IMPUE" && dt.Rows[i][5].ToString().Trim() != "A33" && dt.Rows[i][5].ToString().Trim() != "WNT")) //jeśli sp. nie tagowana
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][28].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_20(DataTable dt, int i)  // VAT od sprzedaży krajowej ze stawką 22%
        {
            if (decimal.Parse(dt.Rows[i][33].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() != "OOZ" && dt.Rows[i][5].ToString().Trim() != "IMP" && dt.Rows[i][5].ToString().Trim() != "IMPUE" && dt.Rows[i][5].ToString().Trim() != "A33" && dt.Rows[i][5].ToString().Trim() != "WNT")) //jeśli sprzedaż zwolniona
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][33].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_21(DataTable dt, int i)  // WDT
        {
            if (decimal.Parse(dt.Rows[i][31].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() == "WDT"))
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][31].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_22(DataTable dt, int i)  //EXP
        {
            if (decimal.Parse(dt.Rows[i][31].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() == "EXP"))
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][31].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_23(DataTable dt, int i)  //WNT
        {
            if (decimal.Parse(dt.Rows[i][6].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() == "WNT"))
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][6].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_24(DataTable dt, int i)  //WNT - VAT
        {
            if (decimal.Parse(dt.Rows[i][6].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() == "WNT"))
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][33].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][34].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][35].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_25(DataTable dt, int i)  //A33
        {
            if (decimal.Parse(dt.Rows[i][6].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() == "A33"))
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][6].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_26(DataTable dt, int i)  //A33 - VAT
        {
            if (decimal.Parse(dt.Rows[i][6].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() == "A33"))
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][33].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][34].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][35].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_27(DataTable dt, int i)  //IMP
        {
            if (decimal.Parse(dt.Rows[i][6].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() == "IMP"))
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][6].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_28(DataTable dt, int i)
        {
            if (decimal.Parse(dt.Rows[i][6].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() == "IMP")) //IMP - VAT
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][33].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][34].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][35].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_29(DataTable dt, int i)  //IMPUE
        {
            if (decimal.Parse(dt.Rows[i][6].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() == "IMPUE"))
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][6].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }



        public static string k_30(DataTable dt, int i)
        {
            if (decimal.Parse(dt.Rows[i][6].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() == "IMPUE")) //IMPUE - VAT
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][33].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][34].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][35].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_31(DataTable dt, int i)  //OOV
        {
            if (decimal.Parse(dt.Rows[i][31].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() == "OOV"))
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][31].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_34(DataTable dt, int i)  //OOZ
        {
            if (decimal.Parse(dt.Rows[i][6].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() == "OOZ"))
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][6].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_35(DataTable dt, int i)
        {
            if (decimal.Parse(dt.Rows[i][6].ToString()) != 0 && (dt.Rows[i][5].ToString().Trim() == "OOZ")) //OOZ - VAT
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][33].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][34].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][35].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_43(DataTable dt, int i)  // wartość netto zakupów  należących do środków trwałych
        {
            if (decimal.Parse(dt.Rows[i][17].ToString()) != 0) //jeśli zakup dotyczy wyposażenia (kswypos>0)
            {
                //return (decimal.Round(decimal.Parse(dt.Rows[i][28].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][29].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][30].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][31].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][32].ToString()), 2)).ToString().Replace(",", ".");
                return (decimal.Round(decimal.Parse(dt.Rows[i][17].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_44(DataTable dt, int i)
        {
            if (decimal.Parse(dt.Rows[i][17].ToString()) != 0) //jeśli zakup dotyczy wyposażenia (kswypos>0)
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][33].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][34].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][35].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }
        }

        public static string k_45(DataTable dt, int i)  // wartość netto zakupów pozostałych (nie należących do środków trwałych)
        {
            //if (decimal.Parse(dt.Rows[i][18].ToString()) > 0 && (decimal.Parse(dt.Rows[i][17].ToString()) == 0)) //
            if (decimal.Parse(dt.Rows[i][18].ToString()) != 0 && (decimal.Parse(dt.Rows[i][17].ToString()) == 0) || decimal.Parse(dt.Rows[i][9].ToString()) != 0 || decimal.Parse(dt.Rows[i][10].ToString()) != 0) //
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][28].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][29].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][30].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][31].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][32].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }

        }

        public static string k_46(DataTable dt, int i)
        {
            if (decimal.Parse(dt.Rows[i][18].ToString()) != 0 && (decimal.Parse(dt.Rows[i][17].ToString()) == 0) || decimal.Parse(dt.Rows[i][9].ToString()) != 0 || decimal.Parse(dt.Rows[i][10].ToString()) != 0) //
            {
                return (decimal.Round(decimal.Parse(dt.Rows[i][33].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][34].ToString()), 2) + decimal.Round(decimal.Parse(dt.Rows[i][35].ToString()), 2)).ToString().Replace(",", ".");
            }
            else
            {
                return "0";
            }

        }
    }
}
