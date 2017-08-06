using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SagraElCoda
{

   


    public partial class FormMENU : Form, MasterForm
    {

        SagraElCoda.Dal.SagraDal _mydal = new SagraElCoda.Dal.SagraDal();
        double _totcomanda;
        Font _fontBold = new Font("Verdana", 14f, FontStyle.Bold);
        Font _fontBoldcors = new Font("Tahoma", 14f, FontStyle.Italic);
        Font _fontNormal = new Font("Verdana", 11f, FontStyle.Bold);
        Font _fontSmall = new Font("Verdana", 7f, FontStyle.Bold);
        Pen mypen = new Pen(Brushes.Black);
        decimal _oldnumcoperti;

        bool _pritnComandaCameriere = true;
        bool _pritnComandaCucina = true;
        bool _pritnComandaCliente = true;
        string _testataComanda;
       
        
        
        bool _ComandaConTotaleXcameriere = false;
        bool _ComandaConTotaleXcucina=false;
        bool _ComandaConTotaleXCliente = false;

        double _singlePriceCoperto;

        public double TotComanda
        {
            get { return _totcomanda; }
            set { _totcomanda = value;
            lbltotcomanda.Text = "Totale comanda: " +  _totcomanda.ToString();
            }
        }

        public int CodiceComanda
        {
            get {

              return   _mydal.GetMaxComanda() + 1;
               

            }
        }
         
        public void AddToTotal(double priceelement)
        {
            TotComanda += priceelement;

        }

        public void RemoveTotal(double priceelemnt)
        {
            TotComanda = _totcomanda - priceelemnt;
        }




        public FormMENU()
        {
            InitializeComponent();

            CaricaMenu();
     
            //CaricaCategoria(Categoria.Antipasti,flowLayoutPanel1); //antipasti
            //CaricaCategoria(Categoria.Primi, flowLayoutPanel2); //primi
            //CaricaCategoria(Categoria.Secondi, flowLayoutPanel3); //primi
            //CaricaCategoria(Categoria.Contorni, flowLayoutPanel4); //primi
            //CaricaCategoria(Categoria.Pizze, flowLayoutPanel5); //primi
            //CaricaCategoria(Categoria.Pizze_Dolci, flowLayoutPanel6); //primi
            CaricaCategoria(Categoria.Bevande, flowLayoutPanel7);
            CaricaCategoria(Categoria.Vini, flowLayoutPanel8); 

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.PerformAutoScale();
            tableLayoutPanel1.PerformLayout();
            _totcomanda = 0; _oldnumcoperti = 0;



            UpdatePanelUp();

              _pritnComandaCameriere =  Convert.ToBoolean(  System.Configuration.ConfigurationManager.AppSettings["stampaComandaCameriere"] );
              _pritnComandaCucina =   Convert.ToBoolean(  System.Configuration.ConfigurationManager.AppSettings["stampaComandaCucina"] );
              _pritnComandaCliente = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["stampaComandaCliente"]);

              _testataComanda = System.Configuration.ConfigurationManager.AppSettings["testataComanda"];


              _ComandaConTotaleXcameriere = false;//Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["ComandaConTotaleXcameriere"]);
              _ComandaConTotaleXcucina = false; //Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["ComandaConTotaleXcucina"]);
              _ComandaConTotaleXCliente = false;// Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["ComandaConTotaleXCliente"]);

              _singlePriceCoperto = _mydal.PriceCoperto();
        }


        void CaricaMenu()
        {
            DataTable dt = _mydal.GetAllMenu();
            foreach (DataRow dr in dt.Rows)
            {
                itemMenu it = new itemMenu(dr["descrizioneMenu"].ToString(), 999999) { prezzo = Convert.ToDouble(dr["prezzo"]), id = Convert.ToInt32(dr["id"]), descrizione = dr["descrizioneMenu"].ToString(),isMenuCompleto=true };
                flowLayoutPanel1.Controls.Add(it);

            }
        }
        void CaricaCategoria(Categoria idcat, FlowLayoutPanel type)
        {
         type.Tag = idcat;
         DataTable dt =   _mydal.GetAlimentiByCategoria((int)idcat);
 
         foreach (DataRow dr in dt.Rows)
         {
             itemMenu it = new itemMenu(dr["descrizione"].ToString(), Convert.ToInt32(dr["quantit"])) { prezzo = Convert.ToDouble(dr["prezzo"]), id = Convert.ToInt32(dr["id"]), descrizione = dr["descrizione"].ToString() };
             type.Controls.Add(it);
     
         }
        }

        private void btnStampaComanda_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                if (numericCoperti.Value <= 0)
                {
                    MessageBox.Show("Inserire numero coperti!!!!");
                    numericCoperti.Focus();
                    return;
                }
            }

            //controllo se ci sono sconti da fare
            bool scontanto = false;
            double _mysconto = 0d;
            try
            {
                  _mysconto = Convert.ToDouble(txtSconto.Text);
                RemoveTotal(_mysconto);
                scontanto = true;

            }
            catch {
                scontanto = false;
                _mysconto = 0d;
            }
            //
            PrintDocument pd = new PrintDocument();
            //per cameriere
          if (_pritnComandaCameriere)
            {
               
                pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                pd.Print();
            }

            ////per cucina
            if (_pritnComandaCucina)
            {
                pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPageCousin);
                pd.Print();
            }

            ///per cliente
            ///
          /*  if (_pritnComandaCliente)
            {
                pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPageCLIENTE);
                pd.Print();
            }
            */

           DialogResult dr =  MessageBox.Show("Stampa eseguita correttamente?", "Info su stampa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
           if (dr == DialogResult.Yes)
           { //devo salvare il tutto nel DB  
               List<SagraElCoda.Dal.ComandaItem> listaDiComand = new List<SagraElCoda.Dal.ComandaItem>();
               foreach (Control c in this.Controls)
               {
                   if (c is TableLayoutPanel)
                   {
                       Control[] adesso = c.Controls.Find("itemMenu", true);
                       foreach (itemMenu ii in adesso)
                       {
                           SagraElCoda.Dal.ComandaItem addedd = ii.DetailsComandaDB;
                           if(addedd!= null)
                              listaDiComand.Add(addedd);
                       }
                   }
               }
               SagraElCoda.Dal.ComandaPrinc myPric = new SagraElCoda.Dal.ComandaPrinc();
               myPric.dataarr = DateTime.Now;
               myPric.ncoperti = (int)numericCoperti.Value;
               myPric.totPriceComanda = _totcomanda;

               //coperto!
               listaDiComand.Add(new SagraElCoda.Dal.ComandaItem() { IdAlimento = 10, totPrice = Convert.ToInt64(numericCoperti.Value) * _singlePriceCoperto, Quant = Convert.ToInt32(numericCoperti.Value)  });

               _mydal.AddComanda(myPric, listaDiComand,_mysconto);

               UpdatePanelUp();
               SvuotaNumeric();
               this.Focus();
           }
        }

        void SvuotaNumeric()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TableLayoutPanel)
                {
                    Control[] adesso = c.Controls.Find("itemMenu", true);
                    foreach (itemMenu ii in adesso)
                    {
                        ii.ResetControl();
                    }
                }
            }

            numericCoperti.Value = 0;
            _oldnumcoperti = 0;
            TotComanda = 0;
            txtSconto.Text = "(Sconto)";
        }

        void UpdatePanelUp()
        { 
               lblnumcomande.Text = "Comanda fatta: "+ _mydal.GetMaxComanda().ToString();
               lbltotincasso.Text = "Totale incassato Eur: "+_mydal.TotGiornataActualIncasso().ToString();
               lbtotlcoperti.Text = "Coperti fatti:" + _mydal.TotGiornataActualNcoperti().ToString();
        }


        private void pd_PrintPageCousin(object sender, PrintPageEventArgs ev)
        {

            ev.Graphics.DrawRectangle(mypen, 8, 8, 795, 105);
            ev.Graphics.DrawImage(Image.FromFile(System.Configuration.ConfigurationSettings.AppSettings["logocip"]), new Point(10, 10));
            ev.Graphics.DrawString(_testataComanda, _fontBold, Brushes.Black, 50, 15);
            ev.Graphics.DrawString("COMANDA NUMERO: " + CodiceComanda.ToString() + "         COPERTI N. " + numericCoperti.Value.ToString() + "  - TAVOLO N. ______", _fontBold, Brushes.Black, 50, 45);
            ev.Graphics.DrawString("Arrivo ore: " + DateTime.Now.ToString("HH:mm")   + " COPIA PER CUCINA - NOME CAMERIERE __________________ ", _fontBoldcors, Brushes.Black, 50, 71);


            float starty = 125f;

            System.Collections.Specialized.NameValueCollection idmensu = GetIDSMenu( flowLayoutPanel1.Controls.Find("itemMenu", true)); 

            starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Antipasti, idmensu, _ComandaConTotaleXcucina), "ANTIPASTI", starty, ev);
           
            starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Primi, idmensu, _ComandaConTotaleXcucina), "PRIMI", starty, ev);
            starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Secondi, idmensu, _ComandaConTotaleXcucina), "SECONDI", starty, ev);
            starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Contorni, idmensu, _ComandaConTotaleXcucina), "CONTORNI", starty, ev);
            starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Pizze, idmensu, _ComandaConTotaleXcucina), "PIZZE", starty, ev);
            starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Dolci, idmensu, _ComandaConTotaleXcucina), "DOLCI", starty, ev);
            starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Bevande, flowLayoutPanel7, _ComandaConTotaleXcameriere), "BEVANDE", starty, ev);
            starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Vini, flowLayoutPanel8, _ComandaConTotaleXcameriere), "VINI", starty, ev);

            starty += 5f;
          //  ev.Graphics.DrawRectangle(mypen, 8, starty, 795, 45);
          //  ev.Graphics.DrawString(lbltotcomanda.Text + "  Euro", _fontBold, Brushes.Black, 50, starty + 10);


        }


      /*  //comada che va in mano al cliente
        private void pd_PrintPageCLIENTE(object sender, PrintPageEventArgs ev)
        {

            ev.Graphics.DrawRectangle(mypen, 8, 8, 795, 105);
            ev.Graphics.DrawImage(Image.FromFile(System.Configuration.ConfigurationSettings.AppSettings["logocip"]), new Point(10, 10));
            ev.Graphics.DrawString(_testataComanda, _fontBold, Brushes.Black, 50, 15);
            ev.Graphics.DrawString("COMANDA NUMERO: " + CodiceComanda.ToString() + "         COPERTI N. " + numericCoperti.Value.ToString() , _fontBold, Brushes.Black, 50, 45);
            ev.Graphics.DrawString("Arrivo ore: " + DateTime.Now.ToString("HH:mm")   + " COPIA CLIENTE", _fontBoldcors, Brushes.Black, 50, 71);


            float starty = 125f;
            if(_ComandaConTotaleXCliente)
               starty = PrinteItemsComanda( string.Format( "Coperto Eur. {0} {1}" , Convert.ToDouble( numericCoperti.Value)*_singlePriceCoperto, Environment.NewLine), "COPERTO", starty, ev);
            
            starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Antipasti, flowLayoutPanel1,_ComandaConTotaleXCliente), "ANTIPASTI", starty, ev);
            starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Primi, flowLayoutPanel2, _ComandaConTotaleXCliente), "PRIMI", starty, ev);
            starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Secondi, flowLayoutPanel3, _ComandaConTotaleXCliente), "SECONDI", starty, ev);
            starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Contorni, flowLayoutPanel4, _ComandaConTotaleXCliente), "CONTORNI", starty, ev);
            starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Pizze, flowLayoutPanel5, _ComandaConTotaleXCliente), "PIZZE", starty, ev);
            starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Dolci, flowLayoutPanel6, _ComandaConTotaleXCliente), "DOLCI", starty, ev);
            starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Bevande, flowLayoutPanel7, _ComandaConTotaleXCliente), "BEVANDE", starty, ev);
            starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Vini, flowLayoutPanel8, _ComandaConTotaleXCliente), "VINI", starty, ev);

            starty += 5f;
            ev.Graphics.DrawRectangle(mypen, 8, starty, 795, 45);
            ev.Graphics.DrawString(lbltotcomanda.Text + "  Euro " , _fontBold, Brushes.Black, 50, starty + 10);


            ev.Graphics.DrawString(System.Configuration.ConfigurationManager.AppSettings["promo"], _fontSmall, Brushes.Black, 50, starty + 29);




        }
        */
        // la comanda del cameriere
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
           
            ev.Graphics.DrawRectangle(mypen, 8, 8, 795, 105); 
            ev.Graphics.DrawImage(Image.FromFile(System.Configuration.ConfigurationSettings.AppSettings["logocip"]),new Point(10,10));
            ev.Graphics.DrawString(_testataComanda, _fontBold, Brushes.Black, 50, 15);
            ev.Graphics.DrawString("COMANDA NUMERO: " + CodiceComanda.ToString() + "         COPERTI N. " + numericCoperti.Value.ToString() + "  - TAVOLO N. ______", _fontBold, Brushes.Black, 50, 45);
            ev.Graphics.DrawString("Arrivo ore: "  +DateTime.Now.ToString("HH:mm") + " COPIA PER CAMERIERE ", _fontBoldcors, Brushes.Black, 50, 71);


             float starty =125f;
             System.Collections.Specialized.NameValueCollection idmensu = GetIDSMenu(flowLayoutPanel1.Controls.Find("itemMenu", true));

             starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Antipasti, idmensu, _ComandaConTotaleXcucina), "ANTIPASTI", starty, ev);

             starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Primi, idmensu, _ComandaConTotaleXcucina), "PRIMI", starty, ev);
             starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Secondi, idmensu, _ComandaConTotaleXcucina), "SECONDI", starty, ev);
             starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Contorni, idmensu, _ComandaConTotaleXcucina), "CONTORNI", starty, ev);
             starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Pizze, idmensu, _ComandaConTotaleXcucina), "PIZZE", starty, ev);
             starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Dolci, idmensu, _ComandaConTotaleXcucina), "DOLCI", starty, ev);
             starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Bevande, flowLayoutPanel7, _ComandaConTotaleXcameriere), "BEVANDE", starty, ev);
             starty = PrinteItemsComanda(GetLinesByCategory(Categoria.Vini, flowLayoutPanel8, _ComandaConTotaleXcameriere), "VINI", starty, ev);

             starty += 5f;
            // ev.Graphics.DrawRectangle(mypen, 8, starty, 795, 45);
            // ev.Graphics.DrawString(lbltotcomanda.Text + "  Euro", _fontBold, Brushes.Black,50, starty+10);

            
        }
      
        float PrinteItemsComanda(string lines, string title,float startyy, PrintPageEventArgs evve)
        {
            if (!string.IsNullOrEmpty(lines))
            {
                evve.Graphics.DrawString(title, _fontBold, Brushes.Black, 38, startyy);
                startyy += 22;
                evve.Graphics.DrawLine(mypen, 10, startyy, 455, startyy);
                startyy += 20;
                evve.Graphics.DrawString(lines, _fontNormal, Brushes.Black, 38, startyy);
                Regex myreg = new Regex(Environment.NewLine);

                startyy += 18 * myreg.Matches(lines).Count;
            }
            return startyy+8;
        }

        string GetLinesByCategory(Categoria cat, System.Collections.Specialized.NameValueCollection col,bool printPrezziAccanto)
        {
            string ret="";

            foreach(string s in col.AllKeys){
           DataTable dt =  _mydal.GetAllAlimentistatisticheXMenuXcategoria(Convert.ToInt32( s)  ,Convert.ToInt32(cat));
           if (dt == null) return "";
           foreach (DataRow dr in dt.Rows)
           {
               ret += string.Format(" N. {0}  -  {1} {2}",col[s] , dr["descrizione"].ToString(), Environment.NewLine);
           }
                  // GetDetailsLineForComanda( flow.Controls.Find("itemMenu", true),printPrezziAccanto);
            }
            return ret;
        }
        


       System.Collections.Specialized.NameValueCollection GetIDSMenu(Control[] arraydicon)
        {
            System.Collections.Specialized.NameValueCollection not = new System.Collections.Specialized.NameValueCollection();
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if (c is itemMenu)
                {
                    itemMenu tmpc = (itemMenu)c;
                    if (tmpc.quantiSel() > 0)
                       not.Add( tmpc.id.ToString(), tmpc.quantiSel().ToString());
                }
            }
            return not;
        }


       string GetLinesByCategory(Categoria cat, FlowLayoutPanel flow, bool printPrezziAccanto)
       {
           string ret = "";


           ret = GetDetailsLineForComanda(flow.Controls.Find("itemMenu", true), printPrezziAccanto);

           return ret;
       }



       string GetDetailsLineForComanda(Control[] arraydicon, bool prezziaccanto)
       {
           string ret = "";
           foreach (Control c in arraydicon)
           {
               if (c is itemMenu)
               {
                   itemMenu tmpc = (itemMenu)c;
                   ret += (prezziaccanto) ? tmpc.GetDescriptionForPseudoConto : tmpc.GetDescriptionForComanda;
               }
           }
           return ret;
       }

        private void numericCoperti_ValueChanged(object sender, EventArgs e)
        {
            //aggiungere il coperto! 
            if (_oldnumcoperti > 0)
            {
                RemoveTotal(Convert.ToInt64(_oldnumcoperti) * _singlePriceCoperto);
            }
            if (numericCoperti.Value > 0)
            {
                _oldnumcoperti = numericCoperti.Value;
                AddToTotal(Convert.ToInt64(_oldnumcoperti) * _singlePriceCoperto);
            }

        }



        private void btnStatistiche_Click(object sender, EventArgs e)
        {
            //da qui apre il pannello dell statistiche
            new FormStatistiche().Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            numericCoperti.Enabled = true;
            if (checkBox1.Checked)
            {
                numericCoperti.Value = 0;
                numericCoperti.Enabled = false;
            }
        }

       
    }
}
