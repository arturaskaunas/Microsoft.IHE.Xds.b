using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.XPath;

using WCF = System.ServiceModel.Channels;

namespace Microsoft.XDS.Test
{
    public partial class frmIheTstHarness : Form
    {
        const string PROVIDEANDREGISTERDOCUMENTSETB_WSAACTION = "urn:ihe:iti:2007:ProvideAndRegisterDocumentSet-b";
        const string REGISTERDOCUMENTSETB_WSAACTION = "urn:ihe:iti:2007:RegisterDocumentSet-b";
        const string RETRIEVEDOCUMENTSET_WSAACTION = "urn:ihe:iti:2007:RetrieveDocumentSet";
        const string STOREDQUERY_WSAACTION = "urn:ihe:iti:2007:RegistryStoredQuery";
        const string PATIENT_REVISED_WSAACTION = "urn:hl7-org:v3:PRPA_IN201302UV";
        const string PATIENT_ADD_WSAACTION = "urn:hl7-org:v3:PRPA_IN201301UV";
        const string PATIENT_DUPLICATES_WSAACTION = "urn:hl7-org:v3:PRPA_IN201304UV";

        static readonly WCF.MessageVersion MESSAGE_VERSION = WCF.MessageVersion.Soap12WSAddressing10;

        private XmlDocument PRDocumentMetadata;
        private XmlDocument RDocumentMetadata;
        BindingList<DocumentRequest> docRequestSrc;


