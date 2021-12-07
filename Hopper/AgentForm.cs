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
    public partial class AgentForm : Form
    {
        Model1 db = new Model1();
        public AgentForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            agentBindingSource.DataSource = db.Agent.ToList();
            agentTypeBindingSource.DataSource = db.AgentType.ToList();
        }

        private void agentBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            Agent agent = (Agent)agentBindingSource.Current;
            try
            {
                if (agent == null) return;
                if (agent.Logo != "")
                {
                    string str = agent.Logo.Substring(1);
                    LogoPict.Image = Image.FromFile(str);
                }
                else
                {
                    LogoPict.Image = Image.FromFile("agents\\picture.png");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            EditAgentForm frm = new EditAgentForm();
            frm.db = db;
            frm.ag = null;
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                agentBindingSource.DataSource = null;
                agentBindingSource.DataSource = db.Agent.ToList();
            }
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.InnerException.Message);
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            Agent ag = (Agent)agentBindingSource.Current;
            EditAgentForm frm = new EditAgentForm();
            frm.db = db;
            frm.ag = ag;
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                agentBindingSource.DataSource = null;
                agentBindingSource.DataSource = db.Agent.ToList();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Agent ag = (Agent)agentBindingSource.Current;
            DialogResult dr = MessageBox.Show("Удалить " + ag.Title + "?", "Удаление",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                db.Agent.Remove(ag);
                try
                {
                    db.SaveChanges();
                    agentBindingSource.DataSource = db.Agent.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.InnerException.Message);
                }
            }
        }

        private void LeaveButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chbutton_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                agentBindingSource.DataSource = db.Agent.OrderByDescending(ag => ag.Priority).ToList();
            }
            else
            {
                agentBindingSource.DataSource = db.Agent.OrderBy(a => a.Priority).ToList();
            }
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        { 
            agentBindingSource.DataSource = db.Agent.Where(p => p.Title.Contains(searchBox.Text)
             || p.Email.Contains(searchBox.Text)).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            agentBindingSource.DataSource = db.Agent.OrderBy(a => a.AgentTypeID).ToList();
        }
    }
}
