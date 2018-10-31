using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Microsoft.IHE.XDS.Common
{
    public class RetrieveDocumentSet
    {
        private string homeCommunityID;

        public string HomeCommunityID
        {
            get { return homeCommunityID; }
            set { homeCommunityID = value; }
        }
        private int contentID;

        public int ContentID
        {
            get { return contentID; }
            set { contentID = value; }
        }
        private string repositoryUniqueID;

        public string RepositoryUniqueID
        {
            get { return repositoryUniqueID; }
            set { repositoryUniqueID = value; }
        }

        //private bool isRepositoryUniqueIDExsists;
        //public bool IsRepositoryUniqueIDExsists
        //{
        //    get { return isRepositoryUniqueIDExsists; }
        //    set { isRepositoryUniqueIDExsists = value; }
        //}

        private string documentID;
        public string DocumentID
        {
            get { return documentID; }
            set { documentID = value; }
        }

        private string mimeType;
        public string MimeType
        {
            get { return mimeType; }
            set { mimeType = value; }
        }

        private Stream content;

        public Stream Content
        {
            get { return content; }
            set { content = value; }
        }

    }
}
