﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
///@author Antti Kuusisto
///version 5.4.2022
/// <summary>
/// Pudotusvalikko päivitää toisen pudodotusvalikon tiedot vastaamaan kohdetta. Kommentoitu
/// </summary>

namespace OppilaitostenTiedot
{
    public partial class OLaiTiForm : Form
    {
        DataTable oppilaitos = new DataTable(); // luodaan uusi DataTable
        DataTable vastuuHenkilot = new DataTable(); // luodaan uusi DataTable
        DataTable yhteys = new DataTable(); // luodaan uusi DataTable
        public OLaiTiForm()
        {
            InitializeComponent();
        }

        private void OLaiTiForm_Load(object sender, EventArgs e) // toiminta, kun ladataan lomake
        {
            taytaOppilaitosTaulukko(); // Kutsutaan metodi joka täyttää oppilaitostaulukon
            taytaVastuuHenkilot(); // Kutsutaan metodi joka täyttää vastuuhenkilöt taulukon
            LaitosCB.DataSource = oppilaitos; //Laitos combobox:n tietolähde
            LaitosCB.DisplayMember = "ONimi"; //Laitos combobox näyttää "ONimi" nimisen jäsenen oppilaitos DataTablesta
        }

        private void LaitosCB_SelectedIndexChanged(object sender, EventArgs e) // toiminta kun muutetaan kyseisessä combobox:ssa olevaa tietoa
        {
            string viite = oppilaitos.Rows[LaitosCB.SelectedIndex]["OID"].ToString(); // Määritetään tunniste jolla etsitään toimihenkilöt kyseiseen laitokseen
            OsLB.Text = oppilaitos.Rows[LaitosCB.SelectedIndex]["OKatuosoite"].ToString(); // Valitaan valitun indeksin mukainentieto samalta riviltä
            PnroLB.Text = oppilaitos.Rows[LaitosCB.SelectedIndex]["OPostinumero"].ToString();
            PtoimiLB.Text = oppilaitos.Rows[LaitosCB.SelectedIndex]["Opostitoimipaikka"].ToString();
            PuhLB.Text = oppilaitos.Rows[LaitosCB.SelectedIndex]["OPuhelin"].ToString();

            yhteys = vastuuHenkilot.Select("OID =" + viite).CopyToDataTable(); // Valitan vastuuhenkilöiden Datatable:sta vastaavalla Id olevat toimihenkilöt
            VastuuhenkiloCB.DataSource = yhteys; // Valitaan oikeat vastuuhenkilöt
            VastuuhenkiloCB.DisplayMember = "VNimi"; // Näytetään combobox:ssa vastuuhenkilön nimi
        }

        private void VastuuhenkiloCB_SelectedIndexChanged(object sender, EventArgs e) // toiminta kun muutetaan kyseisessä combobox:ssa olevaa tietoa
        {
            TitteliLB.Text = yhteys.Rows[VastuuhenkiloCB.SelectedIndex]["VTitteli"].ToString(); // tulostetetaan oikea tieto valitun kohteen riviltä
            SijaintiLB.Text = yhteys.Rows[VastuuhenkiloCB.SelectedIndex]["VSijainti"].ToString();
            EmailLB.Text = yhteys.Rows[VastuuhenkiloCB.SelectedIndex]["VSahkoposti"].ToString();
            PhoneLB.Text = yhteys.Rows[VastuuhenkiloCB.SelectedIndex]["VPuhelin"].ToString();
        }
        private void taytaOppilaitosTaulukko() // oppilaitos DataTablen täyttö
        {   
            //Datatable:n Columns osioon tunniste tiedot
            oppilaitos.Columns.Add("OID",typeof(int));
            oppilaitos.Columns.Add("ONimi");
            oppilaitos.Columns.Add("OKatuosoite");
            oppilaitos.Columns.Add("OPostinumero");
            oppilaitos.Columns.Add("Opostitoimipaikka");
            oppilaitos.Columns.Add("OPuhelin");
            // Datatable:n rows osioon kohteen tiedot
            oppilaitos.Rows.Add(1, "StadinAO", "Hattulantie 2", "00550", "Helsinki", "09 310 8600");
            oppilaitos.Rows.Add(2, "Omnia", "Armas Launiksen katu 9", "02650", "Espoo", "046 877 1371");
            oppilaitos.Rows.Add(3, "Varia", "Rälssitie 13", "01530", "Vantaa", "040 182 4668");
            oppilaitos.Rows.Add(4, "Keuda", "Sibeliuksenväylä 55 A", "04400", "Järvenpää", "09 27 381");
        }

