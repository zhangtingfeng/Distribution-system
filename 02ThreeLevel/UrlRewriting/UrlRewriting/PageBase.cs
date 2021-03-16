namespace UrlRewriting
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI;

    public class PageBase : Page
    {
        private string _CacheFile;

        protected override void Render(HtmlTextWriter writer)
        {
            if (writer is Html32TextWriter)
            {
                writer = new FormFixerHtml32TextWriter(writer.InnerWriter);
            }
            else
            {
                writer = new FormFixerHtmlTextWriter(writer.InnerWriter);
            }
            base.Render(writer);
        }

        public string CacheFile
        {
            get
            {
                return this._CacheFile;
            }
            set
            {
                this._CacheFile = value;
            }
        }

        public class FormFixerHtml32TextWriter : Html32TextWriter
        {
            private string _url;

            public FormFixerHtml32TextWriter(TextWriter writer) : base(writer)
            {
                this._url = HttpContext.Current.Request.RawUrl;
                if (this._url.ToLower().IndexOf("/404.aspx") >= 0)
                {
                    int index = this._url.IndexOf("//");
                    index = this._url.IndexOf("/", index + 2, (int) ((this._url.Length - index) - 2));
                    this._url = this._url.Substring(index, this._url.Length - index);
                }
            }

            public override void WriteAttribute(string name, string value, bool encode)
            {
                if ((this._url != null) && (string.Compare(name, "action", true) == 0))
                {
                    value = this._url;
                }
                base.WriteAttribute(name, value, encode);
            }
        }

        public class FormFixerHtmlTextWriter : HtmlTextWriter
        {
            private string _url;

            public FormFixerHtmlTextWriter(TextWriter writer) : base(writer)
            {
                this._url = HttpContext.Current.Request.RawUrl;
                if (this._url.ToLower().IndexOf("/404.aspx") >= 0)
                {
                    int index = this._url.IndexOf("//");
                    index = this._url.IndexOf("/", index + 2, (int) ((this._url.Length - index) - 2));
                    this._url = this._url.Substring(index, this._url.Length - index);
                }
            }

            public override void WriteAttribute(string name, string value, bool encode)
            {
                if ((this._url != null) && (string.Compare(name, "action", true) == 0))
                {
                    value = this._url;
                }
                base.WriteAttribute(name, value, encode);
            }
        }
    }
}

