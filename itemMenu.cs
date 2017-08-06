using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SagraElCoda
{
    public partial class itemMenu : UserControl
    {
        public int id { get; set; }
        public double prezzo { get; set; }
        public string descrizione { get; set; }
        public int idcategoria { get; set; }
        public int qauntitatolaadisp { get; set; }
        public bool isMenuCompleto { get; set; }




        public decimal quantiSel()
        {
            return   numericDecision.Value;
        }

        public SagraElCoda.Dal.ComandaItem DetailsComandaDB
        {
            get {

                if (numericDecision.Value > 0)
                {
                    SagraElCoda.Dal.ComandaItem tmp = new SagraElCoda.Dal.ComandaItem();

                    tmp.IdAlimento = this.id;
                    tmp.Quant = (int)numericDecision.Value;
                    tmp.totPrice = this.GetTotalSpesa;
                    tmp.quantRimasta = this.qauntitatolaadisp;
                    return tmp;
                }
                return null;
            }
        }

        public double GetTotalSpesa
        {
            get {
                return Convert.ToDouble(numericDecision.Value) * prezzo;
            }
        }

        public string GetDescriptionForComanda
        {
            get {
                if(numericDecision.Value>0)
                return string.Format(" N. {0}  -  {1} {2}", numericDecision.Value, descrizione, Environment.NewLine) ;
                return "";
            }
        }

        public string GetDescriptionForPseudoConto
        {
            get
            {
                if (numericDecision.Value > 0)
                return string.Format(" {1} X {0} Eur. {2}{3}", numericDecision.Value, descrizione, GetTotalSpesa, Environment.NewLine);
                return string.Empty;
            }
        }

        public MasterForm GetThisFormOwner
        {
            get {
                if (this.FindForm() is Form2)
                {
                    return (Form2)this.FindForm();
                }
                else if (this.FindForm() is FormFU)
                {
                    return (FormFU)this.FindForm();
                }
                else if (this.FindForm() is FormMENU)
                {
                    return (FormMENU)this.FindForm();
                }
                return null;
            }
        }

        double _oldvaluenumericdecision;

        public itemMenu(string _desc,int qtaRimasta)
        {
            
            InitializeComponent();
            lblDescrizione.Text = _desc;
            _oldvaluenumericdecision = 0;

            if (qtaRimasta <= 0)
            {
                this.numericDecision.Enabled = false;
            }
            this.qauntitatolaadisp = qtaRimasta;
        }

        /// <summary>
        /// Aggiorna i valori numerici, riportandoli a zero!
        /// </summary>
        public void ResetControl()
        {
            _oldvaluenumericdecision = 0;
            numericDecision.Value = 0;
            if (qauntitatolaadisp <= 0) this.numericDecision.Enabled = false;
        }

        private  void numericDecision_ValueChanged(object sender, EventArgs e)
        {
            if (isMenuCompleto) return;

            if (_oldvaluenumericdecision >= 0)
            {
                qauntitatolaadisp += Convert.ToInt32(_oldvaluenumericdecision);
                GetThisFormOwner.RemoveTotal(_oldvaluenumericdecision * prezzo);
            }

            if (numericDecision.Value >= 0)
            {
                _oldvaluenumericdecision = Convert.ToDouble( numericDecision.Value);
                qauntitatolaadisp -= Convert.ToInt32(_oldvaluenumericdecision);
                if (qauntitatolaadisp < 0)
                {
                    MessageBox.Show(lblDescrizione.Text + " FINITO");
                }
                GetThisFormOwner.AddToTotal(_oldvaluenumericdecision * prezzo);
            }
        }

   }
}
