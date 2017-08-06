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
    public partial class FormStatistiche : Form
    {
        SagraElCoda.Dal.SagraDal sd = new SagraElCoda.Dal.SagraDal();

        public FormStatistiche()
        {
            InitializeComponent();
        }

        private void btnAlimenti_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sd.GetAllAlimentistatistiche();
            dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sd.GetTotaleComandaPerGiorno();
            dataGridView1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sd.GetTotaleCopertiComandaPerGiorno();
            dataGridView1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sd.GetAllAlimentistatisticheXCategoria();
            dataGridView1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sd.GetQuantitRimaste();
            dataGridView1.Refresh();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sd.GetAlimentiAttiviPerGiorno(); dataGridView1.Refresh();
        }
    }
}
