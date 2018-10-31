using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.XDS.Test
{
    public class DocumentRequest
    {
        private string homeCommunityID;

        public string HomeCommunityID
        {
            get { return homeCommunityID; }
            set { homeCommunityID = value; }
        }

        private string repositoryUniqueID;

        public string RepositoryUniqueID
        {
            get { return repositoryUniqueID; }
            set { repositoryUniqueID = value; }
        }

        private string documentUniqueID;

        public string DocumentUniqueID
        {
            get { return documentUniqueID; }
            set { documentUniqueID = value; }
        }

        public DocumentRequest(string homecommunityid, string repositoryuniqueid, string documentuniqueid)
        {
            HomeCommunityID = homecommunityid;
            RepositoryUniqueID = repositoryuniqueid;
            DocumentUniqueID = documentuniqueid;
        }
    }
}
