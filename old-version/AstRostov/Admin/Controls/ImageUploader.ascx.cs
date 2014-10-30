using System;
using System.IO;
using System.Web;
using System.Web.UI;
using AstCore.Helpers;

namespace AstRostov.Admin.Controls
{
    /// <summary>
    ///     Represents the Image Uploader user control class
    /// </summary>
    public partial class AdminImageUploader : UserControl
    {
        private string _fullName = string.Empty;
        private string _appendToFileName = string.Empty;

        #region Public Properties

        /// <summary>
        ///     Gets the Http Posted File.
        /// </summary>
        public HttpPostedFile PostedFile
        {
            get { return UploadImage.PostedFile; }
        }

        /// <summary>
        ///     Gets or sets the Http Posted File.
        /// </summary>
        public string AppendToFileName
        {
            get { return _appendToFileName; }

            set { _appendToFileName = value; }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether to use Site Config Path.
        /// </summary>
        public bool UseSiteConfig { get; set; }

        /// <summary>
        ///     Gets or sets the posted file information
        /// </summary>
        public FileInfo FileInformation
        {
            get
            {
                if (ViewState["FileName"] != null)
                {
                    return ViewState["FileName"] as FileInfo;
                }
                if (UploadImage.PostedFile != null && pnlSaveOption.Visible)
                {
                    return new FileInfo(txtFileName.Text + Path.GetExtension(UploadImage.PostedFile.FileName));
                }
                if (UploadImage.PostedFile != null && !string.IsNullOrEmpty(UploadImage.PostedFile.FileName))
                {
                    return
                        new FileInfo(Path.GetFileNameWithoutExtension(UploadImage.PostedFile.FileName) +
                                     Path.GetExtension(UploadImage.PostedFile.FileName));
                }
                return null;
            }

            set { ViewState["FileName"] = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Save Image Method
        /// </summary>
        /// <returns>Returns true if image saved otheriwse false</returns>
        public void SaveImage()
        {
            string fileName = pnlSaveOption.Visible && rdoNewFileName.Checked
                                  ? txtFileName.Text
                                  : Path.GetFileNameWithoutExtension(UploadImage.PostedFile.FileName);
            _fullName = GetFullName(fileName + _appendToFileName + Path.GetExtension(UploadImage.PostedFile.FileName));


            if (!File.Exists(HttpContext.Current.Server.MapPath(_fullName)))
            {
                var imageData = new byte[UploadImage.PostedFile.InputStream.Length];
                UploadImage.PostedFile.InputStream.Read(imageData, 0, (int)UploadImage.PostedFile.InputStream.Length);
                FileHelper.WriteBinaryStorage(imageData, _fullName);
            }
            else
            {
                // Message "Filename already exist" shown.
                if (pnlSaveOption.Visible && IsPostBack)
                {
                    if (rdoNewFileName.Checked)
                    {
                        _fullName = GetFullName(txtFileName.Text + Path.GetExtension(UploadImage.PostedFile.FileName));
                    }

                    // If overwrite selected then we can use the same file name.
                    var imageData = new byte[UploadImage.PostedFile.InputStream.Length];
                    UploadImage.PostedFile.InputStream.Read(imageData, 0,
                                                            (int)UploadImage.PostedFile.InputStream.Length);
                    FileHelper.WriteBinaryStorage(imageData, _fullName);
                }
                else
                {
                    var imageData = new byte[UploadImage.PostedFile.InputStream.Length];
                    UploadImage.PostedFile.InputStream.Read(imageData, 0,
                                                            (int)UploadImage.PostedFile.InputStream.Length);
                    FileHelper.WriteBinaryStorage(imageData, _fullName);
                }
            }

            FileInformation = new FileInfo(_fullName);
            pnlSaveOption.Visible = false;
        }

        /// <summary>
        ///     Check whether the same file name exist in original folder.
        /// </summary>
        /// <returns>Returns true if file name doesn't exist in original folder, otherwise false.</returns>
        public bool IsFileNameValid()
        {
            bool status = false;
            if (UploadImage.PostedFile != null)
            {
                string fileName = pnlSaveOption.Visible
                                      ? txtFileName.Text
                                      : Path.GetFileNameWithoutExtension(UploadImage.PostedFile.FileName);
                if (UploadImage.PostedFile.FileName == string.Empty)
                {
                    // File not selected 
                    lblMsg.Text = "Please select a valid JPEG, JPG, PNG or GIF image";
                    FileInformation = null;
                    pnlSaveOption.Visible = true;
                    UploadImage.Focus();
                }
                else
                {
                    FileInformation =
                        new FileInfo(fileName + _appendToFileName + Path.GetExtension(UploadImage.PostedFile.FileName));
                    _fullName = GetFullName(FileInformation.Name);

                    if (File.Exists(HttpContext.Current.Server.MapPath(_fullName)))
                    {
                        // Duplicate file name found. Clear the stored view state information.
                        lblMsg.Text = string.Format("File name '{0}' already exist. ", FileInformation.Name);
                        FileInformation = new FileInfo(_fullName);
                        status = true;
                    }
                    else
                    {
                        FileInformation = new FileInfo(_fullName);
                        status = true;
                    }
                }
            }

            return status;
        }

        #endregion

        #region Page Load Event

        /// <summary>
        ///     Page Load Event
        /// </summary>
        /// <param name="sender">Sender object that raised the event.</param>
        /// <param name="e">Event Argument of the object that contains event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (UploadImage.PostedFile != null)
                {
                    UploadImage.Attributes.Add("Value", UploadImage.PostedFile.FileName);
                }

                rdoOverwrite.Attributes.Add("onClick", "setEnabled()");
                rdoNewFileName.Attributes.Add("onClick", "setEnabled()");
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Get the file and return the full physical path with filename.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Returns the full physical path with file name.</returns>
        private string GetFullName(string fileName)
        {
            return String.Format("~/img/uploaded/catalog/original/{0}", fileName);
        }

        #endregion
    }
}