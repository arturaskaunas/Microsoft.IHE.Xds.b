using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;
using System.IO;

namespace Microsoft.XDS.Test
{
    public partial class frmMetadataConversion : Form
    {
        public frmMetadataConversion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtFileToTransform.Text))
                ExecuteTransformation(txtFileToTransform.Text);
        }

        private bool ExecuteTransformation(string initialdirectory)
        {
            try
            {
                XslCompiledTransform  xTransf = new XslCompiledTransform();
                XmlDocument stylesheetDoc = new XmlDocument(); // style sheet document
                stylesheetDoc.Load(System.Configuration.ConfigurationManager.AppSettings["ebRIMTransformFile"].ToString());
                xTransf.Load(stylesheetDoc, new XsltSettings(true,true),new XmlUrlResolver());
                StringBuilder resultLog = new StringBuilder();

                DirectoryInfo myRoot = new DirectoryInfo(initialdirectory);

                foreach (FileInfo myFile in myRoot.GetFiles("*.xml"))
                {
                    string inputfile = myFile.FullName;
                    string outputfile = myFile.FullName.Replace(@"\input\",@"\output\").Replace(".xml", "_30.xml");

                    xTransf.Transform(inputfile, outputfile);

                    resultLog.AppendLine(outputfile + " created.");

                    rtfResult.Text = resultLog.ToString();
                }
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }

        }

        private void btnFindFile_Click(object sender, EventArgs e)
        {
            if (dlgFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtFileToTransform.Text = dlgFolderBrowser.SelectedPath;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmIheTstHarness frm = new frmIheTstHarness();
            frm.ShowDialog();
        }
    }
}