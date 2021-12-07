using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hopper
{
    public partial class EditAgentForm : Form
    {
        public Model1 db { get; set; }
        public Agent ag { get; set; } 
        public EditAgentForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ag == null)
            {
                ag = (Agent)agentBindingSource.Current;
                db.Agent.Add(ag);
            }
            try
            {
                db.SaveChanges();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message);
            }
        }

        private void EditAgentForm_Load(object sender, EventArgs e)
        {
            if (ag == null)
            {
                agentBindingSource.AddNew();
                this.Text = "Добавление новой записи";
            }
            else
            {
                agentBindingSource.Add(ag);
                agentTypeIDTextBox.ReadOnly = true;
            }
        }
    }
}
