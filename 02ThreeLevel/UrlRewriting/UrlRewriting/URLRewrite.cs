namespace UrlRewriting
{
    using System;

    public class URLRewrite
    {
        private string _LookFor;
        private string _SendTo;

        public URLRewrite(string lookFor, string sendTo)
        {
            this._LookFor = lookFor;
            this._SendTo = sendTo;
        }

        public string LookFor
        {
            get
            {
                return this._LookFor;
            }
            set
            {
                this._LookFor = value;
            }
        }

        public string SendTo
        {
            get
            {
                return this._SendTo;
            }
            set
            {
                this._SendTo = value;
            }
        }
    }
}

