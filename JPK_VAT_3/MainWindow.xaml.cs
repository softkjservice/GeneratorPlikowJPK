using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace JPK_VAT_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<FirmyDane> listFirmyDane = new List<FirmyDane>();
        FormularzDane daneFormularza = new FormularzDane();
        public List<JpkCsvWiersz> listJpkCsvWiersz = new List<JpkCsvWiersz>();
        DataTable dt = new DataTable();
        public DataTable dt1 = new DataTable();
        public DataTable dt2 = new DataTable();
        public DataTable dt3 = new DataTable();
        public DataTable dt4 = new DataTable();
        //List<ksiegaDbf> listImport = new List<ksiegaDbf>();
        public int lastFirm = 0;
        public int iloscSprzedazy = 0;

        public MainWindow()
        {
            InitializeComponent();
            setup();
            firmy();
            dg_firmy.ItemsSource = listFirmyDane;
        }

        private void setup()
        {
            //DaneFormularza df = new DaneFormularza();
            PickerOd.Text = DateTime.Now.ToString();
            PickerDo.Text = DateTime.Now.ToString();
            PickerDataWytworzenia.Text = DateTime.Now.ToString();
            //string nazwaPliku = @"c:\c_projekty\daneformularza_csv.csv";
            string nazwaPliku = "formularzDane.csv";
            string textLine = string.Empty;
            string[] splitLine;
            if (File.Exists(nazwaPliku))
            {
                StreamReader sr = new StreamReader(nazwaPliku, Encoding.Default);
                textLine = sr.ReadLine();   // ominięcie wiersza nagłówków
                textLine = sr.ReadLine();
                splitLine = textLine.Split(';');
                daneFormularza.KodFormularza = splitLine[0];
                daneFormularza.kodSystemowy = splitLine[1];
                daneFormularza.wersjaSchemy = splitLine[2].Substring(3, 3);
                //daneFormularza.wersjaSchemy = "1-1";
                daneFormularza.WariantFormularza = splitLine[3];
                daneFormularza.CelZlozenia = splitLine[4];
                daneFormularza.NazwaSystemu = splitLine[5];
                textBox_df10.Text = daneFormularza.KodFormularza;
                textBox_df20.Text = daneFormularza.NazwaSystemu;
                textBox_df30.Text = daneFormularza.wersjaSchemy;
                textBox_df40.Text = daneFormularza.WariantFormularza;
            }
        }

        private void firmy()
        {
            if (File.Exists("firmyDane.csv"))
            {
                string textLine = string.Empty;
                string[] splitLine;
                int i = 1;
                try
                {
                    StreamReader sr = new StreamReader("firmyDane.csv", Encoding.Default);
                    textLine = sr.ReadLine();  // pominięcie nagłówków
                    while (!sr.EndOfStream)
                    {
                        textLine = sr.ReadLine();
                        splitLine = textLine.Split(';');
                        listFirmyDane.Add(new FirmyDane()
                        {
                            Zaznacz = false,
                            KrotkaNazwa = splitLine[1],
                            PelnaNazwa = splitLine[2],
                            NIP = splitLine[3],
                            Email = splitLine[4],
                            KatalogBazy = splitLine[5],
                            KatalogJpk = splitLine[6],
                            ZrodloDanych = splitLine[7]
                        }
                            );
                        i++;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd " + ex);
                    return;
                }

            }
            else    //pusta baza firm - wypełniam pierwszą 
            {
                listFirmyDane.Add(new FirmyDane()
                {
                    Zaznacz = false,
                    KrotkaNazwa = "",
                    PelnaNazwa = "",
                    NIP = "",
                    Email = "",
                    KatalogBazy = "",
                    KatalogJpk = "",
                    ZrodloDanych = ""
                });
            }
        }



        private void butAkceptujton_Click(object sender, RoutedEventArgs e)
        {
            int ileFirm = 0;
            string fir = " firmy";
            //MessageBox.Show("Ilość na liście "+listDaneFirmy.Count.ToString());
            for (int f = 0; f < listFirmyDane.Count; f++)
            {
                if (listFirmyDane[f].Zaznacz == true)
                {
                    listJpkCsvWiersz.Clear();
                    utworzCsvNaglowek(f);
                    utworzCsvPodmiot(f);
                    utworzCsvSprzedaz(f);
                    utworzCsvZakup(f);
                    zapiszCSV(f);
                    zapiszJPK(f);
                    //utworzJPK_fir(f);
                    //utworzJPK(f);
                    ileFirm = ileFirm + 1;
                }
            }
            if (ileFirm > 0)
            {
                if (ileFirm > 1)
                {
                    fir = " firm";
                }
                MessageBox.Show("Utworzono plik *.csv oraz plik *.xml dla " + ileFirm.ToString() + fir + "Ostatnia firma nr " + (lastFirm + 1).ToString());
            }
            else
            {
                MessageBox.Show("Nie wybrano żadnej firmy. Zaznacz i ponów próbę");
            }
        }

        private void butZaznacz_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < listFirmyDane.Count; i++)
            {
                listFirmyDane[i].Zaznacz = true;

            }

            dg_firmy.ItemsSource = null;
            dg_firmy.ItemsSource = listFirmyDane;
        }

        private void butWyczysc_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < listFirmyDane.Count; i++)
            {
                listFirmyDane[i].Zaznacz = false;

            }

            dg_firmy.ItemsSource = null;
            dg_firmy.ItemsSource = listFirmyDane;
        }

        private void butWidok_Click(object sender, RoutedEventArgs e)
        {
            var zob = new zobaczDane(dt1, dt2, dt3, dt4);
            zob.Show();
        }

        private void butJPK_Click(object sender, RoutedEventArgs e)
        {
            var zobJPK = new zobaczJPK(listJpkCsvWiersz);
            zobJPK.Show();
        }
