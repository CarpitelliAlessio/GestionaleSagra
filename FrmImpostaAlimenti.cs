using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SagraElCoda
{
    public partial class FrmImpostaAlimenti : Form
    {
        SagraElCoda.Dal.SagraDal ssd = new SagraElCoda.Dal.SagraDal();
        public FrmImpostaAlimenti()
        {
            InitializeComponent();

            listView1.CheckBoxes = true;
            DataTable dt =   ssd.GetAllAlimenti();
            foreach (DataRow dr in dt.Rows)
            {
                ListViewItem myit = new ListViewItem();
                myit.Text = dr["descrizione"].ToString();
                
                myit.Tag = dr["id"];
                myit.Checked = Convert.ToBoolean(dr["attivo"]);

                listView1.Items.Add(myit);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in listView1.Items)
            {
                int id = Convert.ToInt32(li.Tag);
                int attivo = (li.Checked) ? 1 : 0;
                ssd.AggiornaAlimentoAttivo(id, attivo);
            }
            MessageBox.Show("Aggiornamento effettuato");
        }
    }
}
