using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class FolderCodeList
    {

        private int _folderCodeListID;

        public int FolderCodeListID
        {
            get { return _folderCodeListID; }
            set { _folderCodeListID = value; }
        }

        private int _folderID;

        public int FolderID
        {
            get { return _folderID; }
            set { _folderID = value; }
        }

        private string _eventCodeValue;

        public string EventCodeValue
        {
            get { return _eventCodeValue; }
            set { _eventCodeValue = value; }
        }

        private string _eventCodeDisplayName;

        public string EventCodeDisplayName
        {
            get { return _eventCodeDisplayName; }
            set { _eventCodeDisplayName = value; }
        }

    }
}
