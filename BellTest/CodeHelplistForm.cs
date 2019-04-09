using BellTest.Codes;
using System;
using System.Windows.Forms;

namespace BellTest
{
    /// <summary>
    /// A window to display a CodeList on screen.
    /// </summary>
    public partial class CodeHelplistForm : Form
    {
        private CodeHelplistForm()
        {
            InitializeComponent();
        }

        public CodeHelplistForm(CodeList list) : this()
        {
            lblCodeListTitle.Text = list.Name;
            lblDescription.Text = list.Description;
            foreach (BellCode code in list.Codes)
            {
                dgvCodeList.Rows.Add(code.ToString(), code.Name);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
