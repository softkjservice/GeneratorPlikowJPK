using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPK_VAT_3
{
    class common
    {
        public static DataTable ToDataTable<T>(IList<T> data)// T is any generic type, zamiania listę w tablicę
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }


        //******** Eksport do csv
        public static void CreateCSVFile(DataTable dtDataTablesList, string strFilePath, bool nazwyKolumn)

        {
            // Create the CSV file to which grid data will be exported.

            StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.Default);

            //First we will write the headers.

            int iColCount = dtDataTablesList.Columns.Count;

            if (nazwyKolumn)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    sw.Write(dtDataTablesList.Columns[i]);
                    if (i < iColCount - 1)
                    {
                        //sw.Write(",");
                        sw.Write(";");
                    }
                }
                sw.Write(sw.NewLine);
            }


            // Now write all the rows.

            foreach (DataRow dr in dtDataTablesList.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString());
                    }
                    if (i < iColCount - 1)

                    {
                        //sw.Write(",");
                        sw.Write(";");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        public static string Katalog(string data)
        {
            string lkatalog = "";
            if (DateTime.Parse(data).Month < 10)
            {
                lkatalog = "M0" + DateTime.Parse(data).Month.ToString();
            }
            else
            {
                lkatalog = "M" + DateTime.Parse(data).Month.ToString();
            }
            return lkatalog;
        }

        public static void CreateJPKFile(List<JpkCsvWiersz> jpkVat, string strFilePath, bool nazwyKolumn)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);
            int iloscWierszy = jpkVat.Count;
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            //sw.WriteLine("<JPK xmlns=\"http://jpk.mf.gov.pl/wzor/2016/10/26/10261/\" xmlns:etd=\"http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2016/01/25/eD/DefinicjeTypy/\">");
            //sw.WriteLine("<tns:JPK xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:etd=\"http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2016/01/25/eD/DefinicjeTypy/\" xmlns:kck=\"http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2013/05/23/eD/KodyCECHKRAJOW/\" xsi:schemaLocation=\"http://jpk.mf.gov.pl/wzor/2017/10/12/1012/ http://www.mf.gov.pl/documents/764034/6145258/20171013.Schemat_JPK_VAT(3)_v1-0.xsd\" xmlns:tns=\"http://jpk.mf.gov.pl/wzor/2017/10/12/1012/\">");
            sw.WriteLine("<tns:JPK xmlns:tns=\"http://jpk.mf.gov.pl/wzor/2017/11/13/1113/\" xmlns:etd=\"http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2016/01/25/eD/DefinicjeTypy/\">");

            sw.WriteLine("    <tns:Naglowek>");
            sw.WriteLine("        <tns:KodFormularza kodSystemowy=\"" + jpkVat[0].kodSystemowy + "\" wersjaSchemy=\"" + jpkVat[0].wersjaSchemy + "\" >" + jpkVat[0].KodFormularza + "</tns:KodFormularza>");
            //sw.WriteLine("        <KodFormularza kodSystemowy=\"" + "\" wersjaSchemy=\"" + jpkVat[0].wersjaSchemy + "\" >" + jpkVat[0].KodFormularza + "</KodFormularza>");
            //sw.WriteLine("        <tns:KodFormularza wersjaSchemy=\"" + jpkVat[0].wersjaSchemy + "\" >" + jpkVat[0].KodFormularza + "</tns:KodFormularza>");
            //sw.WriteLine("        <tns:KodFormularza wersjaSchemy=\"" + "1-1" + "\" >" + jpkVat[0].KodFormularza + "</tns:KodFormularza>");
            sw.WriteLine("        <tns:WariantFormularza>" + jpkVat[0].WariantFormularza + "</tns:WariantFormularza>");
            sw.WriteLine("        <tns:CelZlozenia>" + jpkVat[0].CelZlozenia + "</tns:CelZlozenia>");
            sw.WriteLine("        <tns:DataWytworzeniaJPK>" + jpkVat[0].DataWytworzeniaJPK.Substring(0, 10) + "T" + jpkVat[0].DataWytworzeniaJPK.Substring(10, 8) + "</tns:DataWytworzeniaJPK>");
            sw.WriteLine("        <tns:DataOd>" + jpkVat[0].DataOd + "</tns:DataOd>");
            sw.WriteLine("        <tns:DataDo>" + jpkVat[0].DataDo + "</tns:DataDo>");
            sw.WriteLine("        <tns:NazwaSystemu>" + jpkVat[0].NazwaSystemu + "</tns:NazwaSystemu>");
            sw.WriteLine("    </tns:Naglowek>");

            sw.WriteLine("    <tns:Podmiot1>");
            sw.WriteLine("            <tns:NIP>" + jpkVat[1].NIP + "</tns:NIP>");          
            sw.WriteLine("            <tns:PelnaNazwa>" + jpkVat[1].PelnaNazwa + "</tns:PelnaNazwa>");
            if (jpkVat[1].Email != "")
            {
                sw.WriteLine("            <tns:Email>" + jpkVat[1].Email + "</tns:Email>");
            }
            sw.WriteLine("    </tns:Podmiot1>");

            int i = 2;
            //  while (jpkVat[i].typSprzedazy=="G")
            //  {


            while (jpkVat[i].typSprzedazy == "G")
            {
                sw.WriteLine("	<tns:SprzedazWiersz>");
                sw.WriteLine("	    <tns:LpSprzedazy>" + jpkVat[i].LpSprzedazy + "</tns:LpSprzedazy>");
                if (jpkVat[i].NrKontrahenta != "")
                {
                    sw.WriteLine("	    <tns:NrKontrahenta>" + jpkVat[i].NrKontrahenta.Trim() + "</tns:NrKontrahenta>");
                }
                else
                {
                    sw.WriteLine("	    <tns:NrKontrahenta>" + "Brak" + "</tns:NrKontrahenta>");
                }
                sw.WriteLine("		<tns:NazwaKontrahenta>" + jpkVat[i].NazwaKontrahenta + "</tns:NazwaKontrahenta>");
                sw.WriteLine("		<tns:AdresKontrahenta>" + jpkVat[i].AdresKontrahenta + "</tns:AdresKontrahenta>");
                sw.WriteLine("		<tns:DowodSprzedazy>" + jpkVat[i].DowodSprzedazy + "</tns:DowodSprzedazy>");
                sw.WriteLine("		<tns:DataWystawienia>" + jpkVat[i].DataWystawienia.Substring(0, 10) + "</tns:DataWystawienia>");

                /*
                if (jpkVat[i].DataWystawienia != string.Empty && jpkVat[i].DataWystawienia.Substring(0, 10) != jpkVat[i].DataSprzedazy.Substring(0, 10))
                {
                    sw.WriteLine("		<tns:DataSprzedazy>" + jpkVat[i].DataSprzedazy.Substring(0, 10) + "</tns:DataSprzedazy>");
                }
                */
                if (jpkVat[i].K_10 != "0")
                {
                    sw.WriteLine("	    <tns:K_10>" + jpkVat[i].K_10 + "</tns:K_10>");
                }
                if (jpkVat[i].K_11 != "0")
                {
                    sw.WriteLine("	    <tns:K_11>" + jpkVat[i].K_11 + "</tns:K_11>");
                }
                if (jpkVat[i].K_12 != "0")
                {
                    sw.WriteLine("	    <tns:K_12>" + jpkVat[i].K_12 + "</tns:K_12>");
                }
                if (jpkVat[i].K_13 != "0")
                {
                    sw.WriteLine("	    <tns:K_13>" + jpkVat[i].K_13 + "</tns:K_13>");
                }
                if (jpkVat[i].K_14 != "0")
                {
                    sw.WriteLine("	    <tns:K_14>" + jpkVat[i].K_14 + "</tns:K_14>");
                }
                if (jpkVat[i].K_15 != "0")
                {
                    sw.WriteLine("	    <tns:K_15>" + jpkVat[i].K_15 + "</tns:K_15>");
                }
                if (jpkVat[i].K_16 != "0" || jpkVat[i].K_15 != "0")
                {
                    sw.WriteLine("	    <tns:K_16>" + jpkVat[i].K_16 + "</tns:K_16>");
                }
                if (jpkVat[i].K_17 != "0")
                {
                    sw.WriteLine("	    <tns:K_17>" + jpkVat[i].K_17 + "</tns:K_17>");
                }
                if (jpkVat[i].K_18 != "0" || jpkVat[i].K_17 != "0")
                {
                    sw.WriteLine("	    <tns:K_18>" + jpkVat[i].K_18 + "</tns:K_18>");
                }
                if (jpkVat[i].K_19 != "0")
                {
                    sw.WriteLine("	    <tns:K_19>" + jpkVat[i].K_19 + "</tns:K_19>");
                }
                if (jpkVat[i].K_20 != "0" || jpkVat[i].K_19 != "0")
                {
                    sw.WriteLine("	    <tns:K_20>" + jpkVat[i].K_20 + "</tns:K_20>");
                }
                if (jpkVat[i].K_21 != "0")
                {
                    sw.WriteLine("	    <tns:K_21>" + jpkVat[i].K_21 + "</tns:K_21>");
                }
                if (jpkVat[i].K_22 != "0")
                {
                    sw.WriteLine("	    <tns:K_22>" + jpkVat[i].K_22 + "</tns:K_22>");
                }
                if (jpkVat[i].K_23 != "0")
                {
                    sw.WriteLine("	    <tns:K_23>" + jpkVat[i].K_23 + "</tns:K_23>");
                }
                if (jpkVat[i].K_24 != "0" || jpkVat[i].K_23 != "0")
                {
                    sw.WriteLine("	    <tns:K_24>" + jpkVat[i].K_24 + "</tns:K_24>");
                }
                if (jpkVat[i].K_25 != "0")
                {
                    sw.WriteLine("	    <tns:K_25>" + jpkVat[i].K_25 + "</tns:K_25>");
                }
                if (jpkVat[i].K_26 != "0" || jpkVat[i].K_25 != "0")
                {
                    sw.WriteLine("	    <tns:K_26>" + jpkVat[i].K_26 + "</tns:K_26>");
                }
                if (jpkVat[i].K_27 != "0")
                {
                    sw.WriteLine("	    <tns:K_27>" + jpkVat[i].K_27 + "</tns:K_27>");
                }
                if (jpkVat[i].K_28 != "0" || jpkVat[i].K_27 != "0")
                {
                    sw.WriteLine("	    <tns:K_28>" + jpkVat[i].K_28 + "</tns:K_28>");
                }
                if (jpkVat[i].K_29 != "0")
                {
                    sw.WriteLine("	    <tns:K_29>" + jpkVat[i].K_29 + "</tns:K_29>");
                }
                if (jpkVat[i].K_30 != "0" || jpkVat[i].K_29 != "0")
                {
                    sw.WriteLine("	    <tns:K_30>" + jpkVat[i].K_30 + "</tns:K_30>");
                }
                if (jpkVat[i].K_31 != "0")
                {
                    sw.WriteLine("	    <tns:K_31>" + jpkVat[i].K_31 + "</tns:K_31>");
                }
                if (jpkVat[i].K_32 != "0")
                {
                    sw.WriteLine("	    <tns:K_32>" + jpkVat[i].K_32 + "</tns:K_32>");
                }
                if (jpkVat[i].K_33 != "0")
                {
                    sw.WriteLine("	    <tns:K_33>" + jpkVat[i].K_33 + "</tns:K_33>");
                }
                if (jpkVat[i].K_34 != "0")
                {
                    sw.WriteLine("	    <tns:K_34>" + jpkVat[i].K_34 + "</tns:K_34>");
                }
                if (jpkVat[i].K_35 != "0")
                {
                    sw.WriteLine("	    <tns:K_35>" + jpkVat[i].K_35 + "</tns:K_35>");
                }
                if (jpkVat[i].K_36 != "0")
                {
                    sw.WriteLine("	    <tns:K_36>" + jpkVat[i].K_36 + "</tns:K_36>");
                }
                if (jpkVat[i].K_37 != "0")
                {
                    sw.WriteLine("	    <tns:K_37>" + jpkVat[i].K_37 + "</tns:K_37>");
                }
                if (jpkVat[i].K_38 != "0")
                {
                    sw.WriteLine("	    <tns:K_38>" + jpkVat[i].K_38 + "</tns:K_38>");
                }
                if (jpkVat[i].K_39 != "0")
                {
                    sw.WriteLine("	    <tns:K_39>" + jpkVat[i].K_39 + "</tns:K_39>");
                }

                sw.WriteLine("	</tns:SprzedazWiersz>");
                i++;
            }

            if (jpkVat[i].LiczbaWierszySprzedazy != "0")
            {
                sw.WriteLine("	<tns:SprzedazCtrl>");
                sw.WriteLine("		<tns:LiczbaWierszySprzedazy>" + jpkVat[i].LiczbaWierszySprzedazy + "</tns:LiczbaWierszySprzedazy>");
                sw.WriteLine("		<tns:PodatekNalezny>" + jpkVat[i].PodatekNalezny + "</tns:PodatekNalezny>");
                sw.WriteLine("	</tns:SprzedazCtrl>");
                //i = i + 1;
            }
            i = i + 1;

            while (jpkVat[i].typZakupu == "G")
            {
                sw.WriteLine("	<tns:ZakupWiersz>");
                sw.WriteLine("		<tns:LpZakupu>" + jpkVat[i].LpZakupu + "</tns:LpZakupu>");
                if (jpkVat[i].NrDostawcy != "")
                {
                    sw.WriteLine("		<tns:NrDostawcy>" + jpkVat[i].NrDostawcy + "</tns:NrDostawcy>");
                }
                else
                {
                    sw.WriteLine("		<tns:NrDostawcy>" + "Brak" + "</tns:NrDostawcy>");
                }               
                sw.WriteLine("		<tns:NazwaDostawcy>" + jpkVat[i].NazwaDostawcy + "</tns:NazwaDostawcy>");
                sw.WriteLine("		<tns:AdresDostawcy>" + jpkVat[i].AdresDostawcy + "</tns:AdresDostawcy>");
                sw.WriteLine("		<tns:DowodZakupu>" + jpkVat[i].DowodZakupu + "</tns:DowodZakupu>");
                sw.WriteLine("		<tns:DataZakupu>" + jpkVat[i].DataZakupu.Substring(0, 10) + "</tns:DataZakupu>");

                if (jpkVat[i].K_43 != "0")
                {
                    sw.WriteLine("	    <tns:K_43>" + jpkVat[i].K_43 + "</tns:K_43>");
                }
                if (jpkVat[i].K_44 != "0" || jpkVat[i].K_43 != "0")
                {
                    sw.WriteLine("	    <tns:K_44>" + jpkVat[i].K_44 + "</tns:K_44>");
                }
                if (jpkVat[i].K_45 != "0")
                {
                    sw.WriteLine("	    <tns:K_45>" + jpkVat[i].K_45 + "</tns:K_45>");
                }
                if (jpkVat[i].K_46 != "0" || jpkVat[i].K_45 != "0")
                {
                    sw.WriteLine("	    <tns:K_46>" + jpkVat[i].K_46 + "</tns:K_46>");
                }
                if (jpkVat[i].K_47 != "0")
                {
                    sw.WriteLine("	    <tns:K_47>" + jpkVat[i].K_47 + "</tns:K_47>");
                }
                if (jpkVat[i].K_48 != "0")
                {
                    sw.WriteLine("	    <tns:K_48>" + jpkVat[i].K_48 + "</tns:K_48>");
                }
                if (jpkVat[i].K_49 != "0")
                {
                    sw.WriteLine("	    <tns:K_49>" + jpkVat[i].K_49 + "</tns:K_49>");
                }
                if (jpkVat[i].K_50 != "0")
                {
                    sw.WriteLine("	    <tns:K_50>" + jpkVat[i].K_50 + "</tns:K_50>");
                }
                sw.WriteLine("	</tns:ZakupWiersz>");
                i++;
            }

            if (jpkVat[i].LiczbaWierszyZakupu != "0")
            {
                sw.WriteLine("	<tns:ZakupCtrl>");
                sw.WriteLine("		<tns:LiczbaWierszyZakupow>" + jpkVat[i].LiczbaWierszyZakupu + "</tns:LiczbaWierszyZakupow>");
                //sw.WriteLine("		<PodatekNaliczony>"+ Math.Round(decimal)jpkVat[i].PodatekNaliczony,1).ToString()+"</PodatekNaliczony>");
                sw.WriteLine("		<tns:PodatekNaliczony>" + jpkVat[i].PodatekNaliczony + "</tns:PodatekNaliczony>");
                sw.WriteLine("	</tns:ZakupCtrl>");
            }


            sw.WriteLine("</tns:JPK>");
            sw.Close();
        }



        public static void CreateJPKFile_tns_Brak(List<JpkCsvWiersz> jpkVat, string strFilePath, bool nazwyKolumn)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);
            int iloscWierszy = jpkVat.Count;
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            //sw.WriteLine("<JPK xmlns=\"http://jpk.mf.gov.pl/wzor/2016/10/26/10261/\" xmlns:etd=\"http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2016/01/25/eD/DefinicjeTypy/\">");
            //sw.WriteLine("<tns:JPK xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:etd=\"http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2016/01/25/eD/DefinicjeTypy/\" xmlns:kck=\"http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2013/05/23/eD/KodyCECHKRAJOW/\" xsi:schemaLocation=\"http://jpk.mf.gov.pl/wzor/2017/10/12/1012/ http://www.mf.gov.pl/documents/764034/6145258/20171013.Schemat_JPK_VAT(3)_v1-0.xsd\" xmlns:tns=\"http://jpk.mf.gov.pl/wzor/2017/10/12/1012/\">");
            sw.WriteLine("<tns:JPK xmlns:tns=\"http://jpk.mf.gov.pl/wzor/2017/11/13/1113/\" xmlns:etd=\"http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2016/01/25/eD/DefinicjeTypy/\">");

            sw.WriteLine("    <tns:Naglowek>");
            //sw.WriteLine("        <KodFormularza kodSystemowy=\"" + "\" wersjaSchemy=\"" + jpkVat[0].wersjaSchemy + "\" >" + jpkVat[0].KodFormularza + "</KodFormularza>");
            sw.WriteLine("        <tns:KodFormularza wersjaSchemy=\"" + jpkVat[0].wersjaSchemy + "\" >" + jpkVat[0].KodFormularza + "</tns:KodFormularza>");
            sw.WriteLine("        <tns:WariantFormularza>" + jpkVat[0].WariantFormularza + "</tns:WariantFormularza>");
            sw.WriteLine("        <tns:CelZlozenia>" + jpkVat[0].CelZlozenia + "</tns:CelZlozenia>");
            sw.WriteLine("        <tns:DataWytworzeniaJPK>" + jpkVat[0].DataWytworzeniaJPK.Substring(0, 10) + "T" + jpkVat[0].DataWytworzeniaJPK.Substring(10, 8) + "</tns:DataWytworzeniaJPK>");
            sw.WriteLine("        <tns:DataOd>" + jpkVat[0].DataOd + "</tns:DataOd>");
            sw.WriteLine("        <tns:DataDo>" + jpkVat[0].DataDo + "</tns:DataDo>");
            sw.WriteLine("        <tns:NazwaSystemu>" + jpkVat[0].NazwaSystemu + "</tns:NazwaSystemu>");
            sw.WriteLine("    </tns:Naglowek>");

            sw.WriteLine("    <tns:Podmiot1>");
            if (jpkVat[1].NIP != "")
            {
                sw.WriteLine("            <tns:NIP>" + jpkVat[1].NIP + "</tns:NIP>");
            }
            if (jpkVat[1].PelnaNazwa != "")
            {
                sw.WriteLine("            <tns:PelnaNazwa>" + jpkVat[1].PelnaNazwa + "</tns:PelnaNazwa>");
            }
            if (jpkVat[1].Email != "")
            {
                sw.WriteLine("            <tns:Email>" + jpkVat[1].Email + "</tns:Email>");
            }
            sw.WriteLine("    </tns:Podmiot1>");

            int i = 2;
            //  while (jpkVat[i].typSprzedazy=="G")
            //  {


            while (jpkVat[i].typSprzedazy == "G")
            {
                sw.WriteLine("	<tns:SprzedazWiersz>");
                sw.WriteLine("	    <tns:LpSprzedazy>" + jpkVat[i].LpSprzedazy + "</tns:LpSprzedazy>");
                if (jpkVat[i].NrKontrahenta != "")
                {
                    sw.WriteLine("	    <tns:NrKontrahenta>" + jpkVat[i].NrKontrahenta.Trim() + "</tns:NrKontrahenta>");
                }
                else
                {
                    sw.WriteLine("	    <tns:NrKontrahenta>" + "Brak" + "</tns:NrKontrahenta>");
                }
                if (jpkVat[i].NazwaKontrahenta != "")
                {
                    sw.WriteLine("		<tns:NazwaKontrahenta>" + jpkVat[i].NazwaKontrahenta + "</tns:NazwaKontrahenta>");
                }
                else
                {
                    sw.WriteLine("		<tns:NazwaKontrahenta>" + "Brak" + "</tns:NazwaKontrahenta>");
                }
                if (jpkVat[i].AdresKontrahenta != "")
                {
                    sw.WriteLine("		<tns:AdresKontrahenta>" + jpkVat[i].AdresKontrahenta + "</tns:AdresKontrahenta>");
                }
                else
                {
                    sw.WriteLine("		<tns:AdresKontrahenta>" + "Brak" + "</tns:AdresKontrahenta>");
                }
                if (jpkVat[i].DowodSprzedazy != "")
                {
                    sw.WriteLine("		<tns:DowodSprzedazy>" + jpkVat[i].DowodSprzedazy + "</tns:DowodSprzedazy>");
                }
                if (jpkVat[i].DataWystawienia != "")
                {
                    sw.WriteLine("		<tns:DataWystawienia>" + jpkVat[i].DataWystawienia.Substring(0, 10) + "</tns:DataWystawienia>");
                }


                if (jpkVat[i].K_10 != "0")
                {
                    sw.WriteLine("	    <tns:K_10>" + jpkVat[i].K_10 + "</tns:K_10>");
                }
                if (jpkVat[i].K_11 != "0")
                {
                    sw.WriteLine("	    <tns:K_11>" + jpkVat[i].K_11 + "</tns:K_11>");
                }
                if (jpkVat[i].K_12 != "0")
                {
                    sw.WriteLine("	    <tns:K_12>" + jpkVat[i].K_12 + "</tns:K_12>");
                }
                if (jpkVat[i].K_13 != "0")
                {
                    sw.WriteLine("	    <tns:K_13>" + jpkVat[i].K_13 + "</tns:K_13>");
                }
                if (jpkVat[i].K_14 != "0")
                {
                    sw.WriteLine("	    <tns:K_14>" + jpkVat[i].K_14 + "</tns:K_14>");
                }
                if (jpkVat[i].K_15 != "0")
                {
                    sw.WriteLine("	    <tns:K_15>" + jpkVat[i].K_15 + "</tns:K_15>");
                }
                if (jpkVat[i].K_16 != "0" || jpkVat[i].K_15 != "0")
                {
                    sw.WriteLine("	    <tns:K_16>" + jpkVat[i].K_16 + "</tns:K_16>");
                }
                if (jpkVat[i].K_17 != "0")
                {
                    sw.WriteLine("	    <tns:K_17>" + jpkVat[i].K_17 + "</tns:K_17>");
                }
                if (jpkVat[i].K_18 != "0" || jpkVat[i].K_17 != "0")
                {
                    sw.WriteLine("	    <tns:K_18>" + jpkVat[i].K_18 + "</tns:K_18>");
                }
                if (jpkVat[i].K_19 != "0")
                {
                    sw.WriteLine("	    <tns:K_19>" + jpkVat[i].K_19 + "</tns:K_19>");
                }
                if (jpkVat[i].K_20 != "0" || jpkVat[i].K_19 != "0")
                {
                    sw.WriteLine("	    <tns:K_20>" + jpkVat[i].K_20 + "</tns:K_20>");
                }
                if (jpkVat[i].K_21 != "0")
                {
                    sw.WriteLine("	    <tns:K_21>" + jpkVat[i].K_21 + "</tns:K_21>");
                }
                if (jpkVat[i].K_22 != "0")
                {
                    sw.WriteLine("	    <tns:K_22>" + jpkVat[i].K_22 + "</tns:K_22>");
                }
                if (jpkVat[i].K_23 != "0")
                {
                    sw.WriteLine("	    <tns:K_23>" + jpkVat[i].K_23 + "</tns:K_23>");
                }
                if (jpkVat[i].K_24 != "0" || jpkVat[i].K_23 != "0")
                {
                    sw.WriteLine("	    <tns:K_24>" + jpkVat[i].K_24 + "</tns:K_24>");
                }
                if (jpkVat[i].K_25 != "0")
                {
                    sw.WriteLine("	    <tns:K_25>" + jpkVat[i].K_25 + "</tns:K_25>");
                }
                if (jpkVat[i].K_26 != "0" || jpkVat[i].K_25 != "0")
                {
                    sw.WriteLine("	    <tns:K_26>" + jpkVat[i].K_26 + "</tns:K_26>");
                }
                if (jpkVat[i].K_27 != "0")
                {
                    sw.WriteLine("	    <tns:K_27>" + jpkVat[i].K_27 + "</tns:K_27>");
                }
                if (jpkVat[i].K_28 != "0" || jpkVat[i].K_27 != "0")
                {
                    sw.WriteLine("	    <tns:K_28>" + jpkVat[i].K_28 + "</tns:K_28>");
                }
                if (jpkVat[i].K_29 != "0")
                {
                    sw.WriteLine("	    <tns:K_29>" + jpkVat[i].K_29 + "</tns:K_29>");
                }
                if (jpkVat[i].K_30 != "0" || jpkVat[i].K_29 != "0")
                {
                    sw.WriteLine("	    <tns:K_30>" + jpkVat[i].K_30 + "</tns:K_30>");
                }
                if (jpkVat[i].K_31 != "0")
                {
                    sw.WriteLine("	    <tns:K_31>" + jpkVat[i].K_31 + "</tns:K_31>");
                }
                if (jpkVat[i].K_32 != "0")
                {
                    sw.WriteLine("	    <tns:K_32>" + jpkVat[i].K_32 + "</tns:K_32>");
                }
                if (jpkVat[i].K_33 != "0")
                {
                    sw.WriteLine("	    <tns:K_33>" + jpkVat[i].K_33 + "</tns:K_33>");
                }
                if (jpkVat[i].K_34 != "0")
                {
                    sw.WriteLine("	    <tns:K_34>" + jpkVat[i].K_34 + "</tns:K_34>");
                }
                if (jpkVat[i].K_35 != "0")
                {
                    sw.WriteLine("	    <tns:K_35>" + jpkVat[i].K_35 + "</tns:K_35>");
                }
                if (jpkVat[i].K_36 != "0")
                {
                    sw.WriteLine("	    <tns:K_36>" + jpkVat[i].K_36 + "</tns:K_36>");
                }
                if (jpkVat[i].K_37 != "0")
                {
                    sw.WriteLine("	    <tns:K_37>" + jpkVat[i].K_37 + "</tns:K_37>");
                }
                if (jpkVat[i].K_38 != "0")
                {
                    sw.WriteLine("	    <tns:K_38>" + jpkVat[i].K_38 + "</tns:K_38>");
                }
                if (jpkVat[i].K_39 != "0")
                {
                    sw.WriteLine("	    <tns:K_39>" + jpkVat[i].K_39 + "</tns:K_39>");
                }

                sw.WriteLine("	</tns:SprzedazWiersz>");
                i++;
            }

            if (jpkVat[i].LiczbaWierszySprzedazy != "0")
            {
                sw.WriteLine("	<tns:SprzedazCtrl>");
                sw.WriteLine("		<tns:LiczbaWierszySprzedazy>" + jpkVat[i].LiczbaWierszySprzedazy + "</tns:LiczbaWierszySprzedazy>");
                sw.WriteLine("		<tns:PodatekNalezny>" + jpkVat[i].PodatekNalezny + "</tns:PodatekNalezny>");
                sw.WriteLine("	</tns:SprzedazCtrl>");
                //i = i + 1;
            }
            i = i + 1;

            while (jpkVat[i].typZakupu == "G")
            {
                sw.WriteLine("	<tns:ZakupWiersz>");
                sw.WriteLine("		<tns:LpZakupu>" + jpkVat[i].LpZakupu + "</tns:LpZakupu>");
                if (jpkVat[i].NrDostawcy != "")
                {
                    sw.WriteLine("		<tns:NrDostawcy>" + jpkVat[i].NrDostawcy + "</tns:NrDostawcy>");
                }
                else
                {
                    sw.WriteLine("		<tns:NrDostawcy>" + "Brak" + "</tns:NrDostawcy>");
                }
                if (jpkVat[i].NazwaDostawcy != "")
                {
                    sw.WriteLine("		<tns:NazwaDostawcy>" + jpkVat[i].NazwaDostawcy + "</tns:NazwaDostawcy>");
                }
                else
                {
                    sw.WriteLine("		<tns:NazwaDostawcy>" + "Brak" + "</tns:NazwaDostawcy>");
                }
                if (jpkVat[i].AdresDostawcy != "")
                {
                    sw.WriteLine("		<tns:AdresDostawcy>" + jpkVat[i].AdresDostawcy + "</tns:AdresDostawcy>");
                }
                else
                {
                    sw.WriteLine("		<tns:AdresDostawcy>" + "Brak" + "</tns:AdresDostawcy>");
                }
                if (jpkVat[i].DowodZakupu != "")
                {
                    sw.WriteLine("		<tns:DowodZakupu>" + jpkVat[i].DowodZakupu + "</tns:DowodZakupu>");
                }
                if (jpkVat[i].DataZakupu != "")
                {
                    sw.WriteLine("		<tns:DataZakupu>" + jpkVat[i].DataZakupu.Substring(0, 10) + "</tns:DataZakupu>");
                }


                if (jpkVat[i].K_43 != "0")
                {
                    sw.WriteLine("	    <tns:K_43>" + jpkVat[i].K_43 + "</tns:K_43>");
                }
                if (jpkVat[i].K_44 != "0" || jpkVat[i].K_43 != "0")
                {
                    sw.WriteLine("	    <tns:K_44>" + jpkVat[i].K_44 + "</tns:K_44>");
                }
                if (jpkVat[i].K_45 != "0")
                {
                    sw.WriteLine("	    <tns:K_45>" + jpkVat[i].K_45 + "</tns:K_45>");
                }
                if (jpkVat[i].K_46 != "0" || jpkVat[i].K_45 != "0")
                {
                    sw.WriteLine("	    <tns:K_46>" + jpkVat[i].K_46 + "</tns:K_46>");
                }
                if (jpkVat[i].K_47 != "0")
                {
                    sw.WriteLine("	    <tns:K_47>" + jpkVat[i].K_47 + "</tns:K_47>");
                }
                if (jpkVat[i].K_48 != "0")
                {
                    sw.WriteLine("	    <tns:K_48>" + jpkVat[i].K_48 + "</tns:K_48>");
                }
                if (jpkVat[i].K_49 != "0")
                {
                    sw.WriteLine("	    <tns:K_49>" + jpkVat[i].K_49 + "</tns:K_49>");
                }
                if (jpkVat[i].K_50 != "0")
                {
                    sw.WriteLine("	    <tns:K_50>" + jpkVat[i].K_50 + "</tns:K_50>");
                }
                sw.WriteLine("	</tns:ZakupWiersz>");
                i++;
            }

            if (jpkVat[i].LiczbaWierszyZakupu != "0")
            {
                sw.WriteLine("	<tns:ZakupCtrl>");
                sw.WriteLine("		<tns:LiczbaWierszyZakupow>" + jpkVat[i].LiczbaWierszyZakupu + "</tns:LiczbaWierszyZakupow>");
                //sw.WriteLine("		<PodatekNaliczony>"+ Math.Round(decimal)jpkVat[i].PodatekNaliczony,1).ToString()+"</PodatekNaliczony>");
                sw.WriteLine("		<tns:PodatekNaliczony>" + jpkVat[i].PodatekNaliczony + "</tns:PodatekNaliczony>");
                sw.WriteLine("	</tns:ZakupCtrl>");
            }


            sw.WriteLine("</tns:JPK>");
            sw.Close();
        }




        public static void CreateJPKFile_bez_tns(List<JpkCsvWiersz> jpkVat, string strFilePath, bool nazwyKolumn)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);
            int iloscWierszy = jpkVat.Count;
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            //sw.WriteLine("<JPK xmlns=\"http://jpk.mf.gov.pl/wzor/2016/10/26/10261/\" xmlns:etd=\"http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2016/01/25/eD/DefinicjeTypy/\">");
            //sw.WriteLine("<JPK xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:etd=\"http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2016/01/25/eD/DefinicjeTypy/\" xmlns:kck=\"http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2013/05/23/eD/KodyCECHKRAJOW/\" xsi:schemaLocation=\"http://jpk.mf.gov.pl/wzor/2017/10/12/1012/ http://www.mf.gov.pl/documents/764034/6145258/20171013.Schemat_JPK_VAT(3)_v1-0.xsd\" xmlns:tns=\"http://jpk.mf.gov.pl/wzor/2017/10/12/1012/\">");
            sw.WriteLine("<tns:JPK xmlns:tns=\"http://jpk.mf.gov.pl/wzor/2017/11/13/1113/\" xmlns:etd=\"http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2016/01/25/eD/DefinicjeTypy/\">");
            sw.WriteLine("    <Naglowek>");
            //sw.WriteLine("        <KodFormularza kodSystemowy=\"" + "\" wersjaSchemy=\"" + jpkVat[0].wersjaSchemy + "\" >" + jpkVat[0].KodFormularza + "</KodFormularza>");
            sw.WriteLine("        <KodFormularza wersjaSchemy=\"" + jpkVat[0].wersjaSchemy + "\" >" + jpkVat[0].KodFormularza + "</KodFormularza>");
            sw.WriteLine("        <WariantFormularza>" + jpkVat[0].WariantFormularza + "</WariantFormularza>");
            sw.WriteLine("        <CelZlozenia>" + jpkVat[0].CelZlozenia + "</CelZlozenia>");
            sw.WriteLine("        <DataWytworzeniaJPK>" + jpkVat[0].DataWytworzeniaJPK.Substring(0, 10) + "T" + jpkVat[0].DataWytworzeniaJPK.Substring(10, 8) + "</DataWytworzeniaJPK>");
            sw.WriteLine("        <DataOd>" + jpkVat[0].DataOd + "</DataOd>");
            sw.WriteLine("        <DataDo>" + jpkVat[0].DataDo + "</DataDo>");
            sw.WriteLine("        <NazwaSystemu>" + jpkVat[0].NazwaSystemu + "</NazwaSystemu>");
            sw.WriteLine("    </Naglowek>");

            sw.WriteLine("    <Podmiot1>");
            if (jpkVat[1].NIP != "")
            {
                sw.WriteLine("            <tns:NIP>" + jpkVat[1].NIP + "</tns:NIP>");
            }
            if (jpkVat[1].PelnaNazwa != "")
            {
                sw.WriteLine("            <tns:PelnaNazwa>" + jpkVat[1].PelnaNazwa + "</tns:PelnaNazwa>");
            }
            if (jpkVat[1].Email != "")
            {
                sw.WriteLine("            <tns:Email>" + jpkVat[1].Email + "</tns:Email>");
            }
            sw.WriteLine("    </Podmiot1>");

            int i = 2;
            //  while (jpkVat[i].typSprzedazy=="G")
            //  {


            while (jpkVat[i].typSprzedazy == "G")
            {
                sw.WriteLine("	<SprzedazWiersz>");
                sw.WriteLine("	    <LpSprzedazy>" + jpkVat[i].LpSprzedazy + "</LpSprzedazy>");
                if (jpkVat[i].NrKontrahenta != "")
                {
                    sw.WriteLine("	    <NrKontrahenta>" + jpkVat[i].NrKontrahenta + "</NrKontrahenta>");
                }
                else
                {
                    sw.WriteLine("	    <NrKontrahenta>" + "Brak" + "</NrKontrahenta>");
                }
                if (jpkVat[i].NazwaKontrahenta != "")
                {
                    sw.WriteLine("		<NazwaKontrahenta>" + jpkVat[i].NazwaKontrahenta + "</NazwaKontrahenta>");
                }
                else
                {
                    sw.WriteLine("		<NazwaKontrahenta>" + "Brak" + "</NazwaKontrahenta>");
                }
                if (jpkVat[i].AdresKontrahenta != "")
                {
                    sw.WriteLine("		<AdresKontrahenta>" + jpkVat[i].AdresKontrahenta + "</AdresKontrahenta>");
                }
                else
                {
                    sw.WriteLine("		<AdresKontrahenta>" + "Brak" + "</AdresKontrahenta>");
                }
                if (jpkVat[i].DowodSprzedazy != "")
                {
                    sw.WriteLine("		<DowodSprzedazy>" + jpkVat[i].DowodSprzedazy + "</DowodSprzedazy>");
                }
                if (jpkVat[i].DataWystawienia != "")
                {
                    sw.WriteLine("		<DataWystawienia>" + jpkVat[i].DataWystawienia.Substring(0, 10) + "</DataWystawienia>");
                }


                if (jpkVat[i].K_10 != "0")
                {
                    sw.WriteLine("	    <K_10>" + jpkVat[i].K_10 + "</K_10>");
                }
                if (jpkVat[i].K_11 != "0")
                {
                    sw.WriteLine("	    <K_11>" + jpkVat[i].K_11 + "</K_11>");
                }
                if (jpkVat[i].K_12 != "0")
                {
                    sw.WriteLine("	    <K_12>" + jpkVat[i].K_12 + "</K_12>");
                }
                if (jpkVat[i].K_13 != "0")
                {
                    sw.WriteLine("	    <K_13>" + jpkVat[i].K_13 + "</K_13>");
                }
                if (jpkVat[i].K_14 != "0")
                {
                    sw.WriteLine("	    <K_14>" + jpkVat[i].K_14 + "</K_14>");
                }
                if (jpkVat[i].K_15 != "0")
                {
                    sw.WriteLine("	    <K_15>" + jpkVat[i].K_15 + "</K_15>");
                }
                if (jpkVat[i].K_16 != "0" || jpkVat[i].K_15 != "0")
                {
                    sw.WriteLine("	    <K_16>" + jpkVat[i].K_16 + "</K_16>");
                }
                if (jpkVat[i].K_17 != "0")
                {
                    sw.WriteLine("	    <K_17>" + jpkVat[i].K_17 + "</K_17>");
                }
                if (jpkVat[i].K_18 != "0" || jpkVat[i].K_17 != "0")
                {
                    sw.WriteLine("	    <K_18>" + jpkVat[i].K_18 + "</K_18>");
                }
                if (jpkVat[i].K_19 != "0")
                {
                    sw.WriteLine("	    <K_19>" + jpkVat[i].K_19 + "</K_19>");
                }
                if (jpkVat[i].K_20 != "0" || jpkVat[i].K_19 != "0")
                {
                    sw.WriteLine("	    <K_20>" + jpkVat[i].K_20 + "</K_20>");
                }
                if (jpkVat[i].K_21 != "0")
                {
                    sw.WriteLine("	    <K_21>" + jpkVat[i].K_21 + "</K_21>");
                }
                if (jpkVat[i].K_22 != "0")
                {
                    sw.WriteLine("	    <K_22>" + jpkVat[i].K_22 + "</K_22>");
                }
                if (jpkVat[i].K_23 != "0")
                {
                    sw.WriteLine("	    <K_23>" + jpkVat[i].K_23 + "</K_23>");
                }
                if (jpkVat[i].K_24 != "0" || jpkVat[i].K_23 != "0")
                {
                    sw.WriteLine("	    <K_24>" + jpkVat[i].K_24 + "</K_24>");
                }
                if (jpkVat[i].K_25 != "0")
                {
                    sw.WriteLine("	    <K_25>" + jpkVat[i].K_25 + "</K_25>");
                }
                if (jpkVat[i].K_26 != "0" || jpkVat[i].K_25 != "0")
                {
                    sw.WriteLine("	    <K_26>" + jpkVat[i].K_26 + "</K_26>");
                }
                if (jpkVat[i].K_27 != "0")
                {
                    sw.WriteLine("	    <K_27>" + jpkVat[i].K_27 + "</K_27>");
                }
                if (jpkVat[i].K_28 != "0" || jpkVat[i].K_27 != "0")
                {
                    sw.WriteLine("	    <K_28>" + jpkVat[i].K_28 + "</K_28>");
                }
                if (jpkVat[i].K_29 != "0")
                {
                    sw.WriteLine("	    <K_29>" + jpkVat[i].K_29 + "</K_29>");
                }
                if (jpkVat[i].K_30 != "0" || jpkVat[i].K_29 != "0")
                {
                    sw.WriteLine("	    <K_30>" + jpkVat[i].K_30 + "</K_30>");
                }
                if (jpkVat[i].K_31 != "0")
                {
                    sw.WriteLine("	    <K_31>" + jpkVat[i].K_31 + "</K_31>");
                }
                if (jpkVat[i].K_32 != "0")
                {
                    sw.WriteLine("	    <K_32>" + jpkVat[i].K_32 + "</K_32>");
                }
                if (jpkVat[i].K_33 != "0")
                {
                    sw.WriteLine("	    <K_33>" + jpkVat[i].K_33 + "</K_33>");
                }
                if (jpkVat[i].K_34 != "0")
                {
                    sw.WriteLine("	    <K_34>" + jpkVat[i].K_34 + "</K_34>");
                }
                if (jpkVat[i].K_35 != "0")
                {
                    sw.WriteLine("	    <K_35>" + jpkVat[i].K_35 + "</K_35>");
                }
                if (jpkVat[i].K_36 != "0")
                {
                    sw.WriteLine("	    <K_36>" + jpkVat[i].K_36 + "</K_36>");
                }
                if (jpkVat[i].K_37 != "0")
                {
                    sw.WriteLine("	    <K_37>" + jpkVat[i].K_37 + "</K_37>");
                }
                if (jpkVat[i].K_38 != "0")
                {
                    sw.WriteLine("	    <K_38>" + jpkVat[i].K_38 + "</K_38>");
                }
                if (jpkVat[i].K_39 != "0")
                {
                    sw.WriteLine("	    <K_39>" + jpkVat[i].K_39 + "</K_39>");
                }

                sw.WriteLine("	</SprzedazWiersz>");
                i++;
            }

            if (jpkVat[i].LiczbaWierszySprzedazy != "0")
            {
                sw.WriteLine("	<SprzedazCtrl>");
                sw.WriteLine("		<LiczbaWierszySprzedazy>" + jpkVat[i].LiczbaWierszySprzedazy + "</LiczbaWierszySprzedazy>");
                sw.WriteLine("		<PodatekNalezny>" + jpkVat[i].PodatekNalezny + "</PodatekNalezny>");
                sw.WriteLine("	</SprzedazCtrl>");
                //i = i + 1;
            }
            i = i + 1;

            while (jpkVat[i].typZakupu == "G")
            {
                sw.WriteLine("	<ZakupWiersz>");
                sw.WriteLine("		<LpZakupu>" + jpkVat[i].LpZakupu + "</LpZakupu>");
                if (jpkVat[i].NrDostawcy != "")
                {
                    sw.WriteLine("		<NrDostawcy>" + jpkVat[i].NrDostawcy + "</NrDostawcy>");
                }
                else
                {
                    sw.WriteLine("		<NrDostawcy>" + "Brak" + "</NrDostawcy>");
                }
                if (jpkVat[i].NazwaDostawcy != "")
                {
                    sw.WriteLine("		<NazwaDostawcy>" + jpkVat[i].NazwaDostawcy + "</NazwaDostawcy>");
                }
                else
                {
                    sw.WriteLine("		<NazwaDostawcy>" + "Brak" + "</NazwaDostawcy>");
                }
                if (jpkVat[i].AdresDostawcy != "")
                {
                    sw.WriteLine("		<AdresDostawcy>" + jpkVat[i].AdresDostawcy + "</AdresDostawcy>");
                }
                else
                {
                    sw.WriteLine("		<AdresDostawcy>" + "Brak" + "</AdresDostawcy>");
                }
                if (jpkVat[i].DowodZakupu != "")
                {
                    sw.WriteLine("		<DowodZakupu>" + jpkVat[i].DowodZakupu + "</DowodZakupu>");
                }
                if (jpkVat[i].DataZakupu != "")
                {
                    sw.WriteLine("		<DataZakupu>" + jpkVat[i].DataZakupu.Substring(0, 10) + "</DataZakupu>");
                }


                if (jpkVat[i].K_43 != "0")
                {
                    sw.WriteLine("	    <K_43>" + jpkVat[i].K_43 + "</K_43>");
                }
                if (jpkVat[i].K_44 != "0" || jpkVat[i].K_43 != "0")
                {
                    sw.WriteLine("	    <K_44>" + jpkVat[i].K_44 + "</K_44>");
                }
                if (jpkVat[i].K_45 != "0")
                {
                    sw.WriteLine("	    <K_45>" + jpkVat[i].K_45 + "</K_45>");
                }
                if (jpkVat[i].K_46 != "0" || jpkVat[i].K_45 != "0")
                {
                    sw.WriteLine("	    <K_46>" + jpkVat[i].K_46 + "</K_46>");
                }
                if (jpkVat[i].K_47 != "0")
                {
                    sw.WriteLine("	    <K_47>" + jpkVat[i].K_47 + "</K_47>");
                }
                if (jpkVat[i].K_48 != "0")
                {
                    sw.WriteLine("	    <K_48>" + jpkVat[i].K_48 + "</K_48>");
                }
                if (jpkVat[i].K_49 != "0")
                {
                    sw.WriteLine("	    <K_49>" + jpkVat[i].K_49 + "</K_49>");
                }
                if (jpkVat[i].K_50 != "0")
                {
                    sw.WriteLine("	    <K_50>" + jpkVat[i].K_50 + "</K_50>");
                }
                sw.WriteLine("	</ZakupWiersz>");
                i++;
            }

            if (jpkVat[i].LiczbaWierszyZakupu != "0")
            {
                sw.WriteLine("	<ZakupCtrl>");
                sw.WriteLine("		<LiczbaWierszyZakupow>" + jpkVat[i].LiczbaWierszyZakupu + "</LiczbaWierszyZakupow>");
                //sw.WriteLine("		<PodatekNaliczony>"+ Math.Round(decimal)jpkVat[i].PodatekNaliczony,1).ToString()+"</PodatekNaliczony>");
                sw.WriteLine("		<PodatekNaliczony>" + jpkVat[i].PodatekNaliczony + "</PodatekNaliczony>");
                sw.WriteLine("	</ZakupCtrl>");
            }


            sw.WriteLine("</JPK>");
            sw.Close();
        }


        public static int ileDokSprzedazy(List<JpkCsvWiersz> df)
        {
            int ileDokumentow = 0;
            for (int i = 0; i < df.Count; i++)
            {
                if (df[i].typSprzedazy == "G")
                {
                    ileDokumentow = ileDokumentow + 1;
                }
            }
            return ileDokumentow;
        }

       

        public static decimal podatekNalezny(List<JpkCsvWiersz> df)
        {
            decimal nalezny = 0;
            for (int i = 0; i < df.Count; i++)
            {
                if (df[i].typSprzedazy == "G")
                {
                    nalezny = nalezny + decimal.Parse(df[i].K_16.ToString().Replace(".", ",")) + decimal.Parse(df[i].K_18.ToString().Replace(".", ",")) + decimal.Parse(df[i].K_20.ToString().Replace(".", ",")) + decimal.Parse(df[i].K_24.ToString().Replace(".", ",")) + decimal.Parse(df[i].K_26.ToString().Replace(".", ",")) + decimal.Parse(df[i].K_28.ToString().Replace(".", ",")) + decimal.Parse(df[i].K_30.ToString().Replace(".", ",")) + decimal.Parse(df[i].K_33.ToString().Replace(".", ",")) + decimal.Parse(df[i].K_35.ToString().Replace(".", ",")) + decimal.Parse(df[i].K_36.ToString().Replace(".", ",")) + decimal.Parse(df[i].K_37.ToString().Replace(".", ",")) - decimal.Parse(df[i].K_38.ToString().Replace(".", ",")) - decimal.Parse(df[i].K_39.ToString().Replace(".", ","));
                }
            }
            return nalezny;
        }

        


    }
}
