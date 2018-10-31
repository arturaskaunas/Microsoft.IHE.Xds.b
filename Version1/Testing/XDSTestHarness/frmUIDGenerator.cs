using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Microsoft.XDS.Test
{
    public partial class frmUIDGenerator : Form
    {
        public frmUIDGenerator()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtPrefix_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                int quantity = Convert.ToInt32(txtQuantity.Text);

                StringBuilder UUIDList = new StringBuilder();

                for (int x = 0; x < quantity; x++)
                    UUIDList.AppendLine(String.Format("{0}:{1}", txtPrefix.Text, Guid.NewGuid().ToString()));

                rtfResult.Text = UUIDList.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}