        public frmIheTstHarness()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPRMetadataFile.Text))
            {
                byte[] document = LoadFile(txtPRMetadataFile.Text);

                //StringBuilder str = new StringBuilder();
                //for (int x = 0; x < document.Length; x++)
                //    str.Append(document[x].ToString("X2"));

                rtfPRMetadataContent.Text = Convert.ToBase64String(document);

            }
        }

        private void btnPRMetadataGet_Click(object sender, EventArgs e)
        {
            txtPRMetadataFile.Text = GetFileName();
            try
            {
                PRDocumentMetadata = new XmlDocument();
                PRDocumentMetadata.Load(txtPRMetadataFile.Text);
                XmlNodeList docs = PRDocumentMetadata.SelectNodes(@"//*[local-name()='ExtrinsicObject']");
                switch (docs.Count)
                {
                    case 0:
                        txtPRDocument1.Enabled = false;
                        btnPRDocument1Get.Enabled = false;

                        txtPRDocument2.Enabled = false;
                        btnPRDocument2Get.Enabled = false;
                        break;
                    case 1:
                        txtPRDocument1.Enabled = true;
                        btnPRDocument1Get.Enabled = true;
                        
                        txtPRDocument2.Enabled = false;
                        btnPRDocument2Get.Enabled = false;
                        break;
                    default:
                        txtPRDocument1.Enabled = true;
                        btnPRDocument1Get.Enabled = true;
                        
                        txtPRDocument2.Enabled = true;                        
                        btnPRDocument2Get.Enabled = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        static byte[] LoadFile(string filename)
        {
            // Open the file as a FileStream object.
            FileStream infile = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);            
            byte[] document = new byte[infile.Length];
            // Read the file to ensure it is readable.
            int count = infile.Read(document, 0, document.Length);
            if (count != document.Length)
            {
                infile.Close();
                Console.WriteLine("Test Failed: Unable to read data from file");
                return null;
            }
            infile.Close();

            return document;
        }

        private string GetFileName()
        {
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                return dlgOpenFile.FileName;
            }
            else
            {
                return "";
            }
        }

        private void btnPRDocument1Get_Click(object sender, EventArgs e)
        {
            txtPRDocument1.Text = GetFileName();
        }

        private void btnPRDocument2Get_Click(object sender, EventArgs e)
        {
            txtPRDocument2.Text = GetFileName();
        }

        private void btnPRExecute_Click(object sender, EventArgs e)
        {
            try
            {
                string innerXml = null;

                if (PRDocumentMetadata != null)
                {
                    ProvideAndRegisterDocumentSetRequest request = new ProvideAndRegisterDocumentSetRequest();
                    request.DocumentMetadata = PRDocumentMetadata.SelectSingleNode(@".//*[local-name()='SubmitObjectsRequest']");
                    if (!(String.IsNullOrEmpty(txtPRPatientID.Text)))
                    {
                        innerXml = request.DocumentMetadata.InnerXml;
                        innerXml = innerXml.Replace("$PatientId", txtPRPatientID.Text.Replace("&", "&amp;"));
                        request.DocumentMetadata.InnerXml = innerXml;
                        //request.DocumentMetadata.InnerXml = request.DocumentMetadata.InnerXml.Replace("$PatientId", txtPRPatientID.Text);
                    }

                    XmlNodeList docs = request.DocumentMetadata.SelectNodes(@"//*[local-name()='ExtrinsicObject']");

                    if (txtPRDocument1.Enabled == true && !string.IsNullOrEmpty(txtPRDocument1.Text))
                    {
                        IHEDocument doc1 = new IHEDocument();
                        doc1.DocumentID = docs.Item(0).Attributes["id"].Value;
                        doc1.Document = LoadFile(txtPRDocument1.Text);
                        docs.Item(0).Attributes["mimeType"].Value = GetMimeType(txtPRDocument1.Text);
                        request.Documents.Add(doc1);
                    }

                    if (txtPRDocument2.Enabled == true && !string.IsNullOrEmpty(txtPRDocument2.Text))
                    {
                        IHEDocument doc2 = new IHEDocument();
                        doc2.DocumentID = docs.Item(1).Attributes["id"].Value;
                        doc2.Document = LoadFile(txtPRDocument2.Text);
                        docs.Item(1).Attributes["mimeType"].Value = GetMimeType(txtPRDocument2.Text);
                        request.Documents.Add(doc2);
                    }

                    WCF.Message msgInput, msgOutput;
                    msgInput = WCF.Message.CreateMessage(MESSAGE_VERSION
                                                         , PROVIDEANDREGISTERDOCUMENTSETB_WSAACTION
                                                         , request);
                    msgOutput = WCF.Message.CreateMessage(WCF.MessageVersion.Soap12WSAddressing10, "");
                    
                    XDSRepository.XDSRepositoryClient client = new XDSRepository.XDSRepositoryClient(GetRepositoryEndPointName());
                    
                    msgOutput = client.ProvideAndRegisterDocumentSet(msgInput);

                    XmlDictionaryReader rdr = msgOutput.GetReaderAtBodyContents();

                    XmlDocument result = new XmlDocument();
                    result.Load(rdr);

                    XmlNode fault = result.SelectSingleNode(@"//*[local-name()='Fault']");
                    XmlNode errorList = result.SelectSingleNode(@"//*[local-name()='RegistryErrorList']");

                    if (fault != null)
                    {
                        lblPRResult.Text = "Error occurred when executing web service.";
                        rtfPRMetadataContent.Text = fault.OuterXml;
                    }
                    else
                    {
                        if (errorList != null)
                        {
                            lblPRResult.Text = "Transaction resulted in Error!";
                            rtfPRMetadataContent.Text = result.OuterXml;
                        }
                        else
                        {
                            lblPRResult.Text = "Transaction succeded!";
                            rtfPRMetadataContent.Text = result.OuterXml;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btRMetadataFileGet_Click(object sender, EventArgs e)
        {
            try
            {
                txtRMetadataFile.Text = GetFileName();
                if (!string.IsNullOrEmpty(txtRMetadataFile.Text))
                {
                    RDocumentMetadata = new XmlDocument();
                    RDocumentMetadata.Load(txtRMetadataFile.Text);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnRExecute_Click(object sender, EventArgs e)
        {
            RDocumentMetadata.DocumentElement.InnerXml = (String.IsNullOrEmpty(txtRPatientID.Text)) ?
            RDocumentMetadata.DocumentElement.InnerXml:
            RDocumentMetadata.DocumentElement.InnerXml.Replace("$PatientId", txtRPatientID.Text.Replace("&", "&amp;"));

            WCF.Message msgInput, msgOutput;
            msgInput = WCF.Message.CreateMessage(MESSAGE_VERSION
                                                 , REGISTERDOCUMENTSETB_WSAACTION
                                                 , new XmlNodeReader(RDocumentMetadata));
            msgOutput = WCF.Message.CreateMessage(MESSAGE_VERSION, "");

            XDSRegistry.XDSRegistryClient client = new XDSRegistry.XDSRegistryClient(GetRegistryEndPointName());
            
            msgOutput = client.RegisterDocumentSet(msgInput);

            XmlDictionaryReader rdr = msgOutput.GetReaderAtBodyContents();

            XmlDocument result = new XmlDocument();
            result.Load(rdr);

            XmlNode fault = result.SelectSingleNode(@"//*[local-name()='Fault']");
            XmlNode errorList = result.SelectSingleNode(@"//*[local-name()='RegistryErrorList']");

            if (fault != null)
            {
                lblRegisterResult.Text = "Error occurred when executing web service.";
                rtfRMetadatContent.Text = fault.OuterXml;
            }
            else
            {
                if (errorList != null)
                {
                    lblRegisterResult.Text = "Transaction resulted in Error!";
                    rtfRMetadatContent.Text = result.OuterXml;
                }
                else
                {
                    lblRegisterResult.Text = "Transaction succeded!";
                    rtfRMetadatContent.Text = result.OuterXml;
                }
            }
        }

        private void convertEbRIMMetadataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMetadataConversion frm = new frmMetadataConversion();
            frm.ShowDialog();
        }

        private string GetMimeType(string filename)
        {
            System.Security.Permissions.RegistryPermission regPerm = new System.Security.Permissions.RegistryPermission(System.Security.Permissions.RegistryPermissionAccess.Read, "\\HKEY_CLASSES_ROOT");
            Microsoft.Win32.RegistryKey classesRoot = Microsoft.Win32.Registry.ClassesRoot;
            FileInfo fi = new FileInfo(filename);
            string extension = fi.Extension.ToUpper();
            Microsoft.Win32.RegistryKey typeKey = classesRoot.OpenSubKey(@"MIME\Database\Content Type");

            foreach (string keyname in typeKey.GetSubKeyNames())
            {
                Microsoft.Win32.RegistryKey currentKey = classesRoot.OpenSubKey(@"MIME\Database\Content Type\" + keyname);
                string currentExtension = (string) currentKey.GetValue("Extension", null);
                if (!string.IsNullOrEmpty(currentExtension) && currentExtension.ToUpper() == extension)
                    return keyname;
            }
            return string.Empty;
        }

        private void createUniqueIdentifiersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUIDGenerator UIDGenerator = new frmUIDGenerator();
            UIDGenerator.Show();
        }

        private void btnAddRequest_Click(object sender, EventArgs e)
        {
            DocumentRequest newRequest = new DocumentRequest(txtHomeCommunityUniqueID.Text, txtRepositoryUniqueID.Text, txtDocumentUniqueID.Text);
            docRequestSrc.Add(newRequest);

        }

        private void frmIheTstHarness_Load(object sender, EventArgs e)
        {
            docRequestSrc = new BindingList<DocumentRequest>();
            documentRequestBindingSource.DataSource = docRequestSrc;

        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            XmlDocument retrieveDocumentSetRequest = new XmlDocument();

            try
            {


                XmlElement root = retrieveDocumentSetRequest.CreateElement("RetrieveDocumentSetRequest", "urn:ihe:iti:xds-b:2007");

                foreach (DocumentRequest docRequest in docRequestSrc)
                {
                    XmlElement documentRequest = retrieveDocumentSetRequest.CreateElement("DocumentRequest", "urn:ihe:iti:xds-b:2007");
                    XmlElement homeCommunityID = retrieveDocumentSetRequest.CreateElement("HomeCommunityId", "urn:ihe:iti:xds-b:2007");
                    XmlElement repositoryUniqueID = retrieveDocumentSetRequest.CreateElement("RepositoryUniqueId", "urn:ihe:iti:xds-b:2007");
                    XmlElement documentUniqueID = retrieveDocumentSetRequest.CreateElement("DocumentUniqueId", "urn:ihe:iti:xds-b:2007");

                    homeCommunityID.InnerText = docRequest.HomeCommunityID;
                    repositoryUniqueID.InnerText = docRequest.RepositoryUniqueID;
                    documentUniqueID.InnerText = docRequest.DocumentUniqueID;

                    documentRequest.AppendChild(homeCommunityID);
                    documentRequest.AppendChild(repositoryUniqueID);
                    documentRequest.AppendChild(documentUniqueID);

                    root.AppendChild(documentRequest);
                }

                retrieveDocumentSetRequest.AppendChild(root);

                WCF.Message msgInput, msgOutput;
                
                msgInput = WCF.Message.CreateMessage(MESSAGE_VERSION
                                                     , RETRIEVEDOCUMENTSET_WSAACTION
                                                     , new XmlNodeReader(retrieveDocumentSetRequest));
                msgOutput = WCF.Message.CreateMessage(MESSAGE_VERSION, "");

                XDSRepository.XDSRepositoryClient client = new XDSRepository.XDSRepositoryClient(GetRepositoryEndPointName());
                
                msgOutput = client.RetrieveDocumentSet(msgInput);

                XmlDictionaryReader rdr = msgOutput.GetReaderAtBodyContents();

                XmlDocument result = new XmlDocument();
                result.Load(rdr);

                XmlNode fault = result.SelectSingleNode(@"//*[local-name()='Fault']");
                XmlNode errorList = result.SelectSingleNode(@"//*[local-name()='RegistryErrorList']");

                if (fault != null)
                {
                    lblRetrieveDocumentSet.Text = "Error occurred when executing web service.";
                    rtfRetrieveDocumentSet.Text = fault.OuterXml;
                }
                else
                {
                    if (errorList != null)
                    {
                        lblRetrieveDocumentSet.Text = "Transaction resulted in Error!";
                        rtfRetrieveDocumentSet.Text = result.OuterXml;
                    }
                    else
                    {
                        lblRetrieveDocumentSet.Text = "Transaction succeded!";
                        rtfRetrieveDocumentSet.Text = result.OuterXml;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnStoredQueryFileDialog_Click(object sender, EventArgs e)
        {
            txtStoredQueryFileName.Text = GetFileName();
        }

        private void btnStoredQueryExecute_Click(object sender, EventArgs e)
        {
            XmlDocument storedQueryRequest = new XmlDocument();

            storedQueryRequest.Load(txtStoredQueryFileName.Text);

            try
            {

                WCF.Message msgInput, msgOutput;

                msgInput = WCF.Message.CreateMessage(MESSAGE_VERSION
                                                     , STOREDQUERY_WSAACTION
                                                     , new XmlNodeReader(storedQueryRequest));
                msgOutput = WCF.Message.CreateMessage(MESSAGE_VERSION, "");

                XDSRegistry.XDSRegistryClient client = new XDSRegistry.XDSRegistryClient(GetRegistryEndPointName());
                msgOutput = client.RegistryStoredQuery(msgInput);            

                XmlDictionaryReader rdr = msgOutput.GetReaderAtBodyContents();

                XmlDocument result = new XmlDocument();
                result.Load(rdr);

                rtbStoredQueryResult.Text = result.OuterXml;

                lblStoredQueryResult.Text = "SUCCESS";

            }
            catch (Exception ex)
            {
                rtbStoredQueryResult.Text = ex.ToString();

                lblStoredQueryResult.Text = "Failed!!!";
            }

        }

        private void btnPatientRecordAddFileDialog_Click(object sender, EventArgs e)
        {
            txtPatientRecordAddMetadataFile.Text = GetFileName();
        }

        private void btnExecutePatientRecordAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPatientRecordAddMetadataFile.Text))
            {
                MessageBox.Show("Please provide/select input xml file.");
                return;
            }

            try
            {
                XmlDocument xmlDocPatientRequest = new XmlDocument();

                xmlDocPatientRequest.Load(txtPatientRecordAddMetadataFile.Text);

                WCF.Message msgInput, msgOutput;

                msgInput = WCF.Message.CreateMessage(MESSAGE_VERSION
                                                     , PATIENT_ADD_WSAACTION
                                                     , new XmlNodeReader(xmlDocPatientRequest));
                msgOutput = WCF.Message.CreateMessage(MESSAGE_VERSION, "");

                XDSRegistry.XDSRegistryClient client = new XDSRegistry.XDSRegistryClient(GetRegistryEndPointName());
                msgOutput = client.PatientRegistryRecordAdded(msgInput);

                XmlDictionaryReader rdr = msgOutput.GetReaderAtBodyContents();

                XmlDocument result = new XmlDocument();
                result.Load(rdr);

                XmlNode fault = result.SelectSingleNode(@"//*[local-name()='Fault']");
                XmlNode errorList = result.SelectSingleNode(@"//*[local-name()='RegistryErrorList']");

                if (fault != null)
                {
                    lblPatientRecordAddResult.Text = "Error occurred when executing web service.";
                    rtbPatientRecordAddOutput.Text = fault.OuterXml;
                }
                else
                {
                    if (errorList != null)
                    {
                        lblPatientRecordAddResult.Text = "Transaction resulted in Error!";
                        rtbPatientRecordAddOutput.Text = result.OuterXml;
                    }
                    else
                    {
                        lblPatientRecordAddResult.Text = "Transaction succeded!";
                        rtbPatientRecordAddOutput.Text = result.OuterXml;
                    }
                }


                rtbPatientRecordAddOutput.Text = result.OuterXml;

                lblPatientRecordAddResult.Text = "SUCCESS";

            }
            catch (Exception ex)
            {
                rtbPatientRecordAddOutput.Text = ex.ToString();

                lblPatientRecordAddResult.Text = "Failed!!!";
            }
        }

        private void btnPRRevisedFileDialog_Click(object sender, EventArgs e)
        {
            txtPRRevisedMetadataFile.Text = GetFileName();
        }

        private void btnPRRevisedExecute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPRRevisedMetadataFile.Text))
            {
                MessageBox.Show("Please provide/select input xml file.");
                return;
            }

            try
            {

                XmlDocument xmlDocPatientRequest = new XmlDocument();

                xmlDocPatientRequest.Load(txtPRRevisedMetadataFile.Text);

                WCF.Message msgInput, msgOutput;

                msgInput = WCF.Message.CreateMessage(MESSAGE_VERSION
                                                     , PATIENT_REVISED_WSAACTION
                                                     , new XmlNodeReader(xmlDocPatientRequest));
                msgOutput = WCF.Message.CreateMessage(MESSAGE_VERSION, "");

                XDSRegistry.XDSRegistryClient client = new XDSRegistry.XDSRegistryClient(GetRegistryEndPointName());
                msgOutput = client.PatientRegistryRecordRevised(msgInput);

                XmlDictionaryReader rdr = msgOutput.GetReaderAtBodyContents();

                XmlDocument result = new XmlDocument();
                result.Load(rdr);

                XmlNode fault = result.SelectSingleNode(@"//*[local-name()='Fault']");
                XmlNode errorList = result.SelectSingleNode(@"//*[local-name()='RegistryErrorList']");

                if (fault != null)
                {
                    lblPRRevisedResult.Text = "Error occurred when executing web service.";
                    rtbPRRevisedOutput.Text = fault.OuterXml;
                }
                else
                {
                    if (errorList != null)
                    {
                        lblPRRevisedResult.Text = "Transaction resulted in Error!";
                        rtbPRRevisedOutput.Text = result.OuterXml;
                    }
                    else
                    {
                        lblPRRevisedResult.Text = "Transaction succeded!";
                        rtbPRRevisedOutput.Text = result.OuterXml;
                    }
                }

                rtbPRRevisedOutput.Text = result.OuterXml;

                lblPRRevisedResult.Text = "SUCCESS";

            }
            catch (Exception ex)
            {
                rtbPRRevisedOutput.Text = ex.ToString();

                lblPRRevisedResult.Text = "Failed!!!";
            }
        }

        private void btnPRDuplicatesResolvedFileDialog_Click(object sender, EventArgs e)
        {
            txtPRDuplicatesResolved.Text = GetFileName();
        }

        private void btnPRDuplicatesResolvedExecute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPRDuplicatesResolved.Text))
            {
                MessageBox.Show("Please provide/select input xml file.");
                return;
            }

 

            try
            {
                XmlDocument xmlDocPatientRequest = new XmlDocument();

                xmlDocPatientRequest.Load(txtPRDuplicatesResolved.Text);

                WCF.Message msgInput, msgOutput;

                msgInput = WCF.Message.CreateMessage(MESSAGE_VERSION
                                                     , PATIENT_DUPLICATES_WSAACTION
                                                     , new XmlNodeReader(xmlDocPatientRequest));
                msgOutput = WCF.Message.CreateMessage(MESSAGE_VERSION, "");

                XDSRegistry.XDSRegistryClient client = new XDSRegistry.XDSRegistryClient(GetRegistryEndPointName());
                msgOutput = client.PatientRegistryDuplicatesResolved(msgInput);

                XmlDictionaryReader rdr = msgOutput.GetReaderAtBodyContents();

                XmlDocument result = new XmlDocument();
                result.Load(rdr);

                XmlNode fault = result.SelectSingleNode(@"//*[local-name()='Fault']");
                XmlNode errorList = result.SelectSingleNode(@"//*[local-name()='RegistryErrorList']");

                if (fault != null)
                {
                    lblPRDuplicatesResolved.Text = "Error occurred when executing web service.";
                    rtbPRDuplicatesResolved.Text = fault.OuterXml;
                }
                else
                {
                    if (errorList != null)
                    {
                        lblPRDuplicatesResolved.Text = "Transaction resulted in Error!";
                        rtbPRDuplicatesResolved.Text = result.OuterXml;
                    }
                    else
                    {
                        lblPRDuplicatesResolved.Text = "Transaction succeded!";
                        rtbPRDuplicatesResolved.Text = result.OuterXml;
                    }
                }

                rtbPRDuplicatesResolved.Text = result.OuterXml;
                lblPRDuplicatesResolved.Text = "SUCCESS";

            }
            catch (Exception ex)
            {
                rtbPRDuplicatesResolved.Text = ex.ToString();
                lblPRDuplicatesResolved.Text = "Failed!!!";
            }
        }


        private string GetRegistryEndPointName()
        {
            return System.Configuration.ConfigurationManager.AppSettings["REGISTRY_ENDPOINT_NAME"];
        }

        private string GetRepositoryEndPointName()
        {
            return System.Configuration.ConfigurationManager.AppSettings["REPOSITORY_ENDPOINT_NAME"];
        }
    }
}