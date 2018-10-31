using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class Author
    {
        private int authorID;

        public int AuthorID
        {
            get
            { return authorID; }
            set { authorID = value; }
        }

        private string institution;

        public string Institution
        {
            get { return institution; }
            set { institution = value; }
        }

        private string person;

        public string Person
        {
            get { return person; }
            set { person = value; }
        }

        private string role;

        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        private string specialty;

        public string Specialty
        {
            get { return specialty; }
            set { specialty = value; }
        }
	
    }
}