        private void taytaVastuuHenkilot() //Täytetään vastuuhenkilöt DataTable 
        {
            //Datatable:n Columns osioon tunniste tiedot
            vastuuHenkilot.Columns.Add("OID",typeof(int));
            vastuuHenkilot.Columns.Add("VNimi");
            vastuuHenkilot.Columns.Add("VTitteli");
            vastuuHenkilot.Columns.Add("VSijainti");
            vastuuHenkilot.Columns.Add("VSahkoposti");
            vastuuHenkilot.Columns.Add("VPuhelin");
            // Datatable:n rows osioon kohteen tiedot
            vastuuHenkilot.Rows.Add(1, "Sirpa Lindroos", "Rehtori", "Kampus 1", "sirpa.lindroos@hel.fi", "050 540 4434");
            vastuuHenkilot.Rows.Add(1, "Hanna Laurila", "Rehtori", "Kaupus 2", "hanna.laurila@hel.fi", "040 676 5583");
            vastuuHenkilot.Rows.Add(1, "Annele Ranta", "Rehtori", "Kampus 3", "annele.ranta@hel.fi", "040 631 5667");
            vastuuHenkilot.Rows.Add(1, "Eeva Sahlman", "Rehtori", "Kampus 4", "eeva.sahlman@hel.fi", "040 336 1017");
            vastuuHenkilot.Rows.Add(1, "Marko Aaltonen", "Rehtori", "Kampus 5", "marko.aaltonen@hel.fi", "050 511 8115");
            vastuuHenkilot.Rows.Add(2, "Tuula Antola", "Koulutuskuntayhtymän johtaja", "Yleishallinto", "tuula.antola@omnia.fi", "");
            vastuuHenkilot.Rows.Add(2, "Tapio Siukonen", "Hallintojohtaja", "Yleishallinto", "tapio.siukonen@omnia.fi", "044 369 6600");
            vastuuHenkilot.Rows.Add(2, "Tuukko Soini", "Kehittämisjohtaja", "Yleishallinto", "tuukka.soini@omnia.fi", "046 877 2952");
            vastuuHenkilot.Rows.Add(2, "Riikka-Maria Yli-Suomu", "Liiketoimintajohtaja", "Elinvoima- ja työllisyyspalvelut", "riikka-maria.yli-suomu@omnia.fi", "050 348 6544");
            vastuuHenkilot.Rows.Add(2, "Maija Aaltola", "Rehtori", "Koulutus- ja opiskelijapalvelut", "maija-aaltola@omnia.fi", "050 384 9354");
            vastuuHenkilot.Rows.Add(2, "Kai Iivari", "Talousjohtaja", "Talous ja hankintapalvelut", "kai.iivari@omnia.fi", "0400 306 691");
            vastuuHenkilot.Rows.Add(2, "Päivi Korhonen", "Viestintäjohtaja", "Viestintä- ja markkinointipalvelut", "paivi.korhonen@omnia.fi", "040 126 7599");
            vastuuHenkilot.Rows.Add(3, "Pekka Tauriainen", "Rehtori", "", "pekka.tauriainen@vantaa.fi", "050 312 4514");
            vastuuHenkilot.Rows.Add(3, "Anne Heinonen", "Työelämäpalvelupäällikkö", "", "anne.heinonen@vantaa.fi", "040 524 1242");
            vastuuHenkilot.Rows.Add(3, "Tuula Kiistinen", "Yhteisten palveluiden päällikkö", "", "tuula.kiiskinen@vantaa.fi", "040 193 9048");
            vastuuHenkilot.Rows.Add(4, "Tiina Halmevuo", "Kuntayhtymän johtaja", "Hallinto- ja johtamispalvelut", "tiina.halmevuo@keuda.fi", "050 336 9709");
            vastuuHenkilot.Rows.Add(4, "Anna Mari Leinonen", "Rehtori", "Hallinto", "annamari.leinonen@keuda.fi", "040 174 4523");
            vastuuHenkilot.Rows.Add(4, "Anne Vuorinen", "Johtaja", "Yritys- ja elinvoimapalvelut", "anne.vuorinen@keuda.fi", "050 415 0974");
            vastuuHenkilot.Rows.Add(4, "Hanna Nyrönen", "Viestintä- ja markkinointipäällikkö", "Viestintäpalvelut", "hanna.nyronen@keuda.fi", "040 521 8428");
            vastuuHenkilot.Rows.Add(4, "Maarit Flinck", "Asianhallintapäällikkö", "Hallinto- ja johtamispalvelut", "maarit.flinck@keuda.fi", "0500 837 357");
        }
    }
}
