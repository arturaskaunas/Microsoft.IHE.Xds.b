using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class SubmitObjectBase
    {
        int id = 0;
        public int ID
        {
            get{ return id; }
            set { id = value; }
        }

        private string availabilityStatus;

        public string AvailabilityStatus
        {
            get { return availabilityStatus; }
            set { availabilityStatus = value; }
        }

        private string comments;

        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        private string entryUUID;

        public string EntryUUID
        {
            get { return entryUUID; }
            set { entryUUID = value; }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string uniqueID;

        public string UniqueID
        {
            get { return uniqueID; }
            set { uniqueID = value; }
        }

        private string associationType;
        public string AssociationType
        {
            get { return associationType; }
            set { associationType = value; }
        }
	
    }
}