/*
        private void butDodajF_Click(object sender, RoutedEventArgs e)
        {
            int wersja = 1;
            dodajXML(wersja);
        }

        private void butDodajS_Click(object sender, RoutedEventArgs e)
        {
            int wersja = 2;
            dodajXML(wersja);
        }
*/
        private void bt_ZapiszDoPliku_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt_firmy = common.ToDataTable(listFirmyDane); // zamiana listy na tablicę
            common.CreateCSVFile(dt_firmy, @"firmyDane.csv", true);  // utworzenie (modyfikacja) pliku
            MessageBox.Show("Dane wyeksportowanoa do pliku firmyDane.csv; Ilość pozycji = " + listFirmyDane.Count.ToString());
        }

        //*********************************

        private void utworzCsvNaglowek(int idFirmy)
        {
            lastFirm = idFirmy;
            listJpkCsvWiersz.Add(new JpkCsvWiersz()
            {
                KodFormularza = daneFormularza.KodFormularza,
                kodSystemowy = daneFormularza.kodSystemowy,
                NazwaSystemu = daneFormularza.NazwaSystemu,
                wersjaSchemy = daneFormularza.wersjaSchemy,
                WariantFormularza = daneFormularza.WariantFormularza,
                CelZlozenia = comboBox_cel.Text.ToString(),
                DataWytworzeniaJPK = PickerDataWytworzenia.Text.ToString() + DateTime.Now.TimeOfDay.ToString(),
                DataOd = PickerOd.Text.ToString(),
                DataDo = PickerDo.Text.ToString(),
            }
            );
        }

        private void utworzCsvPodmiot(int idFirmy)
        {
            listJpkCsvWiersz.Add(new JpkCsvWiersz()
            {
                NIP = listFirmyDane[idFirmy].NIP,
                PelnaNazwa = listFirmyDane[idFirmy].PelnaNazwa,
                Email = listFirmyDane[idFirmy].Email,
            }
            );
        }

        private void utworzCsvSprzedaz(int idFirmy)
        {
            string plikDbf = "";
            iloscSprzedazy = 0;
            decimal sumVatSp = 0;
            int Lp = 1;
            // Pobieranie danych sprzedażowych z ksiegi i rejestru
            for (int j = 1; j < 3; j++)
            {
                if (j == 1)
                {
                    plikDbf = "ksiega.dbf";
                }
                else
                {
                    plikDbf = "rejestr.dbf";
                }

                pobierzDane(idFirmy, plikDbf, true);
                //liczbaWierszy = liczbaWierszy + dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sumVatSp = sumVatSp + decimal.Parse(dt.Rows[i][33].ToString()) + decimal.Parse(dt.Rows[i][34].ToString()) + decimal.Parse(dt.Rows[i][35].ToString());
                    listJpkCsvWiersz.Add(new JpkCsvWiersz()
                    {
                        //typSprzedazy = "G",
                        typSprzedazy = "G",
                        LpSprzedazy = Lp.ToString(),
                        //NrKontrahenta = (100 + Lp).ToString(),
                        NrKontrahenta = dt.Rows[i][27].ToString().Replace("-", ""),
                        NazwaKontrahenta = dt.Rows[i][3].ToString().Replace("&", "-"),
                        AdresKontrahenta = dt.Rows[i][4].ToString(),
                        DowodSprzedazy = dt.Rows[i][2].ToString(),
                        DataWystawienia = dt.Rows[i][25].ToString(),
                        DataSprzedazy = dt.Rows[i][25].ToString(),
                        //K_10 = dt.Rows[i][32].ToString().Replace(",", "."),
                        K_10 = licz.k_10(dt, i),
                        K_11 = licz.k_11(dt, i),
                        K_12 = licz.k_12(dt, i),
                        //K_13 = dt.Rows[i][31].ToString().Replace(",", "."),
                        //K_13 = "0",  //wyjątkowo na polecenie KIM - wartość tu przechowywana dotyczy rubryki K_31
                        K_13 = licz.k_13(dt, i),
                        K_14 = "0",
                        K_15 = licz.k_15(dt, i),
                        K_16 = licz.k_16(dt, i),
                        K_17 = licz.k_17(dt, i),
                        K_18 = licz.k_18(dt, i),
                        K_19 = licz.k_19(dt, i),
                        K_20 = licz.k_20(dt, i),
                        K_21 = licz.k_21(dt, i),
                        K_22 = licz.k_22(dt, i),
                        //K_23 = "0",
                        //K_24 = "0",
                        //K_25 = "0",
                        //K_26 = "0",
                        K_23 = licz.k_23(dt, i),
                        K_24 = licz.k_24(dt, i),
                        K_25 = licz.k_25(dt, i),
                        K_26 = licz.k_26(dt, i),
                        K_27 = licz.k_27(dt, i),
                        K_28 = licz.k_28(dt, i),
                        K_29 = licz.k_29(dt, i),
                        K_30 = licz.k_30(dt, i),
                        K_31 = licz.k_31(dt, i),
                        //K_31 = dt.Rows[i][31].ToString().Replace(",", "."),
                        K_32 = "0",
                        K_33 = "0",
                        K_34 = licz.k_34(dt, i),
                        K_35 = licz.k_35(dt, i),
                        K_36 = "0",
                        K_37 = "0",
                        K_38 = "0",
                        K_39 = "0",

                    }
                    );
                    Lp = Lp + 1;
                }

            }


            iloscSprzedazy = listJpkCsvWiersz.Count - 2;

            listJpkCsvWiersz.Add(new JpkCsvWiersz()
            {
                LiczbaWierszySprzedazy = iloscSprzedazy.ToString(),
                PodatekNalezny = sumVatSp.ToString().Replace(",", "."),
            }
            );
        }


        private void utworzCsvZakup(int idFirmy)
        {
            string plikDbf = "";
            //int liczbaWierszy = 0;
            decimal sumVatSp = 0;
            int Lp = 1;
            for (int j = 1; j < 3; j++)
            {
                if (j == 1)
                {
                    plikDbf = "ksiega.dbf";
                }
                else
                {
                    plikDbf = "rejestr.dbf";
                }

                pobierzDane(idFirmy, plikDbf, false);
                //liczbaWierszy = liczbaWierszy + dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sumVatSp = sumVatSp + decimal.Parse(dt.Rows[i][33].ToString()) + decimal.Parse(dt.Rows[i][34].ToString()) + decimal.Parse(dt.Rows[i][35].ToString());
                    listJpkCsvWiersz.Add(new JpkCsvWiersz()
                    {
                        //typZakupu = "G",
                        typZakupu = "G",
                        LpZakupu = Lp.ToString(),
                        NrDostawcy = dt.Rows[i][27].ToString().Replace("-", ""),
                        NazwaDostawcy = dt.Rows[i][3].ToString().Replace("&", "-"),
                        AdresDostawcy = dt.Rows[i][4].ToString(),
                        DowodZakupu = dt.Rows[i][2].ToString(),
                        DataZakupu = dt.Rows[i][25].ToString(),
                        DataWplywu = dt.Rows[i][25].ToString(),
                        //K_45 = (decimal.Parse(dt.Rows[i][28].ToString()) + decimal.Parse(dt.Rows[i][29].ToString())+ decimal.Parse(dt.Rows[i][30].ToString())+ decimal.Parse(dt.Rows[i][31].ToString())+ decimal.Parse(dt.Rows[i][32].ToString())).ToString(),
                        K_42 = "0",
                        K_43 = licz.k_43(dt, i),
                        K_44 = licz.k_44(dt, i),
                        K_45 = licz.k_45(dt, i),
                        K_46 = licz.k_46(dt, i),
                        K_47 = "0",
                        K_48 = "0",
                        K_49 = "0",
                        K_50 = "0",
                    }
                    );
                    Lp = Lp + 1;

                }

            }

            int iloscZakupu = listJpkCsvWiersz.Count - (iloscSprzedazy + 3);
            listJpkCsvWiersz.Add(new JpkCsvWiersz()
            {

                LiczbaWierszyZakupu = iloscZakupu.ToString(),
                PodatekNaliczony = sumVatSp.ToString().Replace(",", "."),
                //PodatekNaliczony = Math.Round(sumVatSp,0).ToString(),
            }
            );

        }


        private void pobierzDane(int numeFirmy, string plik, bool sprzedaz)
        {
            string baza = listFirmyDane[numeFirmy].KatalogBazy + common.Katalog(PickerOd.Text.ToString()) + "\\";
            string str_conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + baza + ";Extended Properties=DBASE IV;";
            OleDbConnection conn = new OleDbConnection(str_conn);
            conn.Open();
            dt.Clear();
            string sqlStr = "";
            if (sprzedaz == true)
            {
                sqlStr = "Select * from " + plik + " where kssprzed=true AND (NOT TRIM(ksnic)='-R' OR ksnic IS NULL)";
            }
            else
            {
                //sqlStr = "Select * from " + plik + " where kssprzed=false";
                sqlStr = "Select * from " + plik + " where kssprzed=false AND (NOT TRIM(ksnic)='-R' OR ksnic IS NULL)";
            }

            DataSet ds = new DataSet();
            //Using the OleDbDataAdapter execute the query
            OleDbDataAdapter myAdapter = new OleDbDataAdapter(sqlStr, conn);
            myAdapter.Fill(dt);
            if (sprzedaz == true && plik == "ksiega.dbf")
            {
                dt1.Clear();
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    dt1.Rows.Add();
                    if (a == 0)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            dt1.Columns.Add();
                        }
                    }
                    for (int b = 0; b < dt.Columns.Count; b++)
                    {
                        dt1.Rows[a][b] = dt.Rows[a][b];
                    }
                }
                //dgKsiega.DataContext = dt1;
            }

            if (sprzedaz == true && plik == "rejestr.dbf")
            {
                dt2.Clear();
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    dt2.Rows.Add();
                    if (a == 0)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            dt2.Columns.Add();
                        }
                    }
                    for (int b = 0; b < dt.Columns.Count; b++)
                    {
                        dt2.Rows[a][b] = dt.Rows[a][b];
                    }
                }
                //dgKsiega.DataContext = dt2;
            }

            if (sprzedaz == false && plik == "ksiega.dbf")
            {
                dt3.Clear();
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    dt3.Rows.Add();
                    if (a == 0)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            dt3.Columns.Add();
                        }
                    }
                    for (int b = 0; b < dt.Columns.Count; b++)
                    {
                        dt3.Rows[a][b] = dt.Rows[a][b];
                    }
                }
                //dgKsiega.DataContext = dt3;
            }

            if (sprzedaz == false && plik == "rejestr.dbf")
            {
                dt4.Clear();
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    dt4.Rows.Add();
                    if (a == 0)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            dt4.Columns.Add();
                        }
                    }
                    for (int b = 0; b < dt.Columns.Count; b++)
                    {
                        dt4.Rows[a][b] = dt.Rows[a][b];
                    }
                }
            }


            conn.Close();

        }

        private void zapiszCSV(int numerRirmy)
        {
            string plik = listFirmyDane[numerRirmy].KatalogJpk + "\\" + listFirmyDane[numerRirmy].KrotkaNazwa + "_" + DateTime.Parse(PickerOd.Text.ToString()).Month.ToString() + "_" + DateTime.Parse(PickerOd.Text.ToString()).Year.ToString() + ".csv";
            DataTable dtJPK = common.ToDataTable(listJpkCsvWiersz);
            common.CreateCSVFile(dtJPK, plik, true);
        }

        private void zapiszJPK(int numerRirmy)
        {
            string plik = listFirmyDane[numerRirmy].KatalogJpk + "\\" + listFirmyDane[numerRirmy].KrotkaNazwa + "_" + DateTime.Parse(PickerOd.Text.ToString()).Month.ToString() + "_" + DateTime.Parse(PickerOd.Text.ToString()).Year.ToString() + ".xml";
            common.CreateJPKFile(listJpkCsvWiersz, plik, true);
        }


        private void dodajXML(int wersja)    //  Moduł nie wykorzystywany
        {
            string TagSprzedazWiersz = "tns:SprzedazWiersz";
            string TagLpSprzedazy = "tns:LpSprzedazy";
            string TagNrKontrahenta = "tns:NrKontrahenta";
            string TagNazwaKontrahenta = "tns:NazwaKontrahenta";
            string TagAdresKontrahenta = "tns:AdresKontrahenta";
            string TagDowodSprzedazy = "tns:DowodSprzedazy";
            string TagDataWystawienia = "tns:DataWystawienia";
            string TagK_10 = "tns:K_10";
            string TagK_13 = "tns:K_13";
            string TagK_15 = "tns:K_15";
            string TagK_16 = "tns:K_16";
            string TagK_17 = "tns:K_17";
            string TagK_18 = "tns:K_18";
            string TagK_19 = "tns:K_19";
            string TagK_20 = "tns:K_20";
            if (wersja == 1)
            {
                TagSprzedazWiersz = "Faktura";
                //LpSprzedazy = "LpSprzedazy";
                TagNrKontrahenta = "P_5B";
                TagNazwaKontrahenta = "P_3A";
                TagAdresKontrahenta = "P_3B";
                TagDowodSprzedazy = "P_2A";
                TagDataWystawienia = "P_1";
                TagK_10 = "P_13_7";
                TagK_13 = "P_13_6";
                TagK_15 = "P_13_3";
                TagK_16 = "P_14_3";
                TagK_17 = "P_13_2";
                TagK_18 = "P_14_2";
                TagK_19 = "P_13_1";
                TagK_20 = "P_14_1";
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            Nullable<bool> result = ofd.ShowDialog();
            if (result == true)
            {
                string nazwa_pliku = ofd.FileName;
                XmlDocument doc = new XmlDocument();
                doc.Load(nazwa_pliku);
                
                /*
                for (int k=0; k<10; k++)
                {
                    MessageBox.Show(k.ToString() +"  "+ doc.GetElementsByTagName("tns:DowodSprzedazy").Item(k).InnerText.ToString()+ "   " + doc.GetElementsByTagName("tns:K_19").Item(k).InnerText.ToString() + "   " + doc.GetElementsByTagName("tns:K_23").Item(k).InnerText.ToString());
                    //MessageBox.Show(k.ToString() + "  " + doc.GetElementsByTagName("tns:DowodSprzedazy").Item(k).InnerText.ToString() + "   " + doc.GetElementsByTagName("tns:K_23").Item(k).InnerText.ToString());
                }
                return;
                */

                int ileFaktur = doc.GetElementsByTagName(TagSprzedazWiersz).Count;
                MessageBox.Show(ileFaktur.ToString());
                int ileDokSp = common.ileDokSprzedazy(listJpkCsvWiersz);
                decimal pNalezny = common.podatekNalezny(listJpkCsvWiersz);
                MessageBox.Show(ileDokSp.ToString() + "  " + pNalezny.ToString());
                for (int i = 0; i < ileFaktur; i++)
                //for (int i = 0; i < 10; i++)
                {
                    //int? k = null;
                    //string k10 = n ull;
                    string k10 = "";
                    string k13 = "";
                    string k15 = "";
                    string k17 = "";
                    string k19 = "";
                    string k16 = "";
                    string k18 = "";
                    string k20 = "";


                    //k10 = doc.GetElementsByTagName(TagK_10).Item(i).InnerText.ToString() ?? "0";
                    try
                    {
                        k10 = doc.GetElementsByTagName(TagK_10).Item(i).InnerText.ToString();
                    }
                    catch
                    {
                        k10 = "0";
                    }
                    try
                    {
                        k13 = doc.GetElementsByTagName(TagK_13).Item(i).InnerText.ToString();
                    }
                    catch
                    {
                        k13 = "0";
                    }
                    try
                    {
                        k15 = doc.GetElementsByTagName(TagK_15).Item(i).InnerText.ToString();
                    }
                    catch
                    {
                        k15 = "0";
                    }
                    try
                    {
                        k17 = doc.GetElementsByTagName(TagK_17).Item(i).InnerText.ToString();
                    }
                    catch
                    {
                        k17 = "0";
                    }
                    try
                    {
                        k19 = doc.GetElementsByTagName(TagK_19).Item(i).InnerText.ToString();
                    }
                    catch
                    {
                        k19 = "0";
                    }
                    try
                    {
                        k16 = doc.GetElementsByTagName(TagK_16).Item(i).InnerText.ToString();
                    }
                    catch
                    {
                        k16 = "0";
                    }
                    try
                    {
                        k18 = doc.GetElementsByTagName(TagK_18).Item(i).InnerText.ToString();
                    }
                    catch
                    {
                        k18 = "0";
                    }
                    try
                    {
                        k20 = doc.GetElementsByTagName(TagK_20).Item(i).InnerText.ToString();
                    }
                    catch
                    {
                        k20 = "0";
                    }

                    //MessageBox.Show(i.ToString()+ "   "+k19 +"  "+k20);


                    listJpkCsvWiersz.Insert(ileDokSp + 2 + i, new JpkCsvWiersz()
                    {
                        typSprzedazy = "G",
                        LpSprzedazy = (ileDokSp + 1 + i).ToString(),
                        NrKontrahenta = doc.GetElementsByTagName(TagNrKontrahenta).Item(i).InnerText.ToString().Replace("&", "-"),
                        NazwaKontrahenta = doc.GetElementsByTagName(TagNazwaKontrahenta).Item(i).InnerText.ToString().Replace("&", "-"),
                        AdresKontrahenta = doc.GetElementsByTagName(TagAdresKontrahenta).Item(i).InnerText.ToString().Replace("&", "-"),
                        DowodSprzedazy = doc.GetElementsByTagName(TagDowodSprzedazy).Item(i).InnerText.ToString().Replace("&", "-"),
                        DataWystawienia = doc.GetElementsByTagName(TagDataWystawienia).Item(i).InnerText,
                        K_10 = k10,
                        K_11 = "0",
                        //K_11 = i.ToString(),
                        K_12 = "0",
                        K_13 = k13,
                        K_14 = "0",
                        K_15 = k15,
                        K_16 = k16,
                        K_17 = k17,
                        K_18 = k18,
                        K_19 = k19,
                        K_20 = k20,
                        K_21 = "0",
                        K_22 = "0",
                        K_23 = "0",
                        K_24 = "0",
                        K_25 = "0",
                        K_26 = "0",
                        K_27 = "0",
                        K_28 = "0",
                        K_29 = "0",
                        K_30 = "0",
                        K_31 = "0",
                        K_32 = "0",
                        K_33 = "0",
                        K_34 = "0",
                        K_35 = "0",
                        K_36 = "0",
                        K_37 = "0",
                        K_38 = "0",
                        K_39 = "0",
                        //NrKontrahenta = "12345",
                        //NazwaKontrahenta = "Soft-KJ-Service"
                    });
                }
                ileDokSp = common.ileDokSprzedazy(listJpkCsvWiersz);
                pNalezny = common.podatekNalezny(listJpkCsvWiersz);
                listJpkCsvWiersz[ileDokSp + 2].LiczbaWierszySprzedazy = ileDokSp.ToString().Replace(",", ".");
                listJpkCsvWiersz[ileDokSp + 2].PodatekNalezny = pNalezny.ToString().Replace(",", ".");
                zapiszJPK(lastFirm);
                //utworzJPK(lastFirm);
                //MessageBox.Show(ileDokSp.ToString() + "  " + pNalezny.ToString());

                //doc.Save(@"c:\c_projekty\XML\wynik.xml");
            }
            else
            {
                MessageBox.Show("Nie wybrano pliku !");
            }
        }

        private void butDodajXML_Click(object sender, RoutedEventArgs e)
        {
            string nazwa_pliku = "";
            

            //Inicjalizacja zmiennych
            string lpSprzedazy = "";
            string nrKontrahenta = "";
            string nazwaKontrahenta = "";
            string adresKontrahenta = "";
            string dowodSprzedazy = "";
            string dataWystawienia = "";
            string dataSprzedazy = "";
            string k_10 = "0";
            string k_11 = "0";
            string k_12 = "0";
            string k_13 = "0";
            string k_14 = "0";
            string k_15 = "0";
            string k_16 = "0";
            string k_17 = "0";
            string k_18 = "0";
            string k_19 = "0";
            string k_20 = "0";
            string k_21 = "0";
            string k_22 = "0";
            string k_23 = "0";
            string k_24 = "0";
            string k_25 = "0";
            string k_26 = "0";
            string k_27 = "0";
            string k_28 = "0";
            string k_29 = "0";
            string k_30 = "0";
            string k_31 = "0";
            string k_32 = "0";
            string k_33 = "0";
            string k_34 = "0";
            string k_35 = "0";
            string k_36 = "0";
            string k_37 = "0";
            string k_38 = "0";
            string k_39 = "0";
            string node = "";
            int tryb = 1;
            int j = 0;
            int ileChild = 0;
            int iloscWierszySprzedazy = 0;
            // Wskazanie pliku
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            Nullable<bool> result = ofd.ShowDialog();
            if (result == true)
            {
                nazwa_pliku = ofd.FileName;
            }
            else
            {
                MessageBox.Show("Nie wybrano pliku");
                return;
            }

            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            XmlNodeList xmlnode1;
            int i = 0;
            int ii = 0;
            string str = null;
            FileStream fs = new FileStream(nazwa_pliku, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnode = xmldoc.GetElementsByTagName("tns:SprzedazWiersz");
            xmlnode1 = xmldoc.GetElementsByTagName("Faktura");
            if (xmlnode.Count > 0)
            {
                iloscWierszySprzedazy = xmlnode.Count;
                tryb = 1;
            }
            else
            {
                iloscWierszySprzedazy = xmlnode1.Count;
                tryb = 2;
            }

            
            //iloscWierszySprzedazy = xmlnode.Count + xmlnode1.Count;  //uniezależnia od rodzaju : JPK_VAT, JPK_FA

            MessageBox.Show("ilość dokumentów do importu = " + iloscWierszySprzedazy.ToString());
            int ileDokSp = common.ileDokSprzedazy(listJpkCsvWiersz);
            decimal pNalezny = common.podatekNalezny(listJpkCsvWiersz);
            MessageBox.Show("Ilość dokumentów w pliku bazowym = "+ileDokSp.ToString() + "  Podatek należny w pliku bazowym = " + pNalezny.ToString());

            //MessageBox.Show("ilość dokumentów do importu = " + iloscWierszySprzedazy.ToString());
            //return;
            //for (i = 0; i <= xmlnode.Count - 1; i++)
            ii = ileDokSp +2;
            for (i = 0; i <= iloscWierszySprzedazy - 1; i++)
            {
                //Zerowanie zmiennych
                lpSprzedazy = "";
                nrKontrahenta = "";
                nazwaKontrahenta = "";
                adresKontrahenta = "";
                dowodSprzedazy = "";
                dataWystawienia = "";
                dataSprzedazy = "";
                k_10 = "0";
                k_11 = "0";
                k_12 = "0";
                k_13 = "0";
                k_14 = "0";
                k_15 = "0";
                k_16 = "0";
                k_17 = "0";
                k_18 = "0";
                k_19 = "0";
                k_20 = "0";
                k_21 = "0";
                k_22 = "0";
                k_23 = "0";
                k_24 = "0";
                k_25 = "0";
                k_26 = "0";
                k_27 = "0";
                k_28 = "0";
                k_29 = "0";
                k_30 = "0";
                k_31 = "0";
                k_32 = "0";
                k_33 = "0";
                k_34 = "0";
                k_35 = "0";
                k_36 = "0";
                k_37 = "0";
                k_38 = "0";
                k_39 = "0";
                if (tryb == 1)
                {
                    ileChild = xmlnode[i].ChildNodes.Count;
                    for (j = 0; j < ileChild; j++)
                    {
                        switch (xmlnode[i].ChildNodes.Item(j).Name.Trim())  //zwraca nazwę węzła                     
                        {
                            case "tns:NrKontrahenta":
                                nrKontrahenta = xmlnode[i].ChildNodes.Item(j).InnerText.Trim().Replace("&", "-");
                                break;
                            case "tns:NazwaKontrahenta":
                                nazwaKontrahenta = xmlnode[i].ChildNodes.Item(j).InnerText.Trim().Replace("&", "-");
                                break;
                            case "tns:AdresKontrahenta":
                                adresKontrahenta = xmlnode[i].ChildNodes.Item(j).InnerText.Trim().Replace("&", "-");
                                break;
                            case "tns:DowodSprzedazy":
                                dowodSprzedazy = xmlnode[i].ChildNodes.Item(j).InnerText.Trim().Replace("&", "-");
                                break;
                            case "tns:DataWystawienia":
                                dataWystawienia = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;

                            case "tns:K_10":
                                k_10 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_11":
                                k_11 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_12":
                                k_12 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_13":
                                k_13 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_14":
                                k_14 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_15":
                                k_15 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_16":
                                k_16 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_17":
                                k_17 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_18":
                                k_18 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;

                            case "tns:K_19":
                                k_19 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_20":
                                k_20 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;

                            case "tns:K_21":
                                k_21 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_22":
                                k_22 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_23":
                                k_23 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_24":
                                k_24 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;

                            case "tns:K_25":
                                k_25 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_26":
                                k_26 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;

                            case "tns:K_27":
                                k_27 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_28":
                                k_28 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;

                            case "tns:K_29":
                                k_29 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_30":
                                k_30 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;

                            case "tns:K_31":
                                k_31 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_32":
                                k_32 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_33":
                                k_33 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;

                            case "tns:K_34":
                                k_34 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_35":
                                k_35 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;

                            case "tns:K_36":
                                k_36 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_37":
                                k_37 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_38":
                                k_38 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "tns:K_39":
                                k_39 = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                        }
                    }
                }
                else
                {
                    ileChild = xmlnode1[i].ChildNodes.Count;
                    for (j = 0; j < ileChild; j++)
                    {
                        
                        switch (xmlnode1[i].ChildNodes.Item(j).Name.Trim())  //zwraca nazwę węzła                       
                        {
                            case "P_5B":
                                nrKontrahenta = xmlnode1[i].ChildNodes.Item(j).InnerText.Trim().Replace("&", "-");
                                break;
                            case "P_3A":
                                nazwaKontrahenta = xmlnode1[i].ChildNodes.Item(j).InnerText.Trim().Replace("&", "-");
                                break;
                            case "P_3B":
                                adresKontrahenta = xmlnode1[i].ChildNodes.Item(j).InnerText.Trim().Replace("&", "-");
                                break;
                            case "P_2A":
                                dowodSprzedazy = xmlnode1[i].ChildNodes.Item(j).InnerText.Trim().Replace("&", "-");
                                break;
                            case "P_1":
                                dataWystawienia = xmlnode1[i].ChildNodes.Item(j).InnerText.Trim();
                                break;

                            case "P_13_7":
                                k_10 = xmlnode1[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "P_13_6":
                                k_13 = xmlnode1[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "P_13_3":
                                k_15 = xmlnode1[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "P_14_3":
                                k_16 = xmlnode1[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "P_13_2":
                                k_17 = xmlnode1[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "P_14_2":
                                k_18 = xmlnode1[i].ChildNodes.Item(j).InnerText.Trim();
                                break;

                            case "P_13_1":
                                k_19 = xmlnode1[i].ChildNodes.Item(j).InnerText.Trim();
                                break;
                            case "P_14_1":
                                k_20 = xmlnode1[i].ChildNodes.Item(j).InnerText.Trim();
                                break;

                        }
                    }
                }

                
                //    if (true)
                if (dowodSprzedazy.Substring(0, 2) != "PA")
                {
                    //str = nrKontrahenta + "  " + nazwaKontrahenta + "  " + adresKontrahenta + "  " + dowodSprzedazy + "  " + dataWystawienia + "  " + k_17 + "  " + k_18 + "  " + k_19 + "  " + k_20 + "  " + k_23 + "  " + k_24;
                    //MessageBox.Show(str);

                    //listJpkCsvWiersz.Insert(ileDokSp + 2 + i, new JpkCsvWiersz()
                    listJpkCsvWiersz.Insert(ii, new JpkCsvWiersz()
                    {
                        typSprzedazy = "G",
                        //LpSprzedazy = (ileDokSp + 1 + i).ToString(),
                        LpSprzedazy = (ii-1).ToString(),
                        NrKontrahenta = nrKontrahenta,
                        NazwaKontrahenta = nazwaKontrahenta,
                        AdresKontrahenta = adresKontrahenta,
                        DowodSprzedazy = dowodSprzedazy,
                        DataWystawienia = dataWystawienia,
                        K_10 = k_10,
                        K_11 = k_11,
                        K_12 = k_12,
                        K_13 = k_13,
                        K_14 = k_14,
                        K_15 = k_15,
                        K_16 = k_16,
                        K_17 = k_17,
                        K_18 = k_18,
                        K_19 = k_19,
                        K_20 = k_20,
                        K_21 = k_21,
                        K_22 = k_22,
                        K_23 = k_23,
                        K_24 = k_24,
                        K_25 = k_25,
                        K_26 = k_26,
                        K_27 = k_27,
                        K_28 = k_28,
                        K_29 = k_29,
                        K_30 = k_30,
                        K_31 = k_31,
                        K_32 = k_32,
                        K_33 = k_33,
                        K_34 = k_34,
                        K_35 = k_35,
                        K_36 = k_36,
                        K_37 = k_37,
                        K_38 = k_38,
                        K_39 = k_39,

                    });
                    //str = listJpkCsvWiersz[ileDokSp+2+i].typSprzedazy + "  " + listJpkCsvWiersz[ileDokSp + 2 + i].LpSprzedazy;
                    //MessageBox.Show(str);
                    ii = ii + 1;
                }
                

            }
            ileDokSp = common.ileDokSprzedazy(listJpkCsvWiersz);
            pNalezny = common.podatekNalezny(listJpkCsvWiersz);
            listJpkCsvWiersz[ileDokSp + 2].LiczbaWierszySprzedazy = ileDokSp.ToString().Replace(",", ".");
            listJpkCsvWiersz[ileDokSp + 2].PodatekNalezny = pNalezny.ToString().Replace(",", ".");
            zapiszJPK(lastFirm);
            zapiszCSV(lastFirm);
            MessageBox.Show("Ilość dokumentów sprzedaży po imporcie = " + ileDokSp.ToString()+ "  Podatek należny po imporcie = " + pNalezny.ToString());
        }
    }
    }